using ISMS.Interfaces;
using ISMS.Models;
using ISMS.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMS.Controllers
{
    public class ManagerController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IAlertService _alertService;
        private readonly ISupplierService _supplierService;

        public ManagerController()
        {
            _productService = new ProductService();
            _orderService = new OrderService();
            _alertService = new AlertService();
            _supplierService = new SupplierService();
        }
        public void RunManager()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hello Manager!!");
                Console.WriteLine("1.Manage Products");
                Console.WriteLine("2.Manage Suppliers");
                Console.WriteLine("3.Manage Orders");
                Console.WriteLine("4.View Orders");
                Console.WriteLine("5.Check Alerts");
                Console.WriteLine("6.Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ManageProducts();
                        break;
                    case 2:
                        ManageSuppliers();
                        break;
                    case 3:
                        ManageOrders();
                        break;
                    case 4:
                        ViewOrders();
                        break;

                    case 5:
                        CheckAlerts();
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }


        public void ManageProducts()
        {
            Console.Clear();
            Console.WriteLine("1.Add Product");
            Console.WriteLine("2.Update Stock");
            Console.WriteLine("3.Delete Stock");
            Console.WriteLine("4.Check Low Stock Products");
            Console.WriteLine("5.get product by id");
            Console.WriteLine("6.get all products");
            Console.WriteLine("7.exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var suppliers = _supplierService.GetAllSuppliers();
                    Console.WriteLine("Suppliers:");
                    foreach (var supplier in suppliers)
                    {
                        Console.WriteLine($"ID: {supplier.Id},");
                    }
                    Console.WriteLine("supplierId: ");
                    int supplierId = int.Parse(Console.ReadLine());
                    Console.Write("Product Name: ");
                    string productName = Console.ReadLine();
                    Console.Write("Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Reorder Level: ");
                    int reorderLevel = int.Parse(Console.ReadLine());
                    _productService.AddProduct(new Product { Name = productName, QuantityInStock = quantity, ReorderLevel = reorderLevel, SupplierId = supplierId });
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Write("productId: ");
                    int productId = int.Parse(Console.ReadLine());
                    Console.Write("Quantity: ");
                    int quantity1 = int.Parse(Console.ReadLine());

                    _productService.UpdateStock(productId, quantity1);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.Write("productId: ");
                    int productId1 = int.Parse(Console.ReadLine());

                    _productService.DeleteStock(productId1);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 4:
                    var lowStockProducts = _productService.GetLowStockProducts();
                    foreach (var product in lowStockProducts)
                    {
                        Console.WriteLine($"{product.Id} {product.Name} Stock: {product.QuantityInStock}");
                    }
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.Write("productId: ");
                    int productId2 = int.Parse(Console.ReadLine());

                    var selectedProduct = _productService.GetProductById(productId2);
                    var supplierOfThatProduct = selectedProduct.Supplier;

                    Console.WriteLine($"product: {selectedProduct.Name}, {selectedProduct.QuantityInStock},supplier: {supplierOfThatProduct.Name} {supplierOfThatProduct.ContactInfo}");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 6:
                    var allProducts = _productService.GetAllProducts();
                   
                    foreach (var product in allProducts)
                    {
                        Console.WriteLine($"Product: Id: {product.Id} Name: {product.Name}, Stock:{product.QuantityInStock}, ReorderLevel:{product.ReorderLevel}, supplier: {product.Supplier.Name}, contact info: {product.Supplier.ContactInfo}");
                    }
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 7:

                    break;
                default: Console.WriteLine("invalid option"); break;


            }
        }

        public void ManageSuppliers()
        {
            Console.Clear();
            Console.WriteLine("1.Add Supplier");
            Console.WriteLine("2.Update Supplier");
            Console.WriteLine("3.Delete Supplier");
            Console.WriteLine("4.Get supplier by id");
            Console.WriteLine("5.get all suppliers");
            Console.WriteLine("6.exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter supplier name:");
                    string supplierName = Console.ReadLine();
                    Console.WriteLine("enter contact info: (optional) ");
                    string contactInfo = Console.ReadLine();

                    _supplierService.AddSupplier(new Supplier { Name = supplierName, ContactInfo = contactInfo });
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("enter supplierId:");
                    int supplierId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter supplier name:");
                    string supplierName1 = Console.ReadLine();
                    Console.WriteLine("enter contact info: (optional) ");
                    string contactInfo1 = Console.ReadLine();

                    _supplierService.UpdateSupplier(supplierId, new Supplier { Name = supplierName1, ContactInfo = contactInfo1 });
                    
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Write("SupplierId: ");
                    int supplierId1 = int.Parse(Console.ReadLine());

                    _supplierService.DeleteSupplier(supplierId1);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine("enter supplierId:");
                    int supplierId2 = int.Parse(Console.ReadLine());

                    try
                    {
                        var supplier = _supplierService.GetSupplierById(supplierId2);
                        Console.WriteLine($"Supplier ID: {supplier.Id}");
                        Console.WriteLine($"Name: {supplier.Name}");
                        Console.WriteLine($"Contact Info: {supplier.ContactInfo ?? "No contact info available"}");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"{error.Message}");
                    }

                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;


                case 5:
                    var suppliers = _supplierService.GetAllSuppliers();
                    Console.WriteLine("Suppliers:");
                    foreach (var supplier in suppliers)
                    {
                        Console.WriteLine($"ID: {supplier.Id}, Name: {supplier.Name}, Contact Info: {supplier.ContactInfo ?? "N/A"}");
                    }
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 6:
                    break;

                default: Console.WriteLine("Invalid choice"); break;
            }

        }

        public void ManageOrders()
        {
            Console.Clear();
            Console.WriteLine("1.Create Order");
            Console.WriteLine("2.Delete Order");
            Console.WriteLine("3.Get Order by ID");
            Console.WriteLine("4.Get All Orders");
            Console.WriteLine("5.Exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter Product ID:");
                    int productId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Quantity Ordered:");
                    int quantityOrdered = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Delivery Date: (optional, format: YYYY-MM-DD):");
                    string deliveryDateInput = Console.ReadLine();

                    DateTime? deliveryDate = null;
                    if (!string.IsNullOrWhiteSpace(deliveryDateInput))
                    {
                        deliveryDate = DateTime.Parse(deliveryDateInput);
                    }

                    try
                    {
                        _orderService.CreateOrder(new Order
                        {
                            ProductId = productId,
                            QuantityOrdered = quantityOrdered,
                            OrderDate = DateTime.Now,
                            DeliveryDate = deliveryDate
                        });
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"{error.Message}");
                    }
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Enter Product ID:");
                    int productId1 = int.Parse(Console.ReadLine());

                    _orderService.DeleteOrder(productId1);

                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("Enter order ID:");
                    int orderId = int.Parse(Console.ReadLine());

                    var selectedOrder = _orderService.GetOrderById(orderId);


                    Console.WriteLine($"Order ID: {selectedOrder.Id} Selected Product: {selectedOrder.Product.Name}, The Stock ordered: {selectedOrder.QuantityOrdered}");

                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 4:
                    ViewOrders();
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 5:
                    break;
                default: Console.WriteLine("invalid Choice"); break;
            }
        }
        public void ViewOrders()
        {
            Console.Clear();
            var orders = _orderService.GetAllOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Id: {order.Id} Product: {order.Product.Name} Quantity: {order.QuantityOrdered} Order Date: - {order.OrderDate}, Date to deliver {order.DeliveryDate}");
            }
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        public void CheckAlerts()
        {
            Console.Clear();
            _alertService.NotifyLowStockAlerts();
            _alertService.NotifyOverdueOrders();
            
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();      
            }
    }
}
