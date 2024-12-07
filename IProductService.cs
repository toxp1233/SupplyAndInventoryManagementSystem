using ISMS.Models;

namespace ISMS.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProductById(int productId);
        public void AddProduct(Product product);
        public void UpdateStock(int productId, int quantity);
        public void DeleteStock(int productId);
        public IEnumerable<Product> GetLowStockProducts();
    }
}
