using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.DomainModel
{
    public class DriverModel
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNr { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        public string CurrentLocation { get; set; }
        public string LicenseNumber { get; set; }
        public OrderModel Order { get; set; } //all the properties is so the driver page model can be used to retreive those info
        public CustomerModel CustomerM { get; set; } //all the properties is so the driver page model can be used to retreive those info
        public LocationModel Location { get; set; } //all the properties is so the driver page model can be used to retreive those info
        public ShopModel ShopModel { get; set; } //all the properties is so the driver page model can be used to retreive those info
        public OrderStatus OrderStatus { get; set; }
    }
}
