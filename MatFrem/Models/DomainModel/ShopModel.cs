using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class ShopModel
    {
        public int ShopID { get; set; }
        [Required]
        public string ShopName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNr { get; set; }
        
        public string LocationID { get; set; } //foreign key for LocationModel
        public LocationModel Location { get; set; } //navigation property for LocationModel

    }
}
