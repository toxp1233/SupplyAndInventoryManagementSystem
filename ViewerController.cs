using ISMS.Interfaces;
using ISMS.Services;

namespace ISMS.Controllers
{
    public class ViewerController
    {
        private readonly IProductService _productService;
        private readonly OrderService _orderService;
        private readonly AlertService _alertService;
        private readonly ManagerController _managerController;

        public ViewerController()
        {
            _productService = new ProductService();
            _orderService = new OrderService();
            _alertService = new AlertService();
            _managerController = new ManagerController();
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hello Viewer!!!!");
                Console.WriteLine("1.View Products");
                Console.WriteLine("2.View Orders");
                Console.WriteLine("3.Check Alerts");
                Console.WriteLine("4.Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ViewProducts();
                        break;
                    case 2:
                        _managerController.ViewOrders();
                        break;
                    case 3:
                        _managerController.CheckAlerts();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ViewProducts()
        {
            Console.Clear();
            var products = _productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"id: {product.Id} product: {product.Name} Stock: {product.QuantityInStock}");
            }

            Console.WriteLine("press next");
            Console.ReadKey();
        }
    }
}
