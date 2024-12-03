using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.DomainModel
{
	public class ProductModel
	{
		public int ProductID { get; set; }
		public string? Description { get; set; }

		[MaxLength(100)]
		public string? Category { get; set; }
		[MaxLength(100)]
		public string? ProductName { get; set; }
		[MaxLength(100)]
		public string? ProductCalories { get; set; }
		public double ProductPrice { get; set; }
		[MaxLength(100)]
		public string? ProductLocation { get; set; }
		[ValidateNever]
		public string? ImageUrl { get; set; }
		[MaxLength(100)]
		public string? CustomerId { get; set; }
		[MaxLength(100)]
		public string? DriverId { get; set; }
		[ValidateNever]

		//public int? ShoppingCartID { get; set; }
        public ApplicationUser? Customer { get; set; }
		public ApplicationUser? Driver { get; set; } //You need to add property whenever you to design a foreign key in the database
		//public ShoppingCartModel? ShoppingCart { get; set; }
    }
}
