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
		public string? ProductName { get; set; }
		[MaxLength(100)]
		public string? ProductCalories { get; set; }
		public double ProductPrice { get; set; }
		[MaxLength(100)]
		public string? ProductLocation { get; set; }
		[ValidateNever]
		public string? ImageUrl { get; set; }
		[MaxLength(100)]
		public string? CustomerName { get; set; }
		[MaxLength(100)]
		public ShopModel? ShopModelO { get; set; }
		public string? ProductCategory { get; set; }
		public ICollection<OrderProducts> OrderProduct { get; set; } = new List<OrderProducts>(); //indicates many to many relationship
        public int ShopId { get; set; }
    }
}
