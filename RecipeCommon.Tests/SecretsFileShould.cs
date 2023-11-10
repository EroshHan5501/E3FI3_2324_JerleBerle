using RecipeCommon.Secrets;
using RecipeCommon.Testing;

using System.Text.Json;

using Xunit;

namespace RecipeCommon.Tests;

public class SecretsFileShould
{
    [Fact]
    public void ThrowExceptionIfFileDoesNotExists()
    {
        DirectoryBuilder builder = new();

        DirectoryEnvironment env = builder.Build();

        Assert.Throws<FileNotFoundException>(() => SecretsFile.GetFrom("secrets.json", env.BasePath));       
    }

    public static IEnumerable<object[]> GetInvalidJsonTestingData()
    {
        yield return new object[]
        {
            string.Empty
        };

        yield return new object[]
        {
            "{"
        };

        yield return new object[]
        {
            "{\"nice\":200"
        };

        yield return new object[]
        {
            "{\"connectionString\":\"hello\""
        };
    }

    [Theory]
    [MemberData(nameof(GetInvalidJsonTestingData))]
    public void ThrowsExceptionForInvalid(string json)
    {
        DirectoryBuilder builder = new DirectoryBuilder();

        DirectoryEnvironment env = builder
            .WithFile("secrets.json", builder =>
            {
                builder.WithText(json);
            })
            .Build();

        Assert.Throws<JsonException>(() => 
            SecretsFile.GetFrom("secrets.json", env.BasePath));
    }

    [Fact]
    public void ReturnsCorrectConnectionString()
    {
        DirectoryBuilder builder = new DirectoryBuilder();

        DirectoryEnvironment env = builder
            .WithFile("secrets.json", builder =>
            {
                builder.WithText("{\"connectionString\":\"Hello world\"}");
            })
            .Build();

        SecretsFile file = SecretsFile.GetFrom("secrets.json", env.BasePath);

        Assert.Equal("Hello world", file.ConnectionString);
    }
}
