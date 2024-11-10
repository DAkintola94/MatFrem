using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public DateOnly OrderCreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int DriverID { get; set; } //foreign key for DriverModel, this is the primary key for DriverMode
        public int CustomerID { get; set; }
        public Guid LocationID { get; set; }
        public int ShopID { get; set; }
        public int OrderStatusID { get; set; }

        public int ProductID { get; set; }

        public CustomerModel Customer { get; set; } //navigation property for CustomerModel, namely, OrderModel has a Customer
        public DriverModel Driver { get; set; } //navigation property for DriverModel, namely, OrderModel has a Driver. To get all of Driver properties/inf
        public LocationModel Location { get; set; } 
        public ShopModel ShopModel { get; set; } //all the properties is so the driver page model can be used to retreive those info
        public OrderStatus OrderStatus { get; set; }
        public ProductModel Product { get; set; }

    }
}
