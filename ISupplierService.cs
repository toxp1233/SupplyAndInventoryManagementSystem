
using ISMS.Models;

namespace ISMS.Interfaces
{
    internal interface ISupplierService
    {
        public IEnumerable<Supplier> GetAllSuppliers();
        public Supplier GetSupplierById(int supplierId);
        public void AddSupplier(Supplier supplier);
        public void DeleteSupplier(int supplierId);
        public void UpdateSupplier(int supplierId, Supplier supplier);
    }
}
