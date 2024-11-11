using MatFrem.DataContext;
using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IProductRepository
    {
        //IEnumerable is an interface that allows you to iterate over a collection
        //of elements
        //but it doesn not provide methods to modify the collection (add, remove, etc)
        Task<IEnumerable<ProductModel>> GetAllItems(int pageNumber = 1, int pageSize = 100);
        Task<ProductModel> InsertProduct(ProductModel model);
        Task<ProductModel?> GetItemById(int id);
        Task<ProductModel?> DeleteItem(int id);
        Task<ProductModel?> UpdateItems(ProductModel productModel);

        Task<int> CountPage();
 
        Task<ProductModel> Save();

    }
}
