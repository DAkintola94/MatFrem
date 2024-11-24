using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public DateOnly OrderCreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? DriverId { get; set; } //foreign key for DriverModel, this is the primary key for DriverMode
        public string? CustomerId { get; set; }
        public int OrderStatusID { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public int OrderItemID { get; set; }
        public ApplicationUser Customer { get; set; } //navigation property for CustomerModel, namely, OrderModel has a Customer
        public ApplicationUser Driver { get; set; } //navigation property for DriverModel, namely, OrderModel has a Driver. To get all of Driver properties/inf
        public OrderStatus? OrderStatusModel { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); //indicates many to many relationship
        public ICollection<ProductModel> Product { get; set; } = new List<ProductModel>(); //indicates many to many relationship

        public ProductModel ProductM { get; set; } //navigation property for ProductModel, namely, OrderModel has a ProductModel
        public OrderItem OrderItem { get; set; } //navigation property for OrderItem, namely, OrderModel has a OrderItem

    }
}
