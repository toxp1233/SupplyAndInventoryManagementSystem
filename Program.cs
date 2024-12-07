using InventoryManagement.Services;
using ISMS;
using ISMS.Controllers;
using ISMS.Interfaces;
using ISMS.Models;
using ISMS.Services;
using System.Diagnostics.Contracts;


namespace ISMS
{
    class Program
    {
     

        static void Main()
        {

            bool Exit = false;
            while (!Exit)
            {
                programService program = new programService();
                Console.WriteLine("Hello Guest!!!!, please create a account first or log into the existing one");
                Console.WriteLine("1.create Account");
                Console.WriteLine("2.log in");
                Console.WriteLine("3.exit");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        program.CreateAccount();
                        break;
                    case 2:
                        program.LogIn();
                        break;
                    case 3:
                        Console.WriteLine("GoodBye");
                        Exit = true;
                        break;
                    default: Console.WriteLine("invalid Choice"); break;

                }
            }
        }
    }
}
