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
    // private readonly string _baseDirectory;
    // private readonly string _learningNotesRootDirectory;
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



    public DirectoryPackagesTests()
    {
        _fileSystem = new FileSystem();

        /// ======= obsolete (uncommented it just due to back compatibility) ======= ///
        // _baseDirectory = AppContext.BaseDirectory;
        // _learningNotesRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_baseDirectory, "../../../../../"));
        // _csharpNotesRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_learningNotesRootDirectory, "CSharp"));

        /// ======= new ======= ///
        _solutionRootDirectoryOfUnitTestAboutMsbuild = @"D:\workspace\LearningNotes\Tests";

        _directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuild, Constants.DIRECTORY_PACKAGES_PROPS));

        _learningNotesAsContainerRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuild, ".."));
        _solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_learningNotesAsContainerRootDirectory, "CSharp"));
    }

    /// <summary>
    /// According to the tags (e.g. `<ManagePackageVersionsCentrally>`, `<CentralPackageTransitivePinningEnabled>` ) in 
    /// `"D:\workspace\LearningNotes\CSharp\Directory.Packages.props"`, 
    /// checks when msbuild start to read configuration from `"D:\workspace\LearningNotes\CSharp\Directory.Packages.props"`
    /// the msbuild have expected value of these tags (e.g. `<ManagePackageVersionsCentrally>`, `<CentralPackageTransitivePinningEnabled>` )
    /// </summary>
    [Fact]
    public void WhenManagePackageVersionsCentrallyTagIsSetToTrueAndWhenManagePackageVersionsCentrallyTagIsSetToTrueInRealEnvironment1ForExampleInDworkspaceLearningNotesCSharp_ShouldHaveCentralPackageManagementEnabledAndCentralPackageTransitivePinningEnabled()
    {
        // 1. 定義路徑：透過 _fileSystem 的 Path 抽象來處理，提升防禦性與可測試性
        string directoryPackagesProps = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory, Constants.DIRECTORY_PACKAGES_PROPS));
        string dummyProjectCsProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(_solutionRootDirectoryOfUnitTestAboutMsbuildBehaviorOnCSharpNotesRootDirectory, "DummyProject.csproj"));

        // 使用 FluentAssertions 取代 xUnit 原生斷言
        _fileSystem.File.Exists(directoryPackagesProps).Should().BeTrue($"Can't find global configurationa at `{directoryPackagesProps}`");
        var dummyFileWriter = new DummyFileWriter(_fileSystem);
        dummyFileWriter.CreateDummyProject(dummyFilePath: dummyProjectCsProject);
        _fileSystem.File.Exists(dummyProjectCsProject).Should().BeTrue($"Can't find dummy file `{dummyProjectCsProject}` that should be created just now.");

        var projectCollectionSettings = new ProjectCollectionSettings
        {
            ManagePackageVersionsCentrally = null,
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
        managePackageValue.Should().Be("true", "Because <ManagePackageVersionsCentrally> tag is set to be false");

        string? transitivePinningValue = project.GetPropertyValue("CentralPackageTransitivePinningEnabled");
        transitivePinningValue.Should().Be("true",
        "Because <CentralPackageTransitivePinningEnabled> tag should be set to true");
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


    /// <summary>
    /// According to the tags (e.g. `<ManagePackageVersionsCentrally>`, `<CentralPackageTransitivePinningEnabled>` ) in 
    /// `"D:\workspace\LearningNotes\CSharp\Directory.Packages.props"`, 
    /// checks when msbuild start to read configuration from `"D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj"`
    /// the msbuild have expected value of these tags (e.g. `<ManagePackageVersionsCentrally>`, `<CentralPackageTransitivePinningEnabled>` )
    /// due to the recursively search from down to top until its configurations files are found 
    /// </summary>
    /// <remarks>
    /// > [!NOTES]
    /// > When a configuration file (e.g.) is found, msbuild will not search from down to top file with same name.
    /// > 
    /// > For example,
    /// > 
    /// > Suppose msbuild starts find a file from `"D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj"`,
    /// > it checks `Directory.Packages.props` exists in "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1"
    /// > If not found, then 
    /// > it checks `Directory.Packages.props exists` in "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\"
    /// > If not found, then 
    /// > it checks `Directory.Packages.props` exists in "D:\workspace\LearningNotes\CSharp\Examples\Ex1\"
    /// > If not found, then 
    /// > it checks `Directory.Packages.props` exists in "D:\workspace\LearningNotes\CSharp\Examples\"
    /// > If not found, then 
    /// > it checks `Directory.Packages.props` exists in "D:\workspace\LearningNotes\CSharp\"
    /// > If it is found, then
    /// > it will not check `Directory.Packages.props` exists in "D:\workspace\LearningNotes" 
    /// > However, it may check `Directory.Build.props` exists in "D:\workspace\LearningNotes" 
    /// </remarks>
    [Fact]
    public void OptimizedVersion_WhenAProjectUnderDirectoryDWorkspaceLearningNotesCSharpIsLoaded_ShouldHaveCentralPackageManagementEnabledAndCentralPackageTransitivePinningEnabled()
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
            ManagePackageVersionsCentrally = null,
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

    /// uncommented due to duplicated code.
    /// <summary>
    /// Verifies that all projects correctly enable Central Package Management (CPM).
    /// </summary>
    // [Fact]
    // public void ShouldFindCorrectPathOfCentralConfiguration()
    // {
    //     // Arrange
    //     string solutionDir = _solutionRootDirectoryOfUnitTestAboutMsbuild;
    //     string targetProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(solutionDir, "Examples", "Ex1", "Ex1-1", "MeProject1"));
    //     string targetProjectCsProject = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(targetProject, "MeProject1.csproj"));

    //     bool directoryExists = _fileSystem.Path.Exists(targetProject);
    //     directoryExists.Should().BeTrue($"`{targetProject}` does not exists.");

    //     bool fileExist = _fileSystem.File.Exists(targetProjectCsProject);
    //     fileExist.Should().BeTrue($"`{targetProjectCsProject}` does not exists. It should exist since the project exists at `{targetProject}`");

    //     bool directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild_exists = _fileSystem.File.Exists(_directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild);

    //     directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild_exists.Should().BeTrue($"`{_directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild}` configuration file should exist");

    //     var projectCollectionSettings = new ProjectCollectionSettings
    //     {
    //         DirectoryPackagesPropsPath = _directoryPackagesProps_globalConfiguration_of_solutionRootDirectoryOfUnitTestAboutMsbuild,
    //         // force to stop recursively searching when the ${DirectoryPackagesPropsPath} is found.
    //         ShouldStopRecursivelySearchWhenFound = true,
    //     };

    //     // Act
    //     Action act = () =>
    //     {
    //         using var projectCollection = new ProjectCollection(ProjectCollectionExtension.GetDefaultProjectCollectionSettings(projectCollectionSettings));
    //         var project = projectCollection.LoadProject(targetProject);
            
    //         // Assert inside target validation
    //         var cpmProperty = project.GetProperty("ManagePackageVersionsCentrally");
    //         cpmProperty.Should().NotBeNull("because Central Package Management must be explicitly configured");
    //         cpmProperty.EvaluatedValue.Should().BeEquivalentTo("true");
    //     };

    //     // Assert - Ensure no MSBuild evaluation exception is thrown during project loading
    //     act.Should().NotThrow<Exception>("because the project collection should be isolated from environment side-effects");
    // }
}