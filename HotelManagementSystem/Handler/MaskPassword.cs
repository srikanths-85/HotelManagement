using System;

namespace Form.MaskPassword
{
    //this class used to get password from console without showing to others
    public class MaskPassword
    {
        // * is printed for each character instead of password
        public static string getMaskedPassword()
        {
            string password = string.Empty;
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true); // will return ConsoleKeyInfo object
                if(pressedKey.Key == ConsoleKey.Backspace){
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (pressedKey.Key == ConsoleKey.Enter) 
                    break;
                else{
                    password += pressedKey.KeyChar;
                    Console.Write("*"); }
            }
            Console.WriteLine();
            return password;
        }
    
    }
}