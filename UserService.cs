using ISMS.Data;
using ISMS.Interfaces;
using ISMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ISMS.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;


        public UserService()
        {
            _context = new AppDbContext();
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Supplier cannot be null.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User GetUserById(int userId)
        {
            var user = _context.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = _context.Users.Include(u => u.Role).SingleOrDefault(u => u.UserName == username);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;

        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public User Login(string username, string password)
        {
            var user = _context.Users.Include(u => u.Role).SingleOrDefault(u => u.UserName == username);

            if (user == null)
            {
                throw new Exception("user not found, try again or register");
            }

            if (!VerifyPasswordHash(password, user.PasswordHush, user.PasswordSalt))
            {
                throw new Exception("Invalid password");
            }

            return user;

        }

        public void Register(User user, string password)
        {
            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                throw new Exception("username already exists");
            }

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                throw new Exception("Email Is Already Used");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHush = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.DateCreated = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();

            Console.WriteLine("Welcome new user!");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        public void CreateAccount()
        {
            Console.Clear();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);



            Console.WriteLine("Select Role:");
            Console.WriteLine("1.Admin");
            Console.WriteLine("2.Manager");
            Console.WriteLine("3.Viewer");
            Console.Write("Enter your choice (1-3): ");



            int roleChoice = int.Parse(Console.ReadLine());
            var role = roleChoice switch
            {
                1 => ISMS.Enums.RoleType.Admin,
                2 => ISMS.Enums.RoleType.Manager,
                3 => ISMS.Enums.RoleType.Viewer,
                _ => throw new InvalidOperationException("Invalid role.")
            };

            // Set canGrantAdminRights to true if the role is Admin
            bool canGrantAdminRights = false;
            if (roleChoice == 1)
            {
                canGrantAdminRights = true;
            }

            try
            {
                Register(new User
                {
                    UserName = username,
                    Email = email,
                    PasswordHush = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = (int)role,
                    CanGrantAdminRights = canGrantAdminRights,
                    DateCreated = DateTime.Now
                }, password);

                Console.WriteLine("Account created successfully!");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error creating account: {error.Message}");
            }
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User cannot be null.");
            }

            var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            // Update only non-sensitive fields
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.RoleId = user.RoleId;

            // DO NOT update PasswordHash or PasswordSalt unless explicitly required
            _context.SaveChanges();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new Exception("try again (empty)");
            }

            using (var sha256 = SHA256.Create())
            {
                passwordSalt = GenerateSalt();
                var combinedPassword = Combine(password, passwordSalt);
                passwordHash = sha256.ComputeHash(combinedPassword);
            }

        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var sha256 = SHA256.Create())
            {

                var combinedPassowrd = Combine(password, storedSalt);
                var computedHash = sha256.ComputeHash(combinedPassowrd);
                return storedHash.SequenceEqual(computedHash);
            }
        }

        private byte[] GenerateSalt()
        {
            var Salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }
            return Salt;
        }

        private byte[] Combine(string password, byte[] salt)
        {
            var passwordByte = Encoding.UTF8.GetBytes(password);
            var combined = new byte[passwordByte.Length + salt.Length];
            Buffer.BlockCopy(passwordByte, 0, combined, 0, passwordByte.Length);
            Buffer.BlockCopy(salt, 0, combined, passwordByte.Length, salt.Length);
            return combined;
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            _context.Remove(user);
            _context.SaveChanges();

            Console.WriteLine("succesfully removed the user");
        }
    }
}
