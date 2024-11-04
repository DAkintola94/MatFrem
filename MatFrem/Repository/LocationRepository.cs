using MatFrem.Controllers;
using MatFrem.DataContext;

namespace MatFrem.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDBContext _context;
        public LocationRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Location> AddLocationToBase(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return location;

        }

    }
}
