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
		public CategoryModel? CategoryModel { get; set; }
		public ShopModel? ShopModelO { get; set; }
		public int ShopId { get; set; }
		public int CategoryId { get; set; }
    }
}
