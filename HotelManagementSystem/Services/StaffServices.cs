using System;
using DBConnection;
using Microsoft.Data.SqlClient;
using Validation;

namespace Staff
{
    public class StaffControl
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string or any other dependencies
        public StaffControl(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Change password method - instance method (non-static)
        public void ChangePassword(string userName, string oldPassword)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open(); // Open connection before executing commands

                    // Query to get the stored password
                    string query = "SELECT password FROM hotelstaff WHERE username = @userName";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@userName", userName);

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string currentPassword = reader["password"].ToString();
                        reader.Close(); // Close the reader before continuing

                        // Check if the old password is correct
                        if (oldPassword == currentPassword)
                        {
                            string newPassword;
                            string confirmPassword;

                            do
                            {
                                // Get validated new password
                                newPassword = ValidationHandler.GetValidatedPassword("Enter the new Password: ");
                                confirmPassword = ValidationHandler.GetValidatedPassword("Enter the Confirm Password: ");

                                if (newPassword == confirmPassword)
                                {
                                    // Now update the password in the database
                                    string updateQuery = "UPDATE hotelstaff SET password = @newPassword WHERE username = @userName";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                                    updateCommand.Parameters.AddWithValue("@newPassword", newPassword);
                                    updateCommand.Parameters.AddWithValue("@userName", userName);

                                    try
                                    {
                                        int rowsAffected = updateCommand.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            Console.WriteLine("Password changed successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Password change failed.");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error updating password: " + ex.Message);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Passwords do not match. Please try again.");
                                }
                            } while (newPassword != confirmPassword); // Repeat if passwords don't match
                        }
                        else
                        {
                            Console.WriteLine("Incorrect old password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
