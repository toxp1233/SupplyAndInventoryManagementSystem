using ISMS.Enums;
using ISMS.Models;

namespace ISMS.Interfaces
{
    public interface IUserService
    {
        public void Register(User user, string password);
        public void CreateAccount();
        public User Login(string username, string password);
        public User GetUserById(int userId);
        public void DeleteUser(int userId);
        public void UpdateUser(User user);
        public User GetUserByUsername(string username);
        public IEnumerable<User> GetUsers();
        public void AddUser(User user);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
