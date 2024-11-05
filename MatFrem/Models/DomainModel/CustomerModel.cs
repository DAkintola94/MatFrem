using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.DomainModel
{
    public class CustomerModel
    {
        
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNr { get; set; }

        public ICollection<OrderModel> Orders { get; set; }


        public int DriverID { get; set; }

        public DriverModel Driver { get; set; } //Navigaiton property to get all of Driver properties/info

    }
}
