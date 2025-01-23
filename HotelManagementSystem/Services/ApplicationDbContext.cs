using System;
using Microsoft.Extensions.Configuration;

namespace DBConnection
{
    //Database Connection is handled here
    public static class DataBaseConnection
    {
        public static string connectionString;
        //Static constructor to be atomatically invoked
        static DataBaseConnection()
        {
            try
            {
                {
                    //Database connection and details are configured here
                    IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"D:\HotelManagementSystem\HotelManagementSystem\Services\appsettings.json", reloadOnChange: true, optional: false).Build();

                    connectionString = configuration.GetConnectionString("DefaultConnection");
                }
            }
            catch (Exception ex) //unhandled errors are caught here
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}