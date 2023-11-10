using RecipeCommon.Secrets;
using RecipeCommon.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

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
            "{\"nice\":200}"
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

    }
}
