using ISMS.Enums;
using ISMS.Models;

namespace InventoryManagement.Services
{
    public interface IRoleService
    {
        public IEnumerable<Role> GetAllRoles();
        public void AssignRoleToUser(int userId, int roleId, RoleType newRole);
    }
}