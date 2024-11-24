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

        /// <summary>
        /// Retrieve a paginated list of products from the database.
        /// </summary>
        /// <param name="pageNumber">
        /// The page number to retrieve, starting from 1. Defaults to 1 if not specified.
        /// </param>
        /// <param name="pageSize">
        /// The number of items to retrieve per page. Defaults to 100 if not specified.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of <see cref="ProductModel"/> objects, representing the products on the specified page.
        /// Null values are allowed for individual items in the list.
        /// </returns>

        public async Task<IEnumerable<ProductModel>> GetAllItems(int pageNumber = 1, int pageSize = 100)
        {
            var query = _context.Product_detail.AsQueryable(); 
            //pagination - a formula of skipping a certain number of items and taking a certain number of items
            var skipResult = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResult).Take(pageSize);

            return await query.ToListAsync(); // We take data from the database, and split them into how the pagination is setup above.


        }

        /// <summary>
        /// Deletes a product from the database by its ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the product to be deleted.
        /// </param>
        /// <returns>
        /// The <see cref="ProductModel"/> object that was deleted, or <c>null</c> if no product with the specified ID was found.
        /// </returns>
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
        /// <summary>
        /// Retrieves a single product from the database by its unique ID.
        /// </summary>
        /// <param name="elementId">
        /// The unique identifier of the product to retrieve.
        /// </param>
        /// <returns>
        /// The <see cref="ProductModel"/> object corresponding to the specified ID, 
        /// or <c>null</c> if no product with the given ID exists in the database.
        /// </returns>
        public async Task<ProductModel> GetItemById(int Id)
        {

           return await _context.Product_detail.Where(p => p.ProductID == Id).FirstOrDefaultAsync();

        }

        /// <summary>
		/// Add a product to the database
		/// </summary>
		/// <param name="productModel"> This product taking parameter will be added to the database. </param>
		/// /// <returns>The <see cref="ProductModel"/> object that was added, with updated information from the database (e.g., generated ID).</returns>
		/// <returns></returns>
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

        /// <summary>
        /// Update a product's details in the database.
        /// </summary>
        /// <param name="productModel">
        /// The <see cref="ProductModel"/> containing updated details that will replace existing data in the database.
        /// </param>
        /// <returns>
        /// The updated <see cref="ProductModel"/> object after the changes have been saved to the database.
        /// </returns>
        public async Task<ProductModel> UpdateItems(ProductModel productModel)
        {
             _context.Product_detail.Update(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        /// <summary>
        /// Get the total count of products in the database.
        /// </summary>
        /// <returns>
        /// The total number of products in the <see cref="Product_detail"/> table.
        /// </returns>
        public async Task<int> CountPage()
        {
            return await _context.Product_detail.CountAsync();
        }


    }
}
