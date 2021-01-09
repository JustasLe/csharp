using System;
using System.Security.Cryptography;

namespace RandomPasswordGenerator
{
    class Program
    {
        static int GetPasswordLength()
        {
            Console.WriteLine("The program will generate 10 random passwords.");
            Console.WriteLine("Please input your desired password length. It must be at least 8 and at most 32.");
            int passwordLength = -1;
            bool successfullyRead = false;
            while (!successfullyRead)
            {
                if (!Int32.TryParse(Console.ReadLine(), out passwordLength))
                {
                    Console.WriteLine("The number was not inputted correctly.");
                    Console.WriteLine("Please try again.");
                }
                else
                {
                    if (passwordLength < 8 || passwordLength > 32)
                    {
                        Console.WriteLine("Password length must be at least 8 and at most 32.");
                        Console.WriteLine("Please try again.");
                    }
                    else
                    {
                        successfullyRead = true;
                    }
                }
            }
            return passwordLength;
        }

        static string GenerateRandomPassword(int length)
        {
            string alphabet = "";
            for (char c = 'a'; c <= 'z'; c++)
            {
                alphabet += c;
                alphabet += Char.ToUpper(c);
            }
            for (char c = '0'; c <= '9'; c++)
            {
                alphabet += c;
            }
            byte[] randomBytes = new byte[length];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            string password = "";
            for (int i = 0; i < length; i++)
            {
                password += alphabet[randomBytes[i] % alphabet.Length];
            }
            rng.Dispose();
            return password;
        }

        static void Main(string[] args)
        {
            int length = GetPasswordLength();
            Console.WriteLine($"\n10 random passwords of length {length}:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(GenerateRandomPassword(length));
            }
        }
    }
}
