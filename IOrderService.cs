
namespace ISMS.Interfaces
{
    public interface IOrderService
    {
        public void CreateOrder(Order order);
        public Order GetOrderById(int orderId);
        public IEnumerable<Order> GetAllOrders();
        public void DeleteOrder(int orderId);
    }
}
