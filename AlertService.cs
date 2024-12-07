
using ISMS.Data;
using ISMS.Interfaces;
using ISMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ISMS.Services
{
    public class AlertService : IAlertService
    {
        private readonly AppDbContext _context;

        public AlertService()
        {
            _context = new AppDbContext();
        }

        public IEnumerable<Product> GetLowStockProducts()
        {
            return _context.Products
                   .Where(p => p.QuantityInStock < p.ReorderLevel)
                   .ToList();
        }

        public IEnumerable<Order> GetOverdueOrders()
        {
            return _context.Orders
               .Where(o => o.DeliveryDate != null && o.DeliveryDate < DateTime.Now)
               .Include(o => o.Product)
               .ToList();
        }

        public void NotifyLowStockAlerts()
        {
            var lowStockProducts = GetLowStockProducts();
            foreach (var product in lowStockProducts)
            {
                Console.WriteLine($"ALERT: Product '{product.Name}' is low on stock. Current stock: {product.QuantityInStock}");
            }
        }

        public void NotifyOverdueOrders()
        {
            var overdueOrders = GetOverdueOrders();
            foreach (var order in overdueOrders)
            {
                Console.WriteLine($"ALERT: Order for product '{order.Product.Name}' is overdue. Delivery was expected by {order.DeliveryDate}");
            }
        }
    }
}
