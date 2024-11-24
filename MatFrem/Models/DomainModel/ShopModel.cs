

namespace MatFrem.Models.DomainModel
{
    public class ShopModel
    {
        public int ShopID { get; set; }
        public string? ShopName { get; set; }
        public ICollection<OrderModel> OrderModels { get; set; } //indicates many to many relationship
	}
}
