using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class OrderModel
    {
        
        public int OrderID { get; set; }
        public DateOnly OrderCreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public int DriverID { get; set; } //foreign key for DriverModel, this is the primary key for DriverModel
        public DriverModel Driver { get; set; } //navigation property for DriverModel, namely, OrderModel has a Driver. To get all of Driver properties/info

        public int CustomerID { get; set; } //foreign key for CustomerModel, this is the primary key for CustomerModel
        public CustomerModel Customer { get; set; } //navigation property for CustomerModel, namely, OrderModel has a Customer

    }
}
