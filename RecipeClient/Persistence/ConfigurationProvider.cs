using System;

namespace RecipeClient.Persistence;

internal static class ConfigurationProvider
{
    private static string ApiBasePath => "https://localhost:8065/api/";

    public static Uri CreateApiPath(string path)
    {
        return new Uri($"{ApiBasePath}/{path}");
    }
}
