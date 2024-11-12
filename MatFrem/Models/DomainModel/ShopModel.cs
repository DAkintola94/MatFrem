

namespace MatFrem.Models.DomainModel
{
    public class ShopModel
    {
        public int ShopID { get; set; }
        public string? ShopName { get; set; }
        public Guid LocationID { get; set; } //foreign key for LocationModel
        public LocationModel Location { get; set; } //Navigation property for LocationModel, ShopModel have a Location, this helps to get all of Location properties/info
        public ICollection<ProductModel> ProductsModel { get; set; }  //the list indicate many to many
        public ICollection<OrderModel> OrderModels { get; set; } //indicates many to many relationship
	}
}
