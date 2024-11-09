using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetAllOrder();
        Task<OrderModel?> AddOrder(OrderModel orderModel);
        Task<OrderModel?> GetOrderByID(int id);
        Task<OrderModel?> UpdateOrder(OrderModel orderModel);
        Task<OrderModel?> DeleteOrder(int id);
        Task<OrderModel> Save();

    }
}
