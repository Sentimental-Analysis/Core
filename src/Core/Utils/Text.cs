namespace Core.Utils
{
    public class Text
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
    }
}