using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace MatFrem.Models.DomainModel
{
    public class LocationModel
    {
        
        public string LocationID { get; set; }
        public string Address { get; set; }
        public string GeoLocation { get; set; }

        public string LocationMessage { get; set; }

        public int CustomerID { get; set; } //foreign key for CustomerModel
        public CustomerModel Customer { get; set; } //navigation property for CustomerModel

        public int ShopID { get; set; }
        public ShopModel Shop { get; set; }

    }
}
