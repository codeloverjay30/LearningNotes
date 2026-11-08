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
auto-unit-tests.ps1
BuildSystem.Tests.csproj
Constants.cs
DictionaryExtensions.cs
DirectoryPackagesTests.cs
dotnet-test-powershell-interactions-2026061339.txt
dotnet-test-powershell-interactions-202606291558.txt
obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
obj/Debug/net10.0/BuildSystem.Tests.AssemblyInfo.cs
obj/Debug/net10.0/BuildSystem.Tests.AssemblyInfoInputs.cache
obj/Debug/net10.0/BuildSystem.Tests.GeneratedMSBuildEditorConfig.editorconfig
obj/Debug/net10.0/BuildSystem.Tests.GlobalUsings.g.cs
ProjectCollectionExtension.cs
ProjectCollectionSettings.cs
repomix.config.json
修改以下檔案後說明.txt
修改以下檔案後說明(2).txt
組態檔的搬移或建立.txt
```

# Files

## File: .repomixignore
```
# Add patterns to ignore here, one per line
# Example:
# *.log
# tmp/
```

## File: auto-unit-tests.ps1
```powershell
function TryToDeleteEntry{
    param(
        [string] $entryPath
    )

    if(Test-Path "$entryPath")
    {
        Remove-Item "$entryPath"
        # 修正點：移除 ${entryPath} 前面的反引號
        Write-Host -ForegroundColor Green "Delete ${entryPath} successfully";
    }
    else
    {
        # 修正點：移除 ${entryPath} 前面的反引號
        Write-Host -ForegroundColor Blue "Does not delete ${entryPath} since it does not exist.";
    }
}

function TryToChangeCurrentWorkingDirectory{
    param(
        [string] $directoryPath
    )

    if(Test-Path "$directoryPath")
    {
        cd "$directoryPath"
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Green "Change current working directory to ${directoryPath} successfully";
    }
    else
    {
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Blue "Does not change current working directory to ${directoryPath} since it does not exist.";
    }
}

function TryToPerformUnitTest{
    param(
        [string] $directoryPath
    )

    if(Test-Path "$directoryPath")
    {
        cd "$directoryPath"
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Green "Change current working directory to ${directoryPath} successfully";

        dotnet nuget locals all --clear
        
        if(-not $?){
            Write-Host -ForegroundColor Red "Failed clear all local cached packages.";
            Write-Host -ForegroundColor Red "And thus it will stop and return.";            
            return;
        }
        Write-Host -ForegroundColor Green "Successfully clear all local cached packages";

        dotnet restore -fl -flp:"logFile=BuildSystem_Tests-msbuild-restore.txt;Append=false"
        if(-not $?){
            # 修正點：移除 ${directoryPath} 前面的反引號
            Write-Host -ForegroundColor Red "Failed to restore ${directoryPath} ";
            Write-Host -ForegroundColor Red "And thus it will stop and return.";            
            return;
        }
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Green "Successfully restore ${directoryPath} ";

        dotnet build -fl -flp:"logFile=BuildSystem_Tests-msbuild-build.txt;Append=false"
        if(-not $?){
            # 修正點：移除 ${directoryPath} 前面的反引號
            Write-Host -ForegroundColor Red "Failed to build ${directoryPath} ";
            Write-Host -ForegroundColor Red "And thus it will stop and return.";            
            return;
        }
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Green "Successfully build ${directoryPath} ";

        dotnet test -fl -flp:"logFile=BuildSystem_Tests-msbuild-test.txt;Append=false"
        if(-not $?){
            # 修正點：移除 ${directoryPath} 前面的反引號
            Write-Host -ForegroundColor Red "Failed to pass one or more test cases at ${directoryPath} ";
            Write-Host -ForegroundColor Red "And thus it will stop and return.";            
            return;
        }
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Green "Successfully passes all test cases at ${directoryPath} ";    
    }
    else
    {
        # 修正點：移除 ${directoryPath} 前面的反引號
        Write-Host -ForegroundColor Blue "Does not change current working directory to ${directoryPath} since it does not exist.";
        Write-Host -ForegroundColor Blue "And thus does not perform unit tests.";
    }
}


$test_solution = "D:\workspace\LearningNotes\Tests"
$test_project = "D:\workspace\LearningNotes\Tests\BuildSystem.Tests"

TryToChangeCurrentWorkingDirectory "$test_solution"

Get-ChildItem bin,obj,artifacts,.artifacts -Recurse | Remove-Item -Recurse

$repomix_output_md = "Root-Directory-Of-Container-That-Contains-Tests-for-LearningNotes-Directory-repomix-output.md";
TryToDeleteEntry "${repomix_output_md}"

repomix 

TryToChangeCurrentWorkingDirectory "$test_project"

$repomix_output_md = "BuildSystem.Tests-test-project-repomix-output.md";
TryToDeleteEntry "${repomix_output_md}"

$restore_log = "D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-restore.txt";
TryToDeleteEntry "${restore_log}"

$build_log = "D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-build.txt";
TryToDeleteEntry "${build_log}"

$test_log = "D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-test.txt";
TryToDeleteEntry "${test_log}"

repomix

TryToPerformUnitTest "$test_project"
```

## File: BuildSystem.Tests.csproj
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

## File: Constants.cs
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

## File: DictionaryExtensions.cs
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

## File: DirectoryPackagesTests.cs
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

## File: dotnet-test-powershell-interactions-2026061339.txt
```
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> dotnet nuget locals all --clear
Clearing NuGet HTTP cache: D:\nuget\Local\http-cache
Clearing NuGet global packages folder: D:\DevTools\nuget_cache
Clearing NuGet Temp cache: C:\Users\userJay30\AppData\Local\Temp\NuGetScratch
Clearing NuGet plugins cache: C:\Users\userJay30\AppData\Local\NuGet\plugins-cache
Local resources cleared.
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> Get-ChildItem bin,obj,artifacts,.artifacts -Recurse | Remove-Item -Recurse
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> Remove-Item BuildSystem_Tests-msbuild-restore.txt
Remove-Item: Cannot find path 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-restore.txt' because it does not exist.
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> Remove-Item BuildSystem_Tests-msbuild-build.txt
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> Remove-Item BuildSystem_Tests-msbuild-test.txt
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> dotnet build -fl -flp:"logFile=BuildSystem_Tests-msbuild-build.txt;Append=false"
Restore succeeded with 4 warning(s) in 10.6s
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
  BuildSystem.Tests net10.0 succeeded with 4 warning(s) (1.6s) → bin\Debug\net10.0\BuildSystem.Tests.dll
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq

Build succeeded with 8 warning(s) in 13.1s
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> dotnet test -fl -flp:"logFile=BuildSystem_Tests-msbuild-test.txt;Append=false"
Restore succeeded with 4 warning(s) in 0.5s
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
  BuildSystem.Tests net10.0 succeeded with 4 warning(s) (0.3s) → bin\Debug\net10.0\BuildSystem.Tests.dll
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.8.2+699d445a1a (64-bit .NET 10.0.3)
[xUnit.net 00:00:00.11]   Discovering: BuildSystem.Tests
[xUnit.net 00:00:00.16]   Discovered:  BuildSystem.Tests
[xUnit.net 00:00:00.16]   Starting:    BuildSystem.Tests
     Warning:
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     A paid commercial license supports the development and continued increasing support of
     Fluent Assertions users under both commercial and community licenses. Help us
     keep Fluent Assertions at the forefront of unit testing.
     For more information, visit https://xceed.com/products/unit-testing/fluent-assertions/
[xUnit.net 00:00:00.58]     BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes [FAIL]
[xUnit.net 00:00:00.58]       Did not expect System.Exception because the project collection should be isolated from environment side-effects, but found Microsoft.Build.Exceptions.InvalidProjectFileException: The SDK 'Microsoft.NET.SDK.WorkloadAutoImportPropsLocator' specified could not be found.  D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportWorkloads.props
[xUnit.net 00:00:00.58]          at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject(String errorSubCategoryResourceName, IElementLocation elementLocation, String resourceName, Object[] args)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject[T1](IElementLocation elementLocation, String resourceName, T1 arg0)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImportsFromUnescapedImportExpressionConditioned(String directoryOfImportingFile, ProjectImportElement importElement, List`1& projects, SdkResult& sdkResult)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImports(String directoryOfImportingFile, ProjectImportElement importElement, SdkResult& sdkResult)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.Evaluate()
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Evaluator`4.Evaluate(IEvaluatorData`4 data, Project project, ProjectRootElement root, ProjectLoadSettings loadSettings, Int32 maxNodeCount, PropertyDictionary`1 environmentProperties, ILoggingService loggingService, IItemFactory`2 itemFactory, IToolsetProvider toolsetProvider, IDirectoryCacheFactory directoryCacheFactory, ProjectRootElementCacheBase projectRootElementCache, BuildEventContext buildEventContext, ISdkResolverService sdkResolverService, Int32 submissionId, EvaluationContext evaluationContext, Boolean interactive)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project.ProjectImpl.Reevaluate(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, EvaluationContext evaluationContext)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(EvaluationContext evaluationContext)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project.ProjectImpl.Initialize(IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext, Boolean interactive)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext, IDirectoryCacheFactory directoryCacheFactory, Boolean interactive)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, IDictionary`2 globalProperties, String toolsVersion)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, String toolsVersion)
[xUnit.net 00:00:00.58]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName)
[xUnit.net 00:00:00.58]          at BuildSystem.Tests.DirectoryPackagesTests.<>c__DisplayClass9_0.<OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes>b__0() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 145
[xUnit.net 00:00:00.58]          at FluentAssertions.Specialized.ActionAssertions.InvokeSubject()
[xUnit.net 00:00:00.58]          at FluentAssertions.Specialized.DelegateAssertions`2.InvokeSubjectWithInterception().
[xUnit.net 00:00:00.58]       Stack Trace:
[xUnit.net 00:00:00.58]            at FluentAssertions.Specialized.DelegateAssertionsBase`2.NotThrowInternal[TException](Exception exception, String because, Object[] becauseArgs)
[xUnit.net 00:00:00.58]            at FluentAssertions.Specialized.DelegateAssertions`2.NotThrow[TException](String because, Object[] becauseArgs)
[xUnit.net 00:00:00.58]         D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(154,0): at BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes()
[xUnit.net 00:00:00.58]            at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
[xUnit.net 00:00:00.58]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:00.59]     BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration [FAIL]
[xUnit.net 00:00:00.59]       Did not expect System.Exception because the project collection should be isolated from environment side-effects, but found Microsoft.Build.Exceptions.InvalidProjectFileException: The project file could not be loaded. Could not find file 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj'.  D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj
[xUnit.net 00:00:00.59]        ---> System.IO.FileNotFoundException: Could not find file 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj'.
[xUnit.net 00:00:00.59]       File name: 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj'
[xUnit.net 00:00:00.59]          at Microsoft.Win32.SafeHandles.SafeFileHandle.CreateFile(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options)
[xUnit.net 00:00:00.59]          at Microsoft.Win32.SafeHandles.SafeFileHandle.Open(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
[xUnit.net 00:00:00.59]          at System.IO.Strategies.OSFileStreamStrategy..ctor(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
[xUnit.net 00:00:00.59]          at System.IO.Strategies.FileStreamHelpers.ChooseStrategyCore(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
[xUnit.net 00:00:00.59]          at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Internal.XmlReaderExtension..ctor(String file, Boolean loadAsReadOnly)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Internal.XmlReaderExtension.Create(String filePath, Boolean loadAsReadOnly)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Construction.ProjectRootElement.LoadDocument(String fullPath, Boolean preserveFormatting, Boolean loadAsReadOnly)
[xUnit.net 00:00:00.59]          --- End of inner exception stack trace ---
[xUnit.net 00:00:00.59]          at Microsoft.Build.Shared.ProjectFileErrorUtilities.VerifyThrowInvalidProjectFile(Boolean condition, String errorSubCategoryResourceName, BuildEventFileInfo projectFile, Exception innerException, String resourceName, Object[] args)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Shared.ProjectFileErrorUtilities.ThrowInvalidProjectFile(BuildEventFileInfo projectFile, Exception innerException, String resourceName, Object[] args)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Construction.ProjectRootElement.LoadDocument(String fullPath, Boolean preserveFormatting, Boolean loadAsReadOnly)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Construction.ProjectRootElement..ctor(String path, ProjectRootElementCacheBase projectRootElementCache, Boolean preserveFormatting)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Construction.ProjectRootElement.CreateProjectFromPath(String projectFile, ProjectRootElementCacheBase projectRootElementCache, Boolean preserveFormatting)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Construction.ProjectRootElement.<>c.<OpenProjectOrSolution>b__209_0(String path, ProjectRootElementCacheBase cache)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Evaluation.ProjectRootElementCache.GetOrLoad(String projectFile, OpenProjectRootElement loadProjectRootElement, Boolean isExplicitlyLoaded, Nullable`1 preserveFormatting)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Evaluation.ProjectRootElementCache.Get(String projectFile, OpenProjectRootElement loadProjectRootElement, Boolean isExplicitlyLoaded, Nullable`1 preserveFormatting)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Construction.ProjectRootElement.OpenProjectOrSolution(String fullPath, IDictionary`2 globalProperties, String toolsVersion, ProjectRootElementCacheBase projectRootElementCache, Boolean isExplicitlyLoaded)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, IDictionary`2 globalProperties, String toolsVersion)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, String toolsVersion)
[xUnit.net 00:00:00.59]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName)
[xUnit.net 00:00:00.59]          at BuildSystem.Tests.DirectoryPackagesTests.<>c__DisplayClass10_0.<ShouldFindCorrectPathOfCentralConfiguration>b__0() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 171
[xUnit.net 00:00:00.59]          at FluentAssertions.Specialized.ActionAssertions.InvokeSubject()
[xUnit.net 00:00:00.59]          at FluentAssertions.Specialized.DelegateAssertions`2.InvokeSubjectWithInterception().
[xUnit.net 00:00:00.59]       Stack Trace:
[xUnit.net 00:00:00.59]            at FluentAssertions.Specialized.DelegateAssertionsBase`2.NotThrowInternal[TException](Exception exception, String because, Object[] becauseArgs)
[xUnit.net 00:00:00.59]            at FluentAssertions.Specialized.DelegateAssertions`2.NotThrow[TException](String because, Object[] becauseArgs)
[xUnit.net 00:00:00.59]         D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(180,0): at BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration()
[xUnit.net 00:00:00.59]            at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
[xUnit.net 00:00:00.59]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:00.66]   Finished:    BuildSystem.Tests
  BuildSystem.Tests test net10.0 failed with 2 error(s) (2.0s)
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(154): error TESTERROR:
      BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes (377ms): Error Message: Did not expect System.Exception because the project collection should be iso
      lated from environment side-effects, but found Microsoft.Build.Exceptions.InvalidProjectFileException: The SDK 'Microsoft.NET.SDK.WorkloadAutoImportPropsLocator' specified could not be found.  D:\DevTools\dotnet\sdk\10.0.103\Sdks\M
      icrosoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportWorkloads.props
         at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject(String errorSubCategoryResourceName, IElementLocation elementLocation, String resourceName, Object[] args)
         at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject[T1](IElementLocation elementLocation, String resourceName, T1 arg0)
         at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImportsFromUnescapedImportExpressionConditioned(String directoryOfImportingFile, ProjectImportElement importElement, List`1& projects, SdkResult& sdkResult)
         at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImports(String directoryOfImportingFile, ProjectImportElement importElement, SdkResult& sdkResult)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.Evaluate()
         at Microsoft.Build.Evaluation.Evaluator`4.Evaluate(IEvaluatorData`4 data, Project project, ProjectRootElement root, ProjectLoadSettings loadSettings, Int32 maxNodeCount, PropertyDictionary`1 environmentProperties, ILoggingServic
      e loggingService, IItemFactory`2 itemFactory, IToolsetProvider toolsetProvider, IDirectoryCacheFactory directoryCacheFactory, ProjectRootElementCacheBase projectRootElementCache, BuildEventContext buildEventContext, ISdkResolverSer
      vice sdkResolverService, Int32 submissionId, EvaluationContext evaluationContext, Boolean interactive)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.Reevaluate(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.Initialize(IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext, Boolean interacti
      ve)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings, EvaluationCont
      ext evaluationContext, IDirectoryCacheFactory directoryCacheFactory, Boolean interactive)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, IDictionary`2 globalProperties, String toolsVersion)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, String toolsVersion)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName)
         at BuildSystem.Tests.DirectoryPackagesTests.<>c__DisplayClass9_0.<OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes>b__0() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPa
      ckagesTests.cs:line 145
         at FluentAssertions.Specialized.ActionAssertions.InvokeSubject()
         at FluentAssertions.Specialized.DelegateAssertions`2.InvokeSubjectWithInterception().
      Stack Trace:
         at FluentAssertions.Specialized.DelegateAssertionsBase`2.NotThrowInternal[TException](Exception exception, String because, Object[] becauseArgs)
         at FluentAssertions.Specialized.DelegateAssertions`2.NotThrow[TException](String because, Object[] becauseArgs)
         at BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 154
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(180): error TESTERROR:
      BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration (8ms): Error Message: Did not expect System.Exception because the project collection should be isolated from environment side-effects, but found M
      icrosoft.Build.Exceptions.InvalidProjectFileException: The project file could not be loaded. Could not find file 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj'.  D:\workspace\Learnin
      gNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj
       ---> System.IO.FileNotFoundException: Could not find file 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj'.
      File name: 'D:\workspace\LearningNotes\Tests\BuildSystem.Tests\bin\Debug\net10.0\BuildSystem.Tests.csproj'
         at Microsoft.Win32.SafeHandles.SafeFileHandle.CreateFile(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options)
         at Microsoft.Win32.SafeHandles.SafeFileHandle.Open(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
         at System.IO.Strategies.OSFileStreamStrategy..ctor(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
         at System.IO.Strategies.FileStreamHelpers.ChooseStrategyCore(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
         at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share)
         at Microsoft.Build.Internal.XmlReaderExtension..ctor(String file, Boolean loadAsReadOnly)
         at Microsoft.Build.Internal.XmlReaderExtension.Create(String filePath, Boolean loadAsReadOnly)
         at Microsoft.Build.Construction.ProjectRootElement.LoadDocument(String fullPath, Boolean preserveFormatting, Boolean loadAsReadOnly)
         --- End of inner exception stack trace ---
         at Microsoft.Build.Shared.ProjectFileErrorUtilities.VerifyThrowInvalidProjectFile(Boolean condition, String errorSubCategoryResourceName, BuildEventFileInfo projectFile, Exception innerException, String resourceName, Object[] ar
      gs)
         at Microsoft.Build.Shared.ProjectFileErrorUtilities.ThrowInvalidProjectFile(BuildEventFileInfo projectFile, Exception innerException, String resourceName, Object[] args)
         at Microsoft.Build.Construction.ProjectRootElement.LoadDocument(String fullPath, Boolean preserveFormatting, Boolean loadAsReadOnly)
         at Microsoft.Build.Construction.ProjectRootElement..ctor(String path, ProjectRootElementCacheBase projectRootElementCache, Boolean preserveFormatting)
         at Microsoft.Build.Construction.ProjectRootElement.CreateProjectFromPath(String projectFile, ProjectRootElementCacheBase projectRootElementCache, Boolean preserveFormatting)
         at Microsoft.Build.Construction.ProjectRootElement.<>c.<OpenProjectOrSolution>b__209_0(String path, ProjectRootElementCacheBase cache)
         at Microsoft.Build.Evaluation.ProjectRootElementCache.GetOrLoad(String projectFile, OpenProjectRootElement loadProjectRootElement, Boolean isExplicitlyLoaded, Nullable`1 preserveFormatting)
         at Microsoft.Build.Evaluation.ProjectRootElementCache.Get(String projectFile, OpenProjectRootElement loadProjectRootElement, Boolean isExplicitlyLoaded, Nullable`1 preserveFormatting)
         at Microsoft.Build.Construction.ProjectRootElement.OpenProjectOrSolution(String fullPath, IDictionary`2 globalProperties, String toolsVersion, ProjectRootElementCacheBase projectRootElementCache, Boolean isExplicitlyLoaded)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, IDictionary`2 globalProperties, String toolsVersion)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, String toolsVersion)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName)
         at BuildSystem.Tests.DirectoryPackagesTests.<>c__DisplayClass10_0.<ShouldFindCorrectPathOfCentralConfiguration>b__0() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 171
         at FluentAssertions.Specialized.ActionAssertions.InvokeSubject()
         at FluentAssertions.Specialized.DelegateAssertions`2.InvokeSubjectWithInterception().
      Stack Trace:
         at FluentAssertions.Specialized.DelegateAssertionsBase`2.NotThrowInternal[TException](Exception exception, String because, Object[] becauseArgs)
         at FluentAssertions.Specialized.DelegateAssertions`2.NotThrow[TException](String because, Object[] becauseArgs)
         at BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 180
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)

