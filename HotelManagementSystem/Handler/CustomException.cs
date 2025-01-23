// CustomExceptions.cs
using System;

namespace LoginSpace.CustomExceptions
{
    // Custom exception for invalid username (if username starts with a number)
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException(string message) : base(message) { }
    }

    // Custom exception for invalid password (if password is too short)
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message){}
    }

    // Custom exception for username already taken
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException(string message) : base(message) { }
    }

    // Custom exception for invalid login credentials (username or password)
    public class InvalidLoginCredentialsException : Exception
    {
        public InvalidLoginCredentialsException(string message) : base(message) { }
    }
    // Custom exception for invalid age credentials
    public class InvalidAgeException : Exception
    {
        public InvalidAgeException(string message) : base(message) { }
    }
        // Custom exception for invalid login credentials (staff or admin)
    public class InvalidRoleException : Exception
    {
        public InvalidRoleException(string message) : base(message) { }
    }
}
