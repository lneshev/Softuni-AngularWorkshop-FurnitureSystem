using Microsoft.EntityFrameworkCore;
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
            await appDbContext.Database.MigrateAsync();
        }
    }
}