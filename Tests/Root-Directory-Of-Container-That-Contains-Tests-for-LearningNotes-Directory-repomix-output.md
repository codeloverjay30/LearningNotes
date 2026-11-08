This file is a merged representation of the entire codebase, combined into a single document by Repomix.

# File Summary

## Purpose
This file contains a packed representation of the entire repository's contents.
It is designed to be easily consumable by AI systems for analysis, code review,
or other automated processes.

## File Format
The content is organized as follows:
1. This summary section
2. Repository information
3. Directory structure
4. Repository files (if enabled)
5. Multiple file entries, each consisting of:
  a. A header with the file path (## File: path/to/file)
  b. The full contents of the file in a code block

## Usage Guidelines
- This file should be treated as read-only. Any changes should be made to the
  original repository files, not this packed version.
- When processing this file, use the file path to distinguish
  between different files in the repository.
- Be aware that this file may contain sensitive information. Handle it with
  the same level of security as you would the original repository.

## Notes
- Some files may have been excluded based on .gitignore rules and Repomix's configuration
- Binary files are not included in this packed representation. Please refer to the Repository Structure section for a complete list of file paths, including binary files
- Files matching patterns in .gitignore are excluded
- Files matching default ignore patterns are excluded
- Files are sorted by Git change count (files with more changes are at the bottom)

# Directory Structure
```
.repomixignore
BuildSystem.Tests.slnx
BuildSystem.Tests/.repomixignore
BuildSystem.Tests/BuildSystem.Tests.csproj
BuildSystem.Tests/Constants.cs
BuildSystem.Tests/DictionaryExtensions.cs
BuildSystem.Tests/DirectoryPackagesTests.cs
BuildSystem.Tests/ProjectCollectionExtension.cs
BuildSystem.Tests/ProjectCollectionSettings.cs
Directory.Build.props
Directory.Build.targets
Directory.Packages.props
nuget.config
```

# Files

## File: .repomixignore
```
# Add patterns to ignore here, one per line

# cached
## cached about .NET
bin/
obj/

# configuration
## configuration
*.json
*.json5

## configuration about VSC workspace
.vscode/

## configuration about VS workspace
.vs/

## configuration about VS in .NET project
.vssetting/
.vssettings

## configuration about Docker
DockerFile
**/DockerFile

.devcontainer/

# log
## logs
*.log
*.txt

# introduction
## introduction of .NET project
README.md

## others
*.md

# rule
## AI Gemini SDK assistant
GEMINI.md
ai.md

## cline
.clinerules

# backup
## backups
*.bak

# script
## powershell script
*.ps1
*.psm1

## command
*.cmd

## batch file
*.bat

## executable file
*.exe
```

## File: BuildSystem.Tests.slnx
```
<Solution>
  <Project Path="BuildSystem.Tests/BuildSystem.Tests.csproj" />
</Solution>
```

## File: BuildSystem.Tests/.repomixignore
```
# Add patterns to ignore here, one per line
# Example:
# *.log
# tmp/
```

## File: BuildSystem.Tests/BuildSystem.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Build" Version="17.12.0" />
    <PackageReference Include="Microsoft.Build.Utilities.Core" Version="17.12.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
    <PackageReference Include="xunit" Version="2.9.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="FluentAssertions" Version="8.9.0" />
    <PackageReference Include="FluentValidation" Version="12.1.1" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="System.IO.Abstractions" Version="22.1.1" />
    <PackageReference Include="System.IO.Abstractions.TestingHelpers" Version="22.1.1" />
  </ItemGroup>
</Project>
```

## File: BuildSystem.Tests/Constants.cs
```csharp
using Microsoft.Build.Evaluation;
using Xunit;
using System.IO;
using System.IO.Abstractions;
using FluentAssertions;
using System;

namespace BuildSystem.Tests;

public static class Constants
{
    public const string DIRECTORY_PACKAGES_PROPS = "Directory.Packages.props";
}
```

## File: BuildSystem.Tests/DictionaryExtensions.cs
```csharp
public static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(
        this Dictionary<TKey, TValue> dic,
        IEnumerable<KeyValuePair<TKey, TValue>> collection
    )
    {
        foreach (var item in collection)
        {
            if (!dic.ContainsKey(item.Key))
            {
                dic.Add(item.Key, item.Value);
            }
        }
    }
}
```

## File: BuildSystem.Tests/DirectoryPackagesTests.cs
```csharp
using Microsoft.Build.Evaluation;
using Xunit;
using System.IO;
using System.IO.Abstractions;
using FluentAssertions;
using System;
using System.Threading.Tasks.Dataflow;

