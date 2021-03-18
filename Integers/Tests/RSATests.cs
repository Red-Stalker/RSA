using System.Linq;
using NUnit.Framework;

namespace Integers
{
    public class RSATests
    {
        [Test]
        [TestCase("hello", "17", "23")]
        [TestCase("hello", "101", "103")]
        [TestCase("hello", "3571", "3331")]
        [TestCase("hello", "5037569", "5810011")]
        public void CorrectDecode(string value, string number1, string number2)
        {
            var p = new Integer(number1);
            var q = new Integer(number2);
            var module = p * q;
            var phi = (p - Integer.One) * (q - Integer.One);
            var exponent = RSA.CalculatePublicExponent(phi);
            var secretExponent = RSA.CalculateSecretExponent(exponent, phi);
            var chars = value.ToCharArray().Select(x => (int) (byte) x).ToArray();
            var encoded = RSA.Encode(chars,
                                     exponent, module);
            var result = RSA.Decode(encoded, secretExponent, module);
            CollectionAssert.AreEqual(result, chars);
        }
    }
}