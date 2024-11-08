using MatFrem.Controllers;
using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDBContext _context;
        public LocationRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationModel>> ShowEntireLocation()
        {
            var showAll = await _context.Locations.Take(50).ToListAsync();
            return showAll;
        }

        public async Task<LocationModel> AddLocation(LocationModel locationModel)
        {
            await _context.Locations.AddAsync(locationModel); //remember, Locations is the object created in the AppDBContext
            await _context.SaveChangesAsync();
            return locationModel;
        }

        public async Task<LocationModel?> DeleteLocation(Guid id)
        {
            var locationDelete = await _context.Locations.FindAsync(id); //remember, Locations is the object created in the AppDBContext
            if (locationDelete != null)
            {
                _context.Locations.Remove(locationDelete);
                await _context.SaveChangesAsync();
                return locationDelete;
            }

            return null; 
        }

        public async Task<LocationModel?> UpdateLocation(LocationModel locationModel)
        {
            if(locationModel != null)
            {
                _context.Locations.Update(locationModel);
                await _context.SaveChangesAsync();
                return locationModel;
            }
            return null;
        }


        public async Task<LocationModel?> GetLocationByID(Guid id)
        {
            return await _context.Locations.Where(x => x.LocationReportID == id).FirstOrDefaultAsync();
        }

    }
}
