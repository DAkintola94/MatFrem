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
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }

		[DisplayFormat(DataFormatString = "{0:F2}")]
		public decimal? TotalAmount { get; set; }
		public string? DriverName { get; set; }

        public string? CustomerId { get; set; } 
        public string? CustomerName { get; set; }
		public string? CustomerPhoneNr { get; set;}
        public string? DeliveryAddress { get; set; } = "";
        public string? ItemCategory { get; set; }
        [ValidateNever]
        public List<SelectListItem> AvailableDrivers { get; set; } = new List<SelectListItem>();
        public ApplicationUser? Customer { get; set; } //navigation property for CustomerModel, namely, OrderModel has a Customer
        public ApplicationUser? Driver { get; set; } //navigation property for DriverModel, namely, OrderModel has a Driver. To get all of Driver properties/inf
        public ProductModel? ProductM { get; set; } //navigation property for ProductModel, namely, OrderModel has a ProductModel
        public OrderItem? OrderItem { get; set; } //navigation property for OrderItem, namely, OrderModel has a OrderItem

    }
}
