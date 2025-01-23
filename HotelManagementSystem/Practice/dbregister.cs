//  public static void Register(string userName, string password, string role, int age)
//         {
//             bool userExists = true; // Start with the assumption that the user exists
//             while (userExists)
//             {
//                 try
//                 {
//                     if (UserExist(userName))
//                     {
//                         Logger.LogDebug("Username Already in use.");
//                         Console.WriteLine("Username is already taken. Please choose another username.");
//                         AuthenticateHandler.Register();
//                     }

//                     // If the user doesn't exist, break out of the loop and proceed with registration
//                     userExists = false;

//                 }
//                 catch (Exception ex)
//                 {
//                     // Handle other exceptions (e.g., database issues)
//                     Console.WriteLine("Error: " + ex.Message);
//                     return; // Exit the method if there's an exception
//                 }
//             }

//             // Proceed with inserting the new user into the database
//             string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
//             SqlCommand command = new SqlCommand(query, sqlConnection);
//             command.Parameters.AddWithValue("@Username", userName);
//             command.Parameters.AddWithValue("@Password", password);
//             command.Parameters.AddWithValue("@Role", role);

//             try
//             {
//                 sqlConnection.Open();
//                 int count = command.ExecuteNonQuery(); 

//                 if (count > 0)
//                 {
//                     Logger.LogInfo($" {userName} Successfully Registered as {role}");
//                     Console.WriteLine("Registration successful! You can now log in with your credentials.");
//                 }
//                 else
//                 {
//                     Console.WriteLine("An error occurred during registration.");
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Error: " + ex.Message);
//             }
//             finally
//             {
//                 sqlConnection.Close();
//             }
//         }

        // public static void Register()
        // {
        //     Logger.LogInfo("Register Method Invoked");
        //     try
        //     {
        //         Console.WriteLine("\n-------------------- Register --------------------");

        //         Console.Write("Enter a new username: ");
        //         string username = Console.ReadLine();

        //         // Throw exception if the username starts with a number
        //         if (string.IsNullOrEmpty(username) || Char.IsDigit(username[0]))
        //         {
        //             Logger.LogDebug("Wrong User name Entered.");
        //             throw new InvalidUsernameException("Username should not start with a number.");
        //         }

        //         Console.Write("Enter a new password: ");
        //         string password = MaskPassword.getMaskedPassword();

        //         // Throw exception if the password is less than 8 characters
        //         if (password.Length < 8)
        //         {
        //             Logger.LogDebug("Password didn't match the requirements.");
        //             throw new InvalidPasswordException("Password should be at least 8 characters long.");
        //         }
        //        //string emailId = ValidationHandler.GetEmail();
        //        // int age = ValidationHandler.GetAge();
        //        // string phoneNumber = ValidationHandler.GetPhoneNumber();
        //         Console.Write("Enter the address: ");
        //         string address = Console.ReadLine();
        //         Console.Write("Enter the City: ");
        //         string city = Console.ReadLine();
        //         Console.Write("Enter Pin Code: ");
        //         string pinCode = Console.ReadLine();
        //         Console.Write("Enter your State: ");
        //         string state = Console.ReadLine();
        //         Console.Write("Enter role (admin/staff): ");
        //         string role = Console.ReadLine().ToLower();

        //         if (role != "admin" && role != "staff")
        //         {
        //             Logger.LogDebug("Invalid Role Entered.");
        //             throw new InvalidRoleException("Invalid role. Please enter 'admin' or 'staff'.");
        //         }

        //         //passing data to register in Database
        //         //ApplicationDbContext.Register(username,password,role);
        //       // ApplicationDbContext.Register(username,password,emailId,age,phoneNumber,address,city,pinCode,state,role);
        //         //Console.WriteLine("Registration successful! You can now log in with your credentials.");
        //         Console.WriteLine("\n----------------------------------------------------");
        //     }
        //     catch (UsernameTakenException ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        //     catch (InvalidUsernameException ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        //     catch (InvalidPasswordException ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        //     catch (InvalidRoleException ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        //     catch (Exception ex)
        //     {
        //         Logger.LogError("Unexpected Error occured during register" + ex.Message);
        //         Console.WriteLine("An unexpected error occurred: " + ex.Message);
        //     }
        // }


//mayandi kudumpathara
//punnaigai Desam