Test summary: total: 3, failed: 2, succeeded: 1, skipped: 0, duration: 2.0s
Build failed with 2 error(s) and 8 warning(s) in 3.9s
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests>
```

## File: dotnet-test-powershell-interactions-202606291558.txt
```
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests> . "D:\workspace\LearningNotes\Tests\BuildSystem.Tests\auto-unit-tests.ps1"
Change current working directory to D:\workspace\LearningNotes\Tests successfully
Delete Root-Directory-Of-Container-That-Contains-Tests-for-LearningNotes-Directory-repomix-output.md successfully

📦 Repomix v1.14.0

✔ Packing completed successfully!

📈 Top 5 Files by Token Count:
──────────────────────────────
1.  BuildSystem.Tests/DirectoryPackagesTests.cs (2,728 tokens, 12,973 chars, 59%)
2.  BuildSystem.Tests/ProjectCollectionExtension.cs (353 tokens, 1,708 chars, 7.6%)
3.  BuildSystem.Tests/BuildSystem.Tests.csproj (277 tokens, 991 chars, 6%)
4.  .repomixignore (190 tokens, 671 chars, 4.1%)
5.  nuget.config (178 tokens, 644 chars, 3.9%)

🔎 Security Check:
──────────────────
✔ No suspicious files detected.


