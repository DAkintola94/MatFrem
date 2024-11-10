using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MatFrem.Models.DomainModel;

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
        public OrderModel Order { get; set; }
        public LocationModel Location { get; set; }
        public ShopModel Shop { get; set; }

    }
}


