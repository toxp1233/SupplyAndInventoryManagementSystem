using ISMS.Data;
using ISMS.Interfaces;
using ISMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ISMS.Services
{
    internal class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService()
        {
            _context = new AppDbContext(); 
        }
        public void AddSupplier(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new Exception("failed to add supplier (missing information)");
            }

            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void DeleteSupplier(int supplierId)
        {
            var supplier = _context.Suppliers.Include(p => p.Products).SingleOrDefault(s => s.Id == supplierId);

            if (supplier == null)
            {
                throw new Exception("something went wrong (try again)");
            }
            else if (supplier.Products != null)
            {
                throw new Exception("The Supplier is linked to a product and cannot be removed. Remove the products first");
            }
            else
            {
                _context.Remove(supplier);
                _context.SaveChanges();
            }


            Console.WriteLine("succesfully removed the supplier");
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.Include(S => S.Products);
        }

        public Supplier GetSupplierById(int supplierId)
        {
            var supplier = _context.Suppliers.SingleOrDefault(s => s.Id == supplierId);

            if (supplier == null)
            {
                throw new Exception("something went wrong (try again)");
            }

            return supplier;
        }

        public void UpdateSupplier(int supplierId, Supplier supplier)
        {
            var existingSupplier = _context.Suppliers.SingleOrDefault(s => s.Id == supplierId);

            if (existingSupplier == null)
            {
                throw new Exception("Supplier not found.");
            }
            existingSupplier.Name = supplier.Name;
            existingSupplier.ContactInfo = supplier.ContactInfo;
            _context.SaveChanges();
        }
    }
}