📊 Pack Summary:
────────────────
  Total Files: 13 files
 Total Tokens: 4,621 tokens
  Total Chars: 20,958 chars
       Output: Root-Directory-Of-Container-That-Contains-Tests-for-LearningNotes-Directory-repomix-output.md
     Security: ✔ No suspicious files detected

🎉 All Done!
Your repository has been successfully packed.

💡 Repomix is now available in your browser! Try it at https://repomix.com
Change current working directory to D:\workspace\LearningNotes\Tests\BuildSystem.Tests successfully
Delete BuildSystem.Tests-test-project-repomix-output.md successfully
Delete D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-restore.txt successfully
Delete D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-build.txt successfully
Delete D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-test.txt successfully

📦 Repomix v1.14.0

✔ Packing completed successfully!

📈 Top 5 Files by Token Count:
──────────────────────────────
1.  dotnet-test-powershell-interactions-2026061339.txt (7,207 tokens, 31,805 chars, 50.5%)
2.  DirectoryPackagesTests.cs (2,728 tokens, 12,973 chars, 19.1%)
3.  auto-unit-tests.ps1 (1,124 tokens, 4,567 chars, 7.9%)
4.  修改以下檔案後說明(2).txt (444 tokens, 1,132 chars, 3.1%)
5.  ProjectCollectionExtension.cs (353 tokens, 1,708 chars, 2.5%)

🔎 Security Check:
──────────────────
✔ No suspicious files detected.


📊 Pack Summary:
────────────────
  Total Files: 18 files
 Total Tokens: 14,282 tokens
  Total Chars: 61,554 chars
       Output: BuildSystem.Tests-test-project-repomix-output.md
     Security: ✔ No suspicious files detected

🎉 All Done!
Your repository has been successfully packed.

💡 Repomix is now available in your browser! Try it at https://repomix.com
Change current working directory to D:\workspace\LearningNotes\Tests\BuildSystem.Tests successfully
Clearing NuGet HTTP cache: D:\nuget\Local\http-cache
Clearing NuGet global packages folder: D:\DevTools\nuget_cache
Clearing NuGet Temp cache: C:\Users\userJay30\AppData\Local\Temp\NuGetScratch
Clearing NuGet plugins cache: C:\Users\userJay30\AppData\Local\NuGet\plugins-cache
Local resources cleared.
Successfully clear all local cached packages
Restore succeeded with 4 warning(s) in 11.2s
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq

Build succeeded with 4 warning(s) in 11.9s
Successfully restore D:\workspace\LearningNotes\Tests\BuildSystem.Tests
Restore succeeded with 4 warning(s) in 0.5s
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
  BuildSystem.Tests net10.0 succeeded with 6 warning(s) (1.5s) → bin\Debug\net10.0\BuildSystem.Tests.dll
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DictionaryExtensions.cs(4,39): warning CS8714: The type 'TKey' cannot be used as type parameter 'TKey' in the generic type or method 'Dictionary<TKey, TValue>'. Nullability of type argument 'TKey' doesn't match 'notnull' constraint.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\ProjectCollectionExtension.cs(14,9): warning CA2264: Calling 'ArgumentNullException.ThrowIfNull' and passing a non-nullable value is a no-op (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2264)

Build succeeded with 10 warning(s) in 3.0s
Successfully build D:\workspace\LearningNotes\Tests\BuildSystem.Tests
Restore succeeded with 4 warning(s) in 0.5s
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
  BuildSystem.Tests net10.0 succeeded with 4 warning(s) (0.2s) → bin\Debug\net10.0\BuildSystem.Tests.dll
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build (>= 17.12.0) but Microsoft.Build 17.12.0 was not found. Microsoft.Build 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1603: BuildSystem.Tests depends on Microsoft.Build.Utilities.Core (>= 17.12.0) but Microsoft.Build.Utilities.Core 17.12.0 was not found. Microsoft.Build.Utilities.Core 17.12.6 was resolved instead.
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests.csproj : warning NU1903: Package 'Microsoft.Build.Utilities.Core' 17.12.6 has a known high severity vulnerability, https://github.com/advisories/GHSA-w3q9-fxm7-j8fq
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.8.2+699d445a1a (64-bit .NET 10.0.3)
[xUnit.net 00:00:00.17]   Discovering: BuildSystem.Tests
[xUnit.net 00:00:00.23]   Discovered:  BuildSystem.Tests
[xUnit.net 00:00:00.24]   Starting:    BuildSystem.Tests
     Warning:
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     A paid commercial license supports the development and continued increasing support of
     Fluent Assertions users under both commercial and community licenses. Help us
     keep Fluent Assertions at the forefront of unit testing.
     For more information, visit https://xceed.com/products/unit-testing/fluent-assertions/
