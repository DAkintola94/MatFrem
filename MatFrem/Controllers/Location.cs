using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Location : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICustomerRepository _customerRepository;
        public Location(ILocationRepository locationRepo, ICustomerRepository customerRepository)
        {
            _locationRepository = locationRepo;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult MapGeo()
        {
            return View();
        }
    

        [HttpPost]
        public async Task<ActionResult> MapGeo(string geoJson, string description)
        {
            var locationInput = new LocationModel
            {
                LocationReportID = Guid.NewGuid(),
                GeoJson = geoJson,
                LocationMessage = description,
                Date = DateOnly.FromDateTime(DateTime.Now),
            };

            if(ModelState.IsValid)
            {
                await _locationRepository.AddLocation(locationInput); //Adding the location to the database
                return RedirectToAction("LocationTable");
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> LocationTable()
        {
            var listLocations = await _locationRepository.ShowEntireLocation(); //Binding a variable that will show all of the locations to the view
            return View(listLocations);
        }

        [HttpGet]
        public async Task<ActionResult> ViewIndexTable(Guid id)
        {
            
            var showElementById = await _locationRepository.GetLocationByID(id); //this is to get the id from the database, of the original location report in model
            if(showElementById != null)
            {
                EditViewTable editViewTable = new EditViewTable
                {
                    Date = showElementById.Date,
                    GeoJson = showElementById.GeoJson,
                    LocationMessage = showElementById.LocationMessage,
                    LocationReportID = showElementById.LocationReportID
                };
               
                return View(editViewTable); //you need to return the new model, not the original. The edit button is tied to it. 
                //This is after all, a get method. 
            }

            return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> ViewIndexTable(LocationModel locationModel)
        {

            if(locationModel != null)
            {
                var updateMessage = await _locationRepository.UpdateLocation(locationModel);
                return RedirectToAction("LocationTable");
            }
            return View();
        }

    }

}
