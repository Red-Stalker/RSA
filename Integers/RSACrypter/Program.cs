using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Введите команду(encode - зашифровать , decode - расшифровать)");
            var line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                switch (line.Trim())
                {
                    case "encode":
                        Encode();
                        break;
                    case "decode":
                        Decode();
                        break;
                    default:
                        Console.WriteLine("Неправильные команды. Введите encode, чтобы зашифровать, decode, чтобы расшифровать");
                        break;
                }

                Console.WriteLine($"Введите команду(encode - зашифровать , decode - расшифровать)");
                line = Console.ReadLine();
            }
        }

        private static void Encode()
        {
            Console.Write("Первое прайм число: ");
            var p = new Integer(Console.ReadLine());
            Console.Write("Второе прайм число: ");
            var q = new Integer(Console.ReadLine());
            var module = p * q;
            Console.WriteLine($"Ваш модуль {module}");
            var phi = (p - Integer.One) * (q - Integer.One);
            var publicExponent = RSA.CalculatePublicExponent(phi);
            var secretExponent = RSA.CalculateSecretExponent(publicExponent, phi);
            Console.WriteLine("Введите сообщение, которое хотите зашифровать");
            var message = Encoding.ASCII
                                  .GetBytes(Console.ReadLine() ?? string.Empty)
                                  .Select(x => (int) x)
                                  .ToArray();
            var encode = RSA.Encode(message, publicExponent, module);
            Console.WriteLine("Ваше зашифрованное сообщение");
            foreach (var integer in encode)
                Console.WriteLine(integer);
            Console.WriteLine($"Ваш секретный ключ {secretExponent}");
        }

        private static void Decode()
        {
            Console.WriteLine("Введите ваш секретный ключ");
            var secretExponent = new Integer(Console.ReadLine());
            Console.WriteLine("Введите модуль");
            var module = new Integer(Console.ReadLine());
            Console.WriteLine("Введите зашифрованное сообщение");
            var crypted = new List<Integer>();
            var line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                crypted.Add(new Integer(line));
                line = Console.ReadLine();
            }

            var chars = RSA.Decode(crypted, secretExponent, module);
            var result = Encoding.ASCII.GetString(chars.Select(x => (byte) x).ToArray());
            Console.WriteLine($"Ваше сообщение {result}");
        }
    }
}