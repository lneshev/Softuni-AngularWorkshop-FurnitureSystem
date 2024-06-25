﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using MoravianStar.Exceptions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.DbContexts;
using System;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Common
{
    public class DbUpdater : IDbUpdater
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<UserEntity> userManager;

        public DbUpdater(AppDbContext appDbContext, UserManager<UserEntity> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }

        public async Task CreateAndUpdateAsync()
        {
            await MigrateAndSeedAppDB();
        }

        private async Task MigrateAndSeedAppDB()
        {
            var systemDbCreator = (IRelationalDatabaseCreator)appDbContext.GetInfrastructure().GetRequiredService<IDatabaseCreator>();
            if (!await systemDbCreator.ExistsAsync())
            {
                await systemDbCreator.CreateAsync();
            }

            using (var tx = await appDbContext.Database.BeginTransactionAsync())
            {
                await appDbContext.Database.MigrateAsync();
                await SeedAppDbAsync(appDbContext);
                await tx.CommitAsync();
            }
        }

        private async Task SeedAppDbAsync(AppDbContext appDbContext)
        {
            var utcNow = DateTimeOffset.UtcNow;

            #region Admin user
            var adminEmail = "admin@admin.com";
            var adminPassword = "Admin";
            var adminUser = userManager.FindByEmailAsync(adminEmail).Result;
            if (adminUser == null)
            {
                var id = Guid.NewGuid();
                adminUser = new UserEntity
                {
                    Id = id,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    CreatedById = id,
                    CreatedAt = utcNow
                };

                var identityResult = userManager.CreateAsync(adminUser).Result;
                if (!identityResult.Succeeded)
                {
                    throw new BusinessException("The admin user could not be created.");
                }
            }

            var isPasswordValid = userManager.CheckPasswordAsync(adminUser, adminPassword).Result;
            if (!isPasswordValid)
            {
                var token = userManager.GeneratePasswordResetTokenAsync(adminUser).Result;
                var result = userManager.ResetPasswordAsync(adminUser, token, adminPassword).Result;
                if (!result.Succeeded)
                {
                    throw new BusinessException("The admin user's password could not be set.");
                }
            }
            #endregion

            #region Admin role
            RoleEntity adminRole = await CreateRole(appDbContext, utcNow, adminUser, RoleConstants.SuperAdminRoleName);
            RoleEntity userRole = await CreateRole(appDbContext, utcNow, adminUser, RoleConstants.UserRoleName);
            #endregion

            #region Attach admin role to admin user
            var isAdminUserInAdminRole = userManager.IsInRoleAsync(adminUser, adminRole.Name).Result;
            if (!isAdminUserInAdminRole)
            {
                var roleAttachResult = userManager.AddToRoleAsync(adminUser, adminRole.Name).Result;
                if (!roleAttachResult.Succeeded)
                {
                    throw new BusinessException("The admin role could not be attached to admin user.");
                }
            }
            #endregion

            // Save changes
            await appDbContext.SaveChangesAsync();
        }

        private async Task<RoleEntity> CreateRole(AppDbContext appDbContext, DateTimeOffset utcNow, UserEntity adminUser, string roleName)
        {
            var role = await appDbContext.Set<RoleEntity>().FirstOrDefaultAsync(x => x.Name == roleName);
            if (role == null)
            {
                role = new RoleEntity()
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                    CreatedById = adminUser.Id,
                    CreatedAt = utcNow
                };
                await appDbContext.Set<RoleEntity>().AddAsync(role);
                await appDbContext.SaveChangesAsync();
            }

            return role;
        }
    }
}