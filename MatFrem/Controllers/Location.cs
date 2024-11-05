using MatFrem.Models.DomainModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Location : Controller
    {
        private readonly ILocationRepository _locationRepository;
        public Location(ILocationRepository locationRepo)
        {
            _locationRepository = locationRepo;

        }

        [HttpGet]
        public async Task<ActionResult> MapGeo()
        {
            var getLocations = await _locationRepository.ShowEntireLocation(new LocationModel());
            return View(getLocations);
        }
    

        [HttpPost]
        public async Task<ActionResult> MapGeo(string geoJson, string description)
        {
            var locationInput = new LocationModel
            {

                GeoJson = geoJson,
                LocationMessage = description,
                Date = DateOnly.FromDateTime(DateTime.Now),
                CustomerID = 1, //this is a placeholder for now

            };

            if(ModelState.IsValid)
            {
                await _locationRepository.UpdateLocation(locationInput);
                return RedirectToAction("MapGeo");
            }

            return View(locationInput);
        }
    }

}