namespace BuildSystem.Tests;

public class DirectoryPackagesTests
{
    private readonly IFileSystem _fileSystem;
    private readonly string _baseDirectory;
    private readonly string _learningNotesRootDirectory;
    // private readonly string _csharpNotesRootDirectory;

    /// ================== 
    /// 1. avoid to use `AppContext.BaseDirectory` to find its root directory of solution
    /// Though it is more flexible to find path, it returns the directory of executing assembly path 
    /// (usually under `bin/debug/net10.0` of this project) not return the solution root directory or project root directory.
    /// 
    /// 2. Similarly, avoid to use `AppDomain.CurrentDomain.BaseDirectory` to find its root directory of solution for same reason of 1.
    /// And if msbuild don't copy the project configuration file `BuildSystem.Tests.csproj` to `AppDomain.CurrentDomain.BaseDirectory`,
    /// Then it will throw an exception in `All_Projects_Should_Enable_CentralPackageManagement` test case
    /// at `string targetProject = Path.Combine(solutionDir, "BuildSystem.Tests.csproj");` statement due to find not found, 
    /// causing the test case fails. 
    /// 
    /// 3. To resolve 1. and 2. . I will hardcoded the solution root directory
    /// Although it is not maintainable in other devices or environment, 
    /// it can ensure that it has same effect in this environment.
    /// (And these test cases will be used in this environment forever)
    /// ================== ///

    /// <summary>
    /// Solution root directory of this unit test
    /// </summary>
    private readonly string _solutionRootDirectoryOfUnitTestAboutMsbuild;

    /// <summary>
    /// The parent directory that contains 
    /// <see cref="global::BuildSystem.Tests.DirectoryPackagesTests._solutionRootDirectoryOfUnitTestAboutMsbuild"/>,
    /// <see cref="global::BuildSystem.Tests.DirectoryPackagesTests._solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory"/> 
    /// </summary>
    private readonly string _learningNotesAsContainerRootDirectory;
    /// <summary>
    /// Solution root directory of CSharp
    /// </summary>
    private readonly string _solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory;

    private readonly string _directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild;

    private readonly string _csharpNotesRootDirectory;
        


