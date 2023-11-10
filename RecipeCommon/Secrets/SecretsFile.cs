using System.Reflection;
using System.Text.Json;

namespace RecipeCommon.Secrets
{
    public sealed class SecretsFile
    {
        public string ConnectionString { get; private set; }
            

        public static SecretsFile GetFrom(string filename, string? basedir=null)
        {
            string bdir = string.IsNullOrWhiteSpace(basedir) ? 
                Assembly.GetExecutingAssembly().Location : 
                basedir;

            string filepath = Path.Combine(bdir, filename);
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(
                    "There is no secrets file at the expected location!");
            }

            string jsonContent = File.ReadAllText(filepath);

            SecretsFile? file = JsonSerializer.Deserialize<SecretsFile>(jsonContent);

            if (file is null)
            {
                throw new JsonException("Malformed json");
            }

            return file;
        }
    }
}
