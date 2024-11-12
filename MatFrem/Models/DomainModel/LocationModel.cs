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
        public Guid ShopLocationID { get; set; }
        public OrderModel OrderModels { get; set; } 
        public ICollection<ShopModel> ShopsModel { get; set; } 
        


	}
}
