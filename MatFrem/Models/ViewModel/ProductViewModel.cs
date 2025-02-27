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
        public string ProductViewName { get; set; }
        public string? ProductViewCalories { get; set; }
        public double ProductViewPrice { get; set; }
        public string? ProductViewLocation { get; set; }
        public string? CustomerId { get; set; }
        public string? ImageUrl { get; set; }
		public string? DriverId { get; set; }
		public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int ViewMCategoryId { get; set; } //You need to add property whenever you to design a foreign key in the database
        public int ViewMShopId { get; set; }
        public string? ViewCategoryName { get; set; }

        public string? ViewShopName { get; set; }
    }
}


