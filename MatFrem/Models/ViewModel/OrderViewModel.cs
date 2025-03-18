using MatFrem.Models.DomainModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.ViewModel
{
    public class OrderViewModel
    {

        public int OrderID { get; set; }
        public DateOnly DateOrderCreate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? DriverId { get; set; } //foreign key for DriverModel, this is the primary key for DriverMode
        public int OrderStatusID { get; set; }
        public int? OrderQuantitySize { get; set; }
        public string? OrderStatusDescription { get; set; }
        public string? PickUpAddress { get; set; }
        public string? PaymentType { get; set; }
        public int ProductID { get; set; }
        public double? ProductPrice { get; set; }

		[DisplayFormat(DataFormatString = "{0:F2}")]
		public decimal? TotalAmount { get; set; }
		public string? DriverName { get; set; }
        public string? CustomerId { get; set; } 
        public string? CustomerName { get; set; }
		public string? CustomerPhoneNr { get; set;}
        public string? DeliveryAddress { get; set; } = "";
        [ValidateNever]
        public List<SelectListItem> AvailableDrivers { get; set; } = new List<SelectListItem>();
        public ProductModel? ProductM { get; set; } //navigation property for ProductModel, namely, OrderModel has a ProductModel

        public List<string> ItemCategory { get; set; } = new List<string>();
        public List<string> OrderViewProductNames { get; set; } = new List<string>();

        public List<string> OrderviewProductDescription { get; set; } = new List<string>();

        public List<string> ViewGeoJson { get; set; } = new List<string>();

    }
}
