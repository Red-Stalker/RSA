using System;
using System.IO;
using System.Linq;
using Integers;


namespace RSAFileCrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your command");
            var line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                switch (line.Trim())
                {
                    case "encode":
                        Encode();
                        break;
                    default:
                        Console.WriteLine("Enter your command");
                        line = Console.ReadLine();
                        break;
                }
            }
        }
        
        private static void Encode()
        {
            Console.Write("Enter first prime number: ");
            var p = new Integer(Console.ReadLine());
            Console.Write("Enter second prime number: ");
            var q = new Integer(Console.ReadLine());
            var module = p * q;
            Console.WriteLine($"Your module is {module}");
            var phi = (p - Integer.One) * (q - Integer.One);
            var publicExponent = RSA.CalculatePublicExponent(phi);
            var secretExponent = RSA.CalculateSecretExponent(publicExponent, phi);   
            Console.WriteLine($"Your module {module}");
            Console.WriteLine($"Your secret exponent {secretExponent}");
            var message =
                File.ReadAllBytes(@"..\ToEncode.txt")
                    .Select(x => (int)x);
            var encoded = RSA.Encode(message, publicExponent, module);
            foreach (var integer in encoded)
                Console.WriteLine(integer.ToString());
            
        }
    }
}