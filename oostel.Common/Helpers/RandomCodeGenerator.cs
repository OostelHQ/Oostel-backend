using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Helpers
{
    public class RandomCodeGenerator
    {
        public static string GenerateNumbers()
        {
            string numbers = "0123456789";
            Random objrandom = new Random();
            string strrandom = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int temp = objrandom.Next(0, numbers.Length);
                strrandom += temp;
            }

            return strrandom[..4];
        }

        public static string GenerateAlphabetAndNumbers()
        {
            var alphabets = GenerateAlphabet();
            var sixDigitNumbers = GenerateNumbers();
            var val = alphabets[..2];
            return string.Concat(val, sixDigitNumbers);
        }

        public static string GenerateAlphabet()
        {
            string numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random objrandom = new Random();
            string passwordString = "";
            string strrandom = string.Empty;
            for (int i = 0; i < 7; i++)
            {
                int temp = objrandom.Next(0, numbers.Length);
                passwordString = numbers.ToCharArray()[temp].ToString();
                strrandom += passwordString;
            }

            return strrandom;
        }
    }
}
