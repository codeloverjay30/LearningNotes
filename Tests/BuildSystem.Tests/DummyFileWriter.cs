using System.IO.Abstractions;

namespace BuildSystem.Tests;

public class DummyFileWriter
{
    private readonly IFileSystem _fileSystem;
    public DummyFileWriter(
        IFileSystem fileSystem
    )
    {
        ArgumentNullException.ThrowIfNull(fileSystem, nameof(fileSystem));
        _fileSystem = fileSystem;
    }

    public void CreateDummyProject(
        string dummyFilePath
    )
    {
        // if (_fileSystem.File.Exists(dummyFilePath))
        // {
        //     throw new Exception($"The file `{dummyFilePath}` exists. Can't create a dummy file.");
        // }
        _fileSystem.File.WriteAllText(dummyFilePath,DummyFileTemplates.CSPROJ_FORMAT_TEMPLATE);
    }
}
