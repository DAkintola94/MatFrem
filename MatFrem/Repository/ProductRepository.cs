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


        public async Task<IEnumerable<ProductModel>> GetAllItems(int pageNumber = 1, int pageSize = 100)
        {
            var query = _context.Product_detail.Include(sM => sM.ShopModelO)
                .AsQueryable(); 
            //pagination - a formula of skipping a certain number of items and taking a certain number of items
            var skipResult = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResult).Take(pageSize);

            return await query.ToListAsync(); // We take data from the database, and split them into how the pagination is setup above.
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
      
        public async Task<ProductModel?> GetItemById(int Id)
        {

           return await _context.Product_detail
                .Include(sM => sM.ShopModelO)
                .Where(p => p.ProductID == Id)
                .FirstOrDefaultAsync();
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

      
        public async Task<int> CountPage()
        {
            return await _context.Product_detail.CountAsync();
        }


    }
}
