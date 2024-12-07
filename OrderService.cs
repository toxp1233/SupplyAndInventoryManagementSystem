
using ISMS.Data;
using ISMS.Interfaces;
using ISMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ISMS.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService()
        {
            _context = new AppDbContext();
        }
        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new Exception ("order Failed (missing information)");
            }

            var product = _context.Products.SingleOrDefault(p => p.Id == order.ProductId);

            if (product == null)
            {
                throw new Exception($"Product does not exist.");
            }

            else if (order.QuantityOrdered > product.QuantityInStock)
            {
                throw new Exception($"Product is not enough, heres the amount available ${product.QuantityInStock}");
            } else if(order.DeliveryDate < DateTime.UtcNow)
            {
                throw new Exception("wrong time");
            }

            product.QuantityInStock -= order.QuantityOrdered;

            _context.Orders.Add(order);
            _context.SaveChanges();
            Console.WriteLine("Order created Succesfully");
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.SingleOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("order not found");
            }

            var product = _context.Products.SingleOrDefault(p => p.Id == order.ProductId);
            if (product != null)
            {
                product.QuantityInStock += order.QuantityOrdered;
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            Console.WriteLine("Order Deleted Succesfully");

        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Product.Supplier);
        }

        public Order GetOrderById(int orderId)
        {
            var order = _context.Orders.Include(o => o.Product).SingleOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("order not found");
            }

            return order;

        }
    }
}
