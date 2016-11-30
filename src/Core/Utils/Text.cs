using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Utils
{
    public static class Text
    {
        public static bool IsNegate(string text)
        {
            switch (text.ToLower())
            {
                case "not":
                case "don't":
                case "dont":
                case "no":
                case "nope":
                    return true;
                default:
                    return false;
            }
        }

        public static IEnumerable<string> Tokenize(string word)
        {
            return word.Split(' ').Select(x => Regex.Replace(x, @"\W", "").ToLower());
        }
    }
}