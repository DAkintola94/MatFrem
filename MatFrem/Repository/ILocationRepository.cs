using MatFrem.Controllers;
using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface ILocationRepository
    {
        Task<IEnumerable<LocationModel>> ShowEntireLocation(LocationModel locationModel);
        Task<LocationModel?> GetLocationByID(Guid id);
        Task<LocationModel?> UpdateLocation(LocationModel locationModel);
        Task<LocationModel?> DeleteLocation(Guid id);

    }
}