[xUnit.net 00:00:00.70]     BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes [FAIL]
[xUnit.net 00:00:00.70]       Did not expect System.Exception because the project collection should be isolated from environment side-effects, but found Microsoft.Build.Exceptions.InvalidProjectFileException: The SDK 'Microsoft.NET.SDK.WorkloadAutoImportPropsLocator' specified could not be found.  D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportWorkloads.props
[xUnit.net 00:00:00.70]          at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject(String errorSubCategoryResourceName, IElementLocation elementLocation, String resourceName, Object[] args)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject[T1](IElementLocation elementLocation, String resourceName, T1 arg0)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImportsFromUnescapedImportExpressionConditioned(String directoryOfImportingFile, ProjectImportElement importElement, List`1& projects, SdkResult& sdkResult)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImports(String directoryOfImportingFile, ProjectImportElement importElement, SdkResult& sdkResult)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.Evaluate()
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Evaluator`4.Evaluate(IEvaluatorData`4 data, Project project, ProjectRootElement root, ProjectLoadSettings loadSettings, Int32 maxNodeCount, PropertyDictionary`1 environmentProperties, ILoggingService loggingService, IItemFactory`2 itemFactory, IToolsetProvider toolsetProvider, IDirectoryCacheFactory directoryCacheFactory, ProjectRootElementCacheBase projectRootElementCache, BuildEventContext buildEventContext, ISdkResolverService sdkResolverService, Int32 submissionId, EvaluationContext evaluationContext, Boolean interactive)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project.ProjectImpl.Reevaluate(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, EvaluationContext evaluationContext)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(EvaluationContext evaluationContext)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project.ProjectImpl.Initialize(IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext, Boolean interactive)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext, IDirectoryCacheFactory directoryCacheFactory, Boolean interactive)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, IDictionary`2 globalProperties, String toolsVersion)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, String toolsVersion)
[xUnit.net 00:00:00.70]          at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName)
[xUnit.net 00:00:00.70]          at BuildSystem.Tests.DirectoryPackagesTests.<>c__DisplayClass10_0.<OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes>b__0() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 171
[xUnit.net 00:00:00.70]          at FluentAssertions.Specialized.ActionAssertions.InvokeSubject()
[xUnit.net 00:00:00.70]          at FluentAssertions.Specialized.DelegateAssertions`2.InvokeSubjectWithInterception().
[xUnit.net 00:00:00.71]       Stack Trace:
[xUnit.net 00:00:00.71]            at FluentAssertions.Specialized.DelegateAssertionsBase`2.NotThrowInternal[TException](Exception exception, String because, Object[] becauseArgs)
[xUnit.net 00:00:00.71]            at FluentAssertions.Specialized.DelegateAssertions`2.NotThrow[TException](String because, Object[] becauseArgs)
[xUnit.net 00:00:00.71]         D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(180,0): at BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes()
[xUnit.net 00:00:00.71]            at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
[xUnit.net 00:00:00.71]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:00.72]     BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration [FAIL]
[xUnit.net 00:00:00.72]       Expected directoryExists to be True because `D:\workspace\LearningNotes\Tests\Examples\Ex1\Ex1-1\MeProject1` does not exists., but found False.
[xUnit.net 00:00:00.72]       Stack Trace:
[xUnit.net 00:00:00.72]            at FluentAssertions.Primitives.BooleanAssertions`1.BeTrue(String because, Object[] becauseArgs)
[xUnit.net 00:00:00.72]         D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(195,0): at BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration()
[xUnit.net 00:00:00.72]            at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
[xUnit.net 00:00:00.72]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:00.78]     BuildSystem.Tests.DirectoryPackagesTests.Should_Have_CentralPackageManagement_Enabled [FAIL]
[xUnit.net 00:00:00.78]       Expected managePackageValue to be "true" with a length of 4 because 因為我們在架構上強制要求所有專案必須啟用中央套件管理 (CPM), but "false" has a length of 5, differs near "fal" (index 0).
[xUnit.net 00:00:00.78]       Stack Trace:
[xUnit.net 00:00:00.78]            at FluentAssertions.Primitives.StringAssertions`1.Be(String expected, String because, Object[] becauseArgs)
[xUnit.net 00:00:00.78]         D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(102,0): at BuildSystem.Tests.DirectoryPackagesTests.Should_Have_CentralPackageManagement_Enabled()
[xUnit.net 00:00:00.78]            at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
[xUnit.net 00:00:00.78]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:00.78]   Finished:    BuildSystem.Tests
  BuildSystem.Tests test net10.0 failed with 3 error(s) (2.0s)
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(180): error TESTERROR:
      BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes (365ms): Error Message: Did not expect System.Exception because the project collection should be iso
      lated from environment side-effects, but found Microsoft.Build.Exceptions.InvalidProjectFileException: The SDK 'Microsoft.NET.SDK.WorkloadAutoImportPropsLocator' specified could not be found.  D:\DevTools\dotnet\sdk\10.0.103\Sdks\M
      icrosoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportWorkloads.props
         at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject(String errorSubCategoryResourceName, IElementLocation elementLocation, String resourceName, Object[] args)
         at Microsoft.Build.Shared.ProjectErrorUtilities.ThrowInvalidProject[T1](IElementLocation elementLocation, String resourceName, T1 arg0)
         at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImportsFromUnescapedImportExpressionConditioned(String directoryOfImportingFile, ProjectImportElement importElement, List`1& projects, SdkResult& sdkResult)
         at Microsoft.Build.Evaluation.Evaluator`4.ExpandAndLoadImports(String directoryOfImportingFile, ProjectImportElement importElement, SdkResult& sdkResult)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.EvaluateImportElement(String directoryOfImportingFile, ProjectImportElement importElement)
         at Microsoft.Build.Evaluation.Evaluator`4.PerformDepthFirstPass(ProjectRootElement currentProjectOrImport)
         at Microsoft.Build.Evaluation.Evaluator`4.Evaluate()
         at Microsoft.Build.Evaluation.Evaluator`4.Evaluate(IEvaluatorData`4 data, Project project, ProjectRootElement root, ProjectLoadSettings loadSettings, Int32 maxNodeCount, PropertyDictionary`1 environmentProperties, ILoggingServic
      e loggingService, IItemFactory`2 itemFactory, IToolsetProvider toolsetProvider, IDirectoryCacheFactory directoryCacheFactory, ProjectRootElementCacheBase projectRootElementCache, BuildEventContext buildEventContext, ISdkResolverSer
      vice sdkResolverService, Int32 submissionId, EvaluationContext evaluationContext, Boolean interactive)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.Reevaluate(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(ILoggingService loggingServiceForEvaluation, EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.ReevaluateIfNecessary(EvaluationContext evaluationContext)
         at Microsoft.Build.Evaluation.Project.ProjectImpl.Initialize(IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectLoadSettings loadSettings, EvaluationContext evaluationContext, Boolean interacti
      ve)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings, EvaluationCont
      ext evaluationContext, IDirectoryCacheFactory directoryCacheFactory, Boolean interactive)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, String subToolsetVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection, ProjectLoadSettings loadSettings)
         at Microsoft.Build.Evaluation.Project..ctor(String projectFile, IDictionary`2 globalProperties, String toolsVersion, ProjectCollection projectCollection)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, IDictionary`2 globalProperties, String toolsVersion)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName, String toolsVersion)
         at Microsoft.Build.Evaluation.ProjectCollection.LoadProject(String fileName)
         at BuildSystem.Tests.DirectoryPackagesTests.<>c__DisplayClass10_0.<OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes>b__0() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryP
      ackagesTests.cs:line 171
         at FluentAssertions.Specialized.ActionAssertions.InvokeSubject()
         at FluentAssertions.Specialized.DelegateAssertions`2.InvokeSubjectWithInterception().
      Stack Trace:
         at FluentAssertions.Specialized.DelegateAssertionsBase`2.NotThrowInternal[TException](Exception exception, String because, Object[] becauseArgs)
         at FluentAssertions.Specialized.DelegateAssertions`2.NotThrow[TException](String because, Object[] becauseArgs)
         at BuildSystem.Tests.DirectoryPackagesTests.OptimizedVersion_AllProjectsShouldEnableCentralPackageManagementInDWorkspaceLearningNotes() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 180
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(195): error TESTERROR:
      BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration (7ms): Error Message: Expected directoryExists to be True because `D:\workspace\LearningNotes\Tests\Examples\Ex1\Ex1-1\MeProject1` does not exists
      ., but found False.
      Stack Trace:
         at FluentAssertions.Primitives.BooleanAssertions`1.BeTrue(String because, Object[] becauseArgs)
         at BuildSystem.Tests.DirectoryPackagesTests.ShouldFindCorrectPathOfCentralConfiguration() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 195
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
    D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs(102): error TESTERROR:
      BuildSystem.Tests.DirectoryPackagesTests.Should_Have_CentralPackageManagement_Enabled (69ms): Error Message: Expected managePackageValue to be "true" with a length of 4 because 因為我們在架構上強制要求所有專案必須啟用中央套件管理 (CPM), but "false" has a le
      ngth of 5, differs near "fal" (index 0).
      Stack Trace:
         at FluentAssertions.Primitives.StringAssertions`1.Be(String expected, String because, Object[] becauseArgs)
         at BuildSystem.Tests.DirectoryPackagesTests.Should_Have_CentralPackageManagement_Enabled() in D:\workspace\LearningNotes\Tests\BuildSystem.Tests\DirectoryPackagesTests.cs:line 102
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)

