using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace MatFrem.Models.DomainModel
{
    public class LocationModel
    {

        public Guid LocationID { get; set; } = new Guid();
        public string GeoJson { get; set; }
        public string LocationMessage { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public int CustomerID { get; set; } //foreign key for CustomerModel
        public CustomerModel Customer { get; set; } //navigation property for CustomerModel, Also, LocationModel have a Customer

    }
}
