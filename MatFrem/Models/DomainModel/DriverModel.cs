using MatFrem.Models.DomainModel;

public class DriverModel
{
    public int DriverID { get; set; }
    public string DriverName { get; set; }
    public string PhoneNr { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string CurrentLocation { get; set; }
    public string LicenseNumber { get; set; }
    public ICollection<OrderModel> ActiveDeliveries { get; set; } // Collection of orders assigned to the driver
}
