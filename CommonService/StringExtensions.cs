using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public class StringExtensions
    {
        public static string ConvertToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = new StringBuilder();
            bool isPreviousCharSeparator = false;

            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    if (!isPreviousCharSeparator && result.Length > 0)
                        result.Append('_');

                    result.Append(char.ToLower(c));
                    isPreviousCharSeparator = false;
                }
                else if (char.IsLetterOrDigit(c))
                {
                    result.Append(c);
                    isPreviousCharSeparator = false;
                }
                else
                {
                    result.Append('_');
                    isPreviousCharSeparator = true;
                }
            }

            return result.ToString();
        }
    }
}
