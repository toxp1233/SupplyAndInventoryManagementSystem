using InventoryManagement.Services;
using ISMS.Interfaces;
using ISMS.Services;
using ISMS.Models;
using ISMS.Enums;
using System;
namespace ISMS.Controllers
{
    public class AdminController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;


        public AdminController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
      
        }



        public void RunAdmin()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hello Admin!!!!");
                Console.WriteLine("1.Manage Users");
                Console.WriteLine("2.Manage Roles");
                Console.WriteLine("3.Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ManageUsers();
                        break;

                    case 2:
                        ManageRoles();
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void ManageUsers()
        {
            Console.Clear();
            Console.WriteLine("1.Add a User");
            Console.WriteLine("2.Delete a User");
            Console.WriteLine("3.Update a User");
            Console.WriteLine("4.get user by Id");
            Console.WriteLine("5.get user by userName");
            Console.WriteLine("6.View all Users");
            Console.WriteLine("7.Exit");
            int Choice = int.Parse(Console.ReadLine());

            switch (Choice)
            {
                case 1:
                    try
                    {
                        _userService.CreateAccount();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;

                case 2:
                    Console.WriteLine("userId:");
                    int userId = int.Parse(Console.ReadLine());
                    _userService.DeleteUser(userId);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine("enter userId");
                    int userId2 = int.Parse(Console.ReadLine());
                    var selectedUser = _userService.GetUserById(userId2);

                    Console.WriteLine($"user id:{selectedUser.Id}, username: {selectedUser.UserName}, email: {selectedUser.Email}, role: {selectedUser.RoleId}, dateTime created: {selectedUser.DateCreated}");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine();

                    try
                    {
                        var selectedUser1 = _userService.GetUserByUsername(username);
                        Console.WriteLine($"user id:{selectedUser1.Id}, username: {selectedUser1.UserName}, email: {selectedUser1.Email}, role: {selectedUser1.RoleId}, dateTime created: {selectedUser1.DateCreated}");

                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"{error.Message}");
                    }
                    break;


                case 6:
                    var allUsers = _userService.GetUsers();
                    foreach (var users in allUsers)
                    {
                        Console.WriteLine($"user id:{users.Id}, username: {users.UserName}, email: {users.Email}, roleId: {users.RoleId}, dateTime created: {users.DateCreated}");
                    }
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 7:

                    break;

                default: Console.WriteLine("invalid option"); break;
            }

        }

        public void ManageRoles()
        {
            Console.Clear();
            Console.WriteLine("1.Assign a role to a User");
            Console.WriteLine("2.view All Roles");
            Console.WriteLine("3.exit");
            int Choice = int.Parse(Console.ReadLine());

            switch (Choice)
            {

                case 1:
                    Console.WriteLine("enter userId");
                    int userId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter roleId");
                    int roleId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter the role");
                    Console.WriteLine("1. manager");
                    Console.WriteLine("2. admin");
                    Console.WriteLine("3. viewer");
                    int RoleChoice = int.Parse(Console.ReadLine());
                    RoleType chosenRole;

                    if (RoleChoice == 1)
                    {
                        chosenRole = RoleType.Manager;
                    }
                    else if (RoleChoice == 2)
                    {
                        chosenRole = RoleType.Admin;
                    }
                    else
                    {
                        chosenRole = RoleType.Viewer;
                    }


                    _roleService.AssignRoleToUser(userId, roleId, chosenRole);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

       
                 
                case 2:
                    var AllRoles = _roleService.GetAllRoles();
                    foreach (var roles in AllRoles)
                    {
                        Console.WriteLine($"RoleId: {roles.Id}, Description: {roles.description}, ");
                    }
               
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 3:

                    break;

                default: Console.WriteLine("invalid option"); break;

            }
        }
    }
}