    public DirectoryPackagesTests()
    {
        _fileSystem = new FileSystem();

        /// ======= obsolete (uncommented it just due to back compatibility) ======= ///
        _baseDirectory = AppContext.BaseDirectory;
        _learningNotesRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_baseDirectory, "../../../../../"));
        _csharpNotesRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_learningNotesRootDirectory, "CSharp"));

        /// ======= new ======= ///
        _solutionRootDirectoryOfUnitTestAboutMsbuild = @"D:\workspace\LearningNotes\Tests";

        _directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuild, Constants.DIRECTORY_PACKAGES_PROPS));

        _learningNotesAsContainerRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuild, ".."));
        _solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_learningNotesAsContainerRootDirectory, "CSharp"));
    }

    [Fact]
    public void WhenManagePackageVersionsCentrallyTagIsSetToFalse_ShouldHaveCentralPackageManagementDisabled()
    {
        // 1. 定義路徑：透過 _fileSystem 的 Path 抽象來處理，提升防禦性與可測試性
        string directoryPackagesProps = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory, Constants.DIRECTORY_PACKAGES_PROPS));

        // 使用 FluentAssertions 取代 xUnit 原生斷言
        _fileSystem.File.Exists(directoryPackagesProps).Should().BeTrue($"Can't find global configurationa at `{directoryPackagesProps}`");

        var projectCollectionSettings = new ProjectCollectionSettings
        {
            DirectoryPackagesPropsPath = directoryPackagesProps,
            // force to stop recursively searching when the ${DirectoryPackagesPropsPath} is found.
            ShouldStopRecursivelySearchWhenFound = true,
        };

        // 2. 載入並評估 (微軟 MSBuild 評估層需讀取實體檔案)
        // 確保這裡定義了 SDK 目錄或相關屬性
        var globalProperties = ProjectCollectionExtension.GetDefaultProjectCollectionSettings(projectCollectionSettings);

        var projectCollection = new ProjectCollection(globalProperties);
        var project = projectCollection.LoadProject(directoryPackagesProps);

        // 3. 斷言：全面改用 FluentAssertions 的鏈式語法
        string? managePackageValue = project.GetPropertyValue("ManagePackageVersionsCentrally");
        managePackageValue.Should().Be("false", "因為我們在架構上強制要求所有專案必須啟用中央套件管理 (CPM)");

        string? transitivePinningValue = project.GetPropertyValue("CentralPackageTransitivePinningEnabled");
        transitivePinningValue.Should().Be("false", "為了防止遞迴依賴（Transitive Dependencies）版本漂移，必須開啟可傳遞釘選功能");
    }

    /// <summary>
    /// Verifies that all projects correctly enable Central Package Management (CPM).
    /// </summary>
    /// <remarks>
    /// This test case is uncommented due to
    /// + it is highly couple, it uses <see cref="global::System.IO.Path.Combine(string, string)"/> 
    /// not use <see cref="global::System.IO.Abstractions.Path.Combine(string, string)"/>
    /// + it is defected and and likely will fail even if its logics are correct.
    /// see <see cref="global::BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes" />
    /// </remarks>
    // [Fact]
    // public void All_Projects_Should_Enable_CentralPackageManagement()
    // {
    //     // Arrange
    //     // (此處省略您的路徑尋找邏輯，保持與原專案一致)
    //     string solutionDir = AppDomain.CurrentDomain.BaseDirectory;
    //     string targetProject = Path.Combine(solutionDir, "BuildSystem.Tests.csproj");

    //     // Act
    //     Action act = () =>
    //     {
    //         using var projectCollection = new ProjectCollection(ProjectCollectionExtension.GetDefaultProjectCollectionSettings());
    //         var project = projectCollection.LoadProject(targetProject);

    //         // Assert inside target validation
    //         var cpmProperty = project.GetProperty("ManagePackageVersionsCentrally");
    //         cpmProperty.Should().NotBeNull("because Central Package Management must be explicitly configured");
    //         cpmProperty.EvaluatedValue.Should().BeEquivalentTo("true");
    //     };

    //     // Assert - Ensure no MSBuild evaluation exception is thrown during project loading
    //     act.Should().NotThrow<Exception>("because the project collection should be isolated from environment side-effects");
    // }

    [Fact]
    public void OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes()
    {
        // Arrange
        // (此處省略您的路徑尋找邏輯，保持與原專案一致)
        string solutionDir = _solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory;
        string targetProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(solutionDir, "Examples", "Ex1", "Ex1-1", "MeProject1"));
        string targetProjectCsProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(targetProject, "MeProject1.csproj"));

        bool directoryExists = _fileSystem.Path.Exists(targetProject);
        directoryExists.Should().BeTrue($"`{targetProject}` does not exists.");

        bool fileExist = _fileSystem.File.Exists(targetProjectCsProject);
        fileExist.Should().BeTrue($"`{targetProjectCsProject}` does not exists. It should exist since the project exists at `{targetProject}`");

        bool directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild_exists = _fileSystem.File.Exists(_directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild);

        directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild_exists.Should().BeTrue($"`{_directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild}` configuration file should exist");

        var projectCollectionSettings = new ProjectCollectionSettings
        {
            DirectoryPackagesPropsPath = _directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild,
            // force to stop recursively searching when the ${DirectoryPackagesPropsPath} is found.
            ShouldStopRecursivelySearchWhenFound = true,
        };
        // Act
        Action act = () =>
        {
            using var projectCollection = new ProjectCollection(ProjectCollectionExtension.GetDefaultProjectCollectionSettings(projectCollectionSettings));
            var project = projectCollection.LoadProject(targetProjectCsProject);

            // Assert inside target validation
            var cpmProperty = project.GetProperty("ManagePackageVersionsCentrally");
            cpmProperty.Should().NotBeNull("because Central Package Management must be explicitly configured");
            cpmProperty.EvaluatedValue.Should().BeEquivalentTo("true");
        };

        // Assert - Ensure no MSBuild evaluation exception is thrown during project loading
        act.Should().NotThrow<Exception>("because the project collection should be isolated from environment side-effects");
    }

    /// <summary>
    /// Verifies that all projects correctly enable Central Package Management (CPM).
    /// </summary>
    [Fact]
    public void ShouldFindCorrectPathOfCentralConfiguration()
    {
        // Arrange
        string solutionDir = _solutionRootDirectoryOfUnitTestAboutMsbuild;
        string targetProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(solutionDir, "Examples", "Ex1", "Ex1-1", "MeProject1"));
        string targetProjectCsProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(targetProject, "MeProject1.csproj"));

        bool directoryExists = _fileSystem.Path.Exists(targetProject);
        directoryExists.Should().BeTrue($"`{targetProject}` does not exists.");

        bool fileExist = _fileSystem.File.Exists(targetProjectCsProject);
        fileExist.Should().BeTrue($"`{targetProjectCsProject}` does not exists. It should exist since the project exists at `{targetProject}`");

        bool directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild_exists = _fileSystem.File.Exists(_directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild);

        directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild_exists.Should().BeTrue($"`{_directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild}` configuration file should exist");

        var projectCollectionSettings = new ProjectCollectionSettings
        {
            DirectoryPackagesPropsPath = _directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild,
            // force to stop recursively searching when the ${DirectoryPackagesPropsPath} is found.
            ShouldStopRecursivelySearchWhenFound = true,
        };

        // Act
        Action act = () =>
        {
            using var projectCollection = new ProjectCollection(ProjectCollectionExtension.GetDefaultProjectCollectionSettings(projectCollectionSettings));
            var project = projectCollection.LoadProject(targetProject);
            
            // Assert inside target validation
            var cpmProperty = project.GetProperty("ManagePackageVersionsCentrally");
            cpmProperty.Should().NotBeNull("because Central Package Management must be explicitly configured");
            cpmProperty.EvaluatedValue.Should().BeEquivalentTo("true");
        };

        // Assert - Ensure no MSBuild evaluation exception is thrown during project loading
        act.Should().NotThrow<Exception>("because the project collection should be isolated from environment side-effects");
    }
}
```

## File: BuildSystem.Tests/ProjectCollectionExtension.cs
```csharp
namespace BuildSystem.Tests;

