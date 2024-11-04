using MatFrem.Controllers;

namespace MatFrem.Repository
{
    public interface ILocationRepository
    {
        Task<Location> AddLocationToBase(Location location);

    }
}
