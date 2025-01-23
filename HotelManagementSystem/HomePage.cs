using System;
using LoginForm;
using HotelManagement.Utilities;

namespace Home
{
    public class HomePage
    {
        private readonly LoginHandler _loginHandler;

        // Constructor Injection for LoginHandler
        public HomePage(LoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        // Application starts here
        public async Task RunApplication()
        {
            Logger.LogInfo("Hotel Management System Application Started...");

            await Task.Delay(1);
            while (true)
            {
                Console.WriteLine("\nPlease select any one of the options:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");
                Console.Write("Please Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    try
                    {
                        _loginHandler.Run();
                    }
                    catch (Exception ex) //unexpected errors are handled here
                    {
                        Logger.LogError("An error occurred during login: " + ex.Message);
                        Console.WriteLine("An error occurred during login: " + ex.Message);
                    }
                }
                else if (choice == "2")
                {
                    Logger.LogInfo("Application Terminated...");
                    Console.WriteLine("Thank You. Have a pleasant day!!!");
                    break;
                }
                else
                {
                    Logger.LogError("Invalid option. Please enter the correct option and try again.");
                    Console.WriteLine("Invalid option. Please enter the correct option and try again.");
                }
            }
        }
    }
}
