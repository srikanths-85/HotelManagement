using System;
using LoginSpace.CustomExceptions;
using Form.MaskPassword;
using Application;
using HotelManagement.Utilities;
using Validation;

namespace Authenticate
{
    //this class to handle Login and register for staff and admin
    public class AuthenticateHandler
    {
        //this method handles admin or staff login 
        public static bool Login(string role)
        {
            Logger.LogInfo($"{role} : Login Is Invoked.");   
            try
            {
                Console.WriteLine("\n------------------- Login -------------------");
                Console.Write("Enter the username: ");
                string username = Console.ReadLine();

                // Throw exception if the username starts with a number
                if (string.IsNullOrEmpty(username) || Char.IsDigit(username[0]))
                {
                    Logger.LogDebug("Invalid Username");
                    throw new InvalidUsernameException("Username should not start with a number.");
                }

                Console.Write("Enter the password: ");
                string password = MaskPassword.getMaskedPassword();
                
                // Check if the username and password match any registered user
                if(ApplicationDbContext.Login(username, password, role)) 
                {
                    Logger.LogInfo($" {username} Logined Successful");
                    Console.WriteLine("Login successful! Welcome, " + username);
                    return true;
                }
                Logger.LogError("Invalid username or password.");
                throw new InvalidLoginCredentialsException("Invalid username or password.");
            }
            catch (InvalidUsernameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidLoginCredentialsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Unexpected Error occured during Login" + ex.Message);
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            return false;
        }
        //this method handles new admin or staff Register
        public static void Register()
        {
            bool isUsernameTaken;
            Logger.LogInfo("Register Method Invoked");

            try
            {
                Console.WriteLine("\n-------------------- Register --------------------");

                // Loop to get a unique username
                string username;
                do
                {
                    username = ValidationHandler.GetValidatedInput("Enter a new username: ", ValidationHandler.ValidateUsername);
                    isUsernameTaken = ApplicationDbContext.UserExist(username);  // Check if the username is already taken
                    if (isUsernameTaken)
                    {
                        Console.WriteLine("Username is already taken. Please choose another one.");
                    }
                } while (isUsernameTaken);  // Continue until a unique username is provided

                // Collect other details using validation methods
                //all the details are collected here and sent to database
                string password = ValidationHandler.GetValidatedPassword("Enter the Password: ");
                string emailId = ValidationHandler.GetValidatedInput("Enter your email: ", ValidationHandler.ValidateEmail);
                int age = ValidationHandler.GetValidatedAge();
                string phoneNumber = ValidationHandler.GetValidatedInput("Enter your phone number: ", ValidationHandler.ValidatePhoneNumber);
                string address = ValidationHandler.GetValidatedInput("Enter the address: ", ValidationHandler.ValidateNonEmpty);
                string city = ValidationHandler.GetValidatedInput("Enter the City: ", ValidationHandler.ValidateNonEmpty);
                string pinCode = ValidationHandler.GetValidatedInput("Enter Pin Code: ", ValidationHandler.ValidateNonEmpty);
                string state = ValidationHandler.GetValidatedInput("Enter your State: ", ValidationHandler.ValidateNonEmpty);
                string role = ValidationHandler.GetValidatedRole();

                // Passing staff/admin details to Register the user with the validated details
                ApplicationDbContext.Register(username, password, emailId, age, phoneNumber, address, city, pinCode, state, role);

                Console.WriteLine("\n----------------------------------------------------");
            }
            catch (Exception ex) //unhandled errors are caught here
            {
                Logger.LogError("Unexpected error occurred during registration: " + ex.Message);
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

    }
}
