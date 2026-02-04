using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.DataAccess.DbContexts;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Common
{
    public class DbUpdater : IDbUpdater
    {
        private readonly AppDbContext appDbContext;

        public DbUpdater(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task CreateAndUpdateAsync()
        {
            await MigrateAndSeedAppDB();
        }

        private async Task MigrateAndSeedAppDB()
        {
            var appDbCreator = (IRelationalDatabaseCreator)appDbContext.GetInfrastructure().GetRequiredService<IDatabaseCreator>();
            if (!await appDbCreator.ExistsAsync())
            {
                await appDbCreator.CreateAsync();
            }

            using (var tx = await appDbContext.Database.BeginTransactionAsync())
            {
                await appDbContext.Database.MigrateAsync();
                await tx.CommitAsync();
            }
        }
    }
}