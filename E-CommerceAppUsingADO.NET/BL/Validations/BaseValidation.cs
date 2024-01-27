using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Validations
{
    class BaseValidation
    {
        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
        public static bool isRequired(string input)
        {
            input = RemoveWhitespace(input);
            return !(string.IsNullOrEmpty(input));
        }
        public static void DisplayTextWithRedColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void DisplayTextWithGreenColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
