using Microsoft.EntityFrameworkCore;

namespace MatFrem.Models.DomainModel
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Precision(16,2)]
        public decimal UnitPrice { get; set; }

        public ProductModel Product { get; set; } = new ProductModel();

    }
}
