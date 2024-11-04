using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.DomainModel
{
    public class ProductModel
    {
        
        public Guid ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; } = "No description available";
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public string ProductCategory { get; set; }

        [NotMapped]
        public string ProductImage { get; set; }



    }
}
