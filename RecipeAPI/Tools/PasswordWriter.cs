using System.Text;

namespace RecipeAPI.Tools
{
    public class PasswordWriter
    {
        public static string BuildPassword()
        {
            StringBuilder sb = new();
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else
                {
                    sb.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }
            }
            return sb.ToString();
        }
    }
}
