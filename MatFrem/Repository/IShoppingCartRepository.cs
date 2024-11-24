using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCartModel>> GetAllItems();
        Task<ShoppingCartModel> InsertCart(ShoppingCartModel model);
        Task<ShoppingCartModel?> GetCartById(int id);
        Task<ShoppingCartModel?> DeleteCart(int id);
        Task<ShoppingCartModel?> UpdateCart(ShoppingCartModel cartModel);
        Task<ShoppingCartModel> Save();
    }
}
