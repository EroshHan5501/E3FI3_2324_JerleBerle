using RecipeCommon.Testing;
using Xunit;

namespace RecipeCommon.Tests.FileSystem
{
    public class DirectoryBuilderShould
    {
        [Fact]
        public void CreateEmptyDirectoryWithUuidAsName()
        {
            DirectoryEnvironment env = new DirectoryBuilder().Build();

            Assert.True(Directory.Exists(env.BasePath));
        }

        [Fact]
        public void CreateFilesInsideBaseDirectory()
        {
            DirectoryBuilder builder = new DirectoryBuilder();

            DirectoryEnvironment env = builder
                .WithFile("general.json", null)
                .WithFile("testing.txt", null)
                .Build();

            Assert.True(Directory.Exists(env.BasePath));

            IEnumerable<string> filepaths = env.GetFilePaths();

            Assert.All(filepaths, path => Assert.True(File.Exists(path)));  
        }

        internal class JsonEntity
        {
            public string Name { get; set; }

            public int Value { get; set; }
        }

        [Fact]
        public void CreateFilesWithCorrectContent()
        {
            DirectoryBuilder builder = new DirectoryBuilder();

            DirectoryEnvironment env = builder
                .WithFile("general.json", general =>
                {
                    JsonEntity entity = new()
                    {
                        Name = "Testing",
                        Value = 300
                    };

                    general.WithJson<JsonEntity>(entity);
                })
                .WithFile("testing.txt", testing =>
                {
                    testing.WithText("Hello world");
                })
                .Build();

            IEnumerable<string> filepaths = env.GetFilePaths();

            Assert.True(Directory.Exists(env.BasePath));
            Assert.All(filepaths, path => Assert.True(File.Exists(path)));

            string expectedJson ="{\"Name\":\"Testing\",\"Value\":300}\r\n";

            string actualJson = File.ReadAllText(Path.Combine(env.BasePath, "general.json"));
            Assert.Equal(expectedJson, actualJson);

            string expectedText = "Hello world\r\n";

            string actualText = File.ReadAllText(Path.Combine(env.BasePath, "testing.txt"));

            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void CreatesMultipleFilesInBaseDirectory()
        {
            DirectoryBuilder builder = new DirectoryBuilder();

            DirectoryEnvironment env = builder.WithFiles(
                new List<(string, Action<FileContentBuilder>?)>()
                {
                    ("general.json", null),
                    ("testing.txt", null),
                    ("markdown.md", null)
                })
                .Build();

            IEnumerable<string> filepaths = env.GetFilePaths();

            int count = Directory.GetFileSystemEntries(env.BasePath, "*", SearchOption.AllDirectories).Count();
            Assert.Equal(count, filepaths.Count());

            Assert.All(filepaths, path => Assert.True(File.Exists(path)));
        }
    }
}
