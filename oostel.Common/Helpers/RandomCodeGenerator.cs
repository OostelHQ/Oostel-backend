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

            return strrandom[..6];
        }
    }
}
