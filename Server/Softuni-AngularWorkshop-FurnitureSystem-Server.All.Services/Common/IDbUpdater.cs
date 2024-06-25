using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Common
{
    public interface IDbUpdater
    {
        Task CreateAndUpdateAsync();
    }
}