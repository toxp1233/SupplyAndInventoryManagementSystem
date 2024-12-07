using InventoryManagement.Services;
using ISMS.Controllers;
using ISMS.Data;
using ISMS.Interfaces;
using ISMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMS.Services
{
    public class programService
    {
        private readonly IUserService _userService;
        private readonly AdminController _adminController;
        private readonly ManagerController _managerController;
        private readonly ViewerController _viewerController;
        private readonly AppDbContext _context;
        public programService()
        {
            _userService = new UserService();
            _adminController = new AdminController();
            _managerController = new ManagerController();
            _viewerController = new ViewerController();
            _context = new AppDbContext();
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
            _userService.CreatePasswordHash(password, out passwordHash, out passwordSalt);



            Console.WriteLine("Select Role:");
            Console.WriteLine("1.Manager");
            Console.WriteLine("2.Viewer");
            Console.Write("Enter your choice (1-3): ");



            int roleChoice = int.Parse(Console.ReadLine());
            var role = roleChoice switch
            {
                1 => ISMS.Enums.RoleType.Manager,
                2 => ISMS.Enums.RoleType.Viewer,
                _ => throw new InvalidOperationException("Invalid role.")
            };



            try
            {
                _userService.Register(new User
                {
                    UserName = username,
                    Email = email,
                    PasswordHush = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = (int)role,
                    DateCreated = DateTime.Now
                }, password);

                Console.WriteLine("Account created successfully!");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error creating account: {error.Message}");
            }
        }

        public void LogIn()
        {
            Console.Clear();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            try
            {
                var user = _userService.Login(username, password);
                Console.WriteLine($"Welcome back, {user.UserName}!");
                switch (user.RoleId)
                {
                    case (int)ISMS.Enums.RoleType.Admin:
                        _adminController.RunAdmin();
                        break;
                    case (int)ISMS.Enums.RoleType.Manager:
                        _managerController.RunManager();
                        break;
                    case (int)ISMS.Enums.RoleType.Viewer:
                        _viewerController.Run();
                        break;
                    default:
                        Console.WriteLine("Invalid role.");
                        break;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"{error.Message}");
            }
        }
    }
}