Test summary: total: 3, failed: 3, succeeded: 0, skipped: 0, duration: 1.9s
Build failed with 3 error(s) and 8 warning(s) in 3.8s
Failed to pass one or more test cases at D:\workspace\LearningNotes\Tests\BuildSystem.Tests
And thus it will stop and return.
PS D:\workspace\LearningNotes\Tests\BuildSystem.Tests>
```

## File: obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
```csharp
// <autogenerated />
using System;
using System.Reflection;
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v10.0", FrameworkDisplayName = ".NET 10.0")]
```

## File: obj/Debug/net10.0/BuildSystem.Tests.AssemblyInfo.cs
```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Reflection;

[assembly: System.Reflection.AssemblyCompanyAttribute("BuildSystem.Tests")]
[assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
[assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0")]
[assembly: System.Reflection.AssemblyProductAttribute("BuildSystem.Tests")]
[assembly: System.Reflection.AssemblyTitleAttribute("BuildSystem.Tests")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

// Generated by the MSBuild WriteCodeFragment class.
```

## File: obj/Debug/net10.0/BuildSystem.Tests.AssemblyInfoInputs.cache
```
ac0e514a1efa33005be2673e98d56f76a5f998b926ae980f5d9d7f674b664357
```

## File: obj/Debug/net10.0/BuildSystem.Tests.GeneratedMSBuildEditorConfig.editorconfig
```
is_global = true
build_property.TargetFramework = net10.0
build_property.TargetFrameworkIdentifier = .NETCoreApp
build_property.TargetFrameworkVersion = v10.0
build_property.TargetPlatformMinVersion = 
build_property.UsingMicrosoftNETSdkWeb = 
build_property.ProjectTypeGuids = 
build_property.InvariantGlobalization = 
build_property.PlatformNeutralAssembly = 
build_property.EnforceExtendedAnalyzerRules = 
build_property._SupportedPlatformList = Linux,macOS,Windows
build_property.RootNamespace = BuildSystem.Tests
build_property.ProjectDir = D:\workspace\LearningNotes\Tests\BuildSystem.Tests\
build_property.EnableComHosting = 
build_property.EnableGeneratedComInterfaceComImportInterop = 
build_property.EffectiveAnalysisLevelStyle = 10.0
build_property.EnableCodeStyleSeverity =
```

## File: obj/Debug/net10.0/BuildSystem.Tests.GlobalUsings.g.cs
```csharp
// <auto-generated/>
global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Net.Http;
global using System.Threading;
global using System.Threading.Tasks;
```

## File: ProjectCollectionExtension.cs
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

## File: ProjectCollectionSettings.cs
```csharp
namespace BuildSystem.Tests;

public record class ProjectCollectionSettings
{
    public string? DirectoryPackagesPropsPath { get; init; }
    public bool ShouldStopRecursivelySearchWhenFound { get; init; }
}
```

## File: repomix.config.json
```json
{
  "$schema": "https://repomix.com/schemas/latest/schema.json",
  "input": {
    "maxFileSize": 52428800
  },
  "output": {
    "filePath": "BuildSystem.Tests-test-project-repomix-output.md",
    "style": "markdown",
    "parsableStyle": false,
    "fileSummary": true,
    "directoryStructure": true,
    "files": true,
    "removeComments": false,
    "removeEmptyLines": false,
    "compress": false,
    "topFilesLength": 5,
    "showLineNumbers": false,
    "truncateBase64": false,
    "copyToClipboard": false,
    "includeFullDirectoryStructure": false,
    "tokenCountTree": false,
    "git": {
      "sortByChanges": true,
      "sortByChangesMaxCommits": 100,
      "includeDiffs": false,
      "includeLogs": false,
      "includeLogsCount": 50
    }
  },
  "include": [],
  "ignore": {
    "useGitignore": true,
    "useDotIgnore": true,
    "useDefaultPatterns": true,
    "customPatterns": []
  },
  "security": {
    "enableSecurityCheck": true
  },
  "tokenCount": {
    "encoding": "o200k_base"
  }
}
```

## File: 修改以下檔案後說明.txt
```
根據

+"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests-test-project-repomix-output.md"(Repomix工具打包所產生的檔案，包含了BuildSystem.Tests的專案結構和其檔案內容(若是檔案)，這裡面有許多重要線索，**所以請務必詳讀**)

+ 日誌檔

"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-build.txt" (建置)

"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-restore.txt" (還原)

"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-test.txt" (測試)

+ PowerShell互動內容:

D:\workspace\Temp\EnvironmentVariables\20260627-environment-variables.txt

(名稱有些誤導性，檔案內容不只包含在最後一個執行的腳本 "D:\workspace\Scripts\List-All-EnvironmentVariables-NameAndValue.ps1" 而列出出所有的環境變數的名稱和值，**所以請務必詳讀**，還包含清理本地快取套件然後還原、建置、測試) 

幫我找出單元測試失敗的可能原因並解決
```

## File: 修改以下檔案後說明(2).txt
```
根據

+"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem.Tests-test-project-repomix-output.md"(Repomix工具打包所產生的檔案，包含了BuildSystem.Tests的專案結構和其檔案內容(若是檔案)，這裡面有許多重要線索，**所以請務必詳讀**)

+ 日誌檔

"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-build.txt" (建置)

"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-restore.txt" (還原)

"D:\workspace\LearningNotes\Tests\BuildSystem.Tests\BuildSystem_Tests-msbuild-test.txt" (測試)

+ PowerShell互動內容:

D:\workspace\Temp\EnvironmentVariables\20260627-environment-variables.txt

(名稱有些誤導性，檔案內容不只包含在最後一個執行的腳本 "D:\workspace\Scripts\List-All-EnvironmentVariables-NameAndValue.ps1" 而列出出所有的環境變數的名稱和值，**所以請務必詳讀**，還包含清理本地快取套件然後還原、建置、測試) 

幫我找出單元測試失敗的可能原因並解決

備註:

幹，我剛剛忘了說

1. 這個專案的單元測試案例主要是mock msbuild在 `D:\workspace\LearningNotes\CSharp`的行為

2. 我在 `D:\workspace\LearningNotes\CSharp`目錄底下有放這幾個組態檔

+ Directory.Build.props

+ Directory.Build.targets

+ Directory.Packages.props

+ global.json

+ nuget.config

3. 然而在D:\workspace\LearningNotes\Tests目錄底下只有以下跟MSBuild有關的組態檔

+ global.json

+ nuget.config
```

## File: 組態檔的搬移或建立.txt
```
建議我在 `D:\workspace\LearningNotes\CSharp`目錄底下這幾個組態檔



+ Directory.Build.props



+ Directory.Build.targets



+ Directory.Packages.props



複製貼上至 D:\workspace\LearningNotes\Tests目錄底下嗎?
```
