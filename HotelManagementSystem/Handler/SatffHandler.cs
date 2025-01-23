using System;
using HotelManagement.Utilities;
using StoreData;
using Serilog;
using Microsoft.Extensions.Logging; // Ensure to import this namespace

namespace Staff
{
    //this class defines staff controls
    public class StaffControls
    {
        private readonly ILogger<StaffControls> _logger;

        // Constructor injection of Serilog logger
        public StaffControls(ILogger<StaffControls> logger)
        {
            _logger = logger;
        }

        // Make StaffPanel an instance method
        public void StaffPanel()
        {
            _logger.LogInformation("Staff Panel Invoked");  // Log with Serilog
            
            while (true)
            {
                Console.WriteLine("\n--- Staff Panel ---");
                Console.WriteLine("1. Check-In");
                Console.WriteLine("2. Check-Out");
                Console.WriteLine("3. Edit User Details");
                Console.WriteLine("4. View Active Rooms");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Change Password:");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        StoreDetailsInDb.CheckIn(); // Guest are registered here
                        break;
                    case "2":
                        StoreDetailsInDb.CheckOut(); // Guest are removed here
                        break;
                    case "3":
                        StoreDetailsInDb.EditUserDetails(); // Guest details are edited here
                        break;
                    case "4":
                        StoreDetailsInDb.ViewActiveRooms(); // View active rooms
                        break;
                    case "5":
                        _logger.LogInformation("Staff Panel Exited...");  // Log with Serilog
                        Console.WriteLine("Logging out...");
                        return;
                    case "6":
                        // You can implement the logic for changing password here
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
