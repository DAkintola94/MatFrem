using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class LocationModel
    {
        public Guid LocationReportID { get; set; } = new Guid();
        public string GeoJson { get; set; }
        public string? LocationMessage { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public ICollection<OrderModel> OrderModels { get; set; } = new List<OrderModel>();
        public ICollection<ShopModel> ShopsModel { get; set; } = new List<ShopModel>();
        public ICollection<ProductModel> ProductModels { get; set; } = new List<ProductModel>(); // We use icollection when dealing with many to many relationships

		// We use icollection when dealing with many to many relationships
	}
}
