
using InventoryManagement.Services;
using ISMS.Data;
using ISMS.Enums;
using ISMS.Models;

namespace ISMS.Services
{
    public class RoleService : IRoleService
    {
        RoleType AdminRole = RoleType.Admin;
        private readonly AppDbContext _context;

        public RoleService()
        {
            _context = new AppDbContext(); 
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles;
        }
        public void AssignRoleToUser(int userId, int roleId, RoleType newRole)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception($"User with ID {userId} does not exist.");
            }

            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                throw new Exception($"Role with ID {roleId} does not exist.");
            }

            user.RoleId = roleId;

            role.UserRole = newRole;

            if (role.UserRole == AdminRole)
            {
                user.CanGrantAdminRights = true;
            }
            else
            {
                user.CanGrantAdminRights = false;
            }

            _context.SaveChanges();
        }
    }
}
