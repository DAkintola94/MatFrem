using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDBContext _context;
        public ShoppingCartRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartModel>> GetAllItems()
        {
            var getData = await _context.ShoppingCart.Include(p => p.Product).AsQueryable().Take(50).ToListAsync();
            return getData;
        }

        public async Task<ShoppingCartModel> InsertCart(ShoppingCartModel model)
        {
             await _context.ShoppingCart.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;

        }
        public async Task<ShoppingCartModel?> GetCartById(int id)
        {
            return await _context.ShoppingCart
                .Include(p => p.Product)
                .FirstOrDefaultAsync(i => i.ShoppingCartID == id);
            //return await _context.ShoppingCart.FirstOrDefaultAsync(i => i.ShoppingCartID == id);
        }
        public async Task<ShoppingCartModel?> DeleteCart(int id)
        {
            var cartId = await _context.ShoppingCart.FindAsync(id);
            if (cartId != null)
            {
                _context.ShoppingCart.Remove(cartId);
                await _context.SaveChangesAsync();
            }
            return cartId;
        }

        public async Task<ShoppingCartModel?> UpdateCart(ShoppingCartModel cartModel)
        {
            _context.ShoppingCart.Update(cartModel);
            await _context.SaveChangesAsync();
            return cartModel;
        }

        public async Task<ShoppingCartModel> Save()
        {
            await _context.SaveChangesAsync();
            return new ShoppingCartModel();
        }
    }
}
