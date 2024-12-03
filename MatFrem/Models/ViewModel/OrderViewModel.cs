using MatFrem.Models.DomainModel;

namespace MatFrem.Models.ViewModel
{
    public class OrderViewModel
    {

        public int OrderID { get; set; }
        public DateOnly DateOrderCreate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? DriverId { get; set; } //foreign key for DriverModel, this is the primary key for DriverMode
        public string? CustomerName { get; set; }
        public int OrderStatusID { get; set; }
        public string? OrderStatusDescription { get; set; }

        public string? PickUpAddress { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? CustomerPhoneNr { get; set;}

        public string? DeliveryAddress { get; set; } = "";

        public string? ItemCategory { get; set; }
        public ApplicationUser? Customer { get; set; } //navigation property for CustomerModel, namely, OrderModel has a Customer
        public ApplicationUser? Driver { get; set; } //navigation property for DriverModel, namely, OrderModel has a Driver. To get all of Driver properties/inf
        public ProductModel? ProductM { get; set; } //navigation property for ProductModel, namely, OrderModel has a ProductModel
        public OrderItem? OrderItem { get; set; } //navigation property for OrderItem, namely, OrderModel has a OrderItem

    }
}
