using System;
using System.Collections.Generic;
using AdminServices;
using Authenticate;
using HotelManagement.Utilities;
using StoreData;

namespace Admin
{
    public class AdminControls
    {
        //admin controls are defined here
        public static void AdminPanel()
        {
            Logger.LogInfo("Admin Pannel Invoked");
            //instance created for admin class
            AdminControl adminControl = new AdminControl();
            while (true)
            {
                Console.WriteLine("\n--- Admin Panel ---");
                Console.WriteLine("1. View Staffs");
                Console.WriteLine("2. Register Staff/Admin");
                Console.WriteLine("3. View Active Rooms");
                Console.WriteLine("4. Edit User Details");
                Console.WriteLine("5. Logout");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        adminControl.ViewActiveStaffDetails(); //View active staff in hotel
                        break;
                    case "2":
                        AuthenticateHandler.Register(); //using this staff and admin are registered
                        break;
                    case "3":
                        StoreDetailsInDb.ViewActiveRooms(); // View rooms that were added by staff
                        break;
                    case "4":
                        StoreDetailsInDb.EditUserDetails(); // Guest details are edited here
                        break;
                    case "5":
                        Console.WriteLine("Logging out...");// Admin Pannel exists here
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
