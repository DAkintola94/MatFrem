using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetAllOrder(int pageNumber = 1, int pageSize = 100);
        Task<OrderModel?> AddOrder(OrderModel orderModel);
        Task<OrderModel?> GetOrderByID(int id);
        Task<OrderModel?> UpdateOrder(OrderModel orderModel);
        Task<OrderModel?> DeleteOrder(int id);
        Task<OrderModel> Save();

    }
}
