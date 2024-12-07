
using ISMS.Models;

namespace ISMS.Interfaces
{
    public interface IAlertService
    {
        public IEnumerable<Product> GetLowStockProducts();
        public void NotifyLowStockAlerts();
        public IEnumerable<Order> GetOverdueOrders();
        public void NotifyOverdueOrders();
    }
}
