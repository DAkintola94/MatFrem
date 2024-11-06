using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class ProductModel
    {
        
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ProductCalories { get; set; } 
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public string ProductCategory { get; set; }

        [NotMapped]
        public string ProductImage { get; set; }



    }
}
