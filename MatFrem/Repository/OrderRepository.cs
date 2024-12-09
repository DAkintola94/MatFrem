using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrder(int pageNumber = 1, int pageSize = 100)
        {
			var query = _context.Orders.AsQueryable();
			//pagination - a formula of skipping a certain number of items and taking a certain number of items
			var skipResult = (pageNumber - 1) * pageSize;
			query = query.Skip(skipResult).Take(pageSize);

			return await query.ToListAsync();
		}

        public async Task<OrderModel?> AddOrder(OrderModel orderModel)
        {
            await _context.Orders.AddAsync(orderModel);
            await _context.SaveChangesAsync();
            return orderModel;
        }

        public async Task<OrderModel?> DeleteOrder(int id)
        {
            var deleteOrder = await _context.Orders.FindAsync(id); //remember, Locations is the object created in the AppDBContext
            if (deleteOrder != null)
            {
                _context.Orders.Remove(deleteOrder);
                await _context.SaveChangesAsync();
                return deleteOrder;
            }

            return null;
        }

        public async Task<OrderModel?> UpdateOrder(OrderModel orderModel)
        {
            if (orderModel != null)
            {
                _context.Orders.Update(orderModel);
                await _context.SaveChangesAsync();
                return orderModel;
            }
            return null;
        }


        public async Task<OrderModel?> GetOrderByID(int id)
        {
            return await _context.Orders.Where(x => x.OrderID == id).FirstOrDefaultAsync();
        }

        public async Task<OrderModel> Save()
        {
            await _context.SaveChangesAsync();
            return new OrderModel();
        }

    }
}
