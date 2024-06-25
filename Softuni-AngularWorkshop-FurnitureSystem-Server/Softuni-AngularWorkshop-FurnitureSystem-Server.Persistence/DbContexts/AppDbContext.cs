using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoravianStar.Extensions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Extensions.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Resources;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.EntityConfigurations.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.EntityConfigurations.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.DbContexts
{
    public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Set Cascade Delete Behavior to Restrict except for entities in tableWithDeleteCascade
            string[] tableWithDeleteCascade = new string[] { };

            var cascadeFKs = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade && !tableWithDeleteCascade.Contains(fk.DeclaringEntityType.Name.Split('.').Last()));

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

            builder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new RoleEntityConfiguration());
            builder.ApplyConfiguration(new FurnitureEntityConfiguration());
        }

        private void OnBeforeSaving()
        {
            if (Database.CurrentTransaction == null)
            {
                throw new InvalidOperationException(Strings.SavingDataToDBWithoutATransactionIsNotAllowed);
            }

            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.State != EntityState.Unchanged)
                {
                    if (entry.Entity is ITrackableEntityBase trackable)
                    {
                        var userId = trackable.CreatedById.IsNullOrEmpty() ? GetCurrentUser() : trackable.CreatedById;

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                trackable.CreatedAt = trackable.CreatedAt == DateTimeOffset.MinValue ? DateTimeOffset.UtcNow : trackable.CreatedAt;
                                trackable.CreatedById = userId;
                                break;
                            case EntityState.Modified:
                                trackable.ModifiedAt = DateTimeOffset.UtcNow;
                                trackable.ModifiedById = userId;
                                break;
                            case EntityState.Deleted:
                                trackable.ModifiedAt = DateTimeOffset.UtcNow;
                                trackable.ModifiedById = userId;
                                break;
                        }
                    }

                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        var validationContext = new ValidationContext(entry.Entity);
                        Validator.ValidateObject(entry.Entity, validationContext, true);
                    }
                }
            }
        }

        private Guid GetCurrentUser()
        {
            var result = httpContextAccessor?.HttpContext?.User?.DeserializeIdClaim();

            if (!result.HasValue)
            {
                throw new SecurityException(Strings.AnonymousUsersCannotMakeModifications);
            }

            return result.Value;
        }
    }
}