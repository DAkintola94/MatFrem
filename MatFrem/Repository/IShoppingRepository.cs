using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IShoppingRepository
    {
        //IEnumerable is an interface that allows you to iterate over a collection
        //of elements
        //but it doesn not provide methods to modify the collection (add, remove, etc)
        Task<IEnumerable<ProductModel>> GetAllItems(); 
        Task<ProductModel> InsertProduct(ProductModel model);

        Task<ProductModel> GetItemById(int shopModel);
        Task<ProductModel> UpdateItemsAsync(ShopModel shopModel);
        Task<ProductModel> DeleteItem(int id);
        Task<ProductModel> Save();

    }
}
