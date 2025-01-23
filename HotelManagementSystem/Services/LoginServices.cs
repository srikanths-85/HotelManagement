using System;
using Authenticate;
using DBConnection;
using HotelManagement.Utilities;
using LoginSpace.CustomExceptions;
using Microsoft.Data.SqlClient;

namespace Application
{
    //This class is defined to handle the admin and staff login
    public class ApplicationDbContext
    {        
        //Database connection in fetched from ApplicationDbContext
        private static SqlConnection sqlConnection = new SqlConnection(DataBaseConnection.connectionString);
       //Admin or staff login is authenticated here
        public static bool Login(string userName, string password, string role)
        {
            
            string query = "HMS_Login";//store procedure name to fetch login details

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DataBaseConnection.connectionString))
                {
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.Parameters.AddWithValue("@Username", userName);
                    command.CommandType = System.Data.CommandType.StoredProcedure;//given information, that query is from store Procedure
                    sqlConnection.Open();
                    
                    SqlDataReader reader = command.ExecuteReader();//Used to execute the query in the given Connection

                    if (reader.HasRows)
                    {
                        reader.Read();  // Read the first row
                        string storedPasswordHash = reader["Password"].ToString();
                        string storedRole = reader["Role"].ToString(); // Set the role from the database

                        // Verify the password (you should hash the input password for comparison)
                        if (VerifyPassword(password, storedPasswordHash,role,storedRole))
                        {
                            Logger.LogInfo($"{userName} Successfully Logined as {role}");
                            return true; // Successful login
                        }
                    }
                    
                    // If no matching user or invalid credentials
                    return false;
                }
            }
            catch (Exception ex) //unhandled errors are caught here
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;  // In case of any error, return false
            }            finally{
                sqlConnection.Close();
            }
                       
        }
        //This method verifies whether the password and role id matched
        private static bool VerifyPassword(string password, string storedPassword, string role, string stroredRole)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException($"'{nameof(role)}' cannot be null or empty.", nameof(role));
            }

            return password == storedPassword && role==stroredRole;
        }
        //This method Verifies wether the usename already exists are not
        public static bool UserExist(string user)
        {
            Logger.LogDebug("Check For User Exists.");
            string query = "HMS_UserExist";  // Assuming this stored procedure returns a result when user exists

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DataBaseConnection.connectionString))
                {
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    // Explicitly specifying parameter type for better clarity and safety
                    command.Parameters.AddWithValue("@Username", user);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // User exists
                            Logger.LogInfo("Username exists already: " + user);
                            return true;  // Returning true since user exists
                        }
                        else
                        {
                            // User not found
                            Logger.LogInfo("User not found: " + user);
                            return false;  // User doesn't exist
                        }
                    }
                }
            }
            catch (Exception ex)  // Handle unexpected errors are caught here
            {
                Logger.LogError("Unexpected error occurred: " + ex.Message);
                Console.WriteLine("Unexpected error: " + ex.Message);
                return false;  // Return false if an error occurs
            }
        }
        //This method has definition for registring new admin / staff to the DB
        public static void Register(string username, string password, string emailId, int age, string phoneNumber, string address, string city, string pinCode, string state, string role)
        {
            Logger.LogInfo("Invoked Register method");
            // Proceed with registration if username is valid and not taken
            string query = "HotelManagementSystemStaffInsert"; // Stored procedure name

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DataBaseConnection.connectionString))
                {
                    SqlCommand command = new SqlCommand(query, sqlConnection);

                    // Add parameters with exact names matching the stored procedure
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@EmailId", emailId);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@PinCode", pinCode);
                    command.Parameters.AddWithValue("@State", state);
                    command.Parameters.AddWithValue("@Role", role);

                    // Set the command type to StoredProcedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Open the connection and execute the query
                    sqlConnection.Open();
                    int count = command.ExecuteNonQuery();  // Execute the stored procedure

                    if (count > 0)
                    {
                        Logger.LogInfo($"User {username} successfully registered as {role}");
                        Console.WriteLine("Registration successful! You can now log in with your credentials.");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred during registration.");
                    }
                }
            }
            catch (Exception ex)  // Handle unexpected errors are caught here
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}