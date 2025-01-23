using System;
using DBConnection;
using HotelManagement.Utilities;
using Microsoft.Data.SqlClient;
using Validation;

namespace StoreData
{
    //This class is defined to add Guest Details for checkin,check-out,edit Guests, activce rooms
    public class StoreDetailsInDb
    {
        //Database Connection is fetched here
        private protected static SqlConnection sqlConnection = new SqlConnection(DataBaseConnection.connectionString);
        //need to check room entry
        public static void CheckIn()
        {
            Console.WriteLine("\n----------------------------------------------------");
            Console.Write("\nEnter Room Number: ");
            int roomNumber = Convert.ToInt32(Console.ReadLine());

            string query = "HMS_CheckRoomExists";//Store Procedure name to check in 
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();
                
                if (reader.HasRows)  // Check if room exists
                {
                    int room = Convert.ToInt32(reader["RoomNumber"]);
                    if(roomNumber==room)
                    {
                        Logger.LogDebug("Room Already taken  (or) not Availabel.");
                        Console.WriteLine("Room Not Available.");
                        reader.Close();
                        return;
                    }
                    
                }

                reader.Close(); // Close the reader after use
                int noOfPersons = Convert.ToInt32(ValidationHandler.GetValidatedInput("Enter number of persons: ",ValidationHandler.GetIsNumber));
                decimal rentPerDay = Convert.ToDecimal(ValidationHandler.GetValidatedInput("\nEnter Rent Per Day: ",ValidationHandler.GetIsNumber));
                string queryRooms = "HMS_Register_Room";
                SqlCommand sqlCommand2 = new SqlCommand(queryRooms, sqlConnection);
                sqlCommand2.Parameters.AddWithValue("@roomNumber", roomNumber);
                sqlCommand2.Parameters.AddWithValue("@rentPerDay", rentPerDay);
                sqlCommand2.CommandType = System.Data.CommandType.StoredProcedure;

                int count1 = sqlCommand2.ExecuteNonQuery();
                if (count1 > 0)//check wether the room is enter or not 
                {
                    Logger.LogInfo("Room Alloted Successfully");
                    Console.WriteLine("room Registered successful.");
                }

                // adding user to db in Guests table
                for (int i = 0; i < noOfPersons; i++)
                {
                    Console.WriteLine("\n----------------------------------------------------");
                    string name = ValidationHandler.GetValidatedInput("Enter Guest Name: ",ValidationHandler.ValidateGuestName);
                    int age = ValidationHandler.GetValidatedAge();
                    string emailId = ValidationHandler.GetValidatedInput("Enter your email: ", ValidationHandler.ValidateEmail);
                    string phoneNumber = ValidationHandler.GetValidatedInput("Enter your phone number: ", ValidationHandler.ValidatePhoneNumber);
                    //DateTime dateTime = new DateTime();
                    
                        string queryGuests = "HMS_InsertGuest";
                        SqlCommand sqlCommand1 = new SqlCommand(queryGuests, sqlConnection);
                        sqlCommand1.Parameters.AddWithValue("@name", name);
                        sqlCommand1.Parameters.AddWithValue("@age", age);
                        sqlCommand1.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        sqlCommand1.Parameters.AddWithValue("@roomNumber", roomNumber);
                        sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;

                        int count = sqlCommand1.ExecuteNonQuery();
                        if (count > 0)
                        {
                            Logger.LogInfo("Guest details added Successfully");
                            Console.WriteLine("Data Entered Successfully.");
                        }
                }
                
                Console.WriteLine("\n----------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    
        public static void CheckOut()
        {
            Console.WriteLine("\n----------------------------------------------------");
            
            Console.Write("\nEnter Room Number to Check Out: ");
            int roomNumber = Convert.ToInt32(ValidationHandler.GetValidatedInput("\nEnter Room Number to Check Out: ",ValidationHandler.GetIsNumber));

            string query = "HMS_CHECKOUTROOM";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows) // Check if the room exists in the database
                {
                    reader.Read();
                    int storedRoomNumber = Convert.ToInt32(reader["roomNumber"]);
                    decimal rentPerDay = Convert.ToDecimal(reader["RentPerDay"]);

                    if (roomNumber == storedRoomNumber)
                    {
                        reader.Close(); // Close the reader after reading

                        // Delete all guests before removing the room
                        string removeGuestsQuery = "HMS_DELETEROOM";
                        SqlCommand removeGuestsCommand = new SqlCommand(removeGuestsQuery, sqlConnection);
                        removeGuestsCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                        removeGuestsCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        int guestsRemoved = removeGuestsCommand.ExecuteNonQuery();

                        // Now delete the room after removing the guests
                        string removeRoomQuery = "HMS_DELETEROOMFROMROOM";
                        SqlCommand removeRoomCommand = new SqlCommand(removeRoomQuery, sqlConnection);
                        removeRoomCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                        removeRoomCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        int roomRemoved = removeRoomCommand.ExecuteNonQuery();

                        if (guestsRemoved > 0 && roomRemoved > 0)
                        {
                            Console.Write("Enter the number of days stayed: ");
                            int days = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Confirm checkout for all guests (yes/no): ");
                            string confirmation = Console.ReadLine().ToLower();

                            if (confirmation == "yes")
                            {
                                decimal totalBill = rentPerDay * days;
                                Console.WriteLine($"Total Bill for Room {roomNumber}: {totalBill:C}");
                                Console.WriteLine("Checkout successful.");
                            }
                            else
                            {
                                Console.WriteLine("Checkout canceled.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error during checkout process.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nRoom not found or already checked out.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            Console.WriteLine("\n----------------------------------------------------");
        }

        public static void EditUserDetails()
        {
            Console.WriteLine("\n----------------------------------------------------");
            int roomNumber = Convert.ToInt32(ValidationHandler.GetValidatedInput("Enter Room Number to Edit User Details: ",ValidationHandler.GetIsNumber));

            string query = "HMS_GETROOMDETAILS";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows) // Check if the room exists in the database
                {
                    reader.Read();
                    int storedRoomNumber = Convert.ToInt32(reader["roomNumber"]);
                    if (roomNumber == storedRoomNumber)
                    {
                        reader.Close(); // Close the reader as we don't need it anymore

                        // Now, let's allow the user to edit guest details.
                        string guestName = ValidationHandler.GetValidatedInput("Enter the guest name to edit: ",ValidationHandler.ValidateGuestName);

                        // Search for the guest in the Guests table
                        string searchUserQuery = "HMS_GETGUESTDETAILS";
                        
                        sqlCommand = new SqlCommand(searchUserQuery, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@guestName", guestName);
                        sqlCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader reader2 = sqlCommand.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            reader2.Read(); // Read the result
                            reader2.Close(); // Close the reader

                            // Prompt the user for new details
                            string newName = ValidationHandler.GetValidatedInput("Enter new name: ",ValidationHandler.ValidateGuestName);
                            int newAge = ValidationHandler.GetValidatedAge();
                            string newPhone = ValidationHandler.GetValidatedInput("Enter new phone number: ",ValidationHandler.GetPhoneNumber);

                            // Update guest details
                            string editUserQuery = "HMS_UPDATEGUESTDETAILS";
                            sqlCommand = new SqlCommand(editUserQuery, sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@newName", newName);
                            sqlCommand.Parameters.AddWithValue("@newAge", newAge);
                            sqlCommand.Parameters.AddWithValue("@newPhone", newPhone);
                            sqlCommand.Parameters.AddWithValue("@guestName", guestName);
                            sqlCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                            int updatedCount = sqlCommand.ExecuteNonQuery();
                            if (updatedCount > 0)
                            {
                                Logger.LogInfo("Guest details updated successfully.");
                                Console.WriteLine("Guest details updated successfully.");
                                Console.WriteLine("\n----------------------------------------------------");
                            }
                            else
                            {
                                Logger.LogError("Error updating guest details.");
                                Console.WriteLine("Error updating guest details.");
                            }
                        }
                        else
                        {
                            Logger.LogError("Guest not found.");
                            Console.WriteLine("Guest not found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Room not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

       public static void ViewActiveRooms()
        {
            int i = 1;
            Console.WriteLine("\n----------------------------------------------------");

            // Query to select rooms that have guests (i.e., active rooms)
            string selectQuery = "HMS_GETACTIVEROOMS";// Ensure that the room has an assigned guest

            SqlCommand sqlCommand = new SqlCommand(selectQuery, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{i}. Room Number: {reader.GetInt32(0)}");
                        i++; // Increment index
                    }
                    reader.Close();
                }
                else
                {
                    Console.WriteLine("All Rooms are Free.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close(); // Ensure the connection is closed
            }

            Console.WriteLine("\n----------------------------------------------------");
        }
    
    
    }
}
