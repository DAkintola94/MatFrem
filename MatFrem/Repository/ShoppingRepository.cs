using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace MatFrem.Repository
{
    public class ShoppingRepository : IShoppingRepository
    {
        private readonly AppDBContext _context;
        public ShoppingRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel?>> GetAllItems()
        {
            var items = await _context.Product_detail.Take(50).ToListAsync();
            return items;
        }


        public async Task<ShopModel?> FindShopItemByIdAsync(int elementId)
        {

            return await _context.Shopping_items
                .Include(s => s.Location_Id)
                .Include(s => s.ShopID)
                .Include(s => s.ShopName)
                .Include(s => s.PhoneNr)
                .FirstOrDefaultAsync(s => s.ShopID == elementId);


        }

    }
}