public static class ProjectCollectionExtension
{
    /// <summary>
    /// Gets the default project collection settings with defensive global properties to isolate the build environment.
    /// </summary>
    /// <returns>A dictionary containing MSBuild global properties.</returns>
    public static Dictionary<string, string> GetDefaultProjectCollectionSettings(
        this ProjectCollectionSettings projectCollectionSettings
    )
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(projectCollectionSettings.DirectoryPackagesPropsPath, nameof(projectCollectionSettings.DirectoryPackagesPropsPath));
        ArgumentNullException.ThrowIfNull(projectCollectionSettings.ShouldStopRecursivelySearchWhenFound, nameof(projectCollectionSettings.ShouldStopRecursivelySearchWhenFound));

        // 建議修改方式：在測試中注入絕對隔離的設定
        var globalProperties = new Dictionary<string, string>
        {
            { "ManagePackageVersionsCentrally", "false" },
            // 防禦性配置，跳過所有可能會載入全域邏輯的 SDK 掃描
            { "SkipImportWorkloads", "true" },
            { "DisableMSBuildDiagnosticFilters", "true" }
        };

        if (projectCollectionSettings.ShouldStopRecursivelySearchWhenFound == false)
        {
            globalProperties.AddRange(
                new Dictionary<string, string>() {
                // 強制截斷繼承鏈，確保不會向上搜尋到 CSharp 目錄的組態
                { "DirectoryPackagesPropsPath", projectCollectionSettings.DirectoryPackagesPropsPath },
                { "ImportDirectoryBuildProps", "false" },
                { "ImportDirectoryBuildTargets", "false" },
            });
        }

        return globalProperties;
    }
}
```

## File: BuildSystem.Tests/ProjectCollectionSettings.cs
```csharp
namespace BuildSystem.Tests;

public record class ProjectCollectionSettings
{
    public string? DirectoryPackagesPropsPath { get; init; }
    public bool ShouldStopRecursivelySearchWhenFound { get; init; }
}
```

## File: Directory.Build.props
```
<Project>
</Project>
```

## File: Directory.Build.targets
```
<Project>
</Project>
```

## File: Directory.Packages.props
```
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally>
    <DirectoryPackagesPropsPath>$(MSBuildThisFileDirectory)Directory.Packages.props</DirectoryPackagesPropsPath>
  </PropertyGroup>
</Project>
```

## File: nuget.config
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="github" value="https://nuget.pkg.github.com/codeloverjay30/index.json" />
    <add key="MyLocalBlazorWasmSource" value="D:\workspace\MyLocalNuget\BlazorWebAssembly\" />
    <add key="MyLocalNuGet" value="D:\workspace\MyLocalNuget\ClassLibrary\" />
    <add key="MyLocalRepo" value="./local-packages" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="ANY_STRING" />
      <add key="ClearTextPassword" value="%GITHUB_TOKEN_ENV_VAR%" />
    </github>
  </packageSourceCredentials>
</configuration>
```
