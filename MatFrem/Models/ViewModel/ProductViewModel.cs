using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MatFrem.Models.DomainModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MatFrem.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string? ProductViewDescription { get; set; }
        public string? ProductViewName { get; set; }
        public string? ProductViewCalories { get; set; }
        public double ProductViewPrice { get; set; }
        public string? ProductViewLocation { get; set; }
        public string? ProductViewGeoJson { get; set; }
        public string? CustomerId { get; set; }
        public string? ImageUrl { get; set; }
		public string? DriverId { get; set; }
        public int ViewMShopId { get; set; }
        public string? ViewCategoryName { get; set; }
        public string? ViewShopName { get; set; }
    }
}


