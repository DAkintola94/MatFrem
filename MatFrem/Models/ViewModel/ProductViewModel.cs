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

        public string? Category { get; set; }

        public string? Description { get; set; }

        public string ProductName { get; set; }

        public string? ProductCalories { get; set; }

        public double ProductPrice { get; set; }

        public string? ProductLocation { get; set; }

        public string? CustomerId { get; set; }

		[ValidateNever]
        public string? ImageUrl { get; set; }

		public string? DriverId { get; set; }
        [ValidateNever] //This attribute will prevent modelstate to validate this property
		public IEnumerable<SelectListItem> CategoryList { get; set; }

		public ApplicationUser? Customer { get; set; }

        public ApplicationUser? Driver { get; set; }

        public int CategoryId { get; set; } //You need to add property whenever you to design a foreign key in the database
    }
}


