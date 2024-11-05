using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace MatFrem.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _context;
        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel?>> GetAllItems()
        {
            var items = await _context.Product_detail.Take(50).ToListAsync(); //remember, Product_detail is the object created in the AppDBContext
            return items;
        }

        public async Task<ProductModel> DeleteItem(int id)
        {
            var itemId = await _context.Product_detail.FindAsync(id); //remember, Product_detail is the object created in the AppDBContext
            if (itemId != null)
            {
                _context.Product_detail.Remove(itemId);
                await _context.SaveChangesAsync();
            }

            return itemId;
        }

        public async Task<ProductModel?> GetItemById(int elementId)
        {

           return await _context.Product_detail.Where(x => x.ProductID == elementId).FirstOrDefaultAsync();

        }

        public async Task<ProductModel> InsertProduct(ProductModel productModel)
        {
            await _context.Product_detail.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<ProductModel> Save()
        {
            await _context.SaveChangesAsync();
            return new ProductModel();
        }

        public async Task<ProductModel> UpdateItems(ProductModel productModel)
        {
             _context.Product_detail.Update(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

    }
}
