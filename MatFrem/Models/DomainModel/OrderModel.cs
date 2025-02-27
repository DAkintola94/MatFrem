using MatFrem.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public DateOnly OrderCreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? DriverId { get; set; } //foreign key for DriverModel, this is the primary key for DriverMode
        public string? CustomerName { get; set; }
        public string? DeliveryAddress { get; set; } = "";
        public int OrderStatusID { get; set; }
        [ValidateNever]
        public int ProductID { get; set; }
        public string? CustomerPhoneNr { get; set; }
        public string? ProductName { get; set; }
        public string? PickUpAddress { get; set; }
        public string? ProductCategory { get; set; }
        public int? OrderItem { get; set; }
        [ValidateNever]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); //indicates many to many relationship
        [ValidateNever]
        public ICollection<ProductModel> Product { get; set; } = new List<ProductModel>(); //indicates many to many relationship
        public ProductModel? ProductM { get; set; } = new ProductModel(); //navigation property for ProductModel, namely, OrderModel has a ProductModel
        public OrderItem? OrderItemModel { get; set; } //navigation property for OrderItem, namely, OrderModel has a OrderItem
        [NotMapped]
        public ICollection<OrderItem> CartItems { get; set; } = new List<OrderItem>();
        public CategoryModel? CategoryModel { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public int CategoryM_Id { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? PaymentMethod { get; set; } = "";
    }
}
