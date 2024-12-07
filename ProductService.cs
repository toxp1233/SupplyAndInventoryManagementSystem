
using ISMS.Data;
using ISMS.Interfaces;
using ISMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ISMS.Services
{
    internal class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService()
        {
            _context = new AppDbContext();
        }

        public void AddProduct(Product product)
        {

            var newProduct = product;
            if (product == null)
            {
                throw new Exception("failed to add supplier (missing information)");
            }
           else if (newProduct.ReorderLevel > newProduct.QuantityInStock)
            {
                throw new Exception("reorderLevel cannot be higher than quantity");
            }
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        public void DeleteStock(int productId)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception($"Product does not exist.");
            }

            _context.Remove(product);
            _context.SaveChanges();

            Console.WriteLine("succesfully removed the product");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Supplier);
        }

        public IEnumerable<Product> GetLowStockProducts()
        {

            return _context.Products
                  .Where(p => p.QuantityInStock < p.ReorderLevel)
                  .ToList();
        }

        public Product GetProductById(int productId)
        {
            var product = _context.Products
                   .Include(p => p.Supplier)
                   .SingleOrDefault(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception($"Product does not exist.");


            }
            return product;


        }


        public void UpdateStock(int productId, int quantity)
        {
            var product = _context.Products
                  .Include(p => p.Supplier)
                  .SingleOrDefault(p => p.Id == productId);
            if (product == null)
            {
                throw new Exception($"Product does not exist.");
            } 
            
           

            

            product.QuantityInStock += quantity;
            _context.SaveChanges();
            Console.WriteLine($"succes!! now the stock for the product is {product.QuantityInStock}");
        }
    }
}
