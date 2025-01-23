using System;
using Authenticate;
using Admin;
using Staff;
using Microsoft.Extensions.Logging;

namespace LoginForm
{
    // Staff and Admin Pannel are redirected based on the role
    public class LoginHandler
    {
        private readonly ILogger<LoginHandler> _logger;
        private readonly StaffControls _staffControls;
        private readonly AdminControls _adminControls;

        // Constructor Injection for Logger, StaffControls, and AdminControls
        public LoginHandler(ILogger<LoginHandler> logger, StaffControls staffControls, AdminControls adminControls)
        {
            _logger = logger; //Logger to record changes
            _staffControls = staffControls; //staff pannel is accessed using this reference
            _adminControls = adminControls; //admin pannel is accessed using this reference
        }

        public void Run()
        {
            _logger.LogInformation("Login Screen Invoked.");
            while (true)
            {
                Console.WriteLine("\nHotel Management System");
                Console.WriteLine("1. Login as Admin");
                Console.WriteLine("2. Login as Staff");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": 
                            if (AuthenticateHandler.Login("admin"))
                            {
                                _logger.LogInformation("Login Successful. Admin Panel Invoked.");
                                AdminControls.AdminPanel(); // Admin panel logic handled
                                break; 
                            }
                            else
                            {
                                _logger.LogError("Login Failed. Invalid Credentials");
                                Console.WriteLine("Login failed. Please check your credentials and try again.");
                            }
                            break;

                    case "2": 
                            if (AuthenticateHandler.Login("staff"))
                            {
                                _logger.LogInformation("Login Successful. Staff Panel Invoked.");
                                _staffControls.StaffPanel(); // Staff panel logic handled
                                break;
                            }
                            else
                            {
                                _logger.LogError("Login Failed. Invalid Credentials");
                                Console.WriteLine("Login failed. Please check your credentials and try again.");
                            }
                            break;

                    case "3": // Login page is exited
                            _logger.LogInformation("Login Screen Exited");
                            Console.WriteLine("Exiting... Goodbye!");
                            return;

                    default:
                        _logger.LogError("User Entered Invalid input");
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
