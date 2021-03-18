using System.Collections.Generic;

namespace Integers
{
    public class RSA
    {
        public static List<Integer> Encode(IEnumerable<int> bytes, Integer e, Integer module)
        {
            var result = new List<Integer>();
            foreach (var b in bytes)
            {
                var integer = Integer.ModPow(new Integer(b.ToString()), e, module);
                result.Add(integer);
            }

            return result;
        }


        public static int[] Decode(List<Integer> integers, Integer d, Integer module)
        {
            var result = new List<int>();
            foreach (var integer in integers)
            {
                var code = Integer.ModPow(integer, d, module);
                result.Add(int.Parse(code.ToString()));
            }

            return result.ToArray();
        }

        public static Integer CalculatePublicExponent(Integer module)
        {
            var exponent = new Integer(3);
            var one = Integer.One;

            for (var i = new Integer(0); i < module; i++)
            {
                if (Integer.GreatestCommonDivisor(exponent, module, out var _, out var _) == one)
                    return exponent;
                exponent += one;
            }

            return exponent;
        }

        public static Integer CalculateSecretExponent(Integer exponent, Integer phi)
            => Integer.GetModInverse(exponent, phi);

        public static bool IsSimpleNumber(Integer number)
        {
            var two = new Integer(2);
            if (number < two)
                return false;

            if (number == two)
                return true;

            for (var i = two; i < number; i++)
                if ((number % i).IsZero)
                    return false;

            return true;
        }
    }
}