using System;
using DBConnection;
using Microsoft.Data.SqlClient;
using StoreData;

namespace AdminServices
{
    //Additional controls for Admin are given here
    public class AdminControl : StoreDetailsInDb
    {   
        //This method helps to view all staff in the Hotel
        public void ViewActiveStaffDetails()
        {

            string query = "HMS_ViewActiveStaff";//Store Procedure name to fetch the staff details
            try
            {
                //Database connection is fetched here
                    SqlConnection sqlConnection = new SqlConnection(DataBaseConnection.connectionString);
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;//given information, that query is from store Procedure
                        sqlConnection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.HasRows) //Check if data is exists or not
                            {
                                int i = 1;
                                Console.WriteLine("---------------------------------------------");
                                while (reader.Read()) //Iterate over all the fetched data to print it
                                {
                                    Console.WriteLine($"{i}. {reader["username"]} - {reader["EmailId"]}- {reader["Age"]} - {reader["PhoneNumber"]} - {reader["City"]} - {reader["State"]} -{reader["role"]}");
                                    i++;
                                    Thread.Sleep(1000);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No active staff found.");
                            }
                        }
                    }
            }
            catch (Exception ex) //unhandled errors are caught here
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally{
                sqlConnection.Close();
            }
        }    
    }
}

