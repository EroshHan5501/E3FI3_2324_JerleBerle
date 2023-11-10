using System.Text.Json;

namespace RecipeCommon.Testing;

public class FileContentBuilder
{
    public string Filepath { get; set; }

    public List<string> Content { get; private set; }

    public FileContentBuilder(string filepath)
    {
        Filepath = filepath;
        Content = new List<string>();
    }
    
    public FileContentBuilder WithText(string text)
    {
        Content.Add(text);

        return this;
    }

    public FileContentBuilder WithJson<TEntity>(TEntity entity) 
        where TEntity : class
    {
        string json = JsonSerializer.Serialize<TEntity>(entity);
        Content.Add(json);

        return this;
    }

    public void Build()
    {
        File.WriteAllLines(Filepath, Content);
    }
}
