using System;

namespace Toolbox.MethodExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalizes the first letter of the string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string CapitalizeFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            char firstChar = input[0];

            if (char.IsUpper(firstChar)) return input;

            var chars = input.ToCharArray();
            chars[0] = char.ToUpper(firstChar);
            return new string(chars);
        }
    }
}