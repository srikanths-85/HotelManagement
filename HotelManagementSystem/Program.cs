using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using Home;
using Staff;
using LoginForm;
using Admin;
using System;
using System.Threading;

namespace HotelManagement
{
    public class HotelManagementSystem
    {
         static async Task Main(string[] args)
        {
            // Set up Serilog configuration to write logs to the console and a file
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console() // Console logging (plain text)
                .WriteTo.File(
                    path: "logs/log-.log",  // Use `.json` extension for clarity
                    rollingInterval: RollingInterval.Day,  // Ensure you have this correct
                    retainedFileCountLimit: 7,  // Keep logs for the last 7 days
                    formatter: new JsonFormatter() // JSON formatting for file logs
                )
                .CreateLogger();

            // Set up Dependency Injection (DI)
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog())  // Integrates Serilog with Microsoft.Extensions.Logging
                .AddSingleton<StaffControls>()  // Register StaffControls
                .AddSingleton<AdminControls>()  // Register AdminControls
                .AddSingleton<LoginHandler>()  // Register LoginHandler
                .AddSingleton<HomePage>()  // Register HomePage
                .BuildServiceProvider();

            // Resolve the HomePage instance and invoke the RunApplication method
            var homePage = serviceProvider.GetRequiredService<HomePage>();

            try
            {
                Console.WriteLine("Welcome To GINGER Hotel!");
                Console.WriteLine("Please Login...");
                Thread.Sleep(400);

                // Start the application
                await homePage.RunApplication();
            }
            catch (Exception ex)
            {
                // Log the exception if something goes wrong
                Log.Fatal(ex, "An unexpected error occurred while running the Hotel Management System.");
            }
            finally
            {
                // Ensure to flush and close the log when the program ends
                Log.CloseAndFlush();
            }
        }
    }
}
