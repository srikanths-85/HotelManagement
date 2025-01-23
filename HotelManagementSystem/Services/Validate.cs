using System;
using System.Text.RegularExpressions;
using Form.MaskPassword;
using HotelManagement.Utilities;

namespace Validation
{
    public class ValidationHandler
    {
        public static int GetValidatedAge()
        {
            Logger.LogInfo("Age Validation is Checked.");
            int age = 0;

            // Use a while loop to keep asking until the user enters a valid age.
            while (true)
            {
                Console.Write("Enter Age: ");
                string input = Console.ReadLine();

                // Try to convert the input to an integer.
                bool isValid = int.TryParse(input, out age);

                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for age.");
                    continue;
                }

                // Check if the age is within a valid range.
                if (age <= 0 || age >= 120)
                {
                    Console.WriteLine(" Please enter a valid age (between 1 and 119).");
                }
                else
                {
                    break;  // Break the loop if the age is valid.
                }
            }

            return age;
        }
 
        public static bool GetEmail(string email)
        {
            Logger.LogInfo("Email Validation is Checked.");
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex test = new Regex(pattern);

                if (test.IsMatch(email))
                    return true;
                else
                    Console.WriteLine("Please enter a valid email address.");
            return false;
        }

        internal static bool GetPhoneNumber(string phoneNumber)
        {
            Logger.LogInfo("Phone number Validation is Checked.");
                string pattern = @"^[6-9]\d{9}$";
                Regex test = new Regex(pattern);

                if (test.IsMatch(phoneNumber))
                    return true;
                else
                    Console.WriteLine("Please enter a valid Phone Number (must start with 6-9 and have 10 digits).");
            return false;
        }
    
        public static string GetValidatedInput(string message, Func<string, bool> validation)
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (!validation(input))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!validation(input));

            return input;
        }

        public static bool ValidateUsername(string username)
        {
            return !string.IsNullOrEmpty(username) && !char.IsDigit(username[0]);
        }

        public static bool ValidateEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && GetEmail(email);
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && GetPhoneNumber(phoneNumber); // Add more phone number validation if needed
        }

        public static bool ValidateNonEmpty(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static string GetValidatedPassword(string message)
        {
            string password;
            do
            {
                Console.Write(message);
                password = MaskPassword.getMaskedPassword();

                // Check for password requirements and show relevant messages if the conditions are not met

                if (password.Length < 8)
                {
                    Console.WriteLine("Password should be at least 8 characters long.");
                }

                if (!password.Any(char.IsUpper))
                {
                    Console.WriteLine("Password should contain at least one uppercase letter.");
                }

                if (!password.Any(char.IsDigit))
                {
                    Console.WriteLine("Password should contain at least one digit.");
                }

                if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    Console.WriteLine("Password should contain at least one special character.");
                }

                // Continue looping until all conditions are valid
            } while (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsDigit) || !password.Any(ch => !char.IsLetterOrDigit(ch)));

            return password;
        }

        public static string GetValidatedRole()
        {
            string role;
            do
            {
                Console.Write("Enter role (admin/staff): ");
                role = Console.ReadLine().ToLower();
                if (role != "admin" && role != "staff")
                {
                    Console.WriteLine("Invalid role. Please enter 'admin' or 'staff'.");
                }
            } while (role != "admin" && role != "staff");

            return role;
        }
    
        public static bool ValidateGuestName(string guestName)
        {
            guestName = guestName.Trim();
            // Ensure the guest name is not empty and only contains letters and spaces
            return !string.IsNullOrEmpty(guestName) && guestName.All(c => char.IsLetter(c) || c == ' ');
        }

        internal static bool GetIsNumber(string roomNumber)
        {
           
           return roomNumber.All(char.IsDigit)?true:false;
        }
    }
}