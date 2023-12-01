using System.Security.Cryptography;
using System.Text;

namespace RecipeApi.Helper;

public static class HashHelper
{
    public static string GenerateSHA512Hash(string input)
    {
        byte[] bInput = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = SHA512.HashData(bInput);
        return Convert.ToBase64String(hashBytes).Trim();
    }
}