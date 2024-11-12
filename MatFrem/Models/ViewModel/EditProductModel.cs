using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MatFrem.Models.DomainModel;

namespace MatFrem.Models.ViewModel
{
    public class EditProductModel
    {
		public int ProductID { get; set; }

		public string ProductName { get; set; }

		public string? ProductCalories { get; set; }

		public double ProductPrice { get; set; }
		public string? ProductCategory { get; set; }

		public string? ProductLocation { get; set; }

		public int ShopID { get; set; }
		public OrderModel? Order { get; set; }
		public ShopModel? ShopM { get; set; }
		public Guid LocationID { get; set; }
		public LocationModel? LocationM { get; set; }
	}
}


