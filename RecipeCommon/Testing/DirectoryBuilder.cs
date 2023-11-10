namespace RecipeCommon.Testing;

public readonly struct DirectoryEnvironment
{
    public string BasePath { get; }

    public List<string> Filenames { get; }

    public DirectoryEnvironment(string basePath, List<string> filenames)
    {
        BasePath = basePath;
        Filenames = filenames;
    }

    public IEnumerable<string> GetFilePaths()
    {
        foreach (string filename in Filenames)
        {
            yield return Path.Combine(BasePath, filename);
        }
    }
}

public readonly struct FileContentMapping : IEquatable<FileContentMapping>
{
    public string Filename { get; }

    public Action<FileContentBuilder>? ContentBuilder { get; }

    public FileContentMapping(string filename, Action<FileContentBuilder>? builder)
    {
        Filename = filename;
        ContentBuilder = builder;
    }

    public void Deconstruct(out string filename, out Action<FileContentBuilder>? builder)
    {
        filename = Filename;
        builder = ContentBuilder;
    }

    public override int GetHashCode()
    {
        return Filename.GetHashCode();
    }

    public bool Equals(FileContentMapping other)
    {
        return this.GetHashCode() == other.GetHashCode();
    }
}

public sealed class DirectoryBuilder
{
    public string Basepath { get; }

    public HashSet<FileContentMapping> Filenames { get; }

    public DirectoryBuilder() 
        : this(Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString()))
    {
    }

    public DirectoryBuilder(string basepath)
    {
        Basepath = basepath;
        Filenames = new HashSet<FileContentMapping>();
    }

    public void WithFile(string filename, Action<FileContentBuilder>? builder)
    {
        Filenames.Add(new(filename, builder));
    }

    public void WithFiles(IEnumerable<(string, Action<FileContentBuilder>?)> filenames)
    {
        foreach ((string filename, Action<FileContentBuilder>? builder) in filenames)
        {
            WithFile(filename, builder);
        }
    }

    public DirectoryEnvironment Build()
    {
        List<string> filenames = Filenames.Select(x => x.Filename).ToList();

        foreach ((string filename, Action<FileContentBuilder>? initializer) in Filenames)
        {
            string filepath = Path.Combine(Basepath, filename);
            File.Create(filepath);

            FileContentBuilder builder = new FileContentBuilder(filepath);

            initializer?.Invoke(builder);

            builder.Build();
        }

        return new(Basepath, filenames);
    }
}
