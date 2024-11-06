using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.ViewModel
{
    public class EditProductModel
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


