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
.clinerules
.repomixignore
ai-rules.md
Directory.Build.props
Directory.Build.targets
Directory.Packages.props
DummyProject.csproj
Examples/Ex1/Ex1-1/Ex1-1.slnx
Examples/Ex1/Ex1-1/MeProject1/EntryPoint.cs
Examples/Ex1/Ex1-1/MeProject1/EntryPoint1.cs
Examples/Ex1/Ex1-1/MeProject1/Ex1-1_MeProject1-msbuild-restore.txt
Examples/Ex1/Ex1-1/MeProject1/ILogUtilityService.cs
Examples/Ex1/Ex1-1/MeProject1/LogUtilityService.cs
Examples/Ex1/Ex1-1/MeProject1/Me.cs
Examples/Ex1/Ex1-1/MeProject1/MeProject1.csproj
Examples/Ex1/Ex1-1/MeProject1/README.md
Examples/Ex1/Ex1-1/MeProject1/Son.cs
Examples/Ex1/Ex1-1/MeProject2/Me.cs
Examples/Ex1/Ex1-1/MeProject2/MeProject2.csproj
Examples/Ex1/Ex1-1/MeProject2/README.md
Examples/Ex1/Ex1-1/MeProject2/Son.cs
Examples/Ex1/Ex1-1/Questions/Q1.md
Examples/Ex1/Ex1-1/ReflectionUtilityServices/IReflectionUtilityService.cs
Examples/Ex1/Ex1-1/ReflectionUtilityServices/README.md
Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityService.cs
Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityServices.csproj
file-structure.txt
GEMINI.md
global.json
nuget.config
PreloadPropsEntry.props.entry.config
repomix.config.json
```

# Files

## File: .clinerules
````
# 角色與核心哲學

你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。

# 核心硬性規定 (鐵律，絕不可違反)

## 1. 單元測試斷言標準 (最高優先級)

- 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

- 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

- 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

  正例：

  Action act = () => _service.DoSomething();

  act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");


## 2. 測試環境之檔案系統與 Mock 副作用防禦

- 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 當測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。

## 3. 回覆語言

- 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法）保持 C# 原生英文。

- 針對程式碼的XML註解，請永遠使用英文(US, America)

## 4. 程式碼產生

- 易讀性:針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`

- 防禦性檢查:針對非單元測試的類別(和其方法)，請務必進行防禦性檢查(詳見`7. 防禦性檢查`)

- 易維護、易測試:針對涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 易維護、易測試:針對測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，其類別一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 易維護:針對非單元測試的類別(和其方法)，盡可能地使用interface和decoration pattern(不過有例外地，請根據情境判斷之)

- 簡潔:針對非單元測試的類別(和其方法)，請針對我正在開發的Utility package底下，適當地用facade pattern以減少其他要引用該utility package的專案底下需要傳入太多引數至建構子或方法

- 效能:針對我新開發的Utility package，優先使用效能最佳的手段 (詳見`5. 效能最佳之設計`)

- 針對我新開發的Utility package，優先盡可能地設計出一個參數不多的API (詳見`6. 參數最佳之設計`)

- 簡潔:針對非單元測試的類別(和其方法)，根據各種的Design Pattern的優缺點和其應用來決定是否要用哪種，若用到的Design Pattern需要使用介面時，則一併附上介面的定義。

- 高重用性:針對非單元測試的類別(和其方法)，盡可能重用我自己已經開發完的Utility service(位在`~\modules`資料夾底下的專案)、內建API

、第三方套件的API

- 易維護:針對非單元測試的類別，其產生的類別(和其方法、其Property)永遠應該要遵守‵.editorconfig‵的規則(且其掃描機制跟Rolsyn一模一樣)

- 請使用最新語法完成設計(尤其是針對字串,詳見`8. 字串`)

## 5. 效能最佳之設計
### API
包含但不限於

- `struct`

- `ReadOnlySpan<T>`, `ReadOnlyMemory<T>`等系列的API

- `IMemoryCached` 等系列的API 而不是`Dictionary<T>`

- `Stream`等系列的API

- `PipelineWriter`,`PipelineReader` 等系列的API

### 策略
包含但不限於

- 快取機制

- 增量機制

- Zero allocation (詳見`ReadOnlySpan<T>`)

- 減少GC的發生 (詳見`ReadOnlySpan<T>`)

- 減少堆疊使用 (提示:可以使用`struct`而不是`class`)

- 避免一次讀取大檔案 可使用`Stream`甚至是`Pipeline` (參考`PipelineWriter`等)


## 6. 參數最佳之設計
### 策略
包含但不限於

- POCO (`class`, `struct`, `record`, `record class` etc)

- facade pattern (詳見`4. 程式碼產生`的某一條)

## 7. 防禦性檢查
### 檢查項目
包含但不限於

- 判斷傳入的引數的值是否合法 (如是否是null)

- 根據意圖，判斷即將要回傳的回傳值是否合法

### 撰寫模式
盡可能的

- 使用.NET本身就有的內建方法(如:`ArgumentNullException.ThrowIfNull(x)`,`ArgumentOutOfRangeException.ThrowIfGreaterThan(counter,4)`)而不是用`throw new`來拋出異常(如:`throw new Excception("")`)

## 8. 字串
### 撰寫風格

- 針對字串串接，請使用字串插值 (若可以)

- 針對字串，請使用Raw String Literals
````

## File: .repomixignore
````
# Add patterns to ignore here, one per line
# Example:
# *.log
# tmp/
````

## File: ai-rules.md
````markdown
# 角色與核心哲學

你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。



# 核心硬性規定 (鐵律，絕不可違反)



## 1. 單元測試斷言標準 (最高優先級)

- 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

- 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

- 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

  正例：

  Action act = () => _service.DoSomething();

  act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");



## 2. 測試環境之檔案系統與 Mock 副作用防禦

- 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`。

- 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。



## 3. 回覆語言

- 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法）保持 C# 原生英文。



- 針對程式碼的XML註解，請永遠使用英文(US, America)



## 4. 程式碼產生

- 針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`
````

## File: Directory.Build.props
````
<Project>
  <ImportGroup>
    <Import Project="$(MSBuildThisFileDirectory)\PreloadPropsEntry.props.entry.config" Condition="Exists('$(MSBuildThisFileDirectory)\PreloadPropsEntry.props.entry.config')" />
  </ImportGroup>
  <PropertyGroup>
    <Authors>codeloverjay30</Authors>
    <IsClassLibraryPackage>true</IsClassLibraryPackage>
    <IsBlazorWasmPackage>false</IsBlazorWasmPackage>
    <PackageRootPath>$(MSBuildThisFileDirectory)</PackageRootPath>
    <!-- Default path of local NuGet package that will be packed -->
    <MyNuGetPackageOutputPath>$(PackageRootPath)..\..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
  </PropertyGroup>
  <Choose> 
    <When Condition="'$(IsClassLibraryPackage)' == 'true'">
      <!-- The package is class library or not -->
      <PropertyGroup>
        <MyNuGetPackageOutputPath>$(PackageRootPath)..\..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
      </PropertyGroup>
    </When>
    <!-- The package is used for Blazor WebAssembly or not -->
    <When Condition="'$(IsBlazorWasmPackage)' == 'true'">
      <PropertyGroup>
        <MyNuGetPackageOutputPath>$(PackageRootPath)..\..\MyLocalNuget\BlazorWebAssembly</MyNuGetPackageOutputPath>
      </PropertyGroup>
    </When>
    <Otherwise>
      <PropertyGroup>
        <MyNuGetPackageOutputPath>$(PackageRootPath)..\..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
      </PropertyGroup>
    </Otherwise>
  </Choose>
  <PropertyGroup>
    <PackageOutputPath>$(MyNuGetPackageOutputPath)</PackageOutputPath>
    <PackageId>$(MSBuildProjectName)</PackageId>
    <GeneratePackageOnBuild>false</GeneratePackageOnBuild>
    <PackageTags>Utility Service</PackageTags>
    <Description>See README.md</Description>
    <PackageReleaseNotes>See README.md</PackageReleaseNotes>
    <!-- <PackageReadmeFile>$(ProjectDir)README.md</PackageReadmeFile> -->
    <RepositoryUrl>https://github.com/codeloverjay30/utility_packages</RepositoryUrl>
    <RepositoryType>git</RepositoryType>
  </PropertyGroup>
  <!-- define property that belongs to framework at .NET SDK 6.0 -->
  <PropertyGroup>
    <IsAtLeastNet6>false</IsAtLeastNet6>  
    <IsAtLeastNet6 Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '6.0'))">true</IsAtLeastNet6>
  </PropertyGroup>
  <!-- define property that belongs to legacy framework -->
  <PropertyGroup>
    <IsLegacyFramework>true</IsLegacyFramework>  
    <IsLegacyFramework Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '6.0'))">false</IsLegacyFramework>
  </PropertyGroup>
  <!-- define property that belongs to framework at .NET SDK 8.0 -->
  <PropertyGroup>
    <IsAtLeastNet8>false</IsAtLeastNet8>  
    <IsAtLeastNet8 Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '8.0'))">true</IsAtLeastNet8>
  </PropertyGroup>
  <Choose>
      <When Condition="'$(IsAtLeastNet6)'=='true'">
        <PropertyGroup>
          <ImplicitUsings>enable</ImplicitUsings>
          <Nullable>enable</Nullable>
        </PropertyGroup>
      </When>
      <Otherwise>
        <PropertyGroup>
          <ImplicitUsings>disable</ImplicitUsings>
          <Nullable>disable</Nullable>
        </PropertyGroup>
      </Otherwise>
  </Choose>
  <PropertyGroup>
    <!-- set is as packable by default -->
    <IsPackable>true</IsPackable>
    
    <!-- If the project contains "Test", "Demo", or "Sample", set it as unpackable -->
    <IsTestProject Condition="$(MSBuildProjectName.Contains('Test'))">true</IsTestProject>
    <IsDemoProject Condition="$(MSBuildProjectName.Contains('Demo'))">true</IsDemoProject>
    <IsSampleProject Condition="$(MSBuildProjectName.Contains('Sample'))">true</IsSampleProject>
  
    <!-- indicates that project is experimental -->
    <IsExperimental Condition="$(MSBuildProjectDirectory.Contains('experiments'))">true</IsExperimental>

    <!-- indicates that project is stable -->
    <IsStable Condition="$(MSBuildProjectDirectory.Contains('modules'))">true</IsStable>

    <!-- indicates that project is stable and is module -->
    <IsModule Condition="$(MSBuildProjectDirectory.Contains('modules'))">true</IsModule>

    <!-- indicates that project is stable class library -->
    <IsClassLibrary Condition="'$(IsTestProject)' == 'true' Or '$(IsDemoProject)' == 'true' Or '$(IsSampleProject)' == 'true'">false</IsClassLibrary>

    <!-- set it as packable iff it is a class library and it is stable. Otherwise, set is unpackable -->
    <IsPackable Condition="('$(IsStable)' != 'true' And '$(IsModule)' != 'true') Or ('$(IsClassLibrary)' == 'false')">false</IsPackable>
  </PropertyGroup>
  <PropertyGroup Condition="'$(IsModule)' == 'true' And ( '$(IsClassLibrary)' == 'true' Or '$(IsTestProject)' == 'true')">
    <!-- build target path -->
    <OutputPath>$(SolutionDir)\artifacts\$(Configuration)\</OutputPath>
  </PropertyGroup>
  <!-- is it class library and is .NET SDK 8.0+ -->
  <PropertyGroup Condition="'$(IsAtLeastNet8)' == 'true' And '$(IsClassLibrary)' == 'true'">
    <!-- set the build target to put into artifacts path(see`<ArtifactsPath>`), indicating VSCode launcher (`.vscode/launch.json`) uses build target which placed in `<ArtifactsPath>`-->
    <UseArtifactsOutput>true</UseArtifactsOutput>
    <!-- artifacts path -->
    <ArtifactsPath>$(PackageRootPath).artifacts</ArtifactsPath>
  </PropertyGroup>
  <ItemGroup>
    <None Condition="'$(IsPackable)' == 'true'" Include="$(ProjectDir)README.md" Pack="true" PackagePath="\" />
  </ItemGroup>
</Project>
````

## File: Directory.Build.targets
````
<Project>
  <Target Name="RemoveVersions" BeforeTargets="CollectPackageReferences">
    <ItemGroup>
      <PackageReference Update="@(PackageReference)" 
                      Version="" 
                      Condition="'%(Identity)' != 'NETStandard.Library' And !$([System.String]::Copy('%(Identity)').StartsWith('Microsoft.Extensions.'))" />
    </ItemGroup>
  </Target>

  <!-- The Target is triggered once NuGet completes to restore -->
  <Target Name="ValidateCompatibilityPackageVersions" AfterTargets="_CheckForUnsupportedTargetFramework">
    
    <!-- Get all `<PackageVersion>`  -->
    <ItemGroup>
      <!-- If `NETStandard.Library package` is specified but its `Version` attribute is NOT specified or with incorrect value, then setting this tag and thus throws an error -->
      <InvalidCompats Include="@(PackageVersion)" 
                      Condition="'%(Identity)' == 'NETStandard.Library' And '%(Version)' == ''" />
    </ItemGroup>

    <!-- if `InvalidCompats` is set, throws an error -->
    <Error Text="【架構警告】偵測到相容性套件 NETStandard.Library 未在中央明確指定版本號！請至 Directory.Packages.props 補上。" 
           Code="ARCH001" 
           Condition="'@(InvalidCompats)' != ''" />
  </Target>
</Project>
````

## File: Directory.Packages.props
````
<Project>
	<PropertyGroup>
	  <NoWarn>$(NoWarn);NU1507</NoWarn>
	</PropertyGroup>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    <CentralPackageTransitivePinningEnabled>true</CentralPackageTransitivePinningEnabled>
  </PropertyGroup>
  <ItemGroup>
      <PackageVersion Include="Microsoft.Extensions.Hosting" Version="10.0.3" />
  </ItemGroup>

  <!-- If one didn't specify the package version, treat it as errors instead of warnings -->
  <PropertyGroup>
    <WarningsAsErrors>$(WarningsAsErrors);NU1008;NU1605</WarningsAsErrors>
  </PropertyGroup>
  <!-- Let projetcs can be compiled in .NET Framework (older version) when not installed -->
  <ItemGroup>
    <GlobalPackageVersion Include="NETStandard.Library" Version="2.0.3" PrivateAssets="all" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive"/>
  </ItemGroup>
  <ItemGroup>
    <!-- This applies to ALL projects in the repository automatically -->
    <GlobalPackageVersion Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.3" PrivateAssets="all" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
  </ItemGroup>
  <!-- Use .ASP .NET Core Framework -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.AspNetCore.App.Ref" Version="10.0.5" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.TeamFoundationServer.ExtendedClient" Version="20.256.2" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.Extensions.TimeProvider.Testing" Version="10.6.0" />
  </ItemGroup>
  <!-- Mock File System -->
  <ItemGroup>
    <!-- abstraction of File system -->
    <PackageVersion Include="System.IO.Abstractions" Version="22.1.1" />
    <!-- Mock File System -->
    <PackageVersion Include="System.IO.Abstractions.TestingHelpers" Version="22.1.1" />
  </ItemGroup>
  <!-- abstraction of System Process -->
  <ItemGroup>
    <PackageVersion Include="System.Diagnostics.Abstractions" Version="1.0.9" />
  </ItemGroup>
  <!-- abstraction of System Process (used by older test framework such as `xUnit2`) -->
  <ItemGroup>
    <PackageVersion Include="Testably.Abstractions.FileSystem.Interface" Version="10.2.0" />
  </ItemGroup>
  <!-- abstraction of Environment class -->
  <ItemGroup>
    <PackageVersion Include="EnvironmentAbstractions" Version="5.0.0" />
    <!-- Mock Environment -->
    <PackageVersion Include="EnvironmentAbstractions.TestHelpers" Version="5.0.0" />
  </ItemGroup>
  <!-- Official utility packages of command line -->
  <ItemGroup>
    <PackageVersion Include="System.CommandLine" Version="2.0.5" />
  </ItemGroup>
  <!-- `MSBuild`, simulating `MSBuild` behaviour -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.Build" Version="18.4.0" />
  </ItemGroup>
  <!-- Locator of `MSBuild` -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.Build.Locator" Version="1.11.2" />
  </ItemGroup>
  <!-- Official utility packages about accessing version of NuGet package or .NET framework -->
  <ItemGroup>
    <!-- accessing version of  NuGet package -->
    <PackageVersion Include="NuGet.Protocol" Version="7.3.1" />
    <!-- accessing version of  .NET framework -->
    <PackageVersion Include="NuGet.Frameworks" Version="7.3.1" />
  </ItemGroup>
  <!-- Use traditional mode `VSTest` -->
  <!-- `NUnit` as Test Framework -->
  <ItemGroup>
    <PackageVersion Include="NUnit" Version="4.3.2" />
    <PackageVersion Include="NUnit3TestAdapter" Version="6.2.0" />
    <PackageVersion Include="NUnit.Analyzers" Version="4.7.0" />
  </ItemGroup>
  <!-- `xUnit.v3` as Test Framework -->
  <ItemGroup>
    <PackageVersion Include="coverlet.collector" Version="6.0.4" />
    <PackageVersion Include="xunit.v3" Version="3.2.2" />
    <PackageVersion Include="xunit.v3.mtp-v2" Version="3.2.2" />
    <PackageVersion Include="xunit.v3.extensibility.core" Version="3.2.2" />
  </ItemGroup>
  <!-- package that checks the class or property contains specific words, it uses `xUnit.v2` as Test Framework -->
  <ItemGroup>
    <PackageVersion Include="TngTech.ArchUnitNET.xUnit" Version="0.13.3" />
  </ItemGroup>
  <!-- `xUnit.v2` as Test Framework -->
  <ItemGroup>
    <PackageVersion Include="xunit" Version="2.9.3" />
    <PackageVersion Include="xunit.runner.visualstudio" Version="3.1.4" />
  </ItemGroup>
  <!-- Use modern mode `MTP` -->
  <!-- use `MSTest` as Test Framework -->
  <ItemGroup>
    <PackageVersion Include="MSTest.TestAdapter" Version="3.2.2" />
    <PackageVersion Include="MSTest.TestFramework" Version="3.2.2" />
    <PackageVersion Include="Microsoft.Testing.Extensions.VSTestBridge" Version="2.1.0" />
  </ItemGroup>
  <!-- use `NUnit` using `MTP` as Test Framework -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.TestPlatform" Version="18.4.0" />
    <PackageVersion Include="Microsoft.Testing.Platform.MSBuild" Version="2.2.1" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.Testing.Platform" Version="2.2.1" />
  </ItemGroup>
  <!-- packages that makes the assertions be more fluent -->
  <ItemGroup>
    <PackageVersion Include="FluentAssertions" Version="8.9.0" />
    <PackageVersion Include="FluentValidation" Version="12.1.1" />
  </ItemGroup>
  <!-- packages that mock the interface and virtual class -->
  <ItemGroup>
    <PackageVersion Include="Moq" Version="4.20.72" />
  </ItemGroup>
  <!-- packages that checks the AST of class (including, check a class contains property named `property1`) -->
  <ItemGroup>
    <PackageVersion Include="NSubstitute" Version="5.3.0" />
  </ItemGroup>
  <!-- Rolsyn -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.CodeAnalysis.CSharp" Version="5.3.0" />
    <PackageVersion Include="Microsoft.CodeAnalysis.CSharp.Workspaces" Version="5.3.0" />
  </ItemGroup>
  <!-- Rolsyn used for tests -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.CodeAnalysis.CSharp.SourceGenerators.Testing" Version="1.1.3" />
  </ItemGroup>
  <!-- Roslynator -->
  <ItemGroup>
    <PackageVersion Include="Roslynator.Analyzers" Version="4.15.0"/>
  </ItemGroup>
  <!-- SonarAnalyzer -->
  <ItemGroup>
    <PackageVersion Include="SonarAnalyzer.CSharp" Version="10.25.0.139117" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.Bcl.AsyncInterfaces" Version="10.0.7" />
    <PackageVersion Include="System.ClientModel" Version="1.10.0" />
    <PackageVersion Include="System.Memory.Data" Version="10.0.3" />
  </ItemGroup>
   <!-- logger `ILogger` -->
   <ItemGroup>
        <PackageVersion Include="Microsoft.Extensions.Logging" Version="[11.0.0-preview.2.26159.112]" />
        <PackageVersion Include="Microsoft.Extensions.Logging.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
        <PackageVersion Include="Microsoft.Extensions.Options" Version="[11.0.0-preview.2.26159.112]" />
        <PackageVersion Include="Microsoft.Extensions.DependencyInjection" Version="[11.0.0-preview.2.26159.112]" />
        <PackageVersion Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
        <PackageVersion Include="Microsoft.Extensions.Configuration.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
        <PackageVersion Include="Microsoft.Extensions.Primitives" Version="[11.0.0-preview.2.26159.112]" />
        <!-- <PackageVersion Include="Microsoft.Extensions.Hosting" Version="10.0.3" /> -->
        <PackageVersion Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.3" />
    </ItemGroup>

   <!-- Azure -->
  <ItemGroup>
    <PackageVersion Include="Azure.ResourceManager" Version="1.14.0" />
    <PackageVersion Include="Azure.Identity" Version="1.18.0" />
    <PackageVersion Include="Azure.Core" Version="1.51.1" />
    <PackageVersion Include="Microsoft.Extensions.Configuration" Version="10.0.3" />
    <PackageVersion Include="Microsoft.Extensions.Configuration.AzureAppConfiguration" Version="8.5.0" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="System.Management" Version="10.0.2" />
  </ItemGroup>
  <!-- Registry key on Windows OS -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.Win32.Registry" Version="5.0.0" />
  </ItemGroup>
  <!-- ACL on Windows OS -->
  <ItemGroup>
    <PackageVersion Include="System.Security.AccessControl" Version="6.0.1" />
  </ItemGroup>
  <!-- lock -->
  <ItemGroup>
    <PackageVersion Include="AsyncKeyedLock" Version="8.0.2" />
  </ItemGroup>
  <!-- EF Core -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.EntityFrameworkCore" Version="10.0.3" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="StackExchange.Redis" Version="2.11.0" />
  </ItemGroup>
  <!-- `Serilog` -->
  <ItemGroup>
    <PackageVersion Include="Serilog" Version="4.3.1" />
    <PackageVersion Include="Serilog.Settings.Configuration" Version="10.0.0" />
    <PackageVersion Include="Serilog.Enrichers.Environment" Version="3.0.1" />
    <PackageVersion Include="Serilog.Exceptions" Version="8.4.0" />
    <PackageVersion Include="Serilog.Enrichers.Demystifier" Version="1.0.3" />
    <PackageVersion Include="Serilog.Expressions" Version="5.0.0" />
    <PackageVersion Include="Serilog.Sinks.File" Version="7.0.0" />
    <PackageVersion Include="Serilog.Sinks.Console" Version="6.1.1" />
    <PackageVersion Include="Serilog.Extensions.Hosting" Version="10.0.0" />
    <PackageVersion Include="Serilog.Extensions.Logging" Version="10.0.0" />
  </ItemGroup>
  <!-- `Nuke` -->
  <ItemGroup>
    <PackageVersion Include="Nuke.Common" Version="10.1.0" />
  </ItemGroup>
  <!-- `PostSharp` -->
  <ItemGroup>
    <PackageVersion Include="PostSharp" Version="2026.0.6" />
    <PackageVersion Include="PostSharp.Patterns.Diagnostics" Version="2026.0.6" />
  </ItemGroup>
  <!-- `Appium.WebDriver` -->
  <ItemGroup>
    <PackageVersion Include="Appium.WebDriver" Version="8.1.0" />
    <PackageVersion Include="Selenium.WebDriver" Version="4.36.0" />
  </ItemGroup>
  <!-- `Polly` -->
  <ItemGroup>
    <PackageVersion Include="Polly" Version="8.6.6" />
    <PackageVersion Include="Polly.Core" Version="8.6.6" />
    <PackageVersion Include="Polly.RateLimiting" Version="8.6.6" />
    <PackageVersion Include="Polly.Extensions" Version="8.6.6" />
  </ItemGroup>
  <!-- other packages -->
  <ItemGroup>
    <PackageVersion Include="MiniExcel" Version="2.0.0-preview.2" />
    <PackageVersion Include="QRCoder" Version="1.7.0" />
    <PackageVersion Include="Otp.NET" Version="1.4.1" />
    <PackageVersion Include="Spectre.Console" Version="0.54.0" />
    <PackageVersion Include="MediatR" Version="14.0.0" />
  </ItemGroup>
  <!-- about command line (CLI)  -->
  <ItemGroup>
    <PackageVersion Include="CliWrap" Version="3.10.1" />
  </ItemGroup>
   <!-- about encoding -->
  <ItemGroup>
    <PackageVersion Include="System.Text.Encoding.CodePages" Version="10.0.7" />
  </ItemGroup>
  <!-- about sanitization -->
  <ItemGroup>
    <PackageVersion Include="System.Text.Encodings.Web" Version="10.0.7" />
  </ItemGroup>
  <!-- about file system globbing -->
  <ItemGroup>
    <PackageVersion Include="Microsoft.Extensions.FileSystemGlobbing" Version="10.0.7" />
  </ItemGroup>
  <!-- F# compiler and F# core library -->
  <ItemGroup>
    <PackageVersion Include="FSharp.Compiler.Service" Version="43.12.203" />
    <PackageVersion Include="FSharp.Core" Version="10.1.203" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="Dumpify" Version="0.7.0" />
  </ItemGroup>
  <!-- About AhoCorasick algorithm -->
  <ItemGroup>
    <PackageVersion Include="NReco.Text.AhoCorasickDoubleArrayTrie" Version="1.1.1" />
  </ItemGroup>
  <!-- My developed NuGet packages -->
  <!-- -->
  <ItemGroup>
    <PackageVersion Include="AiUtility.AiBaseUtilityServices" Version="[3.0.0-preview-1.0.0]" />
    <PackageVersion Include="AiUtility.ToolKits" Version="[3.0.0-preview-1.0.0]" />
    <PackageVersion Include="AiUtility.GeminiKits" Version="[3.0.0-preview-1.0.0]" />
    <PackageVersion Include="AiUtility.GeminiUtilityServices" Version="[3.0.0-preview-1.0.0]" />
  </ItemGroup>
  <!-- My developed NuGet packages -->
  <!-- Utility services about `enum` -->
  <ItemGroup>
    <PackageVersion Include="EnumUtilityServices" Version="2.0.0" />
  </ItemGroup>
  <!-- my custom Attributes in data annotations that are commonly used -->
  <ItemGroup>
    <PackageVersion Include="CustomDataAnnotations" Version="2.4.0" />
  </ItemGroup>
  <!-- About type (System.Type) -->
  <ItemGroup>
    <PackageVersion Include="TypeConstants" Version="2.1.0" />
    <PackageVersion Include="MimeTypeConstants" Version="3.0.0" />
  </ItemGroup>
  <!-- Extension services that extends existing Exception (or its subclass) and defined my customized exceptions -->
  <ItemGroup>
    <PackageVersion Include="ExceptionsUtilityServices" Version="1.1.0" />
  </ItemGroup>
  <!-- My developed NuGet packages -->
  <!-- About Expression Tree-->
  <ItemGroup>
    <PackageVersion Include="ExpressionTreeUtilityServices" Version="2.0.0" />
  </ItemGroup>
  <!-- About caching Reflection-->
  <ItemGroup>
    <PackageVersion Include="ReflectionUtilityServices" Version="2.0.0" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="LogNameUtilityFactories" Version="[2.0.0-preview-1.0.0]" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="RegexUtilityServices" Version="2.0.0" />
    <PackageVersion Include="AssemblyUtilityServices" Version="[2.0.0-preview-1.0.0]" />
    <PackageVersion Include="ExceptionHandlingUtilityServices" Version="[3.0.0-preview-1.0.0]" />
    <PackageVersion Include="ExceptionFactories" Version="2.0.0" />
    <PackageVersion Include="ExceptionWrappers" Version="[3.0.0-preview-1.0.0]" />
    <PackageVersion Include="FileStreamUtilityServices" Version="2.1.0" />
    <PackageVersion Include="JsonUtilityServices" Version="2.0.0" />
    <PackageVersion Include="LoggerFactoryUtilityServices" Version="[5.0.0-preview-1.0.0]" />
    <PackageVersion Include="TaskUtilityServices" Version="2.0.0" />
    <PackageVersion Include="ThreadLevelLockingUtilityServices" Version="[2.0.0-preview-1.0.0]" />
    <PackageVersion Include="TypeUtilityServices" Version="2.0.0" />
    <PackageVersion Include="SerilogHelperServices" Version="2.0.0" />
    <PackageVersion Include="CoordinateUtilityServices" Version="2.0.0" />
    <PackageVersion Include="TransversalUtilityServices" Version="[2.0.0-preview-1.0.0]" />
    <PackageVersion Include="AzureUtilityServices" Version="[2.0.0-preview-1.0.0]" />
    <PackageVersion Include="ListItemUitilityServices" Version="2.0.0" />
    <PackageVersion Include="MathUtilityServices" Version="2.0.0" />
    <PackageVersion Include="StringUtilityServices" Version="2.0.0" />
    <PackageVersion Include="NetRuntimeUtilityServices" Version="[2.0.0-preview-1.0.0]" />
    <PackageVersion Include="ILoggerBuilderFactoryServices" Version="[2.0.0-preview-1.0.0]" />
    <PackageVersion Include="LoggingCodeTemplateGenerators" Version="2.0.0" />
    <PackageVersion Include="IOOperationUtilityServices" Version="[5.0.0-preview-1.0.0]" />
    <PackageVersion Include="EntriesAndTheirContentsServices" Version="[1.0.0-preview-1.0.0]" />
    <PackageVersion Include="SortingUtilityServices" Version="1.0.1" />
    <PackageVersion Include="CommonModels" Version="2.1.0" />
    <PackageVersion Include="ConsoleUtilityServices" Version="1.0.0" />
    <PackageVersion Include="FileCategorizationUtilityServices" Version="2.0.0" />
  </ItemGroup>
  <ItemGroup>
    <!-- Get or check OS Environment-->
    <PackageVersion Include="EnvironmentUtilityServices" Version="3.0.0" />
    <!-- Get or check specific drive -->
    <PackageVersion Include="DriveInfoUtilityServices" Version="1.0.0-preview-1.0.0" />
    <!-- Utility services about symbolic links (based on low-level API)-->
    <PackageVersion Include="SymbolicLinkUtilityServices" Version="1.0.0-preview-1.0.0" />
  </ItemGroup>
  <!-- About cli -->
  <ItemGroup>
      <!-- wrapper or utility service based on `CliWrap` package -->
      <PackageVersion Include="CliUtilityServices" Version="[4.0.0-preview-1.0.0]" />
  </ItemGroup>
  <ItemGroup>
    <PackageVersion Include="ProjectsVersionUtilityServices" Version="1.0.0" />
    <PackageVersion Include="SolutionUtilityServices" Version="[5.0.0-preview-1.0.0]" />
    <PackageVersion Include="PaketUtilityServices" Version="1.0.0" />
  </ItemGroup>
  <!-- check which programming languages are used -->
  <ItemGroup>
    <PackageVersion Include="ProgrammingLanguageUtilityServices" Version="1.0.0" />
  </ItemGroup>
  <!-- discovering script -->
  <ItemGroup>
    <PackageVersion Include="ScriptDiscoveryUtilityServices" Version="1.0.0" />
  </ItemGroup>
  <!-- Utility services that are shared (or commonly used) -->
  <ItemGroup>
    <PackageVersion Include="SharedUtilityServices" Version="1.0.0" />
  </ItemGroup>
  <!-- About Span or its related classes -->
  <ItemGroup>
    <PackageVersion Include="SpanUtilityServices" Version="[3.0.0-preview-1.0.0]" />
  </ItemGroup>
  <!-- Utility services that wraps searching file (similar to behavior of File Explorer) -->
  <ItemGroup>
    <PackageVersion Include="FileExplorerUtilityServices" Version="[1.0.0-preview-1.0.0]" />
  </ItemGroup>
  <!-- Wrapper of decimal to fraction (my custom defined class named `Fraction`) -->
  <ItemGroup>
    <PackageVersion Include="FractionsUtilityServices" Version="1.0.0" />
  </ItemGroup>
  <!-- Utility services that move one or more apps in Windows -->
  <ItemGroup>
    <PackageVersion Include="WindowsAppUtilityServices" Version="[4.0.0-preview-1.0.0]" />
  </ItemGroup>
</Project>
````

## File: DummyProject.csproj
````
<Project Sdk='Microsoft.NET.Sdk'>
            <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
            </PropertyGroup>
        </Project>
````

## File: Examples/Ex1/Ex1-1/Ex1-1.slnx
````
<Solution>
  <Project Path="MeProject1/MeProject1.csproj" />
  <Project Path="MeProject2/MeProject2.csproj" />
  <Project Path="ReflectionUtilityServices/ReflectionUtilityServices.csproj" />
</Solution>
````

## File: Examples/Ex1/Ex1-1/MeProject1/EntryPoint.cs
````csharp
namespace MeProject1;

public static class EntryPoint
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    }
}
````

## File: Examples/Ex1/Ex1-1/MeProject1/EntryPoint1.cs
````csharp
namespace MeProject1;

public static class EntryPoint1
{
    public static void Main(string[] args)
    {
        
        Me me1 = new() { Password = "MyPassword" };
        me1.Name = "Me1";

        Son son1 = new();
    }
}
````

## File: Examples/Ex1/Ex1-1/MeProject1/Ex1-1_MeProject1-msbuild-restore.txt
````
Build started 6/26/2026 10:12:19 AM.
Logging verbosity is set to: Detailed.     0>Process = "D:\DevTools\dotnet\dotnet.exe"
       MSBuild executable path = "D:\DevTools\dotnet\sdk\10.0.103\MSBuild.dll"
       Command line arguments = "D:\DevTools\dotnet\sdk\10.0.103\MSBuild.dll -maxcpucount --verbosity:m -tlp:default=auto --property:NuGetInteractive=true --target:Restore --verbosity:minimal -fl -flp:LogFile=Ex1-1_MeProject1-msbuild-restore.txt;verbosity=detailed;Append=false -distributedlogger:Microsoft.DotNet.Cli.Commands.MSBuild.MSBuildLogger,D:\DevTools\dotnet\sdk\10.0.103\dotnet.dll*Microsoft.DotNet.Cli.Commands.MSBuild.MSBuildForwardingLogger,D:\DevTools\dotnet\sdk\10.0.103\dotnet.dll -nologo"
       Current directory = "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1"
       MSBuild version = "18.0.11+c2435c3e0"
       Based on the Windows registry key LongPathsEnabled, the LongPaths feature is enabled.
       Based on the Windows registry key VerifiedAndReputablePolicyState, SAC state = 0: turned off.
       The SDK "Microsoft.NET.Sdk" was successfully resolved by the "DefaultSdkResolver" resolver to location "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk" and version "".
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.props(49,11): message : Property 'MSBuildExtensionsPath' with value 'D:\DevTools\dotnet\sdk\10.0.103\' expanded from the environment.
       D:\workspace\LearningNotes\CSharp\Directory.Build.props(94,5): message : Property reassignment: $(IsPackable)="false" (previous value: "true") at D:\workspace\LearningNotes\CSharp\Directory.Build.props (94,5)
       D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props(60,5): message : Property reassignment: $(MSBuildProjectExtensionsPath)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\" (previous value: "obj\") at D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props (60,5)
       D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props(84,119): message : Property 'MSBuildUserExtensionsPath' with value 'C:\Users\userJay30\AppData\Local\Microsoft\MSBuild' expanded from the environment.
       Assembly loaded during Evaluation: System.Threading.Tasks.Parallel, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Threading.Tasks.Parallel.dll, MVID: 484bafc2-9f0e-4e62-8af6-e65d5cd71f84, AssemblyLoadContext: Default)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(90,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem}" (previous value: "{CandidateAssemblyFiles}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (90,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(91,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory}" (previous value: "{CandidateAssemblyFiles};{HintPathFromItem}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (91,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(92,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory};{RawFileName}" (previous value: "{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (92,5)
       The "DefaultSdkResolver" resolver attempted to resolve the SDK "Microsoft.NET.SDK.WorkloadAutoImportPropsLocator".
       Warnings: null
       Errors: MSB4276: The default SDK resolver failed to resolve SDK "Microsoft.NET.SDK.WorkloadAutoImportPropsLocator" because directory "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.SDK.WorkloadAutoImportPropsLocator\Sdk" did not exist.
       Assembly loaded during Evaluation: Microsoft.Build.NuGetSdkResolver, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Build.NuGetSdkResolver.dll, MVID: db6aefef-493b-4de6-a593-ae519e8edd6f, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Build.NuGetSdkResolver.dll)
       Assembly loaded during Evaluation: System.Reflection.Metadata, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Reflection.Metadata.dll, MVID: 27047d9b-17f3-4ec1-8143-6797ef25cd28, AssemblyLoadContext: Default)
       Assembly loaded during Evaluation: NuGet.Common, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Common.dll, MVID: 9a75e9a2-ebcf-4c20-863c-86b8d21a910c, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Build.NuGetSdkResolver.dll)
       Assembly loaded during Evaluation: Microsoft.NET.Sdk.WorkloadMSBuildSdkResolver, Version=10.0.103.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\Microsoft.NET.Sdk.WorkloadMSBuildSdkResolver.dll, MVID: 32d497b5-3f90-4858-91ed-bef3ae025be9, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\Microsoft.NET.Sdk.WorkloadMSBuildSdkResolver.dll)
       Assembly loaded during Evaluation: Microsoft.DotNet.Cli.Utils, Version=10.0.103.0, Culture=neutral, PublicKeyToken=adb9793829ddae60 (location: D:\DevTools\dotnet\sdk\10.0.103\Microsoft.DotNet.Cli.Utils.dll, MVID: 0c51fd09-7ce3-414f-b142-ef79f42ae85b, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\Microsoft.NET.Sdk.WorkloadMSBuildSdkResolver.dll)
       Assembly loaded during Evaluation: Microsoft.NET.Sdk.WorkloadManifestReader, Version=10.0.103.0, Culture=neutral, PublicKeyToken=adb9793829ddae60 (location: D:\DevTools\dotnet\sdk\10.0.103\Microsoft.NET.Sdk.WorkloadManifestReader.dll, MVID: 4fc5b50b-0d78-444c-9b7f-2f8a62d0eac5, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\Microsoft.NET.Sdk.WorkloadMSBuildSdkResolver.dll)
       Assembly loaded during Evaluation: Microsoft.Deployment.DotNet.Releases, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Deployment.DotNet.Releases.dll, MVID: ea43f8b8-b27f-4976-9d85-2b4f42217a94, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\Microsoft.NET.Sdk.WorkloadMSBuildSdkResolver.dll)
       The SDK "Microsoft.NET.SDK.WorkloadAutoImportPropsLocator" was successfully resolved by the "Microsoft.DotNet.MSBuildWorkloadSdkResolver" resolver to location "null" and version "null".
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.props(18,5): message : Property reassignment: $(WarningsAsErrors)=";NU1008;NU1605;NU1605" (previous value: ";NU1008;NU1605") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.props (18,5)
       D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj(9,5): message : Property reassignment: $(ImplicitUsings)="enable" (previous value: "disable") at D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj (9,5)
       D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj(10,5): message : Property reassignment: $(Nullable)="enable" (previous value: "disable") at D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj (10,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportPublishProfile.targets(28,5): message : Property reassignment: $(PublishProfileImported)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportPublishProfile.targets (28,5)
       Assembly loaded during Evaluation: NuGet.Frameworks, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Frameworks.dll, MVID: 1b7b49ca-9945-4cfa-824f-5d73c248e8d6, AssemblyLoadContext: Assembly.LoadFile(D:\DevTools\dotnet\sdk\10.0.103\NuGet.Frameworks.dll))
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets(81,5): message : Property reassignment: $(TargetPlatformVersion)="" (previous value: "0.0") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets (81,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(127,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**" (previous value: ";bin\Debug\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (127,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(142,5): message : Property reassignment: $(OutputPath)="bin\Debug\net10.0\" (previous value: "bin\Debug\") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (142,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(147,5): message : Property reassignment: $(IntermediateOutputPath)="obj\Debug\net10.0\" (previous value: "obj\Debug\") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (147,5)
       The "DefaultSdkResolver" resolver attempted to resolve the SDK "Microsoft.NET.SDK.WorkloadManifestTargetsLocator".
       Warnings: null
       Errors: MSB4276: The default SDK resolver failed to resolve SDK "Microsoft.NET.SDK.WorkloadManifestTargetsLocator" because directory "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.SDK.WorkloadManifestTargetsLocator\Sdk" did not exist.
       The SDK "Microsoft.NET.SDK.WorkloadManifestTargetsLocator" was successfully resolved by the "Microsoft.DotNet.MSBuildWorkloadSdkResolver" resolver to location "D:\DevTools\dotnet\sdk-manifests\10.0.100\microsoft.net.sdk.android\36.1.2" and version "null".
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.BeforeCommon.targets(371,5): message : Property reassignment: $(RoslynTargetsPath)="D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\..\..\Roslyn" (previous value: "D:\DevTools\dotnet\sdk\10.0.103\Roslyn") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.BeforeCommon.targets (371,5)
       D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.Managed.Core.targets(6,5): message : Property reassignment: $(_BuildTasksDirectory)="D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\..\..\Roslyn\" (previous value: "D:\DevTools\dotnet\sdk\10.0.103\Roslyn\") at D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.Managed.Core.targets (6,5)
       D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.CSharp.Core.targets(200,5): message : Property reassignment: $(CommandLineArgsForDesignTimeEvaluation)="-langversion:14.0 -define:TRACE" (previous value: "-langversion:14.0") at D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.CSharp.Core.targets (200,5)
       Assembly loaded during Evaluation: Microsoft.Build.Utilities.Core, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Build.Utilities.Core.dll, MVID: f1451a5e-5f7f-4573-9f12-a7d129e87106, AssemblyLoadContext: Default)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(175,5): message : Property reassignment: $(_DebugSymbolsProduced)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (175,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(185,5): message : Property reassignment: $(_DocumentationFileProduced)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (185,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(393,5): message : Property reassignment: $(MSBuildCopyMarkerName)="MeProject1.csproj.Up2Date" (previous value: "MeProject1.csproj") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (393,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(541,5): message : Property reassignment: $(ProcessorArchitecture)="msil" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (541,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(580,5): message : Property reassignment: $(DelaySign)="" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (580,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(2606,5): message : Property 'LOCALAPPDATA' with value 'C:\Users\userJay30\AppData\Local' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(3934,5): message : Property reassignment: $(_SGenGenerateSerializationAssembliesConfig)="Auto" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (3934,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(3935,5): message : Property reassignment: $(_SGenGenerateSerializationAssembliesConfig)="Off" (previous value: "Auto") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (3935,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message : Property reassignment: $(_TargetsThatPrepareProjectReferences)="
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :       AssignProjectConfiguration;
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :       _SplitProjectReferencesByFileExistence
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :     " (previous value: "_SplitProjectReferencesByFileExistence") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (5146,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(6959,5): message : Property 'MSBuildExtensionsPath32' with value 'D:\DevTools\dotnet\sdk\10.0.103' expanded from the environment.
       The "NuGetInteractive" property is a global property, and cannot be modified.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(24,5): message : Property reassignment: $(ProjectAssetsFile)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\project.assets.json" (previous value: "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\/project.assets.json") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (24,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(29,5): message : Property reassignment: $(ProjectAssetsCacheFile)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.assets.cache" (previous value: "obj\Debug\net10.0\MeProject1.assets.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (29,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message : Property reassignment: $(ResolveAssemblyReferencesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (80,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message : Property reassignment: $(PrepareResourcesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       PrepareResourceNames;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResGen;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       CompileLicxFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       PrepareResourceNames;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResGen;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       CompileLicxFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (86,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(35,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**" (previous value: ";bin\Debug\/**;obj\Debug\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (35,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(37,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (37,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(40,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (40,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(41,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (41,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(42,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (42,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(43,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (43,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(44,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (44,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(45,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc;**/.DS_Store" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (45,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message : Property reassignment: $(ResolveAssemblyReferencesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveTargetingPackAssets;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets (14,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(44,5): message : Property reassignment: $(_GenerateRuntimeConfigurationPropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.genruntimeconfig.cache" (previous value: "obj\Debug\net10.0\MeProject1.genruntimeconfig.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (44,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(46,5): message : Property reassignment: $(_GeneratePublishDependencyFilePropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.genpublishdeps.cache" (previous value: "obj\Debug\net10.0\MeProject1.genpublishdeps.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (46,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(48,5): message : Property reassignment: $(_GenerateSingleFileBundlePropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.genbundle.cache" (previous value: "obj\Debug\net10.0\MeProject1.genbundle.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (48,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(81,5): message : Property reassignment: $(GenerateRuntimeConfigDevFile)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (81,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(98,5): message : Property 'USERPROFILE' with value 'C:\Users\userJay30' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(98,5): message : Property reassignment: $(_DefaultUserProfileRuntimeStorePath)="C:\Users\userJay30" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (98,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(99,5): message : Property reassignment: $(_DefaultUserProfileRuntimeStorePath)="C:\Users\userJay30\.dotnet\store" (previous value: "C:\Users\userJay30") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (99,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(144,5): message : Property reassignment: $(AvailablePlatforms)="Any CPU,x86,x64,ARM32" (previous value: "Any CPU,x86,x64") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (144,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(148,5): message : Property reassignment: $(AvailablePlatforms)="Any CPU,x86,x64,ARM32,ARM64" (previous value: "Any CPU,x86,x64,ARM32") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (148,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message : Property reassignment: $(CoreBuildDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       _CheckForBuildWithNoBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PreBuildEvent;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareResources;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       Compile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ExportWindowsMDFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedUnregistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateSerializationAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       CreateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateManifests;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GetTargetPath;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForRun;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedRegistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       IncrementalClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PostBuildEvent
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateBuildDependencyFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateBuildRuntimeConfigurationFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PreBuildEvent;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareResources;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       Compile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ExportWindowsMDFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedUnregistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateSerializationAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       CreateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateManifests;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GetTargetPath;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForRun;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedRegistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       IncrementalClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PostBuildEvent
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (204,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message : Property reassignment: $(CoreCleanDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :       _SdkBeforeClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :     " (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (213,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message : Property reassignment: $(RebuildDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       _SdkBeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       BeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Clean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Build;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       AfterRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       BeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Clean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Build;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       AfterRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (220,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message : Property reassignment: $(CompileDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       SetWin32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _SetPreferNativeArm64Win32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       FindReferenceAssembliesForReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       BeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampBeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileDependencyCache;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       CoreCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampAfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       AfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _CreateAppHost;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _CreateComHost;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GetIjwHostPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       SetWin32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _SetPreferNativeArm64Win32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       FindReferenceAssembliesForReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       BeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampBeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileDependencyCache;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       CoreCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampAfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       AfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (799,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message : Property reassignment: $(CreateSatelliteAssembliesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       _GenerateSatelliteAssemblyInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ComputeIntermediateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       GenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       CoreGenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       _GenerateSatelliteAssemblyInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ComputeIntermediateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       GenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (1131,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl;_InitializeBitbucketGitSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation;TranslateBitbucketGitUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message : Property reassignment: $(DesignerRuntimeImplementationProjectOutputGroupDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       AssignTargetPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ResolveReferences
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GenerateDesignerDepsFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GenerateDesignerRuntimeConfigFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       GetCopyToOutputDirectoryItems;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GatherDesignerShadowCopyFiles;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       AssignTargetPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ResolveReferences
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets (21,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets(31,5): message : Property reassignment: $(DefineConstants)="TRACE;DEBUG" (previous value: "TRACE") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets (31,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets(45,5): message : Property reassignment: $(WarningsAsErrors)=";NU1008;NU1605;NU1605;SYSLIB0011" (previous value: ";NU1008;NU1605;NU1605") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets (45,5)
       D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.Pack.targets(59,5): message : Property reassignment: $(GenerateNuspecDependsOn)="Build;_LoadPackInputItems; _GetTargetFrameworksOutput; _WalkEachTargetPerFramework; _GetPackageFiles; " (previous value: "_LoadPackInputItems; _GetTargetFrameworksOutput; _WalkEachTargetPerFramework; _GetPackageFiles; ") at D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.Pack.targets (59,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.targets(59,5): message : Property reassignment: $(_IsNotSetContainersTargetsDir)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.targets (59,5)
       D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets(5,5): message : Property reassignment: $(_IsSDKContainerAllowedVersion)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets (5,5)
       D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets(15,5): message : Property reassignment: $(_ContainerIsTargetingNet8TFM)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets (15,5)
       Overriding target "ResolveFrameworkReferences" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "ResolveFrameworkReferences" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets".
       Overriding target "GenerateSupportedTargetFrameworkAlias" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GenerateSupportedTargetFrameworkAlias" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets".
       Overriding target "GetReferenceAssemblyPaths" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetReferenceAssemblyPaths" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DisableStandardFrameworkResolution.targets".
       Overriding target "GetFrameworkPaths" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetFrameworkPaths" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DisableStandardFrameworkResolution.targets".
       Overriding target "DesignerRuntimeImplementationProjectOutputGroup" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "DesignerRuntimeImplementationProjectOutputGroup" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets".
       Overriding target "Publish" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "Publish" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "ResolveReadyToRunCompilers" in project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.CrossGen.targets" with target "ResolveReadyToRunCompilers" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "GetCopyToPublishDirectoryItems" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetCopyToPublishDirectoryItems" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "Run" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "Run" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets".
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" on node 1 (Restore target(s)).
     1>Target "_LoadRestoreGraphEntryPoints" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_FilterRestoreGraphProjectInputItems" depends on it):
     1>Done building target "_LoadRestoreGraphEntryPoints" in project "MeProject1.csproj".
     1>Target "_FilterRestoreGraphProjectInputItems" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreGraph" depends on it):
       Using "RemoveDuplicates" task from assembly "Microsoft.Build.Tasks.Core, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a".
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
       Using "MSBuild" task from assembly "Microsoft.Build.Tasks.Core, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a".
       Task "MSBuild"
         Global Properties:
           ExcludeRestorePackageImports=true
     0>D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.props(49,11): message : Property 'MSBuildExtensionsPath' with value 'D:\DevTools\dotnet\sdk\10.0.103\' expanded from the environment.
       D:\workspace\LearningNotes\CSharp\Directory.Build.props(94,5): message : Property reassignment: $(IsPackable)="false" (previous value: "true") at D:\workspace\LearningNotes\CSharp\Directory.Build.props (94,5)
       D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props(60,5): message : Property reassignment: $(MSBuildProjectExtensionsPath)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\" (previous value: "obj\") at D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props (60,5)
       D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props(84,119): message : Property 'MSBuildUserExtensionsPath' with value 'C:\Users\userJay30\AppData\Local\Microsoft\MSBuild' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(90,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem}" (previous value: "{CandidateAssemblyFiles}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (90,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(91,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory}" (previous value: "{CandidateAssemblyFiles};{HintPathFromItem}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (91,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(92,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory};{RawFileName}" (previous value: "{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (92,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.props(18,5): message : Property reassignment: $(WarningsAsErrors)=";NU1008;NU1605;NU1605" (previous value: ";NU1008;NU1605") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.props (18,5)
       D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj(9,5): message : Property reassignment: $(ImplicitUsings)="enable" (previous value: "disable") at D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj (9,5)
       D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj(10,5): message : Property reassignment: $(Nullable)="enable" (previous value: "disable") at D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj (10,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportPublishProfile.targets(28,5): message : Property reassignment: $(PublishProfileImported)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportPublishProfile.targets (28,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets(81,5): message : Property reassignment: $(TargetPlatformVersion)="" (previous value: "0.0") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets (81,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(127,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**" (previous value: ";bin\Debug\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (127,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(142,5): message : Property reassignment: $(OutputPath)="bin\Debug\net10.0\" (previous value: "bin\Debug\") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (142,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(147,5): message : Property reassignment: $(IntermediateOutputPath)="obj\Debug\net10.0\" (previous value: "obj\Debug\") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (147,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.BeforeCommon.targets(371,5): message : Property reassignment: $(RoslynTargetsPath)="D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\..\..\Roslyn" (previous value: "D:\DevTools\dotnet\sdk\10.0.103\Roslyn") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.BeforeCommon.targets (371,5)
       D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.Managed.Core.targets(6,5): message : Property reassignment: $(_BuildTasksDirectory)="D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\..\..\Roslyn\" (previous value: "D:\DevTools\dotnet\sdk\10.0.103\Roslyn\") at D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.Managed.Core.targets (6,5)
       D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.CSharp.Core.targets(200,5): message : Property reassignment: $(CommandLineArgsForDesignTimeEvaluation)="-langversion:14.0 -define:TRACE" (previous value: "-langversion:14.0") at D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.CSharp.Core.targets (200,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(175,5): message : Property reassignment: $(_DebugSymbolsProduced)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (175,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(185,5): message : Property reassignment: $(_DocumentationFileProduced)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (185,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(393,5): message : Property reassignment: $(MSBuildCopyMarkerName)="MeProject1.csproj.Up2Date" (previous value: "MeProject1.csproj") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (393,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(541,5): message : Property reassignment: $(ProcessorArchitecture)="msil" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (541,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(580,5): message : Property reassignment: $(DelaySign)="" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (580,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(2606,5): message : Property 'LOCALAPPDATA' with value 'C:\Users\userJay30\AppData\Local' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(3934,5): message : Property reassignment: $(_SGenGenerateSerializationAssembliesConfig)="Auto" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (3934,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(3935,5): message : Property reassignment: $(_SGenGenerateSerializationAssembliesConfig)="Off" (previous value: "Auto") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (3935,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message : Property reassignment: $(_TargetsThatPrepareProjectReferences)="
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :       AssignProjectConfiguration;
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :       _SplitProjectReferencesByFileExistence
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :     " (previous value: "_SplitProjectReferencesByFileExistence") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (5146,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(6959,5): message : Property 'MSBuildExtensionsPath32' with value 'D:\DevTools\dotnet\sdk\10.0.103' expanded from the environment.
       The "NuGetInteractive" property is a global property, and cannot be modified.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(24,5): message : Property reassignment: $(ProjectAssetsFile)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\project.assets.json" (previous value: "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\/project.assets.json") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (24,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(29,5): message : Property reassignment: $(ProjectAssetsCacheFile)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.assets.cache" (previous value: "obj\Debug\net10.0\MeProject1.assets.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (29,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message : Property reassignment: $(ResolveAssemblyReferencesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (80,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message : Property reassignment: $(PrepareResourcesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       PrepareResourceNames;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResGen;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       CompileLicxFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       PrepareResourceNames;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResGen;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       CompileLicxFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (86,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(35,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**" (previous value: ";bin\Debug\/**;obj\Debug\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (35,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(37,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (37,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(40,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (40,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(41,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (41,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(42,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (42,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(43,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (43,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(44,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (44,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(45,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc;**/.DS_Store" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (45,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message : Property reassignment: $(ResolveAssemblyReferencesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveTargetingPackAssets;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets (14,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(44,5): message : Property reassignment: $(_GenerateRuntimeConfigurationPropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.genruntimeconfig.cache" (previous value: "obj\Debug\net10.0\MeProject1.genruntimeconfig.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (44,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(46,5): message : Property reassignment: $(_GeneratePublishDependencyFilePropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.genpublishdeps.cache" (previous value: "obj\Debug\net10.0\MeProject1.genpublishdeps.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (46,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(48,5): message : Property reassignment: $(_GenerateSingleFileBundlePropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\Debug\net10.0\MeProject1.genbundle.cache" (previous value: "obj\Debug\net10.0\MeProject1.genbundle.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (48,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(81,5): message : Property reassignment: $(GenerateRuntimeConfigDevFile)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (81,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(98,5): message : Property 'USERPROFILE' with value 'C:\Users\userJay30' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(98,5): message : Property reassignment: $(_DefaultUserProfileRuntimeStorePath)="C:\Users\userJay30" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (98,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(99,5): message : Property reassignment: $(_DefaultUserProfileRuntimeStorePath)="C:\Users\userJay30\.dotnet\store" (previous value: "C:\Users\userJay30") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (99,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(144,5): message : Property reassignment: $(AvailablePlatforms)="Any CPU,x86,x64,ARM32" (previous value: "Any CPU,x86,x64") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (144,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(148,5): message : Property reassignment: $(AvailablePlatforms)="Any CPU,x86,x64,ARM32,ARM64" (previous value: "Any CPU,x86,x64,ARM32") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (148,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message : Property reassignment: $(CoreBuildDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       _CheckForBuildWithNoBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PreBuildEvent;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareResources;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       Compile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ExportWindowsMDFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedUnregistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateSerializationAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       CreateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateManifests;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GetTargetPath;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForRun;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedRegistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       IncrementalClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PostBuildEvent
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateBuildDependencyFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateBuildRuntimeConfigurationFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PreBuildEvent;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareResources;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       Compile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ExportWindowsMDFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedUnregistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateSerializationAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       CreateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateManifests;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GetTargetPath;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForRun;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedRegistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       IncrementalClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PostBuildEvent
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (204,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message : Property reassignment: $(CoreCleanDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :       _SdkBeforeClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :     " (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (213,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message : Property reassignment: $(RebuildDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       _SdkBeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       BeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Clean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Build;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       AfterRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       BeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Clean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Build;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       AfterRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (220,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message : Property reassignment: $(CompileDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       SetWin32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _SetPreferNativeArm64Win32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       FindReferenceAssembliesForReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       BeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampBeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileDependencyCache;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       CoreCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampAfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       AfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _CreateAppHost;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _CreateComHost;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GetIjwHostPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       SetWin32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _SetPreferNativeArm64Win32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       FindReferenceAssembliesForReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       BeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampBeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileDependencyCache;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       CoreCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampAfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       AfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (799,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message : Property reassignment: $(CreateSatelliteAssembliesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       _GenerateSatelliteAssemblyInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ComputeIntermediateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       GenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       CoreGenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       _GenerateSatelliteAssemblyInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ComputeIntermediateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       GenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (1131,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl;_InitializeBitbucketGitSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation;TranslateBitbucketGitUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message : Property reassignment: $(DesignerRuntimeImplementationProjectOutputGroupDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       AssignTargetPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ResolveReferences
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GenerateDesignerDepsFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GenerateDesignerRuntimeConfigFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       GetCopyToOutputDirectoryItems;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GatherDesignerShadowCopyFiles;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       AssignTargetPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ResolveReferences
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets (21,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets(31,5): message : Property reassignment: $(DefineConstants)="TRACE;DEBUG" (previous value: "TRACE") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets (31,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets(45,5): message : Property reassignment: $(WarningsAsErrors)=";NU1008;NU1605;NU1605;SYSLIB0011" (previous value: ";NU1008;NU1605;NU1605") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets (45,5)
       D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.Pack.targets(59,5): message : Property reassignment: $(GenerateNuspecDependsOn)="Build;_LoadPackInputItems; _GetTargetFrameworksOutput; _WalkEachTargetPerFramework; _GetPackageFiles; " (previous value: "_LoadPackInputItems; _GetTargetFrameworksOutput; _WalkEachTargetPerFramework; _GetPackageFiles; ") at D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.Pack.targets (59,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.targets(59,5): message : Property reassignment: $(_IsNotSetContainersTargetsDir)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.targets (59,5)
       D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets(5,5): message : Property reassignment: $(_IsSDKContainerAllowedVersion)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets (5,5)
       D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets(15,5): message : Property reassignment: $(_ContainerIsTargetingNet8TFM)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets (15,5)
       Overriding target "ResolveFrameworkReferences" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "ResolveFrameworkReferences" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets".
       Overriding target "GenerateSupportedTargetFrameworkAlias" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GenerateSupportedTargetFrameworkAlias" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets".
       Overriding target "GetReferenceAssemblyPaths" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetReferenceAssemblyPaths" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DisableStandardFrameworkResolution.targets".
       Overriding target "GetFrameworkPaths" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetFrameworkPaths" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DisableStandardFrameworkResolution.targets".
       Overriding target "DesignerRuntimeImplementationProjectOutputGroup" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "DesignerRuntimeImplementationProjectOutputGroup" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets".
       Overriding target "Publish" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "Publish" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "ResolveReadyToRunCompilers" in project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.CrossGen.targets" with target "ResolveReadyToRunCompilers" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "GetCopyToPublishDirectoryItems" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetCopyToPublishDirectoryItems" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "Run" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "Run" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets".
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1:2) on node 1 (_IsProjectRestoreSupported target(s)).
   1:2>Target "_IsProjectRestoreSupported" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (entry point):
   1:2>Done building target "_IsProjectRestoreSupported" in project "MeProject1.csproj".
   1:2>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (_IsProjectRestoreSupported target(s)).
     1>Done executing task "MSBuild".
       Assembly loaded during TaskRun: NuGet.Build.Tasks, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll, MVID: c55bf910-9abe-4187-805d-dad3881bc30c, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
       Assembly loaded during TaskRun: NuGet.Common, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Common.dll, MVID: 9a75e9a2-ebcf-4c20-863c-86b8d21a910c, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
       Assembly loaded during TaskRun: NuGet.Commands, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Commands.dll, MVID: 678affb3-c99b-4423-9cb9-5b8405af771f, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
       Assembly loaded during TaskRun: NuGet.ProjectModel, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.ProjectModel.dll, MVID: 186e02d1-69bd-4fe8-b063-002229001751, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
       Using "WarnForInvalidProjectsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "WarnForInvalidProjectsTask"
       Done executing task "WarnForInvalidProjectsTask".
     1>Done building target "_FilterRestoreGraphProjectInputItems" in project "MeProject1.csproj".
       Target "_FilterRestoreGraphProjectInputItems" skipped. Previously built successfully.
     1>Target "_GetAllRestoreProjectPathItems" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreGraph" depends on it):
       Using "NuGetMessageTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "NuGetMessageTask"
         Determining projects to restore...
       Done executing task "NuGetMessageTask".
       Task "MSBuild"
         Global Properties:
           ExcludeRestorePackageImports=true
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1:3) on node 1 (_GenerateRestoreProjectPathWalk target(s)).
   1:3>Target "_GenerateRestoreProjectPathItemsPerFramework" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreProjectPathItemsCurrentProject" depends on it):
   1:3>Using "ConvertToAbsolutePath" task from assembly "Microsoft.Build.Tasks.Core, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a".
       Task "ConvertToAbsolutePath"
       Done executing task "ConvertToAbsolutePath".
   1:3>Done building target "_GenerateRestoreProjectPathItemsPerFramework" in project "MeProject1.csproj".
   1:3>Target "_GenerateRestoreProjectPathItemsCurrentProject" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreProjectPathItems" depends on it):
   1:3>Done building target "_GenerateRestoreProjectPathItemsCurrentProject" in project "MeProject1.csproj".
   1:3>Target "_GenerateRestoreProjectPathItems" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreProjectPathWalk" depends on it):
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
   1:3>Done building target "_GenerateRestoreProjectPathItems" in project "MeProject1.csproj".
   1:3>Target "_GenerateRestoreProjectPathWalk" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (entry point):
       Task "MSBuild"
         Global Properties:
           ExcludeRestorePackageImports=true
     0>D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.props(49,11): message : Property 'MSBuildExtensionsPath' with value 'D:\DevTools\dotnet\sdk\10.0.103\' expanded from the environment.
       D:\workspace\LearningNotes\CSharp\Directory.Build.props(94,5): message : Property reassignment: $(IsPackable)="false" (previous value: "true") at D:\workspace\LearningNotes\CSharp\Directory.Build.props (94,5)
       D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props(60,5): message : Property reassignment: $(MSBuildProjectExtensionsPath)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\" (previous value: "obj\") at D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props (60,5)
       D:\DevTools\dotnet\sdk\10.0.103\Current\Microsoft.Common.props(84,119): message : Property 'MSBuildUserExtensionsPath' with value 'C:\Users\userJay30\AppData\Local\Microsoft\MSBuild' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(90,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem}" (previous value: "{CandidateAssemblyFiles}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (90,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(91,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory}" (previous value: "{CandidateAssemblyFiles};{HintPathFromItem}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (91,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props(92,5): message : Property reassignment: $(AssemblySearchPaths)="{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory};{RawFileName}" (previous value: "{CandidateAssemblyFiles};{HintPathFromItem};{TargetFrameworkDirectory}") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.props (92,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.props(18,5): message : Property reassignment: $(WarningsAsErrors)=";NU1008;NU1605;NU1605" (previous value: ";NU1008;NU1605") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.props (18,5)
       D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj(4,5): message : Property reassignment: $(ImplicitUsings)="enable" (previous value: "disable") at D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj (4,5)
       D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj(5,5): message : Property reassignment: $(Nullable)="enable" (previous value: "disable") at D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj (5,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportPublishProfile.targets(28,5): message : Property reassignment: $(PublishProfileImported)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.ImportPublishProfile.targets (28,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets(81,5): message : Property reassignment: $(TargetPlatformVersion)="" (previous value: "0.0") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets (81,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(127,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**" (previous value: ";bin\Debug\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (127,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(142,5): message : Property reassignment: $(OutputPath)="bin\Debug\net10.0\" (previous value: "bin\Debug\") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (142,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets(147,5): message : Property reassignment: $(IntermediateOutputPath)="obj\Debug\net10.0\" (previous value: "obj\Debug\") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DefaultOutputPaths.targets (147,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.BeforeCommon.targets(371,5): message : Property reassignment: $(RoslynTargetsPath)="D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\..\..\Roslyn" (previous value: "D:\DevTools\dotnet\sdk\10.0.103\Roslyn") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.BeforeCommon.targets (371,5)
       D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.Managed.Core.targets(6,5): message : Property reassignment: $(_BuildTasksDirectory)="D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\..\..\Roslyn\" (previous value: "D:\DevTools\dotnet\sdk\10.0.103\Roslyn\") at D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.Managed.Core.targets (6,5)
       D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.CSharp.Core.targets(200,5): message : Property reassignment: $(CommandLineArgsForDesignTimeEvaluation)="-langversion:14.0 -define:TRACE" (previous value: "-langversion:14.0") at D:\DevTools\dotnet\sdk\10.0.103\Roslyn\Microsoft.CSharp.Core.targets (200,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(175,5): message : Property reassignment: $(_DebugSymbolsProduced)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (175,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(185,5): message : Property reassignment: $(_DocumentationFileProduced)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (185,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(392,5): message : Property reassignment: $(MSBuildCopyMarkerName)="Reflecti.025F4339" (previous value: "ReflectionUtilityServices.csproj") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (392,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(393,5): message : Property reassignment: $(MSBuildCopyMarkerName)="Reflecti.025F4339.Up2Date" (previous value: "Reflecti.025F4339") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (393,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(541,5): message : Property reassignment: $(ProcessorArchitecture)="msil" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (541,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(580,5): message : Property reassignment: $(DelaySign)="" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (580,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(2606,5): message : Property 'LOCALAPPDATA' with value 'C:\Users\userJay30\AppData\Local' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(3934,5): message : Property reassignment: $(_SGenGenerateSerializationAssembliesConfig)="Auto" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (3934,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(3935,5): message : Property reassignment: $(_SGenGenerateSerializationAssembliesConfig)="Off" (previous value: "Auto") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (3935,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message : Property reassignment: $(_TargetsThatPrepareProjectReferences)="
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :       AssignProjectConfiguration;
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :       _SplitProjectReferencesByFileExistence
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(5146,5): message :     " (previous value: "_SplitProjectReferencesByFileExistence") at D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets (5146,5)
       D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets(6959,5): message : Property 'MSBuildExtensionsPath32' with value 'D:\DevTools\dotnet\sdk\10.0.103' expanded from the environment.
       The "NuGetInteractive" property is a global property, and cannot be modified.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(24,5): message : Property reassignment: $(ProjectAssetsFile)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\project.assets.json" (previous value: "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\/project.assets.json") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (24,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(29,5): message : Property reassignment: $(ProjectAssetsCacheFile)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\Debug\net10.0\ReflectionUtilityServices.assets.cache" (previous value: "obj\Debug\net10.0\ReflectionUtilityServices.assets.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (29,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message : Property reassignment: $(ResolveAssemblyReferencesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(80,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (80,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message : Property reassignment: $(PrepareResourcesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       PrepareResourceNames;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResGen;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       CompileLicxFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       PrepareResourceNames;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       ResGen;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :       CompileLicxFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(86,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets (86,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(35,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**" (previous value: ";bin\Debug\/**;obj\Debug\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (35,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(37,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (37,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(40,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (40,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(41,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (41,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(42,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (42,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(43,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (43,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(44,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (44,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(45,5): message : Property reassignment: $(DefaultItemExcludes)=";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc;**/.DS_Store" (previous value: ";bin\Debug\/**;obj\Debug\/**;bin\/**;obj\/**;**/*.user;**/*.*proj;**/*.sln;**/*.slnx;**/*.vssscc") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets (45,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message : Property reassignment: $(ResolveAssemblyReferencesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveTargetingPackAssets;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       FindInvalidProjectReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetFrameworkPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       GetReferenceAssemblyPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolveSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ExpandSDKReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       ResolvePackageDependenciesForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :       _HandlePackageFileConflicts;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(14,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets (14,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(44,5): message : Property reassignment: $(_GenerateRuntimeConfigurationPropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\Debug\net10.0\ReflectionUtilityServices.genruntimeconfig.cache" (previous value: "obj\Debug\net10.0\ReflectionUtilityServices.genruntimeconfig.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (44,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(46,5): message : Property reassignment: $(_GeneratePublishDependencyFilePropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\Debug\net10.0\ReflectionUtilityServices.genpublishdeps.cache" (previous value: "obj\Debug\net10.0\ReflectionUtilityServices.genpublishdeps.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (46,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(48,5): message : Property reassignment: $(_GenerateSingleFileBundlePropertyInputsCache)="D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\Debug\net10.0\ReflectionUtilityServices.genbundle.cache" (previous value: "obj\Debug\net10.0\ReflectionUtilityServices.genbundle.cache") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (48,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(81,5): message : Property reassignment: $(GenerateRuntimeConfigDevFile)="false" (previous value: "true") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (81,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(98,5): message : Property 'USERPROFILE' with value 'C:\Users\userJay30' expanded from the environment.
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(98,5): message : Property reassignment: $(_DefaultUserProfileRuntimeStorePath)="C:\Users\userJay30" (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (98,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(99,5): message : Property reassignment: $(_DefaultUserProfileRuntimeStorePath)="C:\Users\userJay30\.dotnet\store" (previous value: "C:\Users\userJay30") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (99,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(144,5): message : Property reassignment: $(AvailablePlatforms)="Any CPU,x86,x64,ARM32" (previous value: "Any CPU,x86,x64") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (144,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(148,5): message : Property reassignment: $(AvailablePlatforms)="Any CPU,x86,x64,ARM32,ARM64" (previous value: "Any CPU,x86,x64,ARM32") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (148,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message : Property reassignment: $(CoreBuildDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       _CheckForBuildWithNoBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PreBuildEvent;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareResources;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       Compile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ExportWindowsMDFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedUnregistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateSerializationAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       CreateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateManifests;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GetTargetPath;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForRun;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedRegistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       IncrementalClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PostBuildEvent
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateBuildDependencyFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateBuildRuntimeConfigurationFiles
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PreBuildEvent;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareResources;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       Compile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       ExportWindowsMDFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedUnregistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateSerializationAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       CreateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GenerateManifests;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       GetTargetPath;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PrepareForRun;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       UnmanagedRegistration;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       IncrementalClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :       PostBuildEvent
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(204,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (204,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message : Property reassignment: $(CoreCleanDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :       _SdkBeforeClean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(213,5): message :     " (previous value: "") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (213,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message : Property reassignment: $(RebuildDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       _SdkBeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       BeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Clean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Build;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       AfterRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       BeforeRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Clean;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       Build;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :       AfterRebuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(220,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (220,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message : Property reassignment: $(CompileDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       SetWin32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _SetPreferNativeArm64Win32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       FindReferenceAssembliesForReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       BeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampBeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileDependencyCache;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       CoreCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampAfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       AfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _CreateAppHost;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _CreateComHost;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GetIjwHostPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       ResolveKeySource;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       SetWin32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _SetPreferNativeArm64Win32ManifestProperties;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       FindReferenceAssembliesForReferences;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       BeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampBeforeCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _GenerateCompileDependencyCache;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       CoreCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       _TimeStampAfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :       AfterCompile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(799,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (799,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message : Property reassignment: $(CreateSatelliteAssembliesDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       _GenerateSatelliteAssemblyInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ComputeIntermediateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       GenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       CoreGenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       _GenerateSatelliteAssemblyInputs;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       ComputeIntermediateSatelliteAssemblies;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :       GenerateSatelliteAssemblies
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(1131,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets (1131,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.GitLab\build\Microsoft.SourceLink.GitLab.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.AzureRepos.Git\build\Microsoft.SourceLink.AzureRepos.Git.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets(12,5): message : Property reassignment: $(SourceLinkUrlInitializerTargets)=";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl;_InitializeBitbucketGitSourceLinkUrl" (previous value: ";_InitializeGitHubSourceLinkUrl;_InitializeGitLabSourceLinkUrl;_InitializeAzureReposGitSourceLinkUrl") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets (12,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets(13,5): message : Property reassignment: $(SourceControlManagerUrlTranslationTargets)=";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation;TranslateBitbucketGitUrlsInSourceControlInformation" (previous value: ";TranslateGitHubUrlsInSourceControlInformation;TranslateGitLabUrlsInSourceControlInformation;TranslateAzureReposGitUrlsInSourceControlInformation") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.SourceLink.Bitbucket.Git\build\Microsoft.SourceLink.Bitbucket.Git.targets (13,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message : Property reassignment: $(DesignerRuntimeImplementationProjectOutputGroupDependsOn)="
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       AssignTargetPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ResolveReferences
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GenerateDesignerDepsFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GenerateDesignerRuntimeConfigFile;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       GetCopyToOutputDirectoryItems;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       _GatherDesignerShadowCopyFiles;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     " (previous value: "
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       BuildOnlySettings;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       PrepareForBuild;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       AssignTargetPaths;
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :       ResolveReferences
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets(21,5): message :     ") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets (21,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets(31,5): message : Property reassignment: $(DefineConstants)="TRACE;DEBUG" (previous value: "TRACE") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets (31,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets(45,5): message : Property reassignment: $(WarningsAsErrors)=";NU1008;NU1605;NU1605;SYSLIB0011" (previous value: ";NU1008;NU1605;NU1605") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.CSharp.targets (45,5)
       D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.Pack.targets(59,5): message : Property reassignment: $(GenerateNuspecDependsOn)="Build;_LoadPackInputItems; _GetTargetFrameworksOutput; _WalkEachTargetPerFramework; _GetPackageFiles; " (previous value: "_LoadPackInputItems; _GetTargetFrameworksOutput; _WalkEachTargetPerFramework; _GetPackageFiles; ") at D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.Pack.targets (59,5)
       D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.targets(59,5): message : Property reassignment: $(_IsNotSetContainersTargetsDir)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\Sdk\Sdk.targets (59,5)
       D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets(5,5): message : Property reassignment: $(_IsSDKContainerAllowedVersion)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets (5,5)
       D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets(15,5): message : Property reassignment: $(_ContainerIsTargetingNet8TFM)="true" (previous value: "false") at D:\DevTools\dotnet\sdk\10.0.103\Containers\build\Microsoft.NET.Build.Containers.targets (15,5)
       Overriding target "ResolveFrameworkReferences" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "ResolveFrameworkReferences" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets".
       Overriding target "GenerateSupportedTargetFrameworkAlias" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GenerateSupportedTargetFrameworkAlias" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets".
       Overriding target "GetReferenceAssemblyPaths" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetReferenceAssemblyPaths" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DisableStandardFrameworkResolution.targets".
       Overriding target "GetFrameworkPaths" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetFrameworkPaths" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DisableStandardFrameworkResolution.targets".
       Overriding target "DesignerRuntimeImplementationProjectOutputGroup" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "DesignerRuntimeImplementationProjectOutputGroup" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.DesignerSupport.targets".
       Overriding target "Publish" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "Publish" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "ResolveReadyToRunCompilers" in project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.CrossGen.targets" with target "ResolveReadyToRunCompilers" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "GetCopyToPublishDirectoryItems" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "GetCopyToPublishDirectoryItems" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets".
       Overriding target "Run" in project "D:\DevTools\dotnet\sdk\10.0.103\Microsoft.Common.CurrentVersion.targets" with target "Run" from project "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets".
   1:3>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1:3) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (2) on node 1 (_GenerateRestoreProjectPathWalk target(s)).
     2>Target "_GenerateRestoreProjectPathItemsPerFramework" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreProjectPathItemsCurrentProject" depends on it):
     2>Task "ConvertToAbsolutePath"
       Done executing task "ConvertToAbsolutePath".
     2>Done building target "_GenerateRestoreProjectPathItemsPerFramework" in project "ReflectionUtilityServices.csproj".
     2>Target "_GenerateRestoreProjectPathItemsCurrentProject" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreProjectPathItems" depends on it):
     2>Done building target "_GenerateRestoreProjectPathItemsCurrentProject" in project "ReflectionUtilityServices.csproj".
     2>Target "_GenerateRestoreProjectPathItems" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreProjectPathWalk" depends on it):
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
     2>Done building target "_GenerateRestoreProjectPathItems" in project "ReflectionUtilityServices.csproj".
     2>Target "_GenerateRestoreProjectPathWalk" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (entry point):
       Task "MSBuild"
       Done executing task "MSBuild".
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
     2>Done building target "_GenerateRestoreProjectPathWalk" in project "ReflectionUtilityServices.csproj".
     2>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (_GenerateRestoreProjectPathWalk target(s)).
   1:3>Done executing task "MSBuild".
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
   1:3>Done building target "_GenerateRestoreProjectPathWalk" in project "MeProject1.csproj".
   1:3>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (_GenerateRestoreProjectPathWalk target(s)).
     1>Done executing task "MSBuild".
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
       Task "MSBuild"
         Global Properties:
           ExcludeRestorePackageImports=true
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1:4) on node 1 (_IsProjectRestoreSupported target(s)).
   1:4>Target "_IsProjectRestoreSupported" skipped. Previously built successfully.
   1:4>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (_IsProjectRestoreSupported target(s)).
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (2:2) on node 1 (_IsProjectRestoreSupported target(s)).
   2:2>Target "_IsProjectRestoreSupported" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (entry point):
   2:2>Done building target "_IsProjectRestoreSupported" in project "ReflectionUtilityServices.csproj".
   2:2>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (_IsProjectRestoreSupported target(s)).
     1>Done executing task "MSBuild".
     1>Done building target "_GetAllRestoreProjectPathItems" in project "MeProject1.csproj".
     1>Target "_GenerateRestoreGraph" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "Restore" depends on it):
       Using "Message" task from assembly "Microsoft.Build.Tasks.Core, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a".
       Task "Message"
         Generating dg file
       Done executing task "Message".
       Task "Message"
         D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj
       Done executing task "Message".
       Task "Message"
         D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj
       Done executing task "Message".
       Task "MSBuild"
         Global Properties:
           ExcludeRestorePackageImports=true
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (2:3) on node 1 (_GenerateRestoreGraphProjectEntry target(s)).
   2:3>Target "_CheckForUnsupportedNETCoreVersion" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPackageReferences" depends on it):
   2:3>Done building target "_CheckForUnsupportedNETCoreVersion" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_CheckForUnsupportedCppNETCoreVersion" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPackageReferences" depends on it):
   2:3>Done building target "_CheckForUnsupportedCppNETCoreVersion" in project "ReflectionUtilityServices.csproj".
   2:3>Target "RemoveVersions" in file "D:\workspace\LearningNotes\CSharp\Directory.Build.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPackageReferences" depends on it):
   2:3>Done building target "RemoveVersions" in project "ReflectionUtilityServices.csproj".
   2:3>Target "CheckForImplicitPackageReferenceOverrides" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.Shared.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPackageReferences" depends on it):
       Using "CheckForImplicitPackageReferenceOverrides" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "CheckForImplicitPackageReferenceOverrides"
       Done executing task "CheckForImplicitPackageReferenceOverrides".
   2:3>Done building target "CheckForImplicitPackageReferenceOverrides" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_CheckForLanguageAndFeatureCombinationSupport" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "ProcessFrameworkReferences" depends on it):
   2:3>Done building target "_CheckForLanguageAndFeatureCombinationSupport" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_ComputeToolPackInputsToProcessFrameworkReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "ProcessFrameworkReferences" depends on it):
   2:3>Done building target "_ComputeToolPackInputsToProcessFrameworkReferences" in project "ReflectionUtilityServices.csproj".
   2:3>Target "ProcessFrameworkReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPackageReferences" depends on it):
       Using "CheckForDuplicateFrameworkReferences" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "CheckForDuplicateFrameworkReferences"
       Done executing task "CheckForDuplicateFrameworkReferences".
       Using "ProcessFrameworkReferences" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "ProcessFrameworkReferences"
         Found 1 known framework references for target framework .NETCoreApp
         Found 5 known runtime packs for target framework .NETCoreApp
         Selected Microsoft.NETCore.App with RIDs 'linux-arm;linux-arm64;linux-musl-arm64;linux-musl-x64;linux-x64;osx-x64;tizen.4.0.0-armel;tizen.5.0.0-armel;win-arm64;win-x64;win-x86;linux-musl-arm;osx-arm64;linux-s390x;linux-loongarch64;linux-bionic-arm;linux-bionic-arm64;linux-bionic-x64;linux-bionic-x86;linux-ppc64le;freebsd-x64;freebsd-arm64;linux-riscv64;linux-musl-riscv64;linux-musl-loongarch64;android-arm64;android-x64'
         Selected targeting pack 'Microsoft.NETCore.App@10.0.3'
         Added runtime framework 'Microsoft.NETCore.App@10.0.0'
         Selected Microsoft.WindowsDesktop.App with RIDs 'win-x64;win-x86;win-arm64'
         Selected targeting pack 'Microsoft.WindowsDesktop.App@10.0.3'
         Added runtime framework 'Microsoft.WindowsDesktop.App@10.0.0'
         Selected Microsoft.WindowsDesktop.App with RIDs 'win-x64;win-x86;win-arm64'
         Selected targeting pack 'Microsoft.WindowsDesktop.App.WPF@10.0.3'
         Added runtime framework 'Microsoft.WindowsDesktop.App@10.0.0'
         Selected Microsoft.WindowsDesktop.App with RIDs 'win-x64;win-x86;win-arm64'
         Selected targeting pack 'Microsoft.WindowsDesktop.App.WindowsForms@10.0.3'
         Added runtime framework 'Microsoft.WindowsDesktop.App@10.0.0'
         Selected Microsoft.AspNetCore.App with RIDs 'win-x64;win-x86;osx-x64;linux-musl-x64;linux-musl-arm64;linux-x64;linux-arm;linux-arm64;linux-musl-arm;win-arm64;osx-arm64;linux-s390x;linux-loongarch64;linux-ppc64le;freebsd-x64;freebsd-arm64;linux-riscv64;linux-musl-riscv64;linux-loongarch64;linux-musl-loongarch64'
         Selected targeting pack 'Microsoft.AspNetCore.App@10.0.3'
         Added runtime framework 'Microsoft.AspNetCore.App@10.0.0'
       Done executing task "ProcessFrameworkReferences".
       Using "ResolveAppHosts" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "ResolveAppHosts"
       Done executing task "ResolveAppHosts".
   2:3>Done building target "ProcessFrameworkReferences" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_CheckForObsoleteDotNetCliToolReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.ObsoleteReferences.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPackageReferences" depends on it):
   2:3>Done building target "_CheckForObsoleteDotNetCliToolReferences" in project "ReflectionUtilityServices.csproj".
   2:3>Target "CollectPackageReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GetRestoreProjectStyle" depends on it):
       Using "CheckForDuplicateNuGetItemsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   2:3>Done building target "CollectPackageReferences" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GetRestoreProjectStyle" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreSpecs" depends on it):
       Using "GetRestoreProjectStyleTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreProjectStyleTask"
       Done executing task "GetRestoreProjectStyleTask".
   2:3>Done building target "_GetRestoreProjectStyle" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GenerateRestoreSpecs" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreGraphProjectEntry" depends on it):
       Task "Message"
         Restore entry point D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj
       Done executing task "Message".
   2:3>Done building target "_GenerateRestoreSpecs" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GetRestoreSettingsOverrides" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GetRestoreSettings" depends on it):
   2:3>Done building target "_GetRestoreSettingsOverrides" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GetRestoreSettingsPerFramework" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GetRestoreSettingsCurrentProject" depends on it):
   2:3>Done building target "_GetRestoreSettingsPerFramework" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GetRestoreSettingsCurrentProject" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GetRestoreSettings" depends on it):
   2:3>Done building target "_GetRestoreSettingsCurrentProject" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GetRestoreSettings" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateDotnetCliToolReferenceSpecs" depends on it):
       Using "GetRestoreSettingsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreSettingsTask"
         Assembly loaded during TaskRun (NuGet.Build.Tasks.GetRestoreSettingsTask): NuGet.Configuration, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Configuration.dll, MVID: 522ba7e1-ae9d-4267-926f-e81ab189d75f, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
       Done executing task "GetRestoreSettingsTask".
   2:3>Done building target "_GetRestoreSettings" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GenerateDotnetCliToolReferenceSpecs" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreGraphProjectEntry" depends on it):
       Using "GetRestoreDotnetCliToolsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreDotnetCliToolsTask"
       Done executing task "GetRestoreDotnetCliToolsTask".
   2:3>Done building target "_GenerateDotnetCliToolReferenceSpecs" in project "ReflectionUtilityServices.csproj".
   2:3>Target "_GenerateRestoreGraphProjectEntry" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (entry point):
   2:3>Done building target "_GenerateRestoreGraphProjectEntry" in project "ReflectionUtilityServices.csproj".
   2:3>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (_GenerateRestoreGraphProjectEntry target(s)).
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1:5) on node 1 (_GenerateRestoreGraphProjectEntry target(s)).
   1:5>Target "_CheckForUnsupportedNETCoreVersion" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPackageReferences" depends on it):
   1:5>Done building target "_CheckForUnsupportedNETCoreVersion" in project "MeProject1.csproj".
   1:5>Target "_CheckForUnsupportedCppNETCoreVersion" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPackageReferences" depends on it):
   1:5>Done building target "_CheckForUnsupportedCppNETCoreVersion" in project "MeProject1.csproj".
   1:5>Target "RemoveVersions" in file "D:\workspace\LearningNotes\CSharp\Directory.Build.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPackageReferences" depends on it):
   1:5>Done building target "RemoveVersions" in project "MeProject1.csproj".
   1:5>Target "UpdateAspNetToFrameworkReference" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.Shared.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "ApplyImplicitVersions" depends on it):
       Using "CheckIfPackageReferenceShouldBeFrameworkReference" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "CheckIfPackageReferenceShouldBeFrameworkReference"
       Done executing task "CheckIfPackageReferenceShouldBeFrameworkReference".
       Task "CheckIfPackageReferenceShouldBeFrameworkReference"
       Done executing task "CheckIfPackageReferenceShouldBeFrameworkReference".
   1:5>Done building target "UpdateAspNetToFrameworkReference" in project "MeProject1.csproj".
   1:5>Target "CheckForImplicitPackageReferenceOverrides" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.Shared.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "ApplyImplicitVersions" depends on it):
       Using "CheckForImplicitPackageReferenceOverrides" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "CheckForImplicitPackageReferenceOverrides"
       Done executing task "CheckForImplicitPackageReferenceOverrides".
   1:5>Done building target "CheckForImplicitPackageReferenceOverrides" in project "MeProject1.csproj".
   1:5>Target "ApplyImplicitVersions" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.Shared.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPackageReferences" depends on it):
       Using "ApplyImplicitVersions" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "ApplyImplicitVersions"
       Done executing task "ApplyImplicitVersions".
   1:5>Done building target "ApplyImplicitVersions" in project "MeProject1.csproj".
       Target "CheckForImplicitPackageReferenceOverrides" skipped. Previously built successfully.
   1:5>Target "_CheckForLanguageAndFeatureCombinationSupport" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "ProcessFrameworkReferences" depends on it):
   1:5>Done building target "_CheckForLanguageAndFeatureCombinationSupport" in project "MeProject1.csproj".
   1:5>Target "_ComputeToolPackInputsToProcessFrameworkReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Publish.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "ProcessFrameworkReferences" depends on it):
   1:5>Done building target "_ComputeToolPackInputsToProcessFrameworkReferences" in project "MeProject1.csproj".
   1:5>Target "ProcessFrameworkReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPackageReferences" depends on it):
       Using "CheckForDuplicateFrameworkReferences" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "CheckForDuplicateFrameworkReferences"
       Done executing task "CheckForDuplicateFrameworkReferences".
       Using "ProcessFrameworkReferences" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "ProcessFrameworkReferences"
         Found 1 known framework references for target framework .NETCoreApp
         Found 5 known runtime packs for target framework .NETCoreApp
         Selected Microsoft.NETCore.App with RIDs 'linux-arm;linux-arm64;linux-musl-arm64;linux-musl-x64;linux-x64;osx-x64;tizen.4.0.0-armel;tizen.5.0.0-armel;win-arm64;win-x64;win-x86;linux-musl-arm;osx-arm64;linux-s390x;linux-loongarch64;linux-bionic-arm;linux-bionic-arm64;linux-bionic-x64;linux-bionic-x86;linux-ppc64le;freebsd-x64;freebsd-arm64;linux-riscv64;linux-musl-riscv64;linux-musl-loongarch64;android-arm64;android-x64'
         Selected targeting pack 'Microsoft.NETCore.App@10.0.3'
         Added runtime framework 'Microsoft.NETCore.App@10.0.0'
         Selected Microsoft.WindowsDesktop.App with RIDs 'win-x64;win-x86;win-arm64'
         Selected targeting pack 'Microsoft.WindowsDesktop.App@10.0.3'
         Added runtime framework 'Microsoft.WindowsDesktop.App@10.0.0'
         Selected Microsoft.WindowsDesktop.App with RIDs 'win-x64;win-x86;win-arm64'
         Selected targeting pack 'Microsoft.WindowsDesktop.App.WPF@10.0.3'
         Added runtime framework 'Microsoft.WindowsDesktop.App@10.0.0'
         Selected Microsoft.WindowsDesktop.App with RIDs 'win-x64;win-x86;win-arm64'
         Selected targeting pack 'Microsoft.WindowsDesktop.App.WindowsForms@10.0.3'
         Added runtime framework 'Microsoft.WindowsDesktop.App@10.0.0'
         Selected Microsoft.AspNetCore.App with RIDs 'win-x64;win-x86;osx-x64;linux-musl-x64;linux-musl-arm64;linux-x64;linux-arm;linux-arm64;linux-musl-arm;win-arm64;osx-arm64;linux-s390x;linux-loongarch64;linux-ppc64le;freebsd-x64;freebsd-arm64;linux-riscv64;linux-musl-riscv64;linux-loongarch64;linux-musl-loongarch64'
         Selected targeting pack 'Microsoft.AspNetCore.App@10.0.3'
         Added runtime framework 'Microsoft.AspNetCore.App@10.0.0'
       Done executing task "ProcessFrameworkReferences".
       Using "ResolveAppHosts" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "ResolveAppHosts"
       Done executing task "ResolveAppHosts".
   1:5>Done building target "ProcessFrameworkReferences" in project "MeProject1.csproj".
   1:5>Target "_CheckForObsoleteDotNetCliToolReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.ObsoleteReferences.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPackageReferences" depends on it):
   1:5>Done building target "_CheckForObsoleteDotNetCliToolReferences" in project "MeProject1.csproj".
   1:5>Target "CollectPackageReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GetRestoreProjectStyle" depends on it):
       Using "CheckForDuplicateNuGetItemsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   1:5>Done building target "CollectPackageReferences" in project "MeProject1.csproj".
   1:5>Target "_GetRestoreProjectStyle" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreSpecs" depends on it):
       Using "GetRestoreProjectStyleTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreProjectStyleTask"
       Done executing task "GetRestoreProjectStyleTask".
   1:5>Done building target "_GetRestoreProjectStyle" in project "MeProject1.csproj".
   1:5>Target "_GenerateRestoreSpecs" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreGraphProjectEntry" depends on it):
       Task "Message"
         Restore entry point D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj
       Done executing task "Message".
   1:5>Done building target "_GenerateRestoreSpecs" in project "MeProject1.csproj".
   1:5>Target "_GetRestoreSettingsOverrides" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GetRestoreSettings" depends on it):
   1:5>Done building target "_GetRestoreSettingsOverrides" in project "MeProject1.csproj".
   1:5>Target "_GetRestoreSettingsPerFramework" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GetRestoreSettingsCurrentProject" depends on it):
   1:5>Done building target "_GetRestoreSettingsPerFramework" in project "MeProject1.csproj".
   1:5>Target "_GetRestoreSettingsCurrentProject" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GetRestoreSettings" depends on it):
   1:5>Done building target "_GetRestoreSettingsCurrentProject" in project "MeProject1.csproj".
   1:5>Target "_GetRestoreSettings" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateDotnetCliToolReferenceSpecs" depends on it):
       Using "GetRestoreSettingsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreSettingsTask"
       Done executing task "GetRestoreSettingsTask".
   1:5>Done building target "_GetRestoreSettings" in project "MeProject1.csproj".
   1:5>Target "_GenerateDotnetCliToolReferenceSpecs" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreGraphProjectEntry" depends on it):
       Using "GetRestoreDotnetCliToolsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreDotnetCliToolsTask"
       Done executing task "GetRestoreDotnetCliToolsTask".
   1:5>Done building target "_GenerateDotnetCliToolReferenceSpecs" in project "MeProject1.csproj".
   1:5>Target "_GenerateRestoreGraphProjectEntry" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (entry point):
   1:5>Done building target "_GenerateRestoreGraphProjectEntry" in project "MeProject1.csproj".
   1:5>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (_GenerateRestoreGraphProjectEntry target(s)).
     1>Done executing task "MSBuild".
       Task "MSBuild"
         Global Properties:
           ExcludeRestorePackageImports=true
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (2:4) on node 1 (_GenerateProjectRestoreGraph target(s)).
   2:4>Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
       Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
       Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
   2:4>Target "_GetRestoreTargetFrameworkOverride" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GetRestoreTargetFrameworksOutput" depends on it):
   2:4>Done building target "_GetRestoreTargetFrameworkOverride" in project "ReflectionUtilityServices.csproj".
   2:4>Target "_GetRestoreTargetFrameworksOutput" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreProjectSpec" depends on it):
       Using "GetProjectTargetFrameworksTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetProjectTargetFrameworksTask"
       Done executing task "GetProjectTargetFrameworksTask".
   2:4>Done building target "_GetRestoreTargetFrameworksOutput" in project "ReflectionUtilityServices.csproj".
       Target "_GetRestoreSettings" skipped. Previously built successfully.
   2:4>Target "_GenerateRestoreProjectSpec" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateProjectRestoreGraph" depends on it):
       Task "ConvertToAbsolutePath"
       Done executing task "ConvertToAbsolutePath".
   2:4>Done building target "_GenerateRestoreProjectSpec" in project "ReflectionUtilityServices.csproj".
       Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
       Target "CollectPackageReferences" skipped. Previously built successfully.
   2:4>Target "CollectPackageDownloads" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   2:4>Done building target "CollectPackageDownloads" in project "ReflectionUtilityServices.csproj".
   2:4>Target "CollectFrameworkReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_CollectRestoreInputs" depends on it):
   2:4>Done building target "CollectFrameworkReferences" in project "ReflectionUtilityServices.csproj".
   2:4>Target "CollectCentralPackageVersions" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   2:4>Done building target "CollectCentralPackageVersions" in project "ReflectionUtilityServices.csproj".
   2:4>Target "CollectNuGetAuditSuppressions" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   2:4>Done building target "CollectNuGetAuditSuppressions" in project "ReflectionUtilityServices.csproj".
       Target "ProcessFrameworkReferences" skipped. Previously built successfully.
   2:4>Target "AddPrunePackageReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "CollectPrunePackageReferences" depends on it):
       Using "GetPackagesToPrune" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "GetPackagesToPrune"
         Loading packages to prune for .NETCoreApp 10.0 Microsoft.NETCore.App
         Loading prune package data from PrunePackageData folder
         Failed to load prune package data from PrunePackageData folder, loading from targeting packs instead
         Looking for targeting packs in D:\DevTools\dotnet\packs\Microsoft.NETCore.App.Ref
         Pack directories found: D:\DevTools\dotnet\packs\Microsoft.NETCore.App.Ref\10.0.3
         Found package overrides file D:\DevTools\dotnet\packs\Microsoft.NETCore.App.Ref\10.0.3\data\PackageOverrides.txt
       Done executing task "GetPackagesToPrune".
   2:4>Done building target "AddPrunePackageReferences" in project "ReflectionUtilityServices.csproj".
   2:4>Target "CollectPrunePackageReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   2:4>Done building target "CollectPrunePackageReferences" in project "ReflectionUtilityServices.csproj".
   2:4>Target "_CollectRestoreInputs" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateProjectRestoreGraphPerFramework" depends on it):
   2:4>Done building target "_CollectRestoreInputs" in project "ReflectionUtilityServices.csproj".
   2:4>Target "_GenerateProjectRestoreGraphPerFramework" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateProjectRestoreGraphCurrentProject" depends on it):
       Using "GetRestoreProjectReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreProjectReferencesTask"
       Done executing task "GetRestoreProjectReferencesTask".
       Using "GetRestorePackageReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestorePackageReferencesTask"
       Done executing task "GetRestorePackageReferencesTask".
       Using "GetCentralPackageVersionsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetCentralPackageVersionsTask"
       Done executing task "GetCentralPackageVersionsTask".
       Using "GetRestorePackageDownloadsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestorePackageDownloadsTask"
       Done executing task "GetRestorePackageDownloadsTask".
       Using "GetRestoreFrameworkReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreFrameworkReferencesTask"
       Done executing task "GetRestoreFrameworkReferencesTask".
       Using "GetRestoreNuGetAuditSuppressionsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreNuGetAuditSuppressionsTask"
       Done executing task "GetRestoreNuGetAuditSuppressionsTask".
       Using "GetRestorePrunePackageReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestorePrunePackageReferencesTask"
       Done executing task "GetRestorePrunePackageReferencesTask".
   2:4>Done building target "_GenerateProjectRestoreGraphPerFramework" in project "ReflectionUtilityServices.csproj".
   2:4>Target "_GenerateProjectRestoreGraphCurrentProject" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateRestoreDependencies" depends on it):
   2:4>Done building target "_GenerateProjectRestoreGraphCurrentProject" in project "ReflectionUtilityServices.csproj".
   2:4>Target "_GenerateRestoreDependencies" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (target "_GenerateProjectRestoreGraph" depends on it):
   2:4>Done building target "_GenerateRestoreDependencies" in project "ReflectionUtilityServices.csproj".
   2:4>Target "_GenerateProjectRestoreGraph" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (entry point):
   2:4>Done building target "_GenerateProjectRestoreGraph" in project "ReflectionUtilityServices.csproj".
   2:4>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj" (_GenerateProjectRestoreGraph target(s)).
     1>Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1) is building "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (1:6) on node 1 (_GenerateProjectRestoreGraph target(s)).
   1:6>Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
       Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
       Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
   1:6>Target "_GetRestoreTargetFrameworkOverride" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GetRestoreTargetFrameworksOutput" depends on it):
   1:6>Done building target "_GetRestoreTargetFrameworkOverride" in project "MeProject1.csproj".
   1:6>Target "_GetRestoreTargetFrameworksOutput" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreProjectSpec" depends on it):
       Using "GetProjectTargetFrameworksTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetProjectTargetFrameworksTask"
       Done executing task "GetProjectTargetFrameworksTask".
   1:6>Done building target "_GetRestoreTargetFrameworksOutput" in project "MeProject1.csproj".
       Target "_GetRestoreSettings" skipped. Previously built successfully.
   1:6>Target "_GenerateRestoreProjectSpec" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateProjectRestoreGraph" depends on it):
       Task "ConvertToAbsolutePath"
       Done executing task "ConvertToAbsolutePath".
   1:6>Done building target "_GenerateRestoreProjectSpec" in project "MeProject1.csproj".
       Target "_GetRestoreProjectStyle" skipped. Previously built successfully.
       Target "CollectPackageReferences" skipped. Previously built successfully.
   1:6>Target "CollectPackageDownloads" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   1:6>Done building target "CollectPackageDownloads" in project "MeProject1.csproj".
   1:6>Target "CollectFrameworkReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_CollectRestoreInputs" depends on it):
   1:6>Done building target "CollectFrameworkReferences" in project "MeProject1.csproj".
   1:6>Target "CollectCentralPackageVersions" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   1:6>Done building target "CollectCentralPackageVersions" in project "MeProject1.csproj".
   1:6>Target "CollectNuGetAuditSuppressions" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   1:6>Done building target "CollectNuGetAuditSuppressions" in project "MeProject1.csproj".
       Target "ProcessFrameworkReferences" skipped. Previously built successfully.
   1:6>Target "AddPrunePackageReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.FrameworkReferenceResolution.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "CollectPrunePackageReferences" depends on it):
       Using "GetPackagesToPrune" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "GetPackagesToPrune"
       Done executing task "GetPackagesToPrune".
   1:6>Done building target "AddPrunePackageReferences" in project "MeProject1.csproj".
   1:6>Target "CollectPrunePackageReferences" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_CollectRestoreInputs" depends on it):
       Task "CheckForDuplicateNuGetItemsTask"
       Done executing task "CheckForDuplicateNuGetItemsTask".
   1:6>Done building target "CollectPrunePackageReferences" in project "MeProject1.csproj".
   1:6>Target "_CollectRestoreInputs" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateProjectRestoreGraphPerFramework" depends on it):
   1:6>Done building target "_CollectRestoreInputs" in project "MeProject1.csproj".
   1:6>Target "_GenerateProjectRestoreGraphPerFramework" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateProjectRestoreGraphCurrentProject" depends on it):
       Using "GetRestoreProjectReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreProjectReferencesTask"
       Done executing task "GetRestoreProjectReferencesTask".
       Using "GetRestorePackageReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestorePackageReferencesTask"
       Done executing task "GetRestorePackageReferencesTask".
       Using "GetCentralPackageVersionsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetCentralPackageVersionsTask"
       Done executing task "GetCentralPackageVersionsTask".
       Using "GetRestorePackageDownloadsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestorePackageDownloadsTask"
       Done executing task "GetRestorePackageDownloadsTask".
       Using "GetRestoreFrameworkReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreFrameworkReferencesTask"
       Done executing task "GetRestoreFrameworkReferencesTask".
       Using "GetRestoreNuGetAuditSuppressionsTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestoreNuGetAuditSuppressionsTask"
       Done executing task "GetRestoreNuGetAuditSuppressionsTask".
       Using "GetRestorePrunePackageReferencesTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "GetRestorePrunePackageReferencesTask"
       Done executing task "GetRestorePrunePackageReferencesTask".
   1:6>Done building target "_GenerateProjectRestoreGraphPerFramework" in project "MeProject1.csproj".
   1:6>Target "_GenerateProjectRestoreGraphCurrentProject" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateRestoreDependencies" depends on it):
   1:6>Done building target "_GenerateProjectRestoreGraphCurrentProject" in project "MeProject1.csproj".
   1:6>Target "_GenerateRestoreDependencies" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "_GenerateProjectRestoreGraph" depends on it):
   1:6>Done building target "_GenerateRestoreDependencies" in project "MeProject1.csproj".
   1:6>Target "_GenerateProjectRestoreGraph" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (entry point):
   1:6>Done building target "_GenerateProjectRestoreGraph" in project "MeProject1.csproj".
   1:6>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (_GenerateProjectRestoreGraph target(s)).
     1>Done executing task "MSBuild".
     1>Done building target "_GenerateRestoreGraph" in project "MeProject1.csproj".
     1>Target "DebugShowPaths" in project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "Restore" depends on it):
       Task "Message"
         [DEBUG] Project Directory: D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1
       Done executing task "Message".
       Task "Message"
         [DEBUG] DirectoryBuildPropsPath: D:\workspace\LearningNotes\CSharp\Directory.Build.props
       Done executing task "Message".
       Task "Message"
         [DEBUG] ManagePackageVersionsCentrally: true
       Done executing task "Message".
     1>Done building target "DebugShowPaths" in project "MeProject1.csproj".
     1>Target "_CollectTargetFrameworkForTelemetry" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "Restore" depends on it):
       Using "AllowEmptyTelemetry" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\..\tools\net10.0\Microsoft.NET.Build.Tasks.dll".
       Task "AllowEmptyTelemetry"
       Done executing task "AllowEmptyTelemetry".
     1>Done building target "_CollectTargetFrameworkForTelemetry" in project "MeProject1.csproj".
     1>Target "ValidateCompatibilityPackageVersions" in file "D:\workspace\LearningNotes\CSharp\Directory.Build.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "Restore" depends on it):
     1>Done building target "ValidateCompatibilityPackageVersions" in project "MeProject1.csproj".
     1>Target "_CheckForUnsupportedNETCoreVersion" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "Restore" depends on it):
     1>Done building target "_CheckForUnsupportedNETCoreVersion" in project "MeProject1.csproj".
     1>Target "_CheckForUnsupportedCppNETCoreVersion" in file "D:\DevTools\dotnet\sdk\10.0.103\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (target "Restore" depends on it):
     1>Done building target "_CheckForUnsupportedCppNETCoreVersion" in project "MeProject1.csproj".
     1>Target "Restore" in file "D:\DevTools\dotnet\sdk\10.0.103\NuGet.targets" from project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (entry point):
       Task "RemoveDuplicates"
       Done executing task "RemoveDuplicates".
       Using "RestoreTask" task from assembly "D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll".
       Task "RestoreTask"
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.Packaging, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Packaging.dll, MVID: 691fbee5-b36c-46fd-a673-db79d3c14051, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.LibraryModel, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.LibraryModel.dll, MVID: a6bbad50-9e54-4ecf-9ee6-06659d261f87, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.Versioning, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Versioning.dll, MVID: 3f1bd844-829c-40f0-b7b2-db643543649c, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.Frameworks, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Frameworks.dll, MVID: 1b7b49ca-9945-4cfa-824f-5d73c248e8d6, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.Protocol, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Protocol.dll, MVID: f9fea46b-5e46-4ce0-a20f-e4253f7ea121, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.Credentials, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.Credentials.dll, MVID: 3086496a-ea04-4486-96b4-8a3e90687378, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Collections.NonGeneric, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Collections.NonGeneric.dll, MVID: 34793951-de27-497a-86fa-ff7f73c34148, AssemblyLoadContext: Default)
         X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
         X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): NuGet.DependencyResolver.Core, Version=7.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 (location: D:\DevTools\dotnet\sdk\10.0.103\NuGet.DependencyResolver.Core.dll, MVID: 8b34e545-21b0-4590-be6b-6b43bfa1968c, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Running restore with 12 concurrent jobs.
         Reading project file D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj.
         Reading project file D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj.
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): Newtonsoft.Json, Version=13.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed (location: D:\DevTools\dotnet\sdk\10.0.103\Newtonsoft.Json.dll, MVID: 7e62198b-eab2-4380-bbac-29171862d1d8, AssemblyLoadContext: MSBuild plugin D:\DevTools\dotnet\sdk\10.0.103\NuGet.Build.Tasks.dll)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Runtime.Serialization.Primitives, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Runtime.Serialization.Primitives.dll, MVID: 9bc74e02-881b-4bc0-82b9-d2a9156b6d41, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Runtime.Numerics, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Runtime.Numerics.dll, MVID: a1d287c5-84bc-4499-8e90-494a1a3f9350, AssemblyLoadContext: Default)
         The restore inputs for 'MeProject1' have changed. Continuing restore.
         Restoring packages for D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj...
         The restore inputs for 'ReflectionUtilityServices' have not changed. No further actions are required to complete the restore.
         Committing restore...
         Assets file has not changed. Skipping assets file writing. Path: D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\project.assets.json
         No-Op restore. The cache will not be updated. Path: D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\obj\project.nuget.cache
         Restored D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\ReflectionUtilityServices.csproj (in 45 ms).
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Net.Sockets, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Net.Sockets.dll, MVID: d4dae19c-a532-4ae0-916b-3763913739a0, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Threading.Overlapped, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Threading.Overlapped.dll, MVID: 092ab194-5254-4084-95a8-da161dcea080, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Runtime.Intrinsics, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51 (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Runtime.Intrinsics.dll, MVID: 9f5af1ce-aa80-4680-b03a-60771503d33f, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Security.Principal.Windows, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Security.Principal.Windows.dll, MVID: 94668085-5eab-454a-aadd-f7411e0617e0, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Security.Claims, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Security.Claims.dll, MVID: 20e6e427-b6b6-4325-b1e2-55052517fb2d, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Linq.Expressions, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Linq.Expressions.dll, MVID: cc5697e3-9d40-4c28-b3b9-4b271a4593e1, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.ComponentModel.TypeConverter, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.ComponentModel.TypeConverter.dll, MVID: 4dbdb69f-1290-4069-bf6a-9a6f24e8f6fc, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.ObjectModel, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.ObjectModel.dll, MVID: 2c1629e1-2ad7-4f58-9c22-4928776a6cb9, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Runtime.Serialization.Formatters, Version=8.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Runtime.Serialization.Formatters.dll, MVID: 2a5142af-889e-4701-a330-f9028d2593da, AssemblyLoadContext: Default)
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.dependencyinjection/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.options/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.abstractions/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.abstractions/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.hosting.abstractions/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.dependencyinjection.abstractions/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.hosting/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.primitives/index.json
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.abstractions/index.json 410ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.options/index.json 498ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.dependencyinjection/index.json 507ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging/index.json 527ms
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Diagnostics.TraceSource, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Diagnostics.TraceSource.dll, MVID: 553fe9f8-6a87-45ba-a259-74dc5900545d, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Data.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Data.Common.dll, MVID: c66fc60f-d43d-4d63-9e6d-aa5baf23c716, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.ComponentModel, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.ComponentModel.dll, MVID: dfbe3f8a-21fc-47c1-8505-2409f96f9862, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): Anonymously Hosted DynamicMethods Assembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null (location: , MVID: b4b40d30-0cbe-48a2-8ab7-205d394c76bc, AssemblyLoadContext: Default)
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.abstractions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection/index.json 15ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.abstractions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting.abstractions/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.abstractions/index.json 31ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection.abstractions/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options/index.json 33ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.abstractions/index.json 31ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/index.json 26ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting.abstractions/index.json 32ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection.abstractions/index.json 27ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging/index.json 21ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection/11.0.0-preview.2.26159.112/microsoft.extensions.dependencyinjection.11.0.0-preview.2.26159.112.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.abstractions/11.0.0-preview.2.26159.112/microsoft.extensions.logging.abstractions.11.0.0-preview.2.26159.112.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.abstractions/11.0.0-preview.2.26159.112/microsoft.extensions.logging.abstractions.11.0.0-preview.2.26159.112.nupkg 20ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection/11.0.0-preview.2.26159.112/microsoft.extensions.dependencyinjection.11.0.0-preview.2.26159.112.nupkg 22ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options/11.0.0-preview.2.26159.112/microsoft.extensions.options.11.0.0-preview.2.26159.112.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.abstractions/11.0.0-preview.2.26159.112/microsoft.extensions.configuration.abstractions.11.0.0-preview.2.26159.112.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting.abstractions/10.0.3/microsoft.extensions.hosting.abstractions.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/11.0.0-preview.2.26159.112/microsoft.extensions.primitives.11.0.0-preview.2.26159.112.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting.abstractions/10.0.3/microsoft.extensions.hosting.abstractions.10.0.3.nupkg 19ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options/11.0.0-preview.2.26159.112/microsoft.extensions.options.11.0.0-preview.2.26159.112.nupkg 33ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging/11.0.0-preview.2.26159.112/microsoft.extensions.logging.11.0.0-preview.2.26159.112.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection.abstractions/11.0.0-preview.2.26159.112/microsoft.extensions.dependencyinjection.abstractions.11.0.0-preview.2.26159.112.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.abstractions/11.0.0-preview.2.26159.112/microsoft.extensions.configuration.abstractions.11.0.0-preview.2.26159.112.nupkg 36ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/11.0.0-preview.2.26159.112/microsoft.extensions.primitives.11.0.0-preview.2.26159.112.nupkg 29ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.hosting.abstractions/index.json 505ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.hosting/index.json 504ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.primitives/index.json 516ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.dependencyinjection.abstractions/index.json 516ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.abstractions/index.json 518ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging/11.0.0-preview.2.26159.112/microsoft.extensions.logging.11.0.0-preview.2.26159.112.nupkg 81ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.dependencyinjection.abstractions/11.0.0-preview.2.26159.112/microsoft.extensions.dependencyinjection.abstractions.11.0.0-preview.2.26159.112.nupkg 118ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting/index.json 269ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting/10.0.3/microsoft.extensions.hosting.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.hosting/10.0.3/microsoft.extensions.hosting.10.0.3.nupkg 21ms
           GET https://nuget.pkg.github.com/codeloverjay30/download/system.diagnostics.diagnosticsource/index.json
           GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/index.json
           OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/index.json 11ms
           GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/11.0.0-preview.2.26159.112/system.diagnostics.diagnosticsource.11.0.0-preview.2.26159.112.nupkg
           OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/11.0.0-preview.2.26159.112/system.diagnostics.diagnosticsource.11.0.0-preview.2.26159.112.nupkg 23ms
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.commandline/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.binder/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.json/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.commandline/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.environmentvariables/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.fileextensions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.binder/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.json/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.fileproviders.abstractions/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.usersecrets/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.diagnostics/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.environmentvariables/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.fileextensions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration/index.json 16ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.abstractions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.usersecrets/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.fileproviders.physical/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.physical/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.console/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.diagnostics.abstractions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.debug/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.configuration/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.debug/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.console/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.eventlog/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.configuration/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics.abstractions/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.binder/index.json 24ms
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.eventsource/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventlog/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventsource/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration/10.0.3/microsoft.extensions.configuration.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.abstractions/index.json 48ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.binder/10.0.3/microsoft.extensions.configuration.binder.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration/10.0.3/microsoft.extensions.configuration.10.0.3.nupkg 31ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.abstractions/10.0.3/microsoft.extensions.fileproviders.abstractions.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.binder/10.0.3/microsoft.extensions.configuration.binder.10.0.3.nupkg 45ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.abstractions/10.0.3/microsoft.extensions.fileproviders.abstractions.10.0.3.nupkg 17ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.environmentvariables/index.json 211ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.fileextensions/index.json 209ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.commandline/index.json 218ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics/index.json 208ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.json/index.json 227ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.usersecrets/index.json 231ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.physical/index.json 238ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.configuration/index.json 240ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics.abstractions/index.json 241ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.console/index.json 279ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventlog/index.json 271ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventsource/index.json 268ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.debug/index.json 283ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.commandline/10.0.3/microsoft.extensions.configuration.commandline.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics/10.0.3/microsoft.extensions.diagnostics.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.fileextensions/10.0.3/microsoft.extensions.configuration.fileextensions.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.environmentvariables/10.0.3/microsoft.extensions.configuration.environmentvariables.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.json/10.0.3/microsoft.extensions.configuration.json.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.commandline/10.0.3/microsoft.extensions.configuration.commandline.10.0.3.nupkg 24ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.usersecrets/10.0.3/microsoft.extensions.configuration.usersecrets.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics.abstractions/10.0.3/microsoft.extensions.diagnostics.abstractions.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.configuration/10.0.3/microsoft.extensions.logging.configuration.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.fileextensions/10.0.3/microsoft.extensions.configuration.fileextensions.10.0.3.nupkg 24ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.physical/10.0.3/microsoft.extensions.fileproviders.physical.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics/10.0.3/microsoft.extensions.diagnostics.10.0.3.nupkg 31ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventsource/10.0.3/microsoft.extensions.logging.eventsource.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.console/10.0.3/microsoft.extensions.logging.console.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventlog/10.0.3/microsoft.extensions.logging.eventlog.10.0.3.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.debug/10.0.3/microsoft.extensions.logging.debug.10.0.3.nupkg
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/system.diagnostics.diagnosticsource/index.json 440ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.environmentvariables/10.0.3/microsoft.extensions.configuration.environmentvariables.10.0.3.nupkg 34ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.json/10.0.3/microsoft.extensions.configuration.json.10.0.3.nupkg 28ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.console/10.0.3/microsoft.extensions.logging.console.10.0.3.nupkg 53ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.configuration.usersecrets/10.0.3/microsoft.extensions.configuration.usersecrets.10.0.3.nupkg 65ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.commandline/index.json 420ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.json/index.json 419ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.fileproviders.physical/10.0.3/microsoft.extensions.fileproviders.physical.10.0.3.nupkg 77ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration/index.json 437ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.usersecrets/index.json 423ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventlog/10.0.3/microsoft.extensions.logging.eventlog.10.0.3.nupkg 77ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.debug/10.0.3/microsoft.extensions.logging.debug.10.0.3.nupkg 74ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.fileproviders.abstractions/index.json 429ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.environmentvariables/index.json 438ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.configuration/10.0.3/microsoft.extensions.logging.configuration.10.0.3.nupkg 106ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.logging.eventsource/10.0.3/microsoft.extensions.logging.eventsource.10.0.3.nupkg 102ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.diagnostics.abstractions/10.0.3/microsoft.extensions.diagnostics.abstractions.10.0.3.nupkg 152ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.diagnostics/index.json 673ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.configuration/index.json 661ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.debug/index.json 661ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.eventlog/index.json 657ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.diagnostics.abstractions/index.json 663ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.console/index.json 667ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.logging.eventsource/index.json 656ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.fileproviders.physical/index.json 670ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.binder/index.json 694ms
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.configuration.fileextensions/index.json 696ms
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.options.configurationextensions/index.json
           GET https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.filesystemglobbing/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options.configurationextensions/index.json
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.filesystemglobbing/index.json
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.filesystemglobbing/index.json 21ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options.configurationextensions/index.json 39ms
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.filesystemglobbing/10.0.7/microsoft.extensions.filesystemglobbing.10.0.7.nupkg
           GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options.configurationextensions/10.0.3/microsoft.extensions.options.configurationextensions.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.filesystemglobbing/10.0.7/microsoft.extensions.filesystemglobbing.10.0.7.nupkg 36ms
           OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.options.configurationextensions/10.0.3/microsoft.extensions.options.configurationextensions.10.0.3.nupkg 23ms
           GET https://nuget.pkg.github.com/codeloverjay30/download/system.diagnostics.eventlog/index.json
           GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/index.json
           OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/index.json 32ms
           GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/10.0.3/system.diagnostics.eventlog.10.0.3.nupkg
           OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/10.0.3/system.diagnostics.eventlog.10.0.3.nupkg 16ms
         Acquiring lock for the installation of Microsoft.Extensions.FileProviders.Physical 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.FileProviders.Physical 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.EnvironmentVariables 10.0.3
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.filesystemglobbing/index.json 410ms
         Acquired lock for the installation of Microsoft.Extensions.Configuration.EnvironmentVariables 10.0.3
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/microsoft.extensions.options.configurationextensions/index.json 414ms
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.FileExtensions 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Configuration.FileExtensions 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Logging.Console 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Logging.Console 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Diagnostics 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Diagnostics 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.CommandLine 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Configuration.CommandLine 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Logging.Debug 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Logging.Debug 10.0.3
         Acquiring lock for the installation of System.Diagnostics.EventLog 10.0.3
         Acquired lock for the installation of System.Diagnostics.EventLog 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.Json 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Configuration.Json 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.Binder 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Configuration.Binder 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Options.ConfigurationExtensions 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Options.ConfigurationExtensions 10.0.3
           NotFound https://nuget.pkg.github.com/codeloverjay30/download/system.diagnostics.eventlog/index.json 409ms
         Acquiring lock for the installation of Microsoft.Extensions.Primitives 11.0.0-preview.2.26159.112
         Acquired lock for the installation of Microsoft.Extensions.Primitives 11.0.0-preview.2.26159.112
         Acquiring lock for the installation of Microsoft.Extensions.DependencyInjection 11.0.0-preview.2.26159.112
         Acquired lock for the installation of Microsoft.Extensions.DependencyInjection 11.0.0-preview.2.26159.112
         Acquiring lock for the installation of Microsoft.Extensions.Logging.EventSource 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Logging.EventSource 10.0.3
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Security.Cryptography.Pkcs, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\sdk\10.0.103\runtimes\win\lib\net10.0\System.Security.Cryptography.Pkcs.dll, MVID: d1f11134-f762-4ce1-8488-27657b55118f, AssemblyLoadContext: Default)
         Acquiring lock for the installation of Microsoft.Extensions.Diagnostics.Abstractions 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Diagnostics.Abstractions 10.0.3
         Acquiring lock for the installation of System.Diagnostics.DiagnosticSource 11.0.0-preview.2.26159.112
         Acquired lock for the installation of System.Diagnostics.DiagnosticSource 11.0.0-preview.2.26159.112
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Formats.Asn1, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51 (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Formats.Asn1.dll, MVID: bfe9a804-d74d-4017-9638-1bd7113ed45d, AssemblyLoadContext: Default)
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.Debug.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Options.ConfigurationExtensions.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.Json.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.FileExtensions.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: System.Diagnostics.DiagnosticSource.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: System.Diagnostics.EventLog.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Diagnostics.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.EnvironmentVariables.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.CommandLine.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.Console.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.FileProviders.Physical.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.Binder.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.Net.Quic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.Net.Quic.dll, MVID: 60b05b51-8208-40f5-ae2a-7b1ce0ccedba, AssemblyLoadContext: Default)
         Assembly loaded during TaskRun (NuGet.Build.Tasks.RestoreTask): System.IO.Pipelines, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51 (location: D:\DevTools\dotnet\shared\Microsoft.NETCore.App\10.0.3\System.IO.Pipelines.dll, MVID: 47531204-037a-4e5d-8cf9-b7a73083f638, AssemblyLoadContext: Default)
         Installed Microsoft.Extensions.Logging.Debug 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging.debug\10.0.3 with content hash OoH8AcYCq74ab5XUIQc84CZk54G/cU+JztiMXgNKGkomJOeuistTMg0PWPC4VXXMSVBEGWJuMDEBttOrHyXe8w==.
         Acquiring lock for the installation of Microsoft.Extensions.Options 11.0.0-preview.2.26159.112
         Acquired lock for the installation of Microsoft.Extensions.Options 11.0.0-preview.2.26159.112
         Installed Microsoft.Extensions.Configuration.FileExtensions 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.fileextensions\10.0.3 with content hash /MLsBbLpwDxsU+7DDNwasf2mKrpMSOWEL377gNZTy5waFkCYvS3GVaLIz6bvikH4rAwHrCOxHw0t/5iCoImYCA==.
         Acquiring lock for the installation of Microsoft.Extensions.Logging.Abstractions 11.0.0-preview.2.26159.112
         Installed Microsoft.Extensions.Options.ConfigurationExtensions 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.options.configurationextensions\10.0.3 with content hash bn6QoBbbvwmzLIFyxrnL2/e+sqoNUOGbHyfWK9DPONMv1mDCYHm/C7MusYASM31b2lUx6OiDmonb3v+dv5t0nA==.
         Acquiring lock for the installation of Microsoft.Extensions.FileProviders.Abstractions 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Logging.Abstractions 11.0.0-preview.2.26159.112
         Acquired lock for the installation of Microsoft.Extensions.FileProviders.Abstractions 10.0.3
         Installed Microsoft.Extensions.Diagnostics 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.diagnostics\10.0.3 with content hash tc0R6i2T+138taoxFPQXb7Sy/4rtq4ytoJrAt4fNGs6k89mHpEhZnXUNgaFKwwb5Ud5rIUeLC6tfpsuHNwiWqg==.
         Acquiring lock for the installation of Microsoft.Extensions.Logging 11.0.0-preview.2.26159.112
         Acquired lock for the installation of Microsoft.Extensions.Logging 11.0.0-preview.2.26159.112
         Installed Microsoft.Extensions.Configuration.CommandLine 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.commandline\10.0.3 with content hash qVytXuCHQCIJcOsJJnp+1mNCAtiWuLqI0qhCcByFuyxDifthefEWX3fVAXbaxn1lDP2iR1KH44Oq7tvmT7dBHg==.
         Installed Microsoft.Extensions.Configuration.EnvironmentVariables 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.environmentvariables\10.0.3 with content hash jBm6bpc5OM2VHM/QYVUyD78xweFzble6UsIt7GUnQAwCm07hktFaUBtRfO7viLGg5qPbc4ByteNB7DeVAYNSfA==.
         Installed System.Diagnostics.DiagnosticSource 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\system.diagnostics.diagnosticsource\11.0.0-preview.2.26159.112 with content hash 53nimBaVbzyZjQfi/hx/mRuqtNBGpu1uk7o8P9esiBVWsWz3+cimhT5pE8exQPYEJMDermE2mv5+HtPSo0TkGg==.
         Acquiring lock for the installation of Microsoft.Extensions.FileSystemGlobbing 10.0.7
         Acquiring lock for the installation of Microsoft.Extensions.Configuration 10.0.3
         Acquiring lock for the installation of Microsoft.Extensions.Hosting 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Hosting 10.0.3
         Installed Microsoft.Extensions.Logging.Console 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging.console\10.0.3 with content hash 7sRvbBH3icaV9qil8fyBKmR+yEZ0yDU6Bq/KgBwswS36164yGaxbf7Kd4hD1iHZ2GfvyoJWWqBUBm9QX/IASAQ==.
         Acquired lock for the installation of Microsoft.Extensions.FileSystemGlobbing 10.0.7
         Acquiring lock for the installation of Microsoft.Extensions.Hosting.Abstractions 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Configuration 10.0.3
         Installed Microsoft.Extensions.Configuration.Json 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.json\10.0.3 with content hash mGGMOA9nkET8OVsQfS41o66eWkckBzNHJK6+5VbLQ2YdyqKphcv27uDZxLf4exSl+5QxLnHkN+W/4qEDgyvCPA==.
         Acquiring lock for the installation of Microsoft.Extensions.Logging.EventLog 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Hosting.Abstractions 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Logging.EventLog 10.0.3
         Installed Microsoft.Extensions.FileProviders.Physical 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.fileproviders.physical\10.0.3 with content hash 8qLl5LXtcj6Z8yPbHAA/a57fvvl9nUCdi59AJFuixcWM4wSuENZ8jjoRATOKs/I4vOi/bDe0d5LqGSSLE634eA==.
         Acquiring lock for the installation of Microsoft.Extensions.Logging.Configuration 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Logging.Configuration 10.0.3
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.DependencyInjection.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Primitives.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.FileProviders.Abstractions.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.EventSource.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Options.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Diagnostics.Abstractions.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.Abstractions.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Hosting.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Hosting.Abstractions.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.FileSystemGlobbing.10.0.7 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.EventLog.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Logging.Configuration.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         Installed System.Diagnostics.EventLog 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\system.diagnostics.eventlog\10.0.3 with content hash +bZnyzt0/vt4g3QSllhsRNGTpa09p7Juy5K8spcK73cOTOefu4+HoY89hZOgIOmzB5A4hqPyEDKnzra7KKnhZw==.
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.UserSecrets 10.0.3
         Acquired lock for the installation of Microsoft.Extensions.Configuration.UserSecrets 10.0.3
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.UserSecrets.10.0.3 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         Installed Microsoft.Extensions.Configuration.Binder 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.binder\10.0.3 with content hash 759UhpKaR5Jsll9kXpkft4z/7tpeF7Dw2rTY/9f9JchaSQTpRFNIPkZFZvoo7fFpbjUaqtDlO5aiGpmQrp/EUA==.
         Acquiring lock for the installation of Microsoft.Extensions.Configuration.Abstractions 11.0.0-preview.2.26159.112
         Acquired lock for the installation of Microsoft.Extensions.Configuration.Abstractions 11.0.0-preview.2.26159.112
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.Configuration.Abstractions.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         Installed Microsoft.Extensions.Logging.EventSource 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging.eventsource\10.0.3 with content hash r2hIVkSrb+f8FqcguHqlzyQ8lNGCtWsOPY9+OzJinrqdzQfszS8fXkHVcNHW4uK6WFxI2tPSiGdms2SeRJq8hg==.
         Acquiring lock for the installation of Microsoft.Extensions.DependencyInjection.Abstractions 11.0.0-preview.2.26159.112
         Installed Microsoft.Extensions.Primitives 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.primitives\11.0.0-preview.2.26159.112 with content hash 1S1pHUdrdgz8tVgJCgMli0cN3np/k6aLTwYASyQFHhuPrUzq0YaTHzge23BUqgIbKCZSFLzQ2V9LKgxfRsCFeQ==.
         Acquired lock for the installation of Microsoft.Extensions.DependencyInjection.Abstractions 11.0.0-preview.2.26159.112
         Installed Microsoft.Extensions.Diagnostics.Abstractions 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.diagnostics.abstractions\10.0.3 with content hash mQiTzAj7PIJ2A9YXR5QhgulS1fTWhmQc3ckd1Mrf3hKW07d03fBDqx8vVaFw+cRTebDOeB6pNqdWdnRxsi1hBA==.
         Installed Microsoft.Extensions.Logging 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging\11.0.0-preview.2.26159.112 with content hash cFbgaWaXdGDx54T2FcaGNQPntTaJMl+Tw2pkg5MO0efVJ+qpavmNu9kqhiJN2vcAlTUFFw4GpkeVcZ8BIXzvSQ==.
         Installed Microsoft.Extensions.DependencyInjection 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.dependencyinjection\11.0.0-preview.2.26159.112 with content hash 7H4WuK6zpCTipF/yzkFGtovofSNeBjFMh8GcLWNG8FqHx0vOqHgvDmcg4Zyay3inu/TH272/0WFCsF54B/sFDw==.
         Installed Microsoft.Extensions.FileProviders.Abstractions 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.fileproviders.abstractions\10.0.3 with content hash 4TD9AXDRsipTmaemwnjt/DM5Ri0de2JzHQhvZ4woBTjUtL4XrPNsMrOk5oiLJAx1gTrE6pOIhxv+lEde5F6CZA==.
         PackageSignatureVerificationLog: PackageIdentity: Microsoft.Extensions.DependencyInjection.Abstractions.11.0.0-preview.2.26159.112 Source: https://api.nuget.org/v3/index.json PackageSignatureValidity: True
         Installed Microsoft.Extensions.Configuration 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration\10.0.3 with content hash H1Cjv2xmm7O3iAGmFTcnSM0ZhLQ/7SqefmAvSJoT1PbXoxeYc2fo0mCLn2JlVbr9E6YpoU9q/o0fI9neDJB0xQ==.
         Installed Microsoft.Extensions.Logging.EventLog 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging.eventlog\10.0.3 with content hash 1V1oRR+0DKyetPKI4POn7+jXH4kI1D69R/Rjje/4/BSkTM2iUCsRkr7Q0gDyXayhCXgPEf/P19kgwN5t0s/p8w==.
         Installed Microsoft.Extensions.FileSystemGlobbing 10.0.7 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.filesystemglobbing\10.0.7 with content hash NTUspqB+vH9g4wAD6KPOBx01xqYuKXR/cHXm449zpbq1GqfjdAxBmg7eJXrNsPw7SKwIdT2cJ05GxYVvc+lvsA==.
         Installed Microsoft.Extensions.Logging.Configuration 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging.configuration\10.0.3 with content hash PBlaoYeusaxNYyN4WFjzcXWlUDSvLUPxy/e6oP1SONOOYA/oBWT2uBmFGJMV9VTtXiXXxCB39LqlYWbsWE4UKA==.
         Installed Microsoft.Extensions.Hosting.Abstractions 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.hosting.abstractions\10.0.3 with content hash GdMpC10Jf6poxSvUJ4lgYpJ5F/kJeaAoJmrPufjBoPYyCTKKY5Dyl0rZA+LBNvFqTq1cZa/lhlptlUhNvU6xrg==.
         Installed Microsoft.Extensions.Hosting 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.hosting\10.0.3 with content hash ssbRCALcbBXfQPXLr4bZ3FVlbnDzeR0F6hPKDUCZLPQul7oBeSGaJq+XLUjwYaptOs4TN5cSy1q6LVLPWFgjKQ==.
         Installed Microsoft.Extensions.Configuration.Abstractions 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.abstractions\11.0.0-preview.2.26159.112 with content hash cv4DpD5pnnaXoI+6pVtaUUwHGv7yVlkWu8nkiIZ0J6dSWYk8SGEqdSvJcASkcShod5cbCUhtzA6nM2KCBrIaKQ==.
         Installed Microsoft.Extensions.Configuration.UserSecrets 10.0.3 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.configuration.usersecrets\10.0.3 with content hash f6FiscfqlLrNUR2x7XcMqMGz5ZFRwTvezZuebIn4v2ste0nL/sEZ7pdveDXqDyonVv/QTKT8vvIEqTQCczzsOg==.
         Installed Microsoft.Extensions.Options 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.options\11.0.0-preview.2.26159.112 with content hash j+c3ChsPtHUk125Av2B685t6fJrgKI02zCOacFpWxtHVm3LoWwVsBsu+HfByf3hko39N3gni1ZbrB7NI1VJk3g==.
         Installed Microsoft.Extensions.DependencyInjection.Abstractions 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.dependencyinjection.abstractions\11.0.0-preview.2.26159.112 with content hash OQXAVaAtsf/HNCFzeby//Q7ibYk8Lf+8dgvRna5mmulHyXU0qPMtERY10CuhBkPdkxS69Dsgk1VjD5e3OF/f3w==.
         Installed Microsoft.Extensions.Logging.Abstractions 11.0.0-preview.2.26159.112 from https://api.nuget.org/v3/index.json to D:\DevTools\nuget_cache\microsoft.extensions.logging.abstractions\11.0.0-preview.2.26159.112 with content hash yE3AafH9H/ybiI5USxYfCEexMM7MogTfziE6dgeT5P09nATl8wfk31clavCRALdoF5pDgxMsKxJ7P0XXhjlbYA==.
           GET https://api.nuget.org/v3/vulnerabilities/index.json
           OK https://api.nuget.org/v3/vulnerabilities/index.json 228ms
           GET https://api.nuget.org/v3-vulnerabilities/2026.06.25.23.55.27/vulnerability.base.json
           GET https://api.nuget.org/v3-vulnerabilities/2026.06.25.23.55.27/2026.06.25.23.55.27/vulnerability.update.json
           OK https://api.nuget.org/v3-vulnerabilities/2026.06.25.23.55.27/vulnerability.base.json 194ms
           OK https://api.nuget.org/v3-vulnerabilities/2026.06.25.23.55.27/2026.06.25.23.55.27/vulnerability.update.json 197ms
         All packages and projects are compatible with net10.0.
         Committing restore...
         Generating MSBuild file D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\MeProject1.csproj.nuget.g.props.
         Generating MSBuild file D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\MeProject1.csproj.nuget.g.targets.
         Writing assets file to disk. Path: D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\project.assets.json
         Writing cache file to disk. Path: D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\project.nuget.cache
         Persisting dg to D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\obj\MeProject1.csproj.nuget.dgspec.json
         Restored D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj (in 7.69 sec).
         
         NuGet Config files used:
             D:\workspace\LearningNotes\CSharp\NuGet.Config
             C:\Users\userJay30\AppData\Roaming\NuGet\NuGet.Config
             C:\Program Files (x86)\NuGet\Config\Microsoft.VisualStudio.FallbackLocation.config
             C:\Program Files (x86)\NuGet\Config\Microsoft.VisualStudio.Offline.config
         
         Feeds used:
             https://nuget.pkg.github.com/codeloverjay30/index.json
             D:\workspace\MyLocalNuget\BlazorWebAssembly\
             D:\workspace\MyLocalNuget\ClassLibrary\
             D:\workspace\LearningNotes\CSharp\local-packages
             https://api.nuget.org/v3/index.json
             C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\
         
         Installed:
             29 package(s) to D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj
         1 of 2 projects are up-to-date for restore.
       Done executing task "RestoreTask".
     1>Done building target "Restore" in project "MeProject1.csproj".
     1>Done Building Project "D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\MeProject1.csproj" (Restore target(s)).

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:08.72
````

## File: Examples/Ex1/Ex1-1/MeProject1/ILogUtilityService.cs
````csharp
namespace MeProject1;

public interface ILogUtilityService
{
    void LogValueOfMeInstance(Me me);
    void LogValueOfSonInstance(Son son);
}
````

## File: Examples/Ex1/Ex1-1/MeProject1/LogUtilityService.cs
````csharp
using ReflectionUtilityServices;

namespace MeProject1;

public class LogUtilityService : ILogUtilityService
{
    private readonly IReflectionUtilityService _reflectionUtilityService;
    public LogUtilityService(
        IReflectionUtilityService reflectionUtilityService
    )
    {
        ArgumentNullException.ThrowIfNull(reflectionUtilityService, nameof(reflectionUtilityService));
        _reflectionUtilityService = reflectionUtilityService;
    }

    public void LogValueOfMeInstance(Me me)
    {
        Console.WriteLine("// ======================= LogValueOfMeInstance method ======================= //");
        Console.WriteLine(me.Name);
        Console.WriteLine(me.Account);
        Console.WriteLine(me.Password);
        Console.WriteLine(_reflectionUtilityService.GetPropertyValue(me, "Name"));
        Console.WriteLine(_reflectionUtilityService.GetPropertyValue(me, "Account"));
        Console.WriteLine(_reflectionUtilityService.GetPropertyValue(me, "Password"));
        Console.WriteLine("// ======================= [end] LogValueOfMeInstance method ======================= //");
    }

    public void LogValueOfSonInstance(Son son)
    {
        Console.WriteLine("// ======================= LogValueOfSonInstance method ======================= //");
        Console.WriteLine(son.Name);
        Console.WriteLine(son.Account);
        Console.WriteLine(son.Password);
        Console.WriteLine(_reflectionUtilityService.GetPropertyValue(son, "Name"));
        Console.WriteLine(_reflectionUtilityService.GetPropertyValue(son, "Account"));
        Console.WriteLine(_reflectionUtilityService.GetPropertyValue(son, "Password"));
        Console.WriteLine("// ======================= [end] LogValueOfSonInstance method ======================= //");
    }
}
````

## File: Examples/Ex1/Ex1-1/MeProject1/Me.cs
````csharp
namespace MeProject1;

public partial class Me
{
    public string Name { get; set; }

    public string Account { get; private set; } = "Account";

    public string Password { get; init; } = "Password";

    private string _name { get; set; }
    private string _password { get; init; }

    protected string __name { get; set; }
    protected string __account { get; private set; }
    protected string __password { get; init; }

}
````

## File: Examples/Ex1/Ex1-1/MeProject1/MeProject1.csproj
````
<Project Sdk="Microsoft.NET.Sdk">
<Target Name="DebugShowPaths" BeforeTargets="Restore">
  <Message Importance="high" Text="[DEBUG] Project Directory: $(MSBuildProjectDirectory)" />
  <Message Importance="high" Text="[DEBUG] DirectoryBuildPropsPath: $(DirectoryBuildPropsPath)" />
  <Message Importance="high" Text="[DEBUG] ManagePackageVersionsCentrally: $(ManagePackageVersionsCentrally)" />
</Target>
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\ReflectionUtilityServices\ReflectionUtilityServices.csproj" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Logging" />
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" />
    <PackageReference Include="Microsoft.Extensions.Options" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" />
    <PackageReference Include="Microsoft.Extensions.Primitives" />
    <PackageReference Include="Microsoft.Extensions.Hosting" />
    <PackageReference Include="Microsoft.Extensions.Hosting.Abstractions" />
  </ItemGroup>
</Project>
````

## File: Examples/Ex1/Ex1-1/MeProject1/README.md
````markdown

````

## File: Examples/Ex1/Ex1-1/MeProject1/Son.cs
````csharp
namespace MeProject1;

public partial class Son : Me
{
    public string _name { get; set; }
    public string _account { get; set; }
    public string _password { get; set; }
    public string Get_Name(){ return _name; }
    public string get_Name(){ return _name; }
    public void Set_Name(){}
    public void set_Name(){}

    public string Get_Account(){ return _account; }
    public string get_Account(){ return _account; }
    public void Set_Account(){}
    public void set_Account(){}

    public string Get_Password(){ return _password; }
    public string get_Password(){ return _password; }
    public void Set_Password(){}
    public void set_Password(){}

    public string Get__Name(){ return _name; }
    public string get__Name(){ return _name; }
    public void Set__Name(){}
    public void set__Name(){}

    public string Get__Password(){ return _password; }
    public string get__Password(){ return _password; }
    public void Set__Password(){}
    public void set__Password(){}

    public string Get___Name(){ return _name; }
    public string get___Name(){ return _name; }
    public void Set___Name(){}
    public void set___Name(){}

    public string Get___Account(){ return _account; }
    public string get___Account(){ return _account; }
    public void Set___Account(){}
    public void set___Account(){}

    public string Get___Password(){ return _password; }
    public string get___Password(){ return _password; }
    public void Set___Password(){}
    public void set___Password(){}
}
````

## File: Examples/Ex1/Ex1-1/MeProject2/Me.cs
````csharp
namespace MeProject2;

public partial class Me
{
    public string Name { get; set; }

    public string Account { get; private set; } = "Account";

    public string Password { get; init; }

    private string _name { get; set; }
    private string _password { get; init; }

    protected string __name { get; set; }
    protected string __account { get; private set; }
    protected string __password { get; init; }

}
````

## File: Examples/Ex1/Ex1-1/MeProject2/MeProject2.csproj
````
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\ReflectionUtilityServices\ReflectionUtilityServices.csproj" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Logging" />
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" />
    <PackageReference Include="Microsoft.Extensions.Options" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" />
    <PackageReference Include="Microsoft.Extensions.Primitives" />
    <PackageReference Include="Microsoft.Extensions.Hosting" />
    <PackageReference Include="Microsoft.Extensions.Hosting.Abstractions" />
  </ItemGroup>
</Project>
````

## File: Examples/Ex1/Ex1-1/MeProject2/README.md
````markdown

````

## File: Examples/Ex1/Ex1-1/MeProject2/Son.cs
````csharp
namespace MeProject2;

public partial class Son : Me
{
    public string _name { get; set; }
    public string _account { get; set; }
    public string _password { get; set; }
    
    public string Get_Name() { return _name; }
    public string get_Name() { return _name; }
    public void Set_Name(string value) {  _name = value; }
    public void set_Name(string value) {  _name = value; }

    public string Get_Account() { return _account; }
    public string get_Account() { return _account; }
    public void Set_Account(string value) { _account = value; }
    public void set_Account(string value) { _account = value;  }

    public string Get_Password() { return _password; }
    public string get_Password() { return _password; }

    public void Set_Password(string value) { _password = value; }

    public void set_Password(string value) { _password = value; }

    public string Get__Name() { return _name; }
    public string get__Name() { return _name; }
    public void Set__Name(string value) { _name = value; }
    public void set__Name(string value) { _name = value; }

    public string Get__Password() { return _password; }
    public string get__Password() { return _password; }
    public void Set__Password(string value) { _password = value; }
    public void set__Password(string value) { _password = value; }

    public string Get___Name() { return _name; }
    public string get___Name() { return _name; }
    public void Set___Name(string value) { _name = value; }
    public void set___Name(string value) { _name = value; }

    public string Get___Account() { return _account; }
    public string get___Account() { return _account; }
    public void Set___Account(string value) { _account = value; }
    public void set___Account(string value) { _account = value; }

    public string Get___Password() { return _password; }
    public string get___Password() { return _password; }
    public void Set___Password(string value) { _password = value; }
    public void set___Password(string value) { _password = value; }
}
````

## File: Examples/Ex1/Ex1-1/Questions/Q1.md
````markdown
# Q1
For these projects [MeProject1](../MeProject1/MeProject1.csproj) and [MeProject2](../MeProject1/MeProject1.csproj),

are there any compiler errors?

If there are no any compiler errors, will it crash when at runtime?

If it will not crash,
````

## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/IReflectionUtilityService.cs
````csharp
namespace ReflectionUtilityServices;

/// <summary>
/// utility service about reflection.
/// </summary>
public interface IReflectionUtilityService
{
    /// <summary>
    /// Set <paramref name="value"/> of property named <paramref name="propertyName"/> of the instance <paramref name="target"/> by reflection.
    /// </summary>
    /// <param name="target">The target object whose property value will be retrieved.</param>
    /// <param name="propertyName">The name of the property to retrieve.</param>
    /// <param name="value">value that will be set</param>
    void SetPropertyValue(
        object target,
        string propertyName,
        object? value
    );

    /// <summary>
    /// Gets the value of a specified property from a given target object.
    /// </summary>
    /// <param name="target">The target object whose property value will be retrieved.</param>
    /// <param name="propertyName">The name of the property to retrieve.</param>
    /// <returns>The value of the property.</returns>
    object? GetPropertyValue(
        object target,
        string propertyName
    );
}
````

## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/README.md
````markdown

````

## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityService.cs
````csharp
using System.Reflection;

namespace ReflectionUtilityServices;

/// <inheritdoc cref="global::ReflectionUtilityServices.IReflectionUtilityService"/>
public class ReflectionUtilityService : IReflectionUtilityService
{
    /// <inheritdoc cref="global::ReflectionUtilityServices.IReflectionUtilityService.SetPropertyValue(object, string, object?)"/>
    public void SetPropertyValue(
        object target,
        string propertyName,
        object? value
    )
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target), "Target object cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
        }

        Type type = target.GetType();

        // 使用 BindingFlags 確保能找到 Public 與 Non-Public 的 Instance 屬性
        PropertyInfo? propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"Property '{propertyName}' was not found on type '{type.FullName}'.");
        }

        if (!propertyInfo.CanWrite)
        {
            throw new InvalidOperationException($"Property '{propertyName}' on type '{type.FullName}' does not have a setter.");
        }

        try
        {
            // 防禦性設計：轉換型別，避免傳入不相符的型別導致底層噴出隱蔽異常
            object? convertedValue = value;
            if (value is not null && !propertyInfo.PropertyType.IsAssignableFrom(value.GetType()))
            {
                convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
            }

            propertyInfo.SetValue(target, convertedValue, null);
        }
        catch (Exception ex) when (ex is not TargetInvocationException)
        {
            // 攔截並包裝非預期的異常
            throw new InvalidOperationException($"Failed to set value for property '{propertyName}' on type '{type.FullName}'.", ex);
        }
    }

    /// <inheritdoc cref="global::ReflectionUtilityServices.IReflectionUtilityService.GetPropertyValue(object, string)"/>
    public object? GetPropertyValue(
        object target,
        string propertyName
    )
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target), "Target object cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
        }

        Type type = target.GetType();

        // 使用 BindingFlags 確保不論是 Public 還是 Non-Public 的實例屬性都能被找到
        PropertyInfo? propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"Property '{propertyName}' was not found on type '{type.FullName}'.");
        }

        if (!propertyInfo.CanRead)
        {
            throw new InvalidOperationException($"Property '{propertyName}' on type '{type.FullName}' does not have a getter.");
        }

        try
        {
            // 取得屬性值
            return propertyInfo.GetValue(target, null);
        }
        catch (Exception ex) when (ex is not TargetInvocationException)
        {
            // 防禦性設計：攔截並包裝底層非預期的異常
            throw new InvalidOperationException($"Failed to get value for property '{propertyName}' on type '{type.FullName}'.", ex);
        }
    }
}
````

## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityServices.csproj
````
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
````

## File: file-structure.txt
````
PS D:\workspace\Scripts> . "D:\workspace\Scripts\List-ChildItem-And-Its-Content-From-Down-To-Top.ps1"

==========================================
Checking Directory: D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1
==========================================
 [Directory] : MeProject1
 [Directory] : MeProject2
 [Directory] : Questions
 [Directory] : ReflectionUtilityServices
 [File]      : Ex1-1.slnx
 --- Content Start ---
   <Solution>
     <Project Path="MeProject1/MeProject1.csproj" />
     <Project Path="MeProject2/MeProject2.csproj" />
     <Project Path="ReflectionUtilityServices/ReflectionUtilityServices.csproj" />
   </Solution>
 --- Content End ---

==========================================
Checking Directory: D:\workspace\LearningNotes\CSharp\Examples\Ex1
==========================================
 [Directory] : Ex1-1

==========================================
Checking Directory: D:\workspace\LearningNotes\CSharp\Examples
==========================================
 [Directory] : Ex1

==========================================
Checking Directory: D:\workspace\LearningNotes\CSharp
==========================================
 [Directory] : Examples
 [File]      : .clinerules
 --- Content Start ---
   # 角色與核心哲學

   你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。

   # 核心硬性規定 (鐵律，絕不可違反)

   ## 1. 單元測試斷言標準 (最高優先級)

   - 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

   - 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

   - 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

     正例：

     Action act = () => _service.DoSomething();

     act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");


   ## 2. 測試環境之檔案系統與 Mock 副作用防禦

   - 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 當測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或 其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。

   ## 3. 回覆語言

   - 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法 ）保持 C# 原生英文。

   - 針對程式碼的XML註解，請永遠使用英文(US, America)

   ## 4. 程式碼產生

   - 易讀性:針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`

   - 防禦性檢查:針對非單元測試的類別(和其方法)，請務必進行防禦性檢查(詳見`7. 防禦性檢查`)

   - 易維護、易測試:針對涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護、易測試:針對測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，其類別一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護:針對非單元測試的類別(和其方法)，盡可能地使用interface和decoration pattern(不過有例外地，請根據情境判斷之)

   - 簡潔:針對非單元測試的類別(和其方法)，請針對我正在開發的Utility package底下，適當地用facade pattern以減少其他要引用 該utility package的專案底下需要傳入太多引數至建構子或方法

   - 效能:針對我新開發的Utility package，優先使用效能最佳的手段 (詳見`5. 效能最佳之設計`)

   - 針對我新開發的Utility package，優先盡可能地設計出一個參數不多的API (詳見`6. 參數最佳之設計`)

   - 簡潔:針對非單元測試的類別(和其方法)，根據各種的Design Pattern的優缺點和其應用來決定是否要用哪種，若用到的Design Pattern需要使用介面時，則一併附上介面的定義。

   - 高重用性:針對非單元測試的類別(和其方法)，盡可能重用我自己已經開發完的Utility service(位在`~\modules`資料夾底下的專 案)、內建API

   、第三方套件的API

   - 易維護:針對非單元測試的類別，其產生的類別(和其方法、其Property)永遠應該要遵守‵.editorconfig‵的規則(且其掃描機制跟Rolsyn一模一樣)

   - 請使用最新語法完成設計(尤其是針對字串,詳見`8. 字串`)

   ## 5. 效能最佳之設計
   ### API
   包含但不限於

   - `struct`

   - `ReadOnlySpan<T>`, `ReadOnlyMemory<T>`等系列的API

   - `IMemoryCached` 等系列的API 而不是`Dictionary<T>`

   - `Stream`等系列的API

   - `PipelineWriter`,`PipelineReader` 等系列的API

   ### 策略
   包含但不限於

   - 快取機制

   - 增量機制

   - Zero allocation (詳見`ReadOnlySpan<T>`)

   - 減少GC的發生 (詳見`ReadOnlySpan<T>`)

   - 減少堆疊使用 (提示:可以使用`struct`而不是`class`)

   - 避免一次讀取大檔案 可使用`Stream`甚至是`Pipeline` (參考`PipelineWriter`等)


   ## 6. 參數最佳之設計
   ### 策略
   包含但不限於

   - POCO (`class`, `struct`, `record`, `record class` etc)

   - facade pattern (詳見`4. 程式碼產生`的某一條)

   ## 7. 防禦性檢查
   ### 檢查項目
   包含但不限於

   - 判斷傳入的引數的值是否合法 (如是否是null)

   - 根據意圖，判斷即將要回傳的回傳值是否合法

   ### 撰寫模式
   盡可能的

   - 使用.NET本身就有的內建方法(如:`ArgumentNullException.ThrowIfNull(x)`,`ArgumentOutOfRangeException.ThrowIfGreaterThan(counter,4)`)而不是用`throw new`來拋出異常(如:`throw new Excception("")`)

   ## 8. 字串
   ### 撰寫風格

   - 針對字串串接，請使用字串插值 (若可以)

   - 針對字串，請使用Raw String Literals

 --- Content End ---
 [File]      : .repomixignore
 --- Content Start ---
   # Add patterns to ignore here, one per line
   # Example:
   # *.log
   # tmp/
 --- Content End ---
 [File]      : ai-rules.md
 --- Content Start ---
   # 角色與核心哲學

   你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。



   # 核心硬性規定 (鐵律，絕不可違反)



   ## 1. 單元測試斷言標準 (最高優先級)

   - 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

   - 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

   - 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

     正例：

     Action act = () => _service.DoSomething();

     act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");



   ## 2. 測試環境之檔案系統與 Mock 副作用防禦

   - 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`。

   - 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或 其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。



   ## 3. 回覆語言

   - 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法 ）保持 C# 原生英文。



   - 針對程式碼的XML註解，請永遠使用英文(US, America)



   ## 4. 程式碼產生

   - 針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`
 --- Content End ---
 [File]      : CSharp-LearningNotes-repomix-output.md
 --- Content Start ---
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
   .clinerules
   .repomixignore
   ai-rules.md
   Directory.Build.props
   Directory.Build.targets
   Directory.Packages.props
   Examples/Ex1/Ex1-1/Ex1-1.slnx
   Examples/Ex1/Ex1-1/MeProject1/EntryPoint.cs
   Examples/Ex1/Ex1-1/MeProject1/EntryPoint1.cs
   Examples/Ex1/Ex1-1/MeProject1/ILogUtilityService.cs
   Examples/Ex1/Ex1-1/MeProject1/LogUtilityService.cs
   Examples/Ex1/Ex1-1/MeProject1/Me.cs
   Examples/Ex1/Ex1-1/MeProject1/MeProject1.csproj
   Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
   Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.AssemblyInfo.cs
   Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.AssemblyInfoInputs.cache
   Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.assets.cache
   Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.GeneratedMSBuildEditorConfig.editorconfig
   Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.GlobalUsings.g.cs
   Examples/Ex1/Ex1-1/MeProject1/obj/MeProject1.csproj.nuget.dgspec.json
   Examples/Ex1/Ex1-1/MeProject1/obj/MeProject1.csproj.nuget.g.props
   Examples/Ex1/Ex1-1/MeProject1/obj/MeProject1.csproj.nuget.g.targets
   Examples/Ex1/Ex1-1/MeProject1/obj/project.assets.json
   Examples/Ex1/Ex1-1/MeProject1/obj/project.nuget.cache
   Examples/Ex1/Ex1-1/MeProject1/Son.cs
   Examples/Ex1/Ex1-1/MeProject2/Me.cs
   Examples/Ex1/Ex1-1/MeProject2/MeProject2.csproj
   Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
   Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.AssemblyInfo.cs
   Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.AssemblyInfoInputs.cache
   Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.assets.cache
   Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.GeneratedMSBuildEditorConfig.editorconfig
   Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.GlobalUsings.g.cs
   Examples/Ex1/Ex1-1/MeProject2/obj/MeProject2.csproj.nuget.dgspec.json
   Examples/Ex1/Ex1-1/MeProject2/obj/MeProject2.csproj.nuget.g.props
   Examples/Ex1/Ex1-1/MeProject2/obj/MeProject2.csproj.nuget.g.targets
   Examples/Ex1/Ex1-1/MeProject2/obj/project.assets.json
   Examples/Ex1/Ex1-1/MeProject2/obj/project.nuget.cache
   Examples/Ex1/Ex1-1/MeProject2/Son.cs
   Examples/Ex1/Ex1-1/Questions/Q1.md
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/IReflectionUtilityService.cs
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.AssemblyInfo.cs
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.AssemblyInfoInputs.cache
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.assets.cache
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.GeneratedMSBuildEditorConfig.editorconfig
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.GlobalUsings.g.cs
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/project.assets.json
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/project.nuget.cache
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/ReflectionUtilityServices.csproj.nuget.dgspec.json
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/ReflectionUtilityServices.csproj.nuget.g.props
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/ReflectionUtilityServices.csproj.nuget.g.targets
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityService.cs
   Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityServices.csproj
   GEMINI.md
   global.json
   nuget.config
   PreloadPropsEntry.props.entry.config
   repomix.config.json
   ```

   # Files

   ## File: .clinerules
   ```
   # 角色與核心哲學

   你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。

   # 核心硬性規定 (鐵律，絕不可違反)

   ## 1. 單元測試斷言標準 (最高優先級)

   - 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

   - 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

   - 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

     正例：

     Action act = () => _service.DoSomething();

     act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");


   ## 2. 測試環境之檔案系統與 Mock 副作用防禦

   - 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 當測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或 其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。

   ## 3. 回覆語言

   - 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法 ）保持 C# 原生英文。

   - 針對程式碼的XML註解，請永遠使用英文(US, America)

   ## 4. 程式碼產生

   - 易讀性:針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`

   - 防禦性檢查:針對非單元測試的類別(和其方法)，請務必進行防禦性檢查(詳見`7. 防禦性檢查`)

   - 易維護、易測試:針對涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護、易測試:針對測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，其類別一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護:針對非單元測試的類別(和其方法)，盡可能地使用interface和decoration pattern(不過有例外地，請根據情境判斷之)

   - 簡潔:針對非單元測試的類別(和其方法)，請針對我正在開發的Utility package底下，適當地用facade pattern以減少其他要引用 該utility package的專案底下需要傳入太多引數至建構子或方法

   - 效能:針對我新開發的Utility package，優先使用效能最佳的手段 (詳見`5. 效能最佳之設計`)

   - 針對我新開發的Utility package，優先盡可能地設計出一個參數不多的API (詳見`6. 參數最佳之設計`)

   - 簡潔:針對非單元測試的類別(和其方法)，根據各種的Design Pattern的優缺點和其應用來決定是否要用哪種，若用到的Design Pattern需要使用介面時，則一併附上介面的定義。

   - 高重用性:針對非單元測試的類別(和其方法)，盡可能重用我自己已經開發完的Utility service(位在`~\modules`資料夾底下的專 案)、內建API

   、第三方套件的API

   - 易維護:針對非單元測試的類別，其產生的類別(和其方法、其Property)永遠應該要遵守‵.editorconfig‵的規則(且其掃描機制跟Rolsyn一模一樣)

   - 請使用最新語法完成設計(尤其是針對字串,詳見`8. 字串`)

   ## 5. 效能最佳之設計
   ### API
   包含但不限於

   - `struct`

   - `ReadOnlySpan<T>`, `ReadOnlyMemory<T>`等系列的API

   - `IMemoryCached` 等系列的API 而不是`Dictionary<T>`

   - `Stream`等系列的API

   - `PipelineWriter`,`PipelineReader` 等系列的API

   ### 策略
   包含但不限於

   - 快取機制

   - 增量機制

   - Zero allocation (詳見`ReadOnlySpan<T>`)

   - 減少GC的發生 (詳見`ReadOnlySpan<T>`)

   - 減少堆疊使用 (提示:可以使用`struct`而不是`class`)

   - 避免一次讀取大檔案 可使用`Stream`甚至是`Pipeline` (參考`PipelineWriter`等)


   ## 6. 參數最佳之設計
   ### 策略
   包含但不限於

   - POCO (`class`, `struct`, `record`, `record class` etc)

   - facade pattern (詳見`4. 程式碼產生`的某一條)

   ## 7. 防禦性檢查
   ### 檢查項目
   包含但不限於

   - 判斷傳入的引數的值是否合法 (如是否是null)

   - 根據意圖，判斷即將要回傳的回傳值是否合法

   ### 撰寫模式
   盡可能的

   - 使用.NET本身就有的內建方法(如:`ArgumentNullException.ThrowIfNull(x)`,`ArgumentOutOfRangeException.ThrowIfGreaterThan(counter,4)`)而不是用`throw new`來拋出異常(如:`throw new Excception("")`)

   ## 8. 字串
   ### 撰寫風格

   - 針對字串串接，請使用字串插值 (若可以)

   - 針對字串，請使用Raw String Literals
   ```

   ## File: .repomixignore
   ```
   # Add patterns to ignore here, one per line
   # Example:
   # *.log
   # tmp/
   ```

   ## File: ai-rules.md
   ```markdown
   # 角色與核心哲學

   你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。



   # 核心硬性規定 (鐵律，絕不可違反)



   ## 1. 單元測試斷言標準 (最高優先級)

   - 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

   - 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

   - 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

     正例：

     Action act = () => _service.DoSomething();

     act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");



   ## 2. 測試環境之檔案系統與 Mock 副作用防禦

   - 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`。

   - 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或 其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。



   ## 3. 回覆語言

   - 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法 ）保持 C# 原生英文。



   - 針對程式碼的XML註解，請永遠使用英文(US, America)



   ## 4. 程式碼產生

   - 針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`
   ```

   ## File: Directory.Build.props
   ```
   <Project>
     <ImportGroup>
       <Import Project="$(MSBuildThisFileDirectory)\PreloadPropsEntry.props.entry.config" Condition="Exists('$(MSBuildThisFileDirectory)\PreloadPropsEntry.props.entry.config')" />
     </ImportGroup>
     <PropertyGroup>
       <Authors>codeloverjay30</Authors>
       <IsClassLibraryPackage>true</IsClassLibraryPackage>
       <IsBlazorWasmPackage>false</IsBlazorWasmPackage>
       <PackageRootPath>$(MSBuildThisFileDirectory)</PackageRootPath>
       <!-- Default path of local NuGet package that will be packed -->
       <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
     </PropertyGroup>
     <Choose>
       <When Condition="'$(IsClassLibraryPackage)' == 'true'">
         <!-- The package is class library or not -->
         <PropertyGroup>
           <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
         </PropertyGroup>
       </When>
       <!-- The package is used for Blazor WebAssembly or not -->
       <When Condition="'$(IsBlazorWasmPackage)' == 'true'">
         <PropertyGroup>
           <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\BlazorWebAssembly</MyNuGetPackageOutputPath>
         </PropertyGroup>
       </When>
       <Otherwise>
         <PropertyGroup>
           <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
         </PropertyGroup>
       </Otherwise>
     </Choose>
     <PropertyGroup>
       <PackageOutputPath>$(MyNuGetPackageOutputPath)</PackageOutputPath>
       <PackageId>$(MSBuildProjectName)</PackageId>
       <GeneratePackageOnBuild>false</GeneratePackageOnBuild>
       <PackageTags>Utility Service</PackageTags>
       <Description>See README.md</Description>
       <PackageReleaseNotes>See README.md</PackageReleaseNotes>
       <PackageReadmeFile>$(ProjectDir)README.md</PackageReadmeFile>
       <RepositoryUrl>https://github.com/codeloverjay30/utility_packages</RepositoryUrl>
       <RepositoryType>git</RepositoryType>
     </PropertyGroup>
     <!-- define property that belongs to framework at .NET SDK 6.0 -->
     <PropertyGroup>
       <IsAtLeastNet6>false</IsAtLeastNet6>
       <IsAtLeastNet6 Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '6.0'))">true</IsAtLeastNet6>
     </PropertyGroup>
     <!-- define property that belongs to legacy framework -->
     <PropertyGroup>
       <IsLegacyFramework>true</IsLegacyFramework>
       <IsLegacyFramework Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '6.0'))">false</IsLegacyFramework>
     </PropertyGroup>
     <!-- define property that belongs to framework at .NET SDK 8.0 -->
     <PropertyGroup>
       <IsAtLeastNet8>false</IsAtLeastNet8>
       <IsAtLeastNet8 Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '8.0'))">true</IsAtLeastNet8>
     </PropertyGroup>
     <Choose>
         <When Condition="'$(IsAtLeastNet6)'=='true'">
           <PropertyGroup>
             <ImplicitUsings>enable</ImplicitUsings>
             <Nullable>enable</Nullable>
           </PropertyGroup>
         </When>
         <Otherwise>
           <PropertyGroup>
             <ImplicitUsings>disable</ImplicitUsings>
             <Nullable>disable</Nullable>
           </PropertyGroup>
         </Otherwise>
     </Choose>
     <PropertyGroup>
       <!-- set is as packable by default -->
       <IsPackable>true</IsPackable>

       <!-- If the project contains "Test", "Demo", or "Sample", set it as unpackable -->
       <IsTestProject Condition="$(MSBuildProjectName.Contains('Test'))">true</IsTestProject>
       <IsDemoProject Condition="$(MSBuildProjectName.Contains('Demo'))">true</IsDemoProject>
       <IsSampleProject Condition="$(MSBuildProjectName.Contains('Sample'))">true</IsSampleProject>

       <!-- indicates that project is experimental -->
       <IsExperimental Condition="$(MSBuildProjectDirectory.Contains('experiments'))">true</IsExperimental>

       <!-- indicates that project is stable -->
       <IsStable Condition="$(MSBuildProjectDirectory.Contains('modules'))">true</IsStable>

       <!-- indicates that project is stable and is module -->
       <IsModule Condition="$(MSBuildProjectDirectory.Contains('modules'))">true</IsModule>

       <!-- indicates that project is stable class library -->
       <IsClassLibrary Condition="'$(IsTestProject)' == 'true' Or '$(IsDemoProject)' == 'true' Or '$(IsSampleProject)' == 'true'">false</IsClassLibrary>

       <!-- set it as packable iff it is a class library and it is stable. Otherwise, set is unpackable -->
       <IsPackable Condition="('$(IsStable)' != 'true' And '$(IsModule)' != 'true') Or ('$(IsClassLibrary)' == 'false')">false</IsPackable>
     </PropertyGroup>
     <PropertyGroup Condition="'$(IsModule)' == 'true' And ( '$(IsClassLibrary)' == 'true' Or '$(IsTestProject)' == 'true')">
       <!-- build target path -->
       <OutputPath>$(SolutionDir)\artifacts\$(Configuration)\</OutputPath>
     </PropertyGroup>
     <!-- is it class library and is .NET SDK 8.0+ -->
     <PropertyGroup Condition="'$(IsAtLeastNet8)' == 'true' And '$(IsClassLibrary)' == 'true'">
       <!-- set the build target to put into artifacts path(see`<ArtifactsPath>`), indicating VSCode launcher (`.vscode/launch.json`) uses build target which placed in `<ArtifactsPath>`-->
       <UseArtifactsOutput>true</UseArtifactsOutput>
       <!-- artifacts path -->
       <ArtifactsPath>$(PackageRootPath).artifacts</ArtifactsPath>
     </PropertyGroup>
     <ItemGroup>
       <None Condition="'$(IsPackable)' == 'true'" Include="$(ProjectDir)README.md" Pack="true" PackagePath="\" />
     </ItemGroup>
   </Project>
   ```

   ## File: Directory.Build.targets
   ```
   <Project>
     <Target Name="RemoveVersions" BeforeTargets="CollectPackageReferences">
       <ItemGroup>
         <!--  Exclude packages related to `.NET`, avoiding the version of packages related to `.NET` are erased to empty string -->
         <PackageReference Update="@(PackageReference)" Version=""  Condition="'%(Identity)' != 'NETStandard.Library'" />
       </ItemGroup>
     </Target>
     <!-- The Target is triggered once NuGet completes to restore -->
     <Target Name="ValidateCompatibilityPackageVersions" AfterTargets="_CheckForUnsupportedTargetFramework">

       <!-- Get all `<PackageVersion>`  -->
       <ItemGroup>
         <!-- If `NETStandard.Library package` is specified but its `Version` attribute is NOT specified or with incorrect value, then setting this tag and thus throws an error -->
         <InvalidCompats Include="@(PackageVersion)"
                         Condition="'%(Identity)' == 'NETStandard.Library' And '%(Version)' == ''" />
       </ItemGroup>

       <!-- if `InvalidCompats` is set, throws an error -->
       <Error Text="【架構警告】偵測到相容性套件 NETStandard.Library 未在中央明確指定版本號！請至 Directory.Packages.props 補上。"
              Code="ARCH001"
              Condition="'@(InvalidCompats)' != ''" />
     </Target>
   </Project>
   ```

   ## File: Directory.Packages.props
   ```
   <Project>
        <PropertyGroup>
          <NoWarn>$(NoWarn);NU1507</NoWarn>
        </PropertyGroup>
     <PropertyGroup>
       <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
       <CentralPackageTransitivePinningEnabled>true</CentralPackageTransitivePinningEnabled>
     </PropertyGroup>
     <!-- If one didn't specify the package version, treat it as errors instead of warnings -->
     <PropertyGroup>
       <WarningsAsErrors>$(WarningsAsErrors);NU1008;NU1605</WarningsAsErrors>
     </PropertyGroup>
     <!-- Let projetcs can be compiled in .NET Framework (older version) when not installed -->
     <ItemGroup>
       <GlobalPackageVersion Include="NETStandard.Library" Version="2.0.3" PrivateAssets="all" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive"/>
     </ItemGroup>
     <ItemGroup>
       <!-- This applies to ALL projects in the repository automatically -->
       <GlobalPackageVersion Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.3" PrivateAssets="all" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive" />
     </ItemGroup>
     <!-- When uses `.NET Test Framework`, `Microsoft.NET.Test.Sdk` is used, but for older framework, uses the older version of `Microsoft.NET.Test.Sdk`  -->
     <!--
     <ItemGroup Condition="'$(IsLegacyFramework)' != 'true' And '$(TargetFramework)' != 'net452' And '$(TargetFramework)' != 'net472' And '$(TargetFramework)' != 'netstandard2.0'">
       <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
     </ItemGroup>
     <ItemGroup Condition="'$(IsLegacyFramework)' == 'true' Or '$(TargetFramework)' == 'net8.0' Or '$(TargetFramework)' == 'net452' Or '$(TargetFramework)' == 'net472' Or '$(TargetFramework)' == 'netstandard2.0'">
       <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
     </ItemGroup>
     -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
     </ItemGroup>
     <!-- Use .ASP .NET Core Framework -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.AspNetCore.App.Ref" Version="10.0.5" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.TeamFoundationServer.ExtendedClient" Version="20.256.2" />
     </ItemGroup>
     <!-- -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Extensions.TimeProvider.Testing" Version="10.6.0" />
     </ItemGroup>
     <!-- Mock File System -->
     <ItemGroup Condition="'$(IsLegacyFramework)' != 'true' And '$(TargetFramework)' != 'net452' And '$(TargetFramework)' != 'net472' And '$(TargetFramework)' != 'netstandard2.0'">
       <!-- abstraction of File system -->
       <PackageVersion Include="System.IO.Abstractions" Version="22.1.1" />
       <!-- Mock File System -->
       <PackageVersion Include="System.IO.Abstractions.TestingHelpers" Version="22.1.1" />
     </ItemGroup>
     <ItemGroup Condition="'$(IsLegacyFramework)' == 'true' Or '$(TargetFramework)' == 'net8.0' Or '$(TargetFramework)' == 'net452' Or '$(TargetFramework)' == 'net472' Or '$(TargetFramework)' == 'netstandard2.0'">    <!-- abstraction of File system -->
       <PackageVersion Include="System.IO.Abstractions" Version="19.2.87" />
       <!-- Mock File System -->
       <PackageVersion Include="System.IO.Abstractions.TestingHelpers" Version="19.2.87" />
     </ItemGroup>
     <!-- abstraction of System Process -->
     <ItemGroup>
       <PackageVersion Include="System.Diagnostics.Abstractions" Version="1.0.9" />
     </ItemGroup>
     <!-- abstraction of System Process (used by older test framework such as `xUnit2`) -->
     <ItemGroup>
       <PackageVersion Include="Testably.Abstractions.FileSystem.Interface" Version="10.2.0" />
     </ItemGroup>
     <!-- abstraction of Environment class -->
     <ItemGroup>
       <PackageVersion Include="EnvironmentAbstractions" Version="5.0.0" />
       <!-- Mock Environment -->
       <PackageVersion Include="EnvironmentAbstractions.TestHelpers" Version="5.0.0" />
     </ItemGroup>
     <!-- Official utility packages of command line -->
     <ItemGroup>
       <PackageVersion Include="System.CommandLine" Version="2.0.5" />
     </ItemGroup>
     <!-- `MSBuild`, simulating `MSBuild` behaviour -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Build" Version="18.4.0" />
     </ItemGroup>
     <!-- Locator of `MSBuild` -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Build.Locator" Version="1.11.2" />
     </ItemGroup>
     <!-- Official utility packages about accessing version of NuGet package or .NET framework -->
     <ItemGroup>
       <!-- accessing version of  NuGet package -->
       <PackageVersion Include="NuGet.Protocol" Version="7.3.1" />
       <!-- accessing version of  .NET framework -->
       <PackageVersion Include="NuGet.Frameworks" Version="7.3.1" />
     </ItemGroup>
     <!-- Use traditional mode `VSTest` -->
     <!-- `NUnit` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="NUnit" Version="4.3.2" />
       <PackageVersion Include="NUnit3TestAdapter" Version="6.2.0" />
       <PackageVersion Include="NUnit.Analyzers" Version="4.7.0" />
     </ItemGroup>
     <!-- `xUnit.v3` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="coverlet.collector" Version="6.0.4" />
       <PackageVersion Include="xunit.v3" Version="3.2.2" />
       <PackageVersion Include="xunit.v3.mtp-v2" Version="3.2.2" />
       <PackageVersion Include="xunit.v3.extensibility.core" Version="3.2.2" />
     </ItemGroup>
     <!-- package that checks the class or property contains specific words, it uses `xUnit.v2` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="TngTech.ArchUnitNET.xUnit" Version="0.13.3" />
     </ItemGroup>
     <!-- `xUnit.v2` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="xunit" Version="2.9.3" />
       <PackageVersion Include="xunit.runner.visualstudio" Version="3.1.4" />
     </ItemGroup>
     <!-- Use modern mode `MTP` -->
     <!-- use `MSTest` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="MSTest.TestAdapter" Version="3.2.2" />
       <PackageVersion Include="MSTest.TestFramework" Version="3.2.2" />
       <PackageVersion Include="Microsoft.Testing.Extensions.VSTestBridge" Version="2.1.0" />
     </ItemGroup>
     <!-- use `NUnit` using `MTP` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.TestPlatform" Version="18.4.0" />
       <PackageVersion Include="Microsoft.Testing.Platform.MSBuild" Version="2.2.1" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.Testing.Platform" Version="2.2.1" />
     </ItemGroup>
     <!-- packages that makes the assertions be more fluent -->
     <ItemGroup Condition="'$(IsLegacyFramework)' != 'true' And '$(TargetFramework)' != 'net452' And '$(TargetFramework)' != 'net472' And '$(TargetFramework)' != 'netstandard2.0'">
       <PackageVersion Include="FluentAssertions" Version="8.9.0" />
       <PackageVersion Include="FluentValidation" Version="12.1.1" />
     </ItemGroup>
     <ItemGroup Condition="'$(IsLegacyFramework)' == 'true' Or '$(TargetFramework)' == 'net8.0' Or '$(TargetFramework)' == 'net452' Or '$(TargetFramework)' == 'net472' Or '$(TargetFramework)' == 'netstandard2.0'">
       <PackageVersion Include="FluentAssertions" Version="5.10.3" />
       <PackageVersion Include="FluentValidation" Version="8.6.3" />
     </ItemGroup>
     <!-- packages that mock the interface and virtual class -->
     <ItemGroup>
       <PackageVersion Include="Moq" Version="4.20.72" />
     </ItemGroup>
     <!-- packages that checks the AST of class (including, check a class contains property named `property1`) -->
     <ItemGroup>
       <PackageVersion Include="NSubstitute" Version="5.3.0" />
     </ItemGroup>
     <!-- Rolsyn -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.CodeAnalysis.CSharp" Version="5.3.0" />
       <PackageVersion Include="Microsoft.CodeAnalysis.CSharp.Workspaces" Version="5.3.0" />
     </ItemGroup>
     <!-- Rolsyn used for tests -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.CodeAnalysis.CSharp.SourceGenerators.Testing" Version="1.1.3" />
     </ItemGroup>
     <!-- Roslynator -->
     <ItemGroup>
       <PackageVersion Include="Roslynator.Analyzers" Version="4.15.0"/>
     </ItemGroup>
     <!-- SonarAnalyzer -->
     <ItemGroup>
       <PackageVersion Include="SonarAnalyzer.CSharp" Version="10.25.0.139117" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.Bcl.AsyncInterfaces" Version="10.0.7" />
       <PackageVersion Include="System.ClientModel" Version="1.10.0" />
       <PackageVersion Include="System.Memory.Data" Version="10.0.3" />
     </ItemGroup>
      <!-- logger `ILogger` -->
     <Choose>
       <When Condition="'$(IsAtLeast8)' == 'true'">
         <ItemGroup>
           <PackageVersion Include="Microsoft.Extensions.Logging" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Logging.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Options" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.DependencyInjection" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Configuration.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Primitives" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Hosting" Version="10.0.3" />
           <PackageVersion Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.3" />
         </ItemGroup>
       </When>
       <When Condition="'$(IsAtLeast6)' == 'true' And '$(IsAtLeast8)' != 'true' ">
         <ItemGroup>
             <PackageVersion Include="Microsoft.Extensions.Logging" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Logging.Abstractions" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Options" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.DependencyInjection" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Configuration.Abstractions" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Primitives" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Hosting" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.3" />
         </ItemGroup>
       </When>
       <Otherwise>
         <ItemGroup>
         <!-- NOT specify version of `Microsoft.Extensions.Logging` and its related packages as it is NOT supported -->
             <PackageVersion Include="Microsoft.Extensions.Logging" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Logging.Abstractions" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Options" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.DependencyInjection" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Configuration.Abstractions" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Primitives" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Hosting" Version="10.0.3" />
             <PackageVersion Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.3" />
           </ItemGroup>
       </Otherwise>
     </Choose>
      <!-- Azure -->
     <ItemGroup>
       <PackageVersion Include="Azure.ResourceManager" Version="1.14.0" />
       <PackageVersion Include="Azure.Identity" Version="1.18.0" />
       <PackageVersion Include="Azure.Core" Version="1.51.1" />
       <PackageVersion Include="Microsoft.Extensions.Configuration" Version="10.0.3" />
       <PackageVersion Include="Microsoft.Extensions.Configuration.AzureAppConfiguration" Version="8.5.0" />
     </ItemGroup>
     <!--  -->
     <ItemGroup>
       <PackageVersion Include="System.Management" Version="10.0.2" />
     </ItemGroup>
     <!-- Registry key on Windows OS -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Win32.Registry" Version="5.0.0" />
     </ItemGroup>
     <!-- ACL on Windows OS -->
     <ItemGroup>
       <PackageVersion Include="System.Security.AccessControl" Version="6.0.1" />
     </ItemGroup>
     <!-- lock -->
     <ItemGroup>
       <PackageVersion Include="AsyncKeyedLock" Version="8.0.2" />
     </ItemGroup>
     <!-- EF Core -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.EntityFrameworkCore" Version="10.0.3" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="StackExchange.Redis" Version="2.11.0" />
     </ItemGroup>
     <!-- `Serilog` -->
     <ItemGroup>
       <PackageVersion Include="Serilog" Version="4.3.1" />
       <PackageVersion Include="Serilog.Settings.Configuration" Version="10.0.0" />
       <PackageVersion Include="Serilog.Enrichers.Environment" Version="3.0.1" />
       <PackageVersion Include="Serilog.Exceptions" Version="8.4.0" />
       <PackageVersion Include="Serilog.Enrichers.Demystifier" Version="1.0.3" />
       <PackageVersion Include="Serilog.Expressions" Version="5.0.0" />
       <PackageVersion Include="Serilog.Sinks.File" Version="7.0.0" />
       <PackageVersion Include="Serilog.Sinks.Console" Version="6.1.1" />
       <PackageVersion Include="Serilog.Extensions.Hosting" Version="10.0.0" />
       <PackageVersion Include="Serilog.Extensions.Logging" Version="10.0.0" />
     </ItemGroup>
     <!-- `Nuke` -->
     <ItemGroup>
       <PackageVersion Include="Nuke.Common" Version="10.1.0" />
     </ItemGroup>
     <!-- `PostSharp` -->
     <ItemGroup>
       <PackageVersion Include="PostSharp" Version="2026.0.6" />
       <PackageVersion Include="PostSharp.Patterns.Diagnostics" Version="2026.0.6" />
     </ItemGroup>
     <!-- `Appium.WebDriver` -->
     <ItemGroup>
       <PackageVersion Include="Appium.WebDriver" Version="8.1.0" />
       <PackageVersion Include="Selenium.WebDriver" Version="4.36.0" />
     </ItemGroup>
     <!-- `Polly` -->
     <ItemGroup>
       <PackageVersion Include="Polly" Version="8.6.6" />
       <PackageVersion Include="Polly.Core" Version="8.6.6" />
       <PackageVersion Include="Polly.RateLimiting" Version="8.6.6" />
       <PackageVersion Include="Polly.Extensions" Version="8.6.6" />
     </ItemGroup>
     <!-- other packages -->
     <ItemGroup>
       <PackageVersion Include="MiniExcel" Version="2.0.0-preview.2" />
       <PackageVersion Include="QRCoder" Version="1.7.0" />
       <PackageVersion Include="Otp.NET" Version="1.4.1" />
       <PackageVersion Include="Spectre.Console" Version="0.54.0" />
       <PackageVersion Include="MediatR" Version="14.0.0" />
     </ItemGroup>
     <!-- about command line (CLI)  -->
     <ItemGroup>
       <PackageVersion Include="CliWrap" Version="3.10.1" />
     </ItemGroup>
      <!-- about encoding -->
     <ItemGroup>
       <PackageVersion Include="System.Text.Encoding.CodePages" Version="10.0.7" />
     </ItemGroup>
     <!-- about sanitization -->
     <ItemGroup>
       <PackageVersion Include="System.Text.Encodings.Web" Version="10.0.7" />
     </ItemGroup>
     <!-- about file system globbing -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Extensions.FileSystemGlobbing" Version="10.0.7" />
     </ItemGroup>
     <!-- F# compiler and F# core library -->
     <ItemGroup>
       <PackageVersion Include="FSharp.Compiler.Service" Version="43.12.203" />
       <PackageVersion Include="FSharp.Core" Version="10.1.203" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Dumpify" Version="0.7.0" />
     </ItemGroup>
     <!-- About AhoCorasick algorithm -->
     <ItemGroup>
       <PackageVersion Include="NReco.Text.AhoCorasickDoubleArrayTrie" Version="1.1.1" />
     </ItemGroup>
     <!-- My developed NuGet packages -->
     <!-- -->
     <ItemGroup>
       <PackageVersion Include="AiUtility.AiBaseUtilityServices" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="AiUtility.ToolKits" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="AiUtility.GeminiKits" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="AiUtility.GeminiUtilityServices" Version="[3.0.0-preview-1.0.0]" />
     </ItemGroup>
     <!-- My developed NuGet packages -->
     <!-- Utility services about `enum` -->
     <ItemGroup>
       <PackageVersion Include="EnumUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <!-- my custom Attributes in data annotations that are commonly used -->
     <ItemGroup>
       <PackageVersion Include="CustomDataAnnotations" Version="2.4.0" />
     </ItemGroup>
     <!-- About type (System.Type) -->
     <ItemGroup>
       <PackageVersion Include="TypeConstants" Version="2.1.0" />
       <PackageVersion Include="MimeTypeConstants" Version="3.0.0" />
     </ItemGroup>
     <!-- Extension services that extends existing Exception (or its subclass) and defined my customized exceptions -->
     <ItemGroup>
       <PackageVersion Include="ExceptionsUtilityServices" Version="1.1.0" />
     </ItemGroup>
     <!-- My developed NuGet packages -->
     <!-- About Expression Tree-->
     <ItemGroup>
       <PackageVersion Include="ExpressionTreeUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <!-- About caching Reflection-->
     <ItemGroup>
       <PackageVersion Include="ReflectionUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="LogNameUtilityFactories" Version="[2.0.0-preview-1.0.0]" />
     </ItemGroup>
       <PackageVersion Include="RegexUtilityServices" Version="2.0.0" />
       <PackageVersion Include="AssemblyUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="ExceptionHandlingUtilityServices" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="ExceptionFactories" Version="2.0.0" />
       <PackageVersion Include="ExceptionWrappers" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="FileStreamUtilityServices" Version="2.1.0" />
       <PackageVersion Include="JsonUtilityServices" Version="2.0.0" />
       <PackageVersion Include="LoggerFactoryUtilityServices" Version="[5.0.0-preview-1.0.0]" />
       <PackageVersion Include="TaskUtilityServices" Version="2.0.0" />
       <PackageVersion Include="ThreadLevelLockingUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="TypeUtilityServices" Version="2.0.0" />
       <PackageVersion Include="SerilogHelperServices" Version="2.0.0" />
       <PackageVersion Include="CoordinateUtilityServices" Version="2.0.0" />
       <PackageVersion Include="TransversalUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="AzureUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="ListItemUitilityServices" Version="2.0.0" />
       <PackageVersion Include="MathUtilityServices" Version="2.0.0" />
       <PackageVersion Include="StringUtilityServices" Version="2.0.0" />
       <PackageVersion Include="NetRuntimeUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="ILoggerBuilderFactoryServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="LoggingCodeTemplateGenerators" Version="2.0.0" />
       <PackageVersion Include="IOOperationUtilityServices" Version="[5.0.0-preview-1.0.0]" />
       <PackageVersion Include="EntriesAndTheirContentsServices" Version="[1.0.0-preview-1.0.0]" />
       <PackageVersion Include="SortingUtilityServices" Version="1.0.1" />
       <PackageVersion Include="CommonModels" Version="2.1.0" />
       <PackageVersion Include="ConsoleUtilityServices" Version="1.0.0" />
       <PackageVersion Include="FileCategorizationUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <!-- About OS-->
     <ItemGroup>
       <!-- Get or check OS Environment-->
       <PackageVersion Include="EnvironmentUtilityServices" Version="3.0.0" />
       <!-- Get or check specific drive -->
       <PackageVersion Include="DriveInfoUtilityServices" Version="1.0.0-preview-1.0.0" />
       <!-- Utility services about symbolic links (based on low-level API)-->
       <PackageVersion Include="SymbolicLinkUtilityServices" Version="1.0.0-preview-1.0.0" />
     </ItemGroup>
     <!-- About cli -->
     <ItemGroup>
         <!-- wrapper or utility service based on `CliWrap` package -->
         <PackageVersion Include="CliUtilityServices" Version="[4.0.0-preview-1.0.0]" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="ProjectsVersionUtilityServices" Version="1.0.0" />
       <PackageVersion Include="SolutionUtilityServices" Version="[5.0.0-preview-1.0.0]" />
       <PackageVersion Include="PaketUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- check which programming languages are used -->
     <ItemGroup>
       <PackageVersion Include="ProgrammingLanguageUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- discovering script -->
     <ItemGroup>
       <PackageVersion Include="ScriptDiscoveryUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- Utility services that are shared (or commonly used) -->
     <ItemGroup>
       <PackageVersion Include="SharedUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- About Span or its related classes -->
     <ItemGroup>
       <PackageVersion Include="SpanUtilityServices" Version="[3.0.0-preview-1.0.0]" />
     </ItemGroup>
     <!-- Utility services that wraps searching file (similar to behavior of File Explorer) -->
     <ItemGroup>
       <PackageVersion Include="FileExplorerUtilityServices" Version="[1.0.0-preview-1.0.0]" />
     </ItemGroup>
     <!-- Wrapper of decimal to fraction (my custom defined class named `Fraction`) -->
     <ItemGroup>
       <PackageVersion Include="FractionsUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- Utility services that move one or more apps in Windows -->
     <ItemGroup>
       <PackageVersion Include="WindowsAppUtilityServices" Version="[4.0.0-preview-1.0.0]" />
     </ItemGroup>
   </Project>
   ```

   ## File: Examples/Ex1/Ex1-1/Ex1-1.slnx
   ```
   <Solution>
     <Project Path="MeProject1/MeProject1.csproj" />
     <Project Path="MeProject2/MeProject2.csproj" />
     <Project Path="ReflectionUtilityServices/ReflectionUtilityServices.csproj" />
   </Solution>
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/EntryPoint.cs
   ```csharp
   namespace MeProject1;

   public static class EntryPoint
   {
       public static void Main(string[] args)
       {
           HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
       }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/EntryPoint1.cs
   ```csharp
   namespace MeProject1;

   public static class EntryPoint1
   {
       public static void Main(string[] args)
       {
           Me me1 = new() { Password = "MyPassword" };
           me1.Name = "Me1";

           Son son1 = new();
       }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/ILogUtilityService.cs
   ```csharp
   namespace MeProject1;

   public interface ILogUtilityService
   {
       void LogValueOfMeInstance(Me me);
       void LogValueOfSonInstance(Son son);
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/LogUtilityService.cs
   ```csharp
   using ReflectionUtilityServices;

   namespace MeProject1;

   public class LogUtilityService : ILogUtilityService
   {
       private readonly IReflectionUtilityService _reflectionUtilityService;
       public LogUtilityService(
           IReflectionUtilityService reflectionUtilityService
       )
       {
           ArgumentNullException.ThrowIfNull(reflectionUtilityService, nameof(reflectionUtilityService));
           _reflectionUtilityService = reflectionUtilityService;
       }

       public void LogValueOfMeInstance(Me me)
       {
           Console.WriteLine("// ======================= LogValueOfMeInstance method ======================= //");
           Console.WriteLine(me.Name);
           Console.WriteLine(me.Account);
           Console.WriteLine(me.Password);
           Console.WriteLine(_reflectionUtilityService.GetPropertyValue(me, "Name"));
           Console.WriteLine(_reflectionUtilityService.GetPropertyValue(me, "Account"));
           Console.WriteLine(_reflectionUtilityService.GetPropertyValue(me, "Password"));
           Console.WriteLine("// ======================= [end] LogValueOfMeInstance method ======================= //");
       }

       public void LogValueOfSonInstance(Son son)
       {
           Console.WriteLine("// ======================= LogValueOfSonInstance method ======================= //");
           Console.WriteLine(son.Name);
           Console.WriteLine(son.Account);
           Console.WriteLine(son.Password);
           Console.WriteLine(_reflectionUtilityService.GetPropertyValue(son, "Name"));
           Console.WriteLine(_reflectionUtilityService.GetPropertyValue(son, "Account"));
           Console.WriteLine(_reflectionUtilityService.GetPropertyValue(son, "Password"));
           Console.WriteLine("// ======================= [end] LogValueOfSonInstance method ======================= //");
       }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/Me.cs
   ```csharp
   namespace MeProject1;

   public partial class Me
   {
       public string Name { get; set; }

       public string Account { get; private set; } = "Account";

       public string Password { get; init; } = "Password";

       private string _name { get; set; }
       private string _password { get; init; }

       protected string __name { get; set; }
       protected string __account { get; private set; }
       protected string __password { get; init; }

   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/MeProject1.csproj
   ```
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net10.0</TargetFramework>
       <ImplicitUsings>enable</ImplicitUsings>
       <Nullable>enable</Nullable>
     </PropertyGroup>
     <ItemGroup>
       <ProjectReference Include="..\ReflectionUtilityServices\ReflectionUtilityServices.csproj" />
     </ItemGroup>
     <ItemGroup>
         <PackageReference Include="Microsoft.Extensions.Logging" />
         <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" />
         <PackageReference Include="Microsoft.Extensions.Options" />
         <PackageReference Include="Microsoft.Extensions.DependencyInjection" />
         <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" />
         <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" />
         <PackageReference Include="Microsoft.Extensions.Primitives" />
         <PackageReference Include="Microsoft.Extensions.Hosting" />
         <PackageReference Include="Microsoft.Extensions.Hosting.Abstractions" />
       </ItemGroup>
   </Project>
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
   ```csharp
   // <autogenerated />
   using System;
   using System.Reflection;
   [assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v10.0", FrameworkDisplayName = ".NET 10.0")]
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.AssemblyInfo.cs
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

   [assembly: System.Reflection.AssemblyCompanyAttribute("MeProject1")]
   [assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
   [assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
   [assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0")]
   [assembly: System.Reflection.AssemblyProductAttribute("MeProject1")]
   [assembly: System.Reflection.AssemblyTitleAttribute("MeProject1")]
   [assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

   // Generated by the MSBuild WriteCodeFragment class.
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.AssemblyInfoInputs.cache
   ```
   c9d8a131ab3ddb0b9d4f08aa18a287f5d8bab61f2433b781345c91bb1228412d
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.GeneratedMSBuildEditorConfig.editorconfig
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
   build_property.RootNamespace = MeProject1
   build_property.ProjectDir = D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject1\
   build_property.EnableComHosting =
   build_property.EnableGeneratedComInterfaceComImportInterop =
   build_property.EffectiveAnalysisLevelStyle = 10.0
   build_property.EnableCodeStyleSeverity =
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/Debug/net10.0/MeProject1.GlobalUsings.g.cs
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

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/MeProject1.csproj.nuget.dgspec.json
   ```json
   {
     "format": 1,
     "restore": {
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj": {}
     },
     "projects": {
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj": {
         "version": "1.0.0",
         "restore": {
           "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
           "projectName": "MeProject1",
           "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
           "packagesPath": "D:\\DevTools\\nuget_cache",
           "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\obj\\",
           "projectStyle": "PackageReference",
           "fallbackFolders": [
             "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
           ],
           "configFilePaths": [
             "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
             "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
           ],
           "originalTargetFrameworks": [
             "net10.0"
           ],
           "sources": {
             "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
             "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
             "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
             "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
             "https://api.nuget.org/v3/index.json": {},
             "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
           },
           "frameworks": {
             "net10.0": {
               "targetAlias": "net10.0",
               "projectReferences": {
                 "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
                   "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj"
                 }
               }
             }
           },
           "warningProperties": {
             "warnAsError": [
               "NU1605"
             ]
           },
           "restoreAuditProperties": {
             "enableAudit": "true",
             "auditLevel": "low",
             "auditMode": "all"
           },
           "SdkAnalysisLevel": "10.0.100"
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "dependencies": {
               "Microsoft.Extensions.Configuration.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.DependencyInjection": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.DependencyInjection.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Hosting": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Hosting.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Logging": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Logging.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Options": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Primitives": {
                 "target": "Package",
                 "version": "(, )"
               }
             },
             "imports": [
               "net461",
               "net462",
               "net47",
               "net471",
               "net472",
               "net48",
               "net481"
             ],
             "assetTargetFallback": true,
             "warn": true,
             "frameworkReferences": {
               "Microsoft.NETCore.App": {
                 "privateAssets": "all"
               }
             },
             "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
             "packagesToPrune": {
               "Microsoft.CSharp": "(,4.7.32767]",
               "Microsoft.VisualBasic": "(,10.4.32767]",
               "Microsoft.Win32.Primitives": "(,4.3.32767]",
               "Microsoft.Win32.Registry": "(,5.0.32767]",
               "runtime.any.System.Collections": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.any.System.Globalization": "(,4.3.32767]",
               "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.any.System.IO": "(,4.3.32767]",
               "runtime.any.System.Reflection": "(,4.3.32767]",
               "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.any.System.Runtime": "(,4.3.32767]",
               "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.any.System.Threading.Timer": "(,4.3.32767]",
               "runtime.aot.System.Collections": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.aot.System.Globalization": "(,4.3.32767]",
               "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.aot.System.IO": "(,4.3.32767]",
               "runtime.aot.System.Reflection": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.aot.System.Runtime": "(,4.3.32767]",
               "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Console": "(,4.3.32767]",
               "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
               "runtime.unix.System.Private.Uri": "(,4.3.32767]",
               "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.win.System.Console": "(,4.3.32767]",
               "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.win.System.Net.Primitives": "(,4.3.32767]",
               "runtime.win.System.Net.Sockets": "(,4.3.32767]",
               "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7.System.Private.Uri": "(,4.3.32767]",
               "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "System.AppContext": "(,4.3.32767]",
               "System.Buffers": "(,5.0.32767]",
               "System.Collections": "(,4.3.32767]",
               "System.Collections.Concurrent": "(,4.3.32767]",
               "System.Collections.Immutable": "(,10.0.32767]",
               "System.Collections.NonGeneric": "(,4.3.32767]",
               "System.Collections.Specialized": "(,4.3.32767]",
               "System.ComponentModel": "(,4.3.32767]",
               "System.ComponentModel.Annotations": "(,4.3.32767]",
               "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
               "System.ComponentModel.Primitives": "(,4.3.32767]",
               "System.ComponentModel.TypeConverter": "(,4.3.32767]",
               "System.Console": "(,4.3.32767]",
               "System.Data.Common": "(,4.3.32767]",
               "System.Data.DataSetExtensions": "(,4.4.32767]",
               "System.Diagnostics.Contracts": "(,4.3.32767]",
               "System.Diagnostics.Debug": "(,4.3.32767]",
               "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
               "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
               "System.Diagnostics.Process": "(,4.3.32767]",
               "System.Diagnostics.StackTrace": "(,4.3.32767]",
               "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
               "System.Diagnostics.Tools": "(,4.3.32767]",
               "System.Diagnostics.TraceSource": "(,4.3.32767]",
               "System.Diagnostics.Tracing": "(,4.3.32767]",
               "System.Drawing.Primitives": "(,4.3.32767]",
               "System.Dynamic.Runtime": "(,4.3.32767]",
               "System.Formats.Asn1": "(,10.0.32767]",
               "System.Formats.Tar": "(,10.0.32767]",
               "System.Globalization": "(,4.3.32767]",
               "System.Globalization.Calendars": "(,4.3.32767]",
               "System.Globalization.Extensions": "(,4.3.32767]",
               "System.IO": "(,4.3.32767]",
               "System.IO.Compression": "(,4.3.32767]",
               "System.IO.Compression.ZipFile": "(,4.3.32767]",
               "System.IO.FileSystem": "(,4.3.32767]",
               "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
               "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
               "System.IO.FileSystem.Primitives": "(,4.3.32767]",
               "System.IO.FileSystem.Watcher": "(,4.3.32767]",
               "System.IO.IsolatedStorage": "(,4.3.32767]",
               "System.IO.MemoryMappedFiles": "(,4.3.32767]",
               "System.IO.Pipelines": "(,10.0.32767]",
               "System.IO.Pipes": "(,4.3.32767]",
               "System.IO.Pipes.AccessControl": "(,5.0.32767]",
               "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
               "System.Linq": "(,4.3.32767]",
               "System.Linq.AsyncEnumerable": "(,10.0.32767]",
               "System.Linq.Expressions": "(,4.3.32767]",
               "System.Linq.Parallel": "(,4.3.32767]",
               "System.Linq.Queryable": "(,4.3.32767]",
               "System.Memory": "(,5.0.32767]",
               "System.Net.Http": "(,4.3.32767]",
               "System.Net.Http.Json": "(,10.0.32767]",
               "System.Net.NameResolution": "(,4.3.32767]",
               "System.Net.NetworkInformation": "(,4.3.32767]",
               "System.Net.Ping": "(,4.3.32767]",
               "System.Net.Primitives": "(,4.3.32767]",
               "System.Net.Requests": "(,4.3.32767]",
               "System.Net.Security": "(,4.3.32767]",
               "System.Net.ServerSentEvents": "(,10.0.32767]",
               "System.Net.Sockets": "(,4.3.32767]",
               "System.Net.WebHeaderCollection": "(,4.3.32767]",
               "System.Net.WebSockets": "(,4.3.32767]",
               "System.Net.WebSockets.Client": "(,4.3.32767]",
               "System.Numerics.Vectors": "(,5.0.32767]",
               "System.ObjectModel": "(,4.3.32767]",
               "System.Private.DataContractSerialization": "(,4.3.32767]",
               "System.Private.Uri": "(,4.3.32767]",
               "System.Reflection": "(,4.3.32767]",
               "System.Reflection.DispatchProxy": "(,6.0.32767]",
               "System.Reflection.Emit": "(,4.7.32767]",
               "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
               "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
               "System.Reflection.Extensions": "(,4.3.32767]",
               "System.Reflection.Metadata": "(,10.0.32767]",
               "System.Reflection.Primitives": "(,4.3.32767]",
               "System.Reflection.TypeExtensions": "(,4.3.32767]",
               "System.Resources.Reader": "(,4.3.32767]",
               "System.Resources.ResourceManager": "(,4.3.32767]",
               "System.Resources.Writer": "(,4.3.32767]",
               "System.Runtime": "(,4.3.32767]",
               "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
               "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
               "System.Runtime.Extensions": "(,4.3.32767]",
               "System.Runtime.Handles": "(,4.3.32767]",
               "System.Runtime.InteropServices": "(,4.3.32767]",
               "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
               "System.Runtime.Loader": "(,4.3.32767]",
               "System.Runtime.Numerics": "(,4.3.32767]",
               "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
               "System.Runtime.Serialization.Json": "(,4.3.32767]",
               "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
               "System.Runtime.Serialization.Xml": "(,4.3.32767]",
               "System.Security.AccessControl": "(,6.0.32767]",
               "System.Security.Claims": "(,4.3.32767]",
               "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
               "System.Security.Cryptography.Cng": "(,5.0.32767]",
               "System.Security.Cryptography.Csp": "(,4.3.32767]",
               "System.Security.Cryptography.Encoding": "(,4.3.32767]",
               "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
               "System.Security.Cryptography.Primitives": "(,4.3.32767]",
               "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
               "System.Security.Principal": "(,4.3.32767]",
               "System.Security.Principal.Windows": "(,5.0.32767]",
               "System.Security.SecureString": "(,4.3.32767]",
               "System.Text.Encoding": "(,4.3.32767]",
               "System.Text.Encoding.CodePages": "(,10.0.32767]",
               "System.Text.Encoding.Extensions": "(,4.3.32767]",
               "System.Text.Encodings.Web": "(,10.0.32767]",
               "System.Text.Json": "(,10.0.32767]",
               "System.Text.RegularExpressions": "(,4.3.32767]",
               "System.Threading": "(,4.3.32767]",
               "System.Threading.AccessControl": "(,10.0.32767]",
               "System.Threading.Channels": "(,10.0.32767]",
               "System.Threading.Overlapped": "(,4.3.32767]",
               "System.Threading.Tasks": "(,4.3.32767]",
               "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
               "System.Threading.Tasks.Extensions": "(,5.0.32767]",
               "System.Threading.Tasks.Parallel": "(,4.3.32767]",
               "System.Threading.Thread": "(,4.3.32767]",
               "System.Threading.ThreadPool": "(,4.3.32767]",
               "System.Threading.Timer": "(,4.3.32767]",
               "System.ValueTuple": "(,4.5.32767]",
               "System.Xml.ReaderWriter": "(,4.3.32767]",
               "System.Xml.XDocument": "(,4.3.32767]",
               "System.Xml.XmlDocument": "(,4.3.32767]",
               "System.Xml.XmlSerializer": "(,4.3.32767]",
               "System.Xml.XPath": "(,4.3.32767]",
               "System.Xml.XPath.XDocument": "(,5.0.32767]"
             }
           }
         }
       },
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
         "version": "1.0.0",
         "restore": {
           "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
           "projectName": "ReflectionUtilityServices",
           "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
           "packagesPath": "D:\\DevTools\\nuget_cache",
           "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\obj\\",
           "projectStyle": "PackageReference",
           "fallbackFolders": [
             "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
           ],
           "configFilePaths": [
             "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
             "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
           ],
           "originalTargetFrameworks": [
             "net10.0"
           ],
           "sources": {
             "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
             "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
             "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
             "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
             "https://api.nuget.org/v3/index.json": {},
             "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
           },
           "frameworks": {
             "net10.0": {
               "targetAlias": "net10.0",
               "projectReferences": {}
             }
           },
           "warningProperties": {
             "warnAsError": [
               "NU1605"
             ]
           },
           "restoreAuditProperties": {
             "enableAudit": "true",
             "auditLevel": "low",
             "auditMode": "all"
           },
           "SdkAnalysisLevel": "10.0.100"
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "imports": [
               "net461",
               "net462",
               "net47",
               "net471",
               "net472",
               "net48",
               "net481"
             ],
             "assetTargetFallback": true,
             "warn": true,
             "frameworkReferences": {
               "Microsoft.NETCore.App": {
                 "privateAssets": "all"
               }
             },
             "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
             "packagesToPrune": {
               "Microsoft.CSharp": "(,4.7.32767]",
               "Microsoft.VisualBasic": "(,10.4.32767]",
               "Microsoft.Win32.Primitives": "(,4.3.32767]",
               "Microsoft.Win32.Registry": "(,5.0.32767]",
               "runtime.any.System.Collections": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.any.System.Globalization": "(,4.3.32767]",
               "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.any.System.IO": "(,4.3.32767]",
               "runtime.any.System.Reflection": "(,4.3.32767]",
               "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.any.System.Runtime": "(,4.3.32767]",
               "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.any.System.Threading.Timer": "(,4.3.32767]",
               "runtime.aot.System.Collections": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.aot.System.Globalization": "(,4.3.32767]",
               "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.aot.System.IO": "(,4.3.32767]",
               "runtime.aot.System.Reflection": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.aot.System.Runtime": "(,4.3.32767]",
               "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Console": "(,4.3.32767]",
               "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
               "runtime.unix.System.Private.Uri": "(,4.3.32767]",
               "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.win.System.Console": "(,4.3.32767]",
               "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.win.System.Net.Primitives": "(,4.3.32767]",
               "runtime.win.System.Net.Sockets": "(,4.3.32767]",
               "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7.System.Private.Uri": "(,4.3.32767]",
               "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "System.AppContext": "(,4.3.32767]",
               "System.Buffers": "(,5.0.32767]",
               "System.Collections": "(,4.3.32767]",
               "System.Collections.Concurrent": "(,4.3.32767]",
               "System.Collections.Immutable": "(,10.0.32767]",
               "System.Collections.NonGeneric": "(,4.3.32767]",
               "System.Collections.Specialized": "(,4.3.32767]",
               "System.ComponentModel": "(,4.3.32767]",
               "System.ComponentModel.Annotations": "(,4.3.32767]",
               "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
               "System.ComponentModel.Primitives": "(,4.3.32767]",
               "System.ComponentModel.TypeConverter": "(,4.3.32767]",
               "System.Console": "(,4.3.32767]",
               "System.Data.Common": "(,4.3.32767]",
               "System.Data.DataSetExtensions": "(,4.4.32767]",
               "System.Diagnostics.Contracts": "(,4.3.32767]",
               "System.Diagnostics.Debug": "(,4.3.32767]",
               "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
               "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
               "System.Diagnostics.Process": "(,4.3.32767]",
               "System.Diagnostics.StackTrace": "(,4.3.32767]",
               "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
               "System.Diagnostics.Tools": "(,4.3.32767]",
               "System.Diagnostics.TraceSource": "(,4.3.32767]",
               "System.Diagnostics.Tracing": "(,4.3.32767]",
               "System.Drawing.Primitives": "(,4.3.32767]",
               "System.Dynamic.Runtime": "(,4.3.32767]",
               "System.Formats.Asn1": "(,10.0.32767]",
               "System.Formats.Tar": "(,10.0.32767]",
               "System.Globalization": "(,4.3.32767]",
               "System.Globalization.Calendars": "(,4.3.32767]",
               "System.Globalization.Extensions": "(,4.3.32767]",
               "System.IO": "(,4.3.32767]",
               "System.IO.Compression": "(,4.3.32767]",
               "System.IO.Compression.ZipFile": "(,4.3.32767]",
               "System.IO.FileSystem": "(,4.3.32767]",
               "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
               "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
               "System.IO.FileSystem.Primitives": "(,4.3.32767]",
               "System.IO.FileSystem.Watcher": "(,4.3.32767]",
               "System.IO.IsolatedStorage": "(,4.3.32767]",
               "System.IO.MemoryMappedFiles": "(,4.3.32767]",
               "System.IO.Pipelines": "(,10.0.32767]",
               "System.IO.Pipes": "(,4.3.32767]",
               "System.IO.Pipes.AccessControl": "(,5.0.32767]",
               "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
               "System.Linq": "(,4.3.32767]",
               "System.Linq.AsyncEnumerable": "(,10.0.32767]",
               "System.Linq.Expressions": "(,4.3.32767]",
               "System.Linq.Parallel": "(,4.3.32767]",
               "System.Linq.Queryable": "(,4.3.32767]",
               "System.Memory": "(,5.0.32767]",
               "System.Net.Http": "(,4.3.32767]",
               "System.Net.Http.Json": "(,10.0.32767]",
               "System.Net.NameResolution": "(,4.3.32767]",
               "System.Net.NetworkInformation": "(,4.3.32767]",
               "System.Net.Ping": "(,4.3.32767]",
               "System.Net.Primitives": "(,4.3.32767]",
               "System.Net.Requests": "(,4.3.32767]",
               "System.Net.Security": "(,4.3.32767]",
               "System.Net.ServerSentEvents": "(,10.0.32767]",
               "System.Net.Sockets": "(,4.3.32767]",
               "System.Net.WebHeaderCollection": "(,4.3.32767]",
               "System.Net.WebSockets": "(,4.3.32767]",
               "System.Net.WebSockets.Client": "(,4.3.32767]",
               "System.Numerics.Vectors": "(,5.0.32767]",
               "System.ObjectModel": "(,4.3.32767]",
               "System.Private.DataContractSerialization": "(,4.3.32767]",
               "System.Private.Uri": "(,4.3.32767]",
               "System.Reflection": "(,4.3.32767]",
               "System.Reflection.DispatchProxy": "(,6.0.32767]",
               "System.Reflection.Emit": "(,4.7.32767]",
               "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
               "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
               "System.Reflection.Extensions": "(,4.3.32767]",
               "System.Reflection.Metadata": "(,10.0.32767]",
               "System.Reflection.Primitives": "(,4.3.32767]",
               "System.Reflection.TypeExtensions": "(,4.3.32767]",
               "System.Resources.Reader": "(,4.3.32767]",
               "System.Resources.ResourceManager": "(,4.3.32767]",
               "System.Resources.Writer": "(,4.3.32767]",
               "System.Runtime": "(,4.3.32767]",
               "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
               "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
               "System.Runtime.Extensions": "(,4.3.32767]",
               "System.Runtime.Handles": "(,4.3.32767]",
               "System.Runtime.InteropServices": "(,4.3.32767]",
               "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
               "System.Runtime.Loader": "(,4.3.32767]",
               "System.Runtime.Numerics": "(,4.3.32767]",
               "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
               "System.Runtime.Serialization.Json": "(,4.3.32767]",
               "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
               "System.Runtime.Serialization.Xml": "(,4.3.32767]",
               "System.Security.AccessControl": "(,6.0.32767]",
               "System.Security.Claims": "(,4.3.32767]",
               "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
               "System.Security.Cryptography.Cng": "(,5.0.32767]",
               "System.Security.Cryptography.Csp": "(,4.3.32767]",
               "System.Security.Cryptography.Encoding": "(,4.3.32767]",
               "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
               "System.Security.Cryptography.Primitives": "(,4.3.32767]",
               "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
               "System.Security.Principal": "(,4.3.32767]",
               "System.Security.Principal.Windows": "(,5.0.32767]",
               "System.Security.SecureString": "(,4.3.32767]",
               "System.Text.Encoding": "(,4.3.32767]",
               "System.Text.Encoding.CodePages": "(,10.0.32767]",
               "System.Text.Encoding.Extensions": "(,4.3.32767]",
               "System.Text.Encodings.Web": "(,10.0.32767]",
               "System.Text.Json": "(,10.0.32767]",
               "System.Text.RegularExpressions": "(,4.3.32767]",
               "System.Threading": "(,4.3.32767]",
               "System.Threading.AccessControl": "(,10.0.32767]",
               "System.Threading.Channels": "(,10.0.32767]",
               "System.Threading.Overlapped": "(,4.3.32767]",
               "System.Threading.Tasks": "(,4.3.32767]",
               "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
               "System.Threading.Tasks.Extensions": "(,5.0.32767]",
               "System.Threading.Tasks.Parallel": "(,4.3.32767]",
               "System.Threading.Thread": "(,4.3.32767]",
               "System.Threading.ThreadPool": "(,4.3.32767]",
               "System.Threading.Timer": "(,4.3.32767]",
               "System.ValueTuple": "(,4.5.32767]",
               "System.Xml.ReaderWriter": "(,4.3.32767]",
               "System.Xml.XDocument": "(,4.3.32767]",
               "System.Xml.XmlDocument": "(,4.3.32767]",
               "System.Xml.XmlSerializer": "(,4.3.32767]",
               "System.Xml.XPath": "(,4.3.32767]",
               "System.Xml.XPath.XDocument": "(,5.0.32767]"
             }
           }
         }
       }
     }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/MeProject1.csproj.nuget.g.props
   ```
   <?xml version="1.0" encoding="utf-8" standalone="no"?>
   <Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
     <PropertyGroup Condition=" '$(ExcludeRestorePackageImports)' != 'true' ">
       <RestoreSuccess Condition=" '$(RestoreSuccess)' == '' ">False</RestoreSuccess>
       <RestoreTool Condition=" '$(RestoreTool)' == '' ">NuGet</RestoreTool>
       <ProjectAssetsFile Condition=" '$(ProjectAssetsFile)' == '' ">$(MSBuildThisFileDirectory)project.assets.json</ProjectAssetsFile>
       <NuGetPackageRoot Condition=" '$(NuGetPackageRoot)' == '' ">D:\DevTools\nuget_cache</NuGetPackageRoot>
       <NuGetPackageFolders Condition=" '$(NuGetPackageFolders)' == '' ">D:\DevTools\nuget_cache;D:\Program Files\Microsoft Visual Studio\Shared\NuGetPackages</NuGetPackageFolders>
       <NuGetProjectStyle Condition=" '$(NuGetProjectStyle)' == '' ">PackageReference</NuGetProjectStyle>
       <NuGetToolVersion Condition=" '$(NuGetToolVersion)' == '' ">7.0.0</NuGetToolVersion>
     </PropertyGroup>
     <ItemGroup Condition=" '$(ExcludeRestorePackageImports)' != 'true' ">
       <SourceRoot Include="D:\DevTools\nuget_cache\" />
       <SourceRoot Include="D:\Program Files\Microsoft Visual Studio\Shared\NuGetPackages\" />
     </ItemGroup>
   </Project>
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/MeProject1.csproj.nuget.g.targets
   ```
   <?xml version="1.0" encoding="utf-8" standalone="no"?>
   <Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003" />
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/project.assets.json
   ```json
   {
     "version": 3,
     "targets": {
       "net10.0": {}
     },
     "libraries": {},
     "projectFileDependencyGroups": {
       "net10.0": [
         "Microsoft.Extensions.Configuration.Abstractions",
         "Microsoft.Extensions.DependencyInjection",
         "Microsoft.Extensions.DependencyInjection.Abstractions",
         "Microsoft.Extensions.Hosting",
         "Microsoft.Extensions.Hosting.Abstractions",
         "Microsoft.Extensions.Logging",
         "Microsoft.Extensions.Logging.Abstractions",
         "Microsoft.Extensions.Options",
         "Microsoft.Extensions.Primitives"
       ]
     },
     "packageFolders": {
       "D:\\DevTools\\nuget_cache": {},
       "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages": {}
     },
     "project": {
       "version": "1.0.0",
       "restore": {
         "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
         "projectName": "MeProject1",
         "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
         "packagesPath": "D:\\DevTools\\nuget_cache",
         "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\obj\\",
         "projectStyle": "PackageReference",
         "fallbackFolders": [
           "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
         ],
         "configFilePaths": [
           "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
           "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
           "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
           "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
         ],
         "originalTargetFrameworks": [
           "net10.0"
         ],
         "sources": {
           "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
           "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
           "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
           "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
           "https://api.nuget.org/v3/index.json": {},
           "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "projectReferences": {
               "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
                 "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj"
               }
             }
           }
         },
         "warningProperties": {
           "warnAsError": [
             "NU1605"
           ]
         },
         "restoreAuditProperties": {
           "enableAudit": "true",
           "auditLevel": "low",
           "auditMode": "all"
         },
         "SdkAnalysisLevel": "10.0.100"
       },
       "frameworks": {
         "net10.0": {
           "targetAlias": "net10.0",
           "dependencies": {
             "Microsoft.Extensions.Configuration.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.DependencyInjection": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.DependencyInjection.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Hosting": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Hosting.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Logging": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Logging.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Options": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Primitives": {
               "target": "Package",
               "version": "(, )"
             }
           },
           "imports": [
             "net461",
             "net462",
             "net47",
             "net471",
             "net472",
             "net48",
             "net481"
           ],
           "assetTargetFallback": true,
           "warn": true,
           "frameworkReferences": {
             "Microsoft.NETCore.App": {
               "privateAssets": "all"
             }
           },
           "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
           "packagesToPrune": {
             "Microsoft.CSharp": "(,4.7.32767]",
             "Microsoft.VisualBasic": "(,10.4.32767]",
             "Microsoft.Win32.Primitives": "(,4.3.32767]",
             "Microsoft.Win32.Registry": "(,5.0.32767]",
             "runtime.any.System.Collections": "(,4.3.32767]",
             "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
             "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
             "runtime.any.System.Globalization": "(,4.3.32767]",
             "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
             "runtime.any.System.IO": "(,4.3.32767]",
             "runtime.any.System.Reflection": "(,4.3.32767]",
             "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
             "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
             "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
             "runtime.any.System.Runtime": "(,4.3.32767]",
             "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
             "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
             "runtime.any.System.Text.Encoding": "(,4.3.32767]",
             "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
             "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
             "runtime.any.System.Threading.Timer": "(,4.3.32767]",
             "runtime.aot.System.Collections": "(,4.3.32767]",
             "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
             "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
             "runtime.aot.System.Globalization": "(,4.3.32767]",
             "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
             "runtime.aot.System.IO": "(,4.3.32767]",
             "runtime.aot.System.Reflection": "(,4.3.32767]",
             "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
             "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
             "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
             "runtime.aot.System.Runtime": "(,4.3.32767]",
             "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
             "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
             "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
             "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
             "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
             "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
             "runtime.unix.System.Console": "(,4.3.32767]",
             "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
             "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
             "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
             "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
             "runtime.unix.System.Private.Uri": "(,4.3.32767]",
             "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
             "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
             "runtime.win.System.Console": "(,4.3.32767]",
             "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
             "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
             "runtime.win.System.Net.Primitives": "(,4.3.32767]",
             "runtime.win.System.Net.Sockets": "(,4.3.32767]",
             "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
             "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win7.System.Private.Uri": "(,4.3.32767]",
             "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "System.AppContext": "(,4.3.32767]",
             "System.Buffers": "(,5.0.32767]",
             "System.Collections": "(,4.3.32767]",
             "System.Collections.Concurrent": "(,4.3.32767]",
             "System.Collections.Immutable": "(,10.0.32767]",
             "System.Collections.NonGeneric": "(,4.3.32767]",
             "System.Collections.Specialized": "(,4.3.32767]",
             "System.ComponentModel": "(,4.3.32767]",
             "System.ComponentModel.Annotations": "(,4.3.32767]",
             "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
             "System.ComponentModel.Primitives": "(,4.3.32767]",
             "System.ComponentModel.TypeConverter": "(,4.3.32767]",
             "System.Console": "(,4.3.32767]",
             "System.Data.Common": "(,4.3.32767]",
             "System.Data.DataSetExtensions": "(,4.4.32767]",
             "System.Diagnostics.Contracts": "(,4.3.32767]",
             "System.Diagnostics.Debug": "(,4.3.32767]",
             "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
             "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
             "System.Diagnostics.Process": "(,4.3.32767]",
             "System.Diagnostics.StackTrace": "(,4.3.32767]",
             "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
             "System.Diagnostics.Tools": "(,4.3.32767]",
             "System.Diagnostics.TraceSource": "(,4.3.32767]",
             "System.Diagnostics.Tracing": "(,4.3.32767]",
             "System.Drawing.Primitives": "(,4.3.32767]",
             "System.Dynamic.Runtime": "(,4.3.32767]",
             "System.Formats.Asn1": "(,10.0.32767]",
             "System.Formats.Tar": "(,10.0.32767]",
             "System.Globalization": "(,4.3.32767]",
             "System.Globalization.Calendars": "(,4.3.32767]",
             "System.Globalization.Extensions": "(,4.3.32767]",
             "System.IO": "(,4.3.32767]",
             "System.IO.Compression": "(,4.3.32767]",
             "System.IO.Compression.ZipFile": "(,4.3.32767]",
             "System.IO.FileSystem": "(,4.3.32767]",
             "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
             "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
             "System.IO.FileSystem.Primitives": "(,4.3.32767]",
             "System.IO.FileSystem.Watcher": "(,4.3.32767]",
             "System.IO.IsolatedStorage": "(,4.3.32767]",
             "System.IO.MemoryMappedFiles": "(,4.3.32767]",
             "System.IO.Pipelines": "(,10.0.32767]",
             "System.IO.Pipes": "(,4.3.32767]",
             "System.IO.Pipes.AccessControl": "(,5.0.32767]",
             "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
             "System.Linq": "(,4.3.32767]",
             "System.Linq.AsyncEnumerable": "(,10.0.32767]",
             "System.Linq.Expressions": "(,4.3.32767]",
             "System.Linq.Parallel": "(,4.3.32767]",
             "System.Linq.Queryable": "(,4.3.32767]",
             "System.Memory": "(,5.0.32767]",
             "System.Net.Http": "(,4.3.32767]",
             "System.Net.Http.Json": "(,10.0.32767]",
             "System.Net.NameResolution": "(,4.3.32767]",
             "System.Net.NetworkInformation": "(,4.3.32767]",
             "System.Net.Ping": "(,4.3.32767]",
             "System.Net.Primitives": "(,4.3.32767]",
             "System.Net.Requests": "(,4.3.32767]",
             "System.Net.Security": "(,4.3.32767]",
             "System.Net.ServerSentEvents": "(,10.0.32767]",
             "System.Net.Sockets": "(,4.3.32767]",
             "System.Net.WebHeaderCollection": "(,4.3.32767]",
             "System.Net.WebSockets": "(,4.3.32767]",
             "System.Net.WebSockets.Client": "(,4.3.32767]",
             "System.Numerics.Vectors": "(,5.0.32767]",
             "System.ObjectModel": "(,4.3.32767]",
             "System.Private.DataContractSerialization": "(,4.3.32767]",
             "System.Private.Uri": "(,4.3.32767]",
             "System.Reflection": "(,4.3.32767]",
             "System.Reflection.DispatchProxy": "(,6.0.32767]",
             "System.Reflection.Emit": "(,4.7.32767]",
             "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
             "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
             "System.Reflection.Extensions": "(,4.3.32767]",
             "System.Reflection.Metadata": "(,10.0.32767]",
             "System.Reflection.Primitives": "(,4.3.32767]",
             "System.Reflection.TypeExtensions": "(,4.3.32767]",
             "System.Resources.Reader": "(,4.3.32767]",
             "System.Resources.ResourceManager": "(,4.3.32767]",
             "System.Resources.Writer": "(,4.3.32767]",
             "System.Runtime": "(,4.3.32767]",
             "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
             "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
             "System.Runtime.Extensions": "(,4.3.32767]",
             "System.Runtime.Handles": "(,4.3.32767]",
             "System.Runtime.InteropServices": "(,4.3.32767]",
             "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
             "System.Runtime.Loader": "(,4.3.32767]",
             "System.Runtime.Numerics": "(,4.3.32767]",
             "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
             "System.Runtime.Serialization.Json": "(,4.3.32767]",
             "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
             "System.Runtime.Serialization.Xml": "(,4.3.32767]",
             "System.Security.AccessControl": "(,6.0.32767]",
             "System.Security.Claims": "(,4.3.32767]",
             "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
             "System.Security.Cryptography.Cng": "(,5.0.32767]",
             "System.Security.Cryptography.Csp": "(,4.3.32767]",
             "System.Security.Cryptography.Encoding": "(,4.3.32767]",
             "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
             "System.Security.Cryptography.Primitives": "(,4.3.32767]",
             "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
             "System.Security.Principal": "(,4.3.32767]",
             "System.Security.Principal.Windows": "(,5.0.32767]",
             "System.Security.SecureString": "(,4.3.32767]",
             "System.Text.Encoding": "(,4.3.32767]",
             "System.Text.Encoding.CodePages": "(,10.0.32767]",
             "System.Text.Encoding.Extensions": "(,4.3.32767]",
             "System.Text.Encodings.Web": "(,10.0.32767]",
             "System.Text.Json": "(,10.0.32767]",
             "System.Text.RegularExpressions": "(,4.3.32767]",
             "System.Threading": "(,4.3.32767]",
             "System.Threading.AccessControl": "(,10.0.32767]",
             "System.Threading.Channels": "(,10.0.32767]",
             "System.Threading.Overlapped": "(,4.3.32767]",
             "System.Threading.Tasks": "(,4.3.32767]",
             "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
             "System.Threading.Tasks.Extensions": "(,5.0.32767]",
             "System.Threading.Tasks.Parallel": "(,4.3.32767]",
             "System.Threading.Thread": "(,4.3.32767]",
             "System.Threading.ThreadPool": "(,4.3.32767]",
             "System.Threading.Timer": "(,4.3.32767]",
             "System.ValueTuple": "(,4.5.32767]",
             "System.Xml.ReaderWriter": "(,4.3.32767]",
             "System.Xml.XDocument": "(,4.3.32767]",
             "System.Xml.XmlDocument": "(,4.3.32767]",
             "System.Xml.XmlSerializer": "(,4.3.32767]",
             "System.Xml.XPath": "(,4.3.32767]",
             "System.Xml.XPath.XDocument": "(,5.0.32767]"
           }
         }
       }
     },
     "logs": [
       {
         "code": "NU1015",
         "level": "Error",
         "message": "The following PackageReference item(s) do not have a version specified: Microsoft.Extensions.Configuration.Abstractions, Microsoft.Extensions.DependencyInjection, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.Extensions.Hosting, Microsoft.Extensions.Hosting.Abstractions, Microsoft.Extensions.Logging, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.Options, Microsoft.Extensions.Primitives"
       }
     ]
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/obj/project.nuget.cache
   ```
   {
     "version": 2,
     "dgSpecHash": "O6XCDS8SU/A=",
     "success": false,
     "projectFilePath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
     "expectedPackageFiles": [],
     "logs": [
       {
         "code": "NU1015",
         "level": "Error",
         "message": "The following PackageReference item(s) do not have a version specified: Microsoft.Extensions.Configuration.Abstractions, Microsoft.Extensions.DependencyInjection, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.Extensions.Hosting, Microsoft.Extensions.Hosting.Abstractions, Microsoft.Extensions.Logging, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.Options, Microsoft.Extensions.Primitives",
         "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
         "filePath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject1\\MeProject1.csproj",
         "targetGraphs": []
       }
     ]
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject1/Son.cs
   ```csharp
   namespace MeProject1;

   public partial class Son : Me
   {
       public string _name { get; set; }
       public string _account { get; set; }
       public string _password { get; set; }
       public string Get_Name(){ return _name; }
       public string get_Name(){ return _name; }
       public void Set_Name(){}
       public void set_Name(){}

       public string Get_Account(){ return _account; }
       public string get_Account(){ return _account; }
       public void Set_Account(){}
       public void set_Account(){}

       public string Get_Password(){ return _password; }
       public string get_Password(){ return _password; }
       public void Set_Password(){}
       public void set_Password(){}

       public string Get__Name(){ return _name; }
       public string get__Name(){ return _name; }
       public void Set__Name(){}
       public void set__Name(){}

       public string Get__Password(){ return _password; }
       public string get__Password(){ return _password; }
       public void Set__Password(){}
       public void set__Password(){}

       public string Get___Name(){ return _name; }
       public string get___Name(){ return _name; }
       public void Set___Name(){}
       public void set___Name(){}

       public string Get___Account(){ return _account; }
       public string get___Account(){ return _account; }
       public void Set___Account(){}
       public void set___Account(){}

       public string Get___Password(){ return _password; }
       public string get___Password(){ return _password; }
       public void Set___Password(){}
       public void set___Password(){}
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/Me.cs
   ```csharp
   namespace MeProject2;

   public partial class Me
   {
       public string Name { get; set; }

       public string Account { get; private set; } = "Account";

       public string Password { get; init; }

       private string _name { get; set; }
       private string _password { get; init; }

       protected string __name { get; set; }
       protected string __account { get; private set; }
       protected string __password { get; init; }

   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/MeProject2.csproj
   ```
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net10.0</TargetFramework>
       <ImplicitUsings>enable</ImplicitUsings>
       <Nullable>enable</Nullable>
     </PropertyGroup>
       <ItemGroup>
         <ProjectReference Include="..\ReflectionUtilityServices\ReflectionUtilityServices.csproj" />
       </ItemGroup>
       <ItemGroup>
         <PackageReference Include="Microsoft.Extensions.Logging" />
         <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" />
         <PackageReference Include="Microsoft.Extensions.Options" />
         <PackageReference Include="Microsoft.Extensions.DependencyInjection" />
         <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" />
         <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" />
         <PackageReference Include="Microsoft.Extensions.Primitives" />
         <PackageReference Include="Microsoft.Extensions.Hosting" />
         <PackageReference Include="Microsoft.Extensions.Hosting.Abstractions" />
       </ItemGroup>
   </Project>
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
   ```csharp
   // <autogenerated />
   using System;
   using System.Reflection;
   [assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v10.0", FrameworkDisplayName = ".NET 10.0")]
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.AssemblyInfo.cs
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

   [assembly: System.Reflection.AssemblyCompanyAttribute("MeProject2")]
   [assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
   [assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
   [assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0")]
   [assembly: System.Reflection.AssemblyProductAttribute("MeProject2")]
   [assembly: System.Reflection.AssemblyTitleAttribute("MeProject2")]
   [assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

   // Generated by the MSBuild WriteCodeFragment class.
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.AssemblyInfoInputs.cache
   ```
   d9af4b7e2659256bff70d2ee08e76778208783414a3aeceba59c61d57cc3eb55
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.GeneratedMSBuildEditorConfig.editorconfig
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
   build_property.RootNamespace = MeProject2
   build_property.ProjectDir = D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\MeProject2\
   build_property.EnableComHosting =
   build_property.EnableGeneratedComInterfaceComImportInterop =
   build_property.EffectiveAnalysisLevelStyle = 10.0
   build_property.EnableCodeStyleSeverity =
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/Debug/net10.0/MeProject2.GlobalUsings.g.cs
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

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/MeProject2.csproj.nuget.dgspec.json
   ```json
   {
     "format": 1,
     "restore": {
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj": {}
     },
     "projects": {
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj": {
         "version": "1.0.0",
         "restore": {
           "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
           "projectName": "MeProject2",
           "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
           "packagesPath": "D:\\DevTools\\nuget_cache",
           "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\obj\\",
           "projectStyle": "PackageReference",
           "fallbackFolders": [
             "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
           ],
           "configFilePaths": [
             "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
             "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
           ],
           "originalTargetFrameworks": [
             "net10.0"
           ],
           "sources": {
             "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
             "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
             "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
             "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
             "https://api.nuget.org/v3/index.json": {},
             "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
           },
           "frameworks": {
             "net10.0": {
               "targetAlias": "net10.0",
               "projectReferences": {
                 "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
                   "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj"
                 }
               }
             }
           },
           "warningProperties": {
             "warnAsError": [
               "NU1605"
             ]
           },
           "restoreAuditProperties": {
             "enableAudit": "true",
             "auditLevel": "low",
             "auditMode": "all"
           },
           "SdkAnalysisLevel": "10.0.100"
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "dependencies": {
               "Microsoft.Extensions.Configuration.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.DependencyInjection": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.DependencyInjection.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Hosting": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Hosting.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Logging": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Logging.Abstractions": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Options": {
                 "target": "Package",
                 "version": "(, )"
               },
               "Microsoft.Extensions.Primitives": {
                 "target": "Package",
                 "version": "(, )"
               }
             },
             "imports": [
               "net461",
               "net462",
               "net47",
               "net471",
               "net472",
               "net48",
               "net481"
             ],
             "assetTargetFallback": true,
             "warn": true,
             "frameworkReferences": {
               "Microsoft.NETCore.App": {
                 "privateAssets": "all"
               }
             },
             "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
             "packagesToPrune": {
               "Microsoft.CSharp": "(,4.7.32767]",
               "Microsoft.VisualBasic": "(,10.4.32767]",
               "Microsoft.Win32.Primitives": "(,4.3.32767]",
               "Microsoft.Win32.Registry": "(,5.0.32767]",
               "runtime.any.System.Collections": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.any.System.Globalization": "(,4.3.32767]",
               "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.any.System.IO": "(,4.3.32767]",
               "runtime.any.System.Reflection": "(,4.3.32767]",
               "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.any.System.Runtime": "(,4.3.32767]",
               "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.any.System.Threading.Timer": "(,4.3.32767]",
               "runtime.aot.System.Collections": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.aot.System.Globalization": "(,4.3.32767]",
               "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.aot.System.IO": "(,4.3.32767]",
               "runtime.aot.System.Reflection": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.aot.System.Runtime": "(,4.3.32767]",
               "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Console": "(,4.3.32767]",
               "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
               "runtime.unix.System.Private.Uri": "(,4.3.32767]",
               "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.win.System.Console": "(,4.3.32767]",
               "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.win.System.Net.Primitives": "(,4.3.32767]",
               "runtime.win.System.Net.Sockets": "(,4.3.32767]",
               "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7.System.Private.Uri": "(,4.3.32767]",
               "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "System.AppContext": "(,4.3.32767]",
               "System.Buffers": "(,5.0.32767]",
               "System.Collections": "(,4.3.32767]",
               "System.Collections.Concurrent": "(,4.3.32767]",
               "System.Collections.Immutable": "(,10.0.32767]",
               "System.Collections.NonGeneric": "(,4.3.32767]",
               "System.Collections.Specialized": "(,4.3.32767]",
               "System.ComponentModel": "(,4.3.32767]",
               "System.ComponentModel.Annotations": "(,4.3.32767]",
               "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
               "System.ComponentModel.Primitives": "(,4.3.32767]",
               "System.ComponentModel.TypeConverter": "(,4.3.32767]",
               "System.Console": "(,4.3.32767]",
               "System.Data.Common": "(,4.3.32767]",
               "System.Data.DataSetExtensions": "(,4.4.32767]",
               "System.Diagnostics.Contracts": "(,4.3.32767]",
               "System.Diagnostics.Debug": "(,4.3.32767]",
               "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
               "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
               "System.Diagnostics.Process": "(,4.3.32767]",
               "System.Diagnostics.StackTrace": "(,4.3.32767]",
               "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
               "System.Diagnostics.Tools": "(,4.3.32767]",
               "System.Diagnostics.TraceSource": "(,4.3.32767]",
               "System.Diagnostics.Tracing": "(,4.3.32767]",
               "System.Drawing.Primitives": "(,4.3.32767]",
               "System.Dynamic.Runtime": "(,4.3.32767]",
               "System.Formats.Asn1": "(,10.0.32767]",
               "System.Formats.Tar": "(,10.0.32767]",
               "System.Globalization": "(,4.3.32767]",
               "System.Globalization.Calendars": "(,4.3.32767]",
               "System.Globalization.Extensions": "(,4.3.32767]",
               "System.IO": "(,4.3.32767]",
               "System.IO.Compression": "(,4.3.32767]",
               "System.IO.Compression.ZipFile": "(,4.3.32767]",
               "System.IO.FileSystem": "(,4.3.32767]",
               "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
               "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
               "System.IO.FileSystem.Primitives": "(,4.3.32767]",
               "System.IO.FileSystem.Watcher": "(,4.3.32767]",
               "System.IO.IsolatedStorage": "(,4.3.32767]",
               "System.IO.MemoryMappedFiles": "(,4.3.32767]",
               "System.IO.Pipelines": "(,10.0.32767]",
               "System.IO.Pipes": "(,4.3.32767]",
               "System.IO.Pipes.AccessControl": "(,5.0.32767]",
               "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
               "System.Linq": "(,4.3.32767]",
               "System.Linq.AsyncEnumerable": "(,10.0.32767]",
               "System.Linq.Expressions": "(,4.3.32767]",
               "System.Linq.Parallel": "(,4.3.32767]",
               "System.Linq.Queryable": "(,4.3.32767]",
               "System.Memory": "(,5.0.32767]",
               "System.Net.Http": "(,4.3.32767]",
               "System.Net.Http.Json": "(,10.0.32767]",
               "System.Net.NameResolution": "(,4.3.32767]",
               "System.Net.NetworkInformation": "(,4.3.32767]",
               "System.Net.Ping": "(,4.3.32767]",
               "System.Net.Primitives": "(,4.3.32767]",
               "System.Net.Requests": "(,4.3.32767]",
               "System.Net.Security": "(,4.3.32767]",
               "System.Net.ServerSentEvents": "(,10.0.32767]",
               "System.Net.Sockets": "(,4.3.32767]",
               "System.Net.WebHeaderCollection": "(,4.3.32767]",
               "System.Net.WebSockets": "(,4.3.32767]",
               "System.Net.WebSockets.Client": "(,4.3.32767]",
               "System.Numerics.Vectors": "(,5.0.32767]",
               "System.ObjectModel": "(,4.3.32767]",
               "System.Private.DataContractSerialization": "(,4.3.32767]",
               "System.Private.Uri": "(,4.3.32767]",
               "System.Reflection": "(,4.3.32767]",
               "System.Reflection.DispatchProxy": "(,6.0.32767]",
               "System.Reflection.Emit": "(,4.7.32767]",
               "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
               "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
               "System.Reflection.Extensions": "(,4.3.32767]",
               "System.Reflection.Metadata": "(,10.0.32767]",
               "System.Reflection.Primitives": "(,4.3.32767]",
               "System.Reflection.TypeExtensions": "(,4.3.32767]",
               "System.Resources.Reader": "(,4.3.32767]",
               "System.Resources.ResourceManager": "(,4.3.32767]",
               "System.Resources.Writer": "(,4.3.32767]",
               "System.Runtime": "(,4.3.32767]",
               "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
               "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
               "System.Runtime.Extensions": "(,4.3.32767]",
               "System.Runtime.Handles": "(,4.3.32767]",
               "System.Runtime.InteropServices": "(,4.3.32767]",
               "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
               "System.Runtime.Loader": "(,4.3.32767]",
               "System.Runtime.Numerics": "(,4.3.32767]",
               "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
               "System.Runtime.Serialization.Json": "(,4.3.32767]",
               "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
               "System.Runtime.Serialization.Xml": "(,4.3.32767]",
               "System.Security.AccessControl": "(,6.0.32767]",
               "System.Security.Claims": "(,4.3.32767]",
               "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
               "System.Security.Cryptography.Cng": "(,5.0.32767]",
               "System.Security.Cryptography.Csp": "(,4.3.32767]",
               "System.Security.Cryptography.Encoding": "(,4.3.32767]",
               "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
               "System.Security.Cryptography.Primitives": "(,4.3.32767]",
               "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
               "System.Security.Principal": "(,4.3.32767]",
               "System.Security.Principal.Windows": "(,5.0.32767]",
               "System.Security.SecureString": "(,4.3.32767]",
               "System.Text.Encoding": "(,4.3.32767]",
               "System.Text.Encoding.CodePages": "(,10.0.32767]",
               "System.Text.Encoding.Extensions": "(,4.3.32767]",
               "System.Text.Encodings.Web": "(,10.0.32767]",
               "System.Text.Json": "(,10.0.32767]",
               "System.Text.RegularExpressions": "(,4.3.32767]",
               "System.Threading": "(,4.3.32767]",
               "System.Threading.AccessControl": "(,10.0.32767]",
               "System.Threading.Channels": "(,10.0.32767]",
               "System.Threading.Overlapped": "(,4.3.32767]",
               "System.Threading.Tasks": "(,4.3.32767]",
               "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
               "System.Threading.Tasks.Extensions": "(,5.0.32767]",
               "System.Threading.Tasks.Parallel": "(,4.3.32767]",
               "System.Threading.Thread": "(,4.3.32767]",
               "System.Threading.ThreadPool": "(,4.3.32767]",
               "System.Threading.Timer": "(,4.3.32767]",
               "System.ValueTuple": "(,4.5.32767]",
               "System.Xml.ReaderWriter": "(,4.3.32767]",
               "System.Xml.XDocument": "(,4.3.32767]",
               "System.Xml.XmlDocument": "(,4.3.32767]",
               "System.Xml.XmlSerializer": "(,4.3.32767]",
               "System.Xml.XPath": "(,4.3.32767]",
               "System.Xml.XPath.XDocument": "(,5.0.32767]"
             }
           }
         }
       },
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
         "version": "1.0.0",
         "restore": {
           "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
           "projectName": "ReflectionUtilityServices",
           "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
           "packagesPath": "D:\\DevTools\\nuget_cache",
           "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\obj\\",
           "projectStyle": "PackageReference",
           "fallbackFolders": [
             "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
           ],
           "configFilePaths": [
             "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
             "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
           ],
           "originalTargetFrameworks": [
             "net10.0"
           ],
           "sources": {
             "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
             "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
             "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
             "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
             "https://api.nuget.org/v3/index.json": {},
             "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
           },
           "frameworks": {
             "net10.0": {
               "targetAlias": "net10.0",
               "projectReferences": {}
             }
           },
           "warningProperties": {
             "warnAsError": [
               "NU1605"
             ]
           },
           "restoreAuditProperties": {
             "enableAudit": "true",
             "auditLevel": "low",
             "auditMode": "all"
           },
           "SdkAnalysisLevel": "10.0.100"
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "imports": [
               "net461",
               "net462",
               "net47",
               "net471",
               "net472",
               "net48",
               "net481"
             ],
             "assetTargetFallback": true,
             "warn": true,
             "frameworkReferences": {
               "Microsoft.NETCore.App": {
                 "privateAssets": "all"
               }
             },
             "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
             "packagesToPrune": {
               "Microsoft.CSharp": "(,4.7.32767]",
               "Microsoft.VisualBasic": "(,10.4.32767]",
               "Microsoft.Win32.Primitives": "(,4.3.32767]",
               "Microsoft.Win32.Registry": "(,5.0.32767]",
               "runtime.any.System.Collections": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.any.System.Globalization": "(,4.3.32767]",
               "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.any.System.IO": "(,4.3.32767]",
               "runtime.any.System.Reflection": "(,4.3.32767]",
               "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.any.System.Runtime": "(,4.3.32767]",
               "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.any.System.Threading.Timer": "(,4.3.32767]",
               "runtime.aot.System.Collections": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.aot.System.Globalization": "(,4.3.32767]",
               "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.aot.System.IO": "(,4.3.32767]",
               "runtime.aot.System.Reflection": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.aot.System.Runtime": "(,4.3.32767]",
               "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Console": "(,4.3.32767]",
               "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
               "runtime.unix.System.Private.Uri": "(,4.3.32767]",
               "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.win.System.Console": "(,4.3.32767]",
               "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.win.System.Net.Primitives": "(,4.3.32767]",
               "runtime.win.System.Net.Sockets": "(,4.3.32767]",
               "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7.System.Private.Uri": "(,4.3.32767]",
               "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "System.AppContext": "(,4.3.32767]",
               "System.Buffers": "(,5.0.32767]",
               "System.Collections": "(,4.3.32767]",
               "System.Collections.Concurrent": "(,4.3.32767]",
               "System.Collections.Immutable": "(,10.0.32767]",
               "System.Collections.NonGeneric": "(,4.3.32767]",
               "System.Collections.Specialized": "(,4.3.32767]",
               "System.ComponentModel": "(,4.3.32767]",
               "System.ComponentModel.Annotations": "(,4.3.32767]",
               "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
               "System.ComponentModel.Primitives": "(,4.3.32767]",
               "System.ComponentModel.TypeConverter": "(,4.3.32767]",
               "System.Console": "(,4.3.32767]",
               "System.Data.Common": "(,4.3.32767]",
               "System.Data.DataSetExtensions": "(,4.4.32767]",
               "System.Diagnostics.Contracts": "(,4.3.32767]",
               "System.Diagnostics.Debug": "(,4.3.32767]",
               "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
               "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
               "System.Diagnostics.Process": "(,4.3.32767]",
               "System.Diagnostics.StackTrace": "(,4.3.32767]",
               "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
               "System.Diagnostics.Tools": "(,4.3.32767]",
               "System.Diagnostics.TraceSource": "(,4.3.32767]",
               "System.Diagnostics.Tracing": "(,4.3.32767]",
               "System.Drawing.Primitives": "(,4.3.32767]",
               "System.Dynamic.Runtime": "(,4.3.32767]",
               "System.Formats.Asn1": "(,10.0.32767]",
               "System.Formats.Tar": "(,10.0.32767]",
               "System.Globalization": "(,4.3.32767]",
               "System.Globalization.Calendars": "(,4.3.32767]",
               "System.Globalization.Extensions": "(,4.3.32767]",
               "System.IO": "(,4.3.32767]",
               "System.IO.Compression": "(,4.3.32767]",
               "System.IO.Compression.ZipFile": "(,4.3.32767]",
               "System.IO.FileSystem": "(,4.3.32767]",
               "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
               "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
               "System.IO.FileSystem.Primitives": "(,4.3.32767]",
               "System.IO.FileSystem.Watcher": "(,4.3.32767]",
               "System.IO.IsolatedStorage": "(,4.3.32767]",
               "System.IO.MemoryMappedFiles": "(,4.3.32767]",
               "System.IO.Pipelines": "(,10.0.32767]",
               "System.IO.Pipes": "(,4.3.32767]",
               "System.IO.Pipes.AccessControl": "(,5.0.32767]",
               "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
               "System.Linq": "(,4.3.32767]",
               "System.Linq.AsyncEnumerable": "(,10.0.32767]",
               "System.Linq.Expressions": "(,4.3.32767]",
               "System.Linq.Parallel": "(,4.3.32767]",
               "System.Linq.Queryable": "(,4.3.32767]",
               "System.Memory": "(,5.0.32767]",
               "System.Net.Http": "(,4.3.32767]",
               "System.Net.Http.Json": "(,10.0.32767]",
               "System.Net.NameResolution": "(,4.3.32767]",
               "System.Net.NetworkInformation": "(,4.3.32767]",
               "System.Net.Ping": "(,4.3.32767]",
               "System.Net.Primitives": "(,4.3.32767]",
               "System.Net.Requests": "(,4.3.32767]",
               "System.Net.Security": "(,4.3.32767]",
               "System.Net.ServerSentEvents": "(,10.0.32767]",
               "System.Net.Sockets": "(,4.3.32767]",
               "System.Net.WebHeaderCollection": "(,4.3.32767]",
               "System.Net.WebSockets": "(,4.3.32767]",
               "System.Net.WebSockets.Client": "(,4.3.32767]",
               "System.Numerics.Vectors": "(,5.0.32767]",
               "System.ObjectModel": "(,4.3.32767]",
               "System.Private.DataContractSerialization": "(,4.3.32767]",
               "System.Private.Uri": "(,4.3.32767]",
               "System.Reflection": "(,4.3.32767]",
               "System.Reflection.DispatchProxy": "(,6.0.32767]",
               "System.Reflection.Emit": "(,4.7.32767]",
               "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
               "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
               "System.Reflection.Extensions": "(,4.3.32767]",
               "System.Reflection.Metadata": "(,10.0.32767]",
               "System.Reflection.Primitives": "(,4.3.32767]",
               "System.Reflection.TypeExtensions": "(,4.3.32767]",
               "System.Resources.Reader": "(,4.3.32767]",
               "System.Resources.ResourceManager": "(,4.3.32767]",
               "System.Resources.Writer": "(,4.3.32767]",
               "System.Runtime": "(,4.3.32767]",
               "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
               "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
               "System.Runtime.Extensions": "(,4.3.32767]",
               "System.Runtime.Handles": "(,4.3.32767]",
               "System.Runtime.InteropServices": "(,4.3.32767]",
               "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
               "System.Runtime.Loader": "(,4.3.32767]",
               "System.Runtime.Numerics": "(,4.3.32767]",
               "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
               "System.Runtime.Serialization.Json": "(,4.3.32767]",
               "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
               "System.Runtime.Serialization.Xml": "(,4.3.32767]",
               "System.Security.AccessControl": "(,6.0.32767]",
               "System.Security.Claims": "(,4.3.32767]",
               "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
               "System.Security.Cryptography.Cng": "(,5.0.32767]",
               "System.Security.Cryptography.Csp": "(,4.3.32767]",
               "System.Security.Cryptography.Encoding": "(,4.3.32767]",
               "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
               "System.Security.Cryptography.Primitives": "(,4.3.32767]",
               "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
               "System.Security.Principal": "(,4.3.32767]",
               "System.Security.Principal.Windows": "(,5.0.32767]",
               "System.Security.SecureString": "(,4.3.32767]",
               "System.Text.Encoding": "(,4.3.32767]",
               "System.Text.Encoding.CodePages": "(,10.0.32767]",
               "System.Text.Encoding.Extensions": "(,4.3.32767]",
               "System.Text.Encodings.Web": "(,10.0.32767]",
               "System.Text.Json": "(,10.0.32767]",
               "System.Text.RegularExpressions": "(,4.3.32767]",
               "System.Threading": "(,4.3.32767]",
               "System.Threading.AccessControl": "(,10.0.32767]",
               "System.Threading.Channels": "(,10.0.32767]",
               "System.Threading.Overlapped": "(,4.3.32767]",
               "System.Threading.Tasks": "(,4.3.32767]",
               "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
               "System.Threading.Tasks.Extensions": "(,5.0.32767]",
               "System.Threading.Tasks.Parallel": "(,4.3.32767]",
               "System.Threading.Thread": "(,4.3.32767]",
               "System.Threading.ThreadPool": "(,4.3.32767]",
               "System.Threading.Timer": "(,4.3.32767]",
               "System.ValueTuple": "(,4.5.32767]",
               "System.Xml.ReaderWriter": "(,4.3.32767]",
               "System.Xml.XDocument": "(,4.3.32767]",
               "System.Xml.XmlDocument": "(,4.3.32767]",
               "System.Xml.XmlSerializer": "(,4.3.32767]",
               "System.Xml.XPath": "(,4.3.32767]",
               "System.Xml.XPath.XDocument": "(,5.0.32767]"
             }
           }
         }
       }
     }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/MeProject2.csproj.nuget.g.props
   ```
   <?xml version="1.0" encoding="utf-8" standalone="no"?>
   <Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
     <PropertyGroup Condition=" '$(ExcludeRestorePackageImports)' != 'true' ">
       <RestoreSuccess Condition=" '$(RestoreSuccess)' == '' ">False</RestoreSuccess>
       <RestoreTool Condition=" '$(RestoreTool)' == '' ">NuGet</RestoreTool>
       <ProjectAssetsFile Condition=" '$(ProjectAssetsFile)' == '' ">$(MSBuildThisFileDirectory)project.assets.json</ProjectAssetsFile>
       <NuGetPackageRoot Condition=" '$(NuGetPackageRoot)' == '' ">D:\DevTools\nuget_cache</NuGetPackageRoot>
       <NuGetPackageFolders Condition=" '$(NuGetPackageFolders)' == '' ">D:\DevTools\nuget_cache;D:\Program Files\Microsoft Visual Studio\Shared\NuGetPackages</NuGetPackageFolders>
       <NuGetProjectStyle Condition=" '$(NuGetProjectStyle)' == '' ">PackageReference</NuGetProjectStyle>
       <NuGetToolVersion Condition=" '$(NuGetToolVersion)' == '' ">7.0.0</NuGetToolVersion>
     </PropertyGroup>
     <ItemGroup Condition=" '$(ExcludeRestorePackageImports)' != 'true' ">
       <SourceRoot Include="D:\DevTools\nuget_cache\" />
       <SourceRoot Include="D:\Program Files\Microsoft Visual Studio\Shared\NuGetPackages\" />
     </ItemGroup>
   </Project>
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/MeProject2.csproj.nuget.g.targets
   ```
   <?xml version="1.0" encoding="utf-8" standalone="no"?>
   <Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003" />
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/project.assets.json
   ```json
   {
     "version": 3,
     "targets": {
       "net10.0": {}
     },
     "libraries": {},
     "projectFileDependencyGroups": {
       "net10.0": [
         "Microsoft.Extensions.Configuration.Abstractions",
         "Microsoft.Extensions.DependencyInjection",
         "Microsoft.Extensions.DependencyInjection.Abstractions",
         "Microsoft.Extensions.Hosting",
         "Microsoft.Extensions.Hosting.Abstractions",
         "Microsoft.Extensions.Logging",
         "Microsoft.Extensions.Logging.Abstractions",
         "Microsoft.Extensions.Options",
         "Microsoft.Extensions.Primitives"
       ]
     },
     "packageFolders": {
       "D:\\DevTools\\nuget_cache": {},
       "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages": {}
     },
     "project": {
       "version": "1.0.0",
       "restore": {
         "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
         "projectName": "MeProject2",
         "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
         "packagesPath": "D:\\DevTools\\nuget_cache",
         "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\obj\\",
         "projectStyle": "PackageReference",
         "fallbackFolders": [
           "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
         ],
         "configFilePaths": [
           "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
           "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
           "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
           "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
         ],
         "originalTargetFrameworks": [
           "net10.0"
         ],
         "sources": {
           "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
           "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
           "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
           "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
           "https://api.nuget.org/v3/index.json": {},
           "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "projectReferences": {
               "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
                 "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj"
               }
             }
           }
         },
         "warningProperties": {
           "warnAsError": [
             "NU1605"
           ]
         },
         "restoreAuditProperties": {
           "enableAudit": "true",
           "auditLevel": "low",
           "auditMode": "all"
         },
         "SdkAnalysisLevel": "10.0.100"
       },
       "frameworks": {
         "net10.0": {
           "targetAlias": "net10.0",
           "dependencies": {
             "Microsoft.Extensions.Configuration.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.DependencyInjection": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.DependencyInjection.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Hosting": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Hosting.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Logging": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Logging.Abstractions": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Options": {
               "target": "Package",
               "version": "(, )"
             },
             "Microsoft.Extensions.Primitives": {
               "target": "Package",
               "version": "(, )"
             }
           },
           "imports": [
             "net461",
             "net462",
             "net47",
             "net471",
             "net472",
             "net48",
             "net481"
           ],
           "assetTargetFallback": true,
           "warn": true,
           "frameworkReferences": {
             "Microsoft.NETCore.App": {
               "privateAssets": "all"
             }
           },
           "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
           "packagesToPrune": {
             "Microsoft.CSharp": "(,4.7.32767]",
             "Microsoft.VisualBasic": "(,10.4.32767]",
             "Microsoft.Win32.Primitives": "(,4.3.32767]",
             "Microsoft.Win32.Registry": "(,5.0.32767]",
             "runtime.any.System.Collections": "(,4.3.32767]",
             "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
             "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
             "runtime.any.System.Globalization": "(,4.3.32767]",
             "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
             "runtime.any.System.IO": "(,4.3.32767]",
             "runtime.any.System.Reflection": "(,4.3.32767]",
             "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
             "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
             "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
             "runtime.any.System.Runtime": "(,4.3.32767]",
             "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
             "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
             "runtime.any.System.Text.Encoding": "(,4.3.32767]",
             "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
             "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
             "runtime.any.System.Threading.Timer": "(,4.3.32767]",
             "runtime.aot.System.Collections": "(,4.3.32767]",
             "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
             "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
             "runtime.aot.System.Globalization": "(,4.3.32767]",
             "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
             "runtime.aot.System.IO": "(,4.3.32767]",
             "runtime.aot.System.Reflection": "(,4.3.32767]",
             "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
             "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
             "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
             "runtime.aot.System.Runtime": "(,4.3.32767]",
             "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
             "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
             "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
             "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
             "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
             "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
             "runtime.unix.System.Console": "(,4.3.32767]",
             "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
             "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
             "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
             "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
             "runtime.unix.System.Private.Uri": "(,4.3.32767]",
             "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
             "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
             "runtime.win.System.Console": "(,4.3.32767]",
             "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
             "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
             "runtime.win.System.Net.Primitives": "(,4.3.32767]",
             "runtime.win.System.Net.Sockets": "(,4.3.32767]",
             "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
             "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win7.System.Private.Uri": "(,4.3.32767]",
             "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "System.AppContext": "(,4.3.32767]",
             "System.Buffers": "(,5.0.32767]",
             "System.Collections": "(,4.3.32767]",
             "System.Collections.Concurrent": "(,4.3.32767]",
             "System.Collections.Immutable": "(,10.0.32767]",
             "System.Collections.NonGeneric": "(,4.3.32767]",
             "System.Collections.Specialized": "(,4.3.32767]",
             "System.ComponentModel": "(,4.3.32767]",
             "System.ComponentModel.Annotations": "(,4.3.32767]",
             "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
             "System.ComponentModel.Primitives": "(,4.3.32767]",
             "System.ComponentModel.TypeConverter": "(,4.3.32767]",
             "System.Console": "(,4.3.32767]",
             "System.Data.Common": "(,4.3.32767]",
             "System.Data.DataSetExtensions": "(,4.4.32767]",
             "System.Diagnostics.Contracts": "(,4.3.32767]",
             "System.Diagnostics.Debug": "(,4.3.32767]",
             "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
             "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
             "System.Diagnostics.Process": "(,4.3.32767]",
             "System.Diagnostics.StackTrace": "(,4.3.32767]",
             "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
             "System.Diagnostics.Tools": "(,4.3.32767]",
             "System.Diagnostics.TraceSource": "(,4.3.32767]",
             "System.Diagnostics.Tracing": "(,4.3.32767]",
             "System.Drawing.Primitives": "(,4.3.32767]",
             "System.Dynamic.Runtime": "(,4.3.32767]",
             "System.Formats.Asn1": "(,10.0.32767]",
             "System.Formats.Tar": "(,10.0.32767]",
             "System.Globalization": "(,4.3.32767]",
             "System.Globalization.Calendars": "(,4.3.32767]",
             "System.Globalization.Extensions": "(,4.3.32767]",
             "System.IO": "(,4.3.32767]",
             "System.IO.Compression": "(,4.3.32767]",
             "System.IO.Compression.ZipFile": "(,4.3.32767]",
             "System.IO.FileSystem": "(,4.3.32767]",
             "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
             "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
             "System.IO.FileSystem.Primitives": "(,4.3.32767]",
             "System.IO.FileSystem.Watcher": "(,4.3.32767]",
             "System.IO.IsolatedStorage": "(,4.3.32767]",
             "System.IO.MemoryMappedFiles": "(,4.3.32767]",
             "System.IO.Pipelines": "(,10.0.32767]",
             "System.IO.Pipes": "(,4.3.32767]",
             "System.IO.Pipes.AccessControl": "(,5.0.32767]",
             "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
             "System.Linq": "(,4.3.32767]",
             "System.Linq.AsyncEnumerable": "(,10.0.32767]",
             "System.Linq.Expressions": "(,4.3.32767]",
             "System.Linq.Parallel": "(,4.3.32767]",
             "System.Linq.Queryable": "(,4.3.32767]",
             "System.Memory": "(,5.0.32767]",
             "System.Net.Http": "(,4.3.32767]",
             "System.Net.Http.Json": "(,10.0.32767]",
             "System.Net.NameResolution": "(,4.3.32767]",
             "System.Net.NetworkInformation": "(,4.3.32767]",
             "System.Net.Ping": "(,4.3.32767]",
             "System.Net.Primitives": "(,4.3.32767]",
             "System.Net.Requests": "(,4.3.32767]",
             "System.Net.Security": "(,4.3.32767]",
             "System.Net.ServerSentEvents": "(,10.0.32767]",
             "System.Net.Sockets": "(,4.3.32767]",
             "System.Net.WebHeaderCollection": "(,4.3.32767]",
             "System.Net.WebSockets": "(,4.3.32767]",
             "System.Net.WebSockets.Client": "(,4.3.32767]",
             "System.Numerics.Vectors": "(,5.0.32767]",
             "System.ObjectModel": "(,4.3.32767]",
             "System.Private.DataContractSerialization": "(,4.3.32767]",
             "System.Private.Uri": "(,4.3.32767]",
             "System.Reflection": "(,4.3.32767]",
             "System.Reflection.DispatchProxy": "(,6.0.32767]",
             "System.Reflection.Emit": "(,4.7.32767]",
             "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
             "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
             "System.Reflection.Extensions": "(,4.3.32767]",
             "System.Reflection.Metadata": "(,10.0.32767]",
             "System.Reflection.Primitives": "(,4.3.32767]",
             "System.Reflection.TypeExtensions": "(,4.3.32767]",
             "System.Resources.Reader": "(,4.3.32767]",
             "System.Resources.ResourceManager": "(,4.3.32767]",
             "System.Resources.Writer": "(,4.3.32767]",
             "System.Runtime": "(,4.3.32767]",
             "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
             "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
             "System.Runtime.Extensions": "(,4.3.32767]",
             "System.Runtime.Handles": "(,4.3.32767]",
             "System.Runtime.InteropServices": "(,4.3.32767]",
             "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
             "System.Runtime.Loader": "(,4.3.32767]",
             "System.Runtime.Numerics": "(,4.3.32767]",
             "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
             "System.Runtime.Serialization.Json": "(,4.3.32767]",
             "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
             "System.Runtime.Serialization.Xml": "(,4.3.32767]",
             "System.Security.AccessControl": "(,6.0.32767]",
             "System.Security.Claims": "(,4.3.32767]",
             "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
             "System.Security.Cryptography.Cng": "(,5.0.32767]",
             "System.Security.Cryptography.Csp": "(,4.3.32767]",
             "System.Security.Cryptography.Encoding": "(,4.3.32767]",
             "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
             "System.Security.Cryptography.Primitives": "(,4.3.32767]",
             "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
             "System.Security.Principal": "(,4.3.32767]",
             "System.Security.Principal.Windows": "(,5.0.32767]",
             "System.Security.SecureString": "(,4.3.32767]",
             "System.Text.Encoding": "(,4.3.32767]",
             "System.Text.Encoding.CodePages": "(,10.0.32767]",
             "System.Text.Encoding.Extensions": "(,4.3.32767]",
             "System.Text.Encodings.Web": "(,10.0.32767]",
             "System.Text.Json": "(,10.0.32767]",
             "System.Text.RegularExpressions": "(,4.3.32767]",
             "System.Threading": "(,4.3.32767]",
             "System.Threading.AccessControl": "(,10.0.32767]",
             "System.Threading.Channels": "(,10.0.32767]",
             "System.Threading.Overlapped": "(,4.3.32767]",
             "System.Threading.Tasks": "(,4.3.32767]",
             "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
             "System.Threading.Tasks.Extensions": "(,5.0.32767]",
             "System.Threading.Tasks.Parallel": "(,4.3.32767]",
             "System.Threading.Thread": "(,4.3.32767]",
             "System.Threading.ThreadPool": "(,4.3.32767]",
             "System.Threading.Timer": "(,4.3.32767]",
             "System.ValueTuple": "(,4.5.32767]",
             "System.Xml.ReaderWriter": "(,4.3.32767]",
             "System.Xml.XDocument": "(,4.3.32767]",
             "System.Xml.XmlDocument": "(,4.3.32767]",
             "System.Xml.XmlSerializer": "(,4.3.32767]",
             "System.Xml.XPath": "(,4.3.32767]",
             "System.Xml.XPath.XDocument": "(,5.0.32767]"
           }
         }
       }
     },
     "logs": [
       {
         "code": "NU1015",
         "level": "Error",
         "message": "The following PackageReference item(s) do not have a version specified: Microsoft.Extensions.Configuration.Abstractions, Microsoft.Extensions.DependencyInjection, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.Extensions.Hosting, Microsoft.Extensions.Hosting.Abstractions, Microsoft.Extensions.Logging, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.Options, Microsoft.Extensions.Primitives"
       }
     ]
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/obj/project.nuget.cache
   ```
   {
     "version": 2,
     "dgSpecHash": "ETuvHLaG2DE=",
     "success": false,
     "projectFilePath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
     "expectedPackageFiles": [],
     "logs": [
       {
         "code": "NU1015",
         "level": "Error",
         "message": "The following PackageReference item(s) do not have a version specified: Microsoft.Extensions.Configuration.Abstractions, Microsoft.Extensions.DependencyInjection, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.Extensions.Hosting, Microsoft.Extensions.Hosting.Abstractions, Microsoft.Extensions.Logging, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.Options, Microsoft.Extensions.Primitives",
         "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
         "filePath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\MeProject2\\MeProject2.csproj",
         "targetGraphs": []
       }
     ]
   }
   ```

   ## File: Examples/Ex1/Ex1-1/MeProject2/Son.cs
   ```csharp
   namespace MeProject2;

   public partial class Son : Me
   {
       public string _name { get; set; }
       public string _account { get; set; }
       public string _password { get; set; }

       public string Get_Name() { return _name; }
       public string get_Name() { return _name; }
       public void Set_Name(string value) {  _name = value; }
       public void set_Name(string value) {  _name = value; }

       public string Get_Account() { return _account; }
       public string get_Account() { return _account; }
       public void Set_Account(string value) { _account = value; }
       public void set_Account(string value) { _account = value;  }

       public string Get_Password() { return _password; }
       public string get_Password() { return _password; }

       public void Set_Password(string value) { _password = value; }

       public void set_Password(string value) { _password = value; }

       public string Get__Name() { return _name; }
       public string get__Name() { return _name; }
       public void Set__Name(string value) { _name = value; }
       public void set__Name(string value) { _name = value; }

       public string Get__Password() { return _password; }
       public string get__Password() { return _password; }
       public void Set__Password(string value) { _password = value; }
       public void set__Password(string value) { _password = value; }

       public string Get___Name() { return _name; }
       public string get___Name() { return _name; }
       public void Set___Name(string value) { _name = value; }
       public void set___Name(string value) { _name = value; }

       public string Get___Account() { return _account; }
       public string get___Account() { return _account; }
       public void Set___Account(string value) { _account = value; }
       public void set___Account(string value) { _account = value; }

       public string Get___Password() { return _password; }
       public string get___Password() { return _password; }
       public void Set___Password(string value) { _password = value; }
       public void set___Password(string value) { _password = value; }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/Questions/Q1.md
   ```markdown
   # Q1
   For these projects [MeProject1](../MeProject1/MeProject1.csproj) and [MeProject2](../MeProject1/MeProject1.csproj),

   are there any compiler errors?

   If there are no any compiler errors, will it crash when at runtime?

   If it will not crash,
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/IReflectionUtilityService.cs
   ```csharp
   namespace ReflectionUtilityServices;

   /// <summary>
   /// utility service about reflection.
   /// </summary>
   public interface IReflectionUtilityService
   {
       /// <summary>
       /// Set <paramref name="value"/> of property named <paramref name="propertyName"/> of the instance <paramref name="target"/> by reflection.
       /// </summary>
       /// <param name="target">The target object whose property value will be retrieved.</param>
       /// <param name="propertyName">The name of the property to retrieve.</param>
       /// <param name="value">value that will be set</param>
       void SetPropertyValue(
           object target,
           string propertyName,
           object? value
       );

       /// <summary>
       /// Gets the value of a specified property from a given target object.
       /// </summary>
       /// <param name="target">The target object whose property value will be retrieved.</param>
       /// <param name="propertyName">The name of the property to retrieve.</param>
       /// <returns>The value of the property.</returns>
       object? GetPropertyValue(
           object target,
           string propertyName
       );
   }
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/.NETCoreApp,Version=v10.0.AssemblyAttributes.cs
   ```csharp
   // <autogenerated />
   using System;
   using System.Reflection;
   [assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v10.0", FrameworkDisplayName = ".NET 10.0")]
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.AssemblyInfo.cs
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

   [assembly: System.Reflection.AssemblyCompanyAttribute("ReflectionUtilityServices")]
   [assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
   [assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
   [assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0")]
   [assembly: System.Reflection.AssemblyProductAttribute("ReflectionUtilityServices")]
   [assembly: System.Reflection.AssemblyTitleAttribute("ReflectionUtilityServices")]
   [assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

   // Generated by the MSBuild WriteCodeFragment class.
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.AssemblyInfoInputs.cache
   ```
   871099dd98879197745f13d3768322f469b4ebd7255ba2b40bd4ee77eb5f18e5
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.GeneratedMSBuildEditorConfig.editorconfig
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
   build_property.RootNamespace = ReflectionUtilityServices
   build_property.ProjectDir = D:\workspace\LearningNotes\CSharp\Examples\Ex1\Ex1-1\ReflectionUtilityServices\
   build_property.EnableComHosting =
   build_property.EnableGeneratedComInterfaceComImportInterop =
   build_property.EffectiveAnalysisLevelStyle = 10.0
   build_property.EnableCodeStyleSeverity =
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/Debug/net10.0/ReflectionUtilityServices.GlobalUsings.g.cs
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

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/project.assets.json
   ```json
   {
     "version": 3,
     "targets": {
       "net10.0": {}
     },
     "libraries": {},
     "projectFileDependencyGroups": {
       "net10.0": []
     },
     "packageFolders": {
       "D:\\DevTools\\nuget_cache": {},
       "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages": {}
     },
     "project": {
       "version": "1.0.0",
       "restore": {
         "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
         "projectName": "ReflectionUtilityServices",
         "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
         "packagesPath": "D:\\DevTools\\nuget_cache",
         "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\obj\\",
         "projectStyle": "PackageReference",
         "fallbackFolders": [
           "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
         ],
         "configFilePaths": [
           "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
           "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
           "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
           "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
         ],
         "originalTargetFrameworks": [
           "net10.0"
         ],
         "sources": {
           "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
           "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
           "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
           "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
           "https://api.nuget.org/v3/index.json": {},
           "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "projectReferences": {}
           }
         },
         "warningProperties": {
           "warnAsError": [
             "NU1605"
           ]
         },
         "restoreAuditProperties": {
           "enableAudit": "true",
           "auditLevel": "low",
           "auditMode": "all"
         },
         "SdkAnalysisLevel": "10.0.100"
       },
       "frameworks": {
         "net10.0": {
           "targetAlias": "net10.0",
           "imports": [
             "net461",
             "net462",
             "net47",
             "net471",
             "net472",
             "net48",
             "net481"
           ],
           "assetTargetFallback": true,
           "warn": true,
           "frameworkReferences": {
             "Microsoft.NETCore.App": {
               "privateAssets": "all"
             }
           },
           "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
           "packagesToPrune": {
             "Microsoft.CSharp": "(,4.7.32767]",
             "Microsoft.VisualBasic": "(,10.4.32767]",
             "Microsoft.Win32.Primitives": "(,4.3.32767]",
             "Microsoft.Win32.Registry": "(,5.0.32767]",
             "runtime.any.System.Collections": "(,4.3.32767]",
             "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
             "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
             "runtime.any.System.Globalization": "(,4.3.32767]",
             "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
             "runtime.any.System.IO": "(,4.3.32767]",
             "runtime.any.System.Reflection": "(,4.3.32767]",
             "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
             "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
             "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
             "runtime.any.System.Runtime": "(,4.3.32767]",
             "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
             "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
             "runtime.any.System.Text.Encoding": "(,4.3.32767]",
             "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
             "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
             "runtime.any.System.Threading.Timer": "(,4.3.32767]",
             "runtime.aot.System.Collections": "(,4.3.32767]",
             "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
             "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
             "runtime.aot.System.Globalization": "(,4.3.32767]",
             "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
             "runtime.aot.System.IO": "(,4.3.32767]",
             "runtime.aot.System.Reflection": "(,4.3.32767]",
             "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
             "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
             "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
             "runtime.aot.System.Runtime": "(,4.3.32767]",
             "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
             "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
             "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
             "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
             "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
             "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
             "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
             "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
             "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
             "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
             "runtime.unix.System.Console": "(,4.3.32767]",
             "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
             "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
             "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
             "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
             "runtime.unix.System.Private.Uri": "(,4.3.32767]",
             "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
             "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
             "runtime.win.System.Console": "(,4.3.32767]",
             "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
             "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
             "runtime.win.System.Net.Primitives": "(,4.3.32767]",
             "runtime.win.System.Net.Sockets": "(,4.3.32767]",
             "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
             "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
             "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "runtime.win7.System.Private.Uri": "(,4.3.32767]",
             "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
             "System.AppContext": "(,4.3.32767]",
             "System.Buffers": "(,5.0.32767]",
             "System.Collections": "(,4.3.32767]",
             "System.Collections.Concurrent": "(,4.3.32767]",
             "System.Collections.Immutable": "(,10.0.32767]",
             "System.Collections.NonGeneric": "(,4.3.32767]",
             "System.Collections.Specialized": "(,4.3.32767]",
             "System.ComponentModel": "(,4.3.32767]",
             "System.ComponentModel.Annotations": "(,4.3.32767]",
             "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
             "System.ComponentModel.Primitives": "(,4.3.32767]",
             "System.ComponentModel.TypeConverter": "(,4.3.32767]",
             "System.Console": "(,4.3.32767]",
             "System.Data.Common": "(,4.3.32767]",
             "System.Data.DataSetExtensions": "(,4.4.32767]",
             "System.Diagnostics.Contracts": "(,4.3.32767]",
             "System.Diagnostics.Debug": "(,4.3.32767]",
             "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
             "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
             "System.Diagnostics.Process": "(,4.3.32767]",
             "System.Diagnostics.StackTrace": "(,4.3.32767]",
             "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
             "System.Diagnostics.Tools": "(,4.3.32767]",
             "System.Diagnostics.TraceSource": "(,4.3.32767]",
             "System.Diagnostics.Tracing": "(,4.3.32767]",
             "System.Drawing.Primitives": "(,4.3.32767]",
             "System.Dynamic.Runtime": "(,4.3.32767]",
             "System.Formats.Asn1": "(,10.0.32767]",
             "System.Formats.Tar": "(,10.0.32767]",
             "System.Globalization": "(,4.3.32767]",
             "System.Globalization.Calendars": "(,4.3.32767]",
             "System.Globalization.Extensions": "(,4.3.32767]",
             "System.IO": "(,4.3.32767]",
             "System.IO.Compression": "(,4.3.32767]",
             "System.IO.Compression.ZipFile": "(,4.3.32767]",
             "System.IO.FileSystem": "(,4.3.32767]",
             "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
             "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
             "System.IO.FileSystem.Primitives": "(,4.3.32767]",
             "System.IO.FileSystem.Watcher": "(,4.3.32767]",
             "System.IO.IsolatedStorage": "(,4.3.32767]",
             "System.IO.MemoryMappedFiles": "(,4.3.32767]",
             "System.IO.Pipelines": "(,10.0.32767]",
             "System.IO.Pipes": "(,4.3.32767]",
             "System.IO.Pipes.AccessControl": "(,5.0.32767]",
             "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
             "System.Linq": "(,4.3.32767]",
             "System.Linq.AsyncEnumerable": "(,10.0.32767]",
             "System.Linq.Expressions": "(,4.3.32767]",
             "System.Linq.Parallel": "(,4.3.32767]",
             "System.Linq.Queryable": "(,4.3.32767]",
             "System.Memory": "(,5.0.32767]",
             "System.Net.Http": "(,4.3.32767]",
             "System.Net.Http.Json": "(,10.0.32767]",
             "System.Net.NameResolution": "(,4.3.32767]",
             "System.Net.NetworkInformation": "(,4.3.32767]",
             "System.Net.Ping": "(,4.3.32767]",
             "System.Net.Primitives": "(,4.3.32767]",
             "System.Net.Requests": "(,4.3.32767]",
             "System.Net.Security": "(,4.3.32767]",
             "System.Net.ServerSentEvents": "(,10.0.32767]",
             "System.Net.Sockets": "(,4.3.32767]",
             "System.Net.WebHeaderCollection": "(,4.3.32767]",
             "System.Net.WebSockets": "(,4.3.32767]",
             "System.Net.WebSockets.Client": "(,4.3.32767]",
             "System.Numerics.Vectors": "(,5.0.32767]",
             "System.ObjectModel": "(,4.3.32767]",
             "System.Private.DataContractSerialization": "(,4.3.32767]",
             "System.Private.Uri": "(,4.3.32767]",
             "System.Reflection": "(,4.3.32767]",
             "System.Reflection.DispatchProxy": "(,6.0.32767]",
             "System.Reflection.Emit": "(,4.7.32767]",
             "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
             "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
             "System.Reflection.Extensions": "(,4.3.32767]",
             "System.Reflection.Metadata": "(,10.0.32767]",
             "System.Reflection.Primitives": "(,4.3.32767]",
             "System.Reflection.TypeExtensions": "(,4.3.32767]",
             "System.Resources.Reader": "(,4.3.32767]",
             "System.Resources.ResourceManager": "(,4.3.32767]",
             "System.Resources.Writer": "(,4.3.32767]",
             "System.Runtime": "(,4.3.32767]",
             "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
             "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
             "System.Runtime.Extensions": "(,4.3.32767]",
             "System.Runtime.Handles": "(,4.3.32767]",
             "System.Runtime.InteropServices": "(,4.3.32767]",
             "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
             "System.Runtime.Loader": "(,4.3.32767]",
             "System.Runtime.Numerics": "(,4.3.32767]",
             "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
             "System.Runtime.Serialization.Json": "(,4.3.32767]",
             "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
             "System.Runtime.Serialization.Xml": "(,4.3.32767]",
             "System.Security.AccessControl": "(,6.0.32767]",
             "System.Security.Claims": "(,4.3.32767]",
             "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
             "System.Security.Cryptography.Cng": "(,5.0.32767]",
             "System.Security.Cryptography.Csp": "(,4.3.32767]",
             "System.Security.Cryptography.Encoding": "(,4.3.32767]",
             "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
             "System.Security.Cryptography.Primitives": "(,4.3.32767]",
             "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
             "System.Security.Principal": "(,4.3.32767]",
             "System.Security.Principal.Windows": "(,5.0.32767]",
             "System.Security.SecureString": "(,4.3.32767]",
             "System.Text.Encoding": "(,4.3.32767]",
             "System.Text.Encoding.CodePages": "(,10.0.32767]",
             "System.Text.Encoding.Extensions": "(,4.3.32767]",
             "System.Text.Encodings.Web": "(,10.0.32767]",
             "System.Text.Json": "(,10.0.32767]",
             "System.Text.RegularExpressions": "(,4.3.32767]",
             "System.Threading": "(,4.3.32767]",
             "System.Threading.AccessControl": "(,10.0.32767]",
             "System.Threading.Channels": "(,10.0.32767]",
             "System.Threading.Overlapped": "(,4.3.32767]",
             "System.Threading.Tasks": "(,4.3.32767]",
             "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
             "System.Threading.Tasks.Extensions": "(,5.0.32767]",
             "System.Threading.Tasks.Parallel": "(,4.3.32767]",
             "System.Threading.Thread": "(,4.3.32767]",
             "System.Threading.ThreadPool": "(,4.3.32767]",
             "System.Threading.Timer": "(,4.3.32767]",
             "System.ValueTuple": "(,4.5.32767]",
             "System.Xml.ReaderWriter": "(,4.3.32767]",
             "System.Xml.XDocument": "(,4.3.32767]",
             "System.Xml.XmlDocument": "(,4.3.32767]",
             "System.Xml.XmlSerializer": "(,4.3.32767]",
             "System.Xml.XPath": "(,4.3.32767]",
             "System.Xml.XPath.XDocument": "(,5.0.32767]"
           }
         }
       }
     }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/project.nuget.cache
   ```
   {
     "version": 2,
     "dgSpecHash": "FyDTpH1LCVM=",
     "success": true,
     "projectFilePath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
     "expectedPackageFiles": [],
     "logs": []
   }
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/ReflectionUtilityServices.csproj.nuget.dgspec.json
   ```json
   {
     "format": 1,
     "restore": {
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {}
     },
     "projects": {
       "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj": {
         "version": "1.0.0",
         "restore": {
           "projectUniqueName": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
           "projectName": "ReflectionUtilityServices",
           "projectPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\ReflectionUtilityServices.csproj",
           "packagesPath": "D:\\DevTools\\nuget_cache",
           "outputPath": "D:\\workspace\\LearningNotes\\CSharp\\Examples\\Ex1\\Ex1-1\\ReflectionUtilityServices\\obj\\",
           "projectStyle": "PackageReference",
           "fallbackFolders": [
             "D:\\Program Files\\Microsoft Visual Studio\\Shared\\NuGetPackages"
           ],
           "configFilePaths": [
             "D:\\workspace\\LearningNotes\\CSharp\\NuGet.Config",
             "C:\\Users\\userJay30\\AppData\\Roaming\\NuGet\\NuGet.Config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config",
             "C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config"
           ],
           "originalTargetFrameworks": [
             "net10.0"
           ],
           "sources": {
             "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\": {},
             "D:\\workspace\\LearningNotes\\CSharp\\local-packages": {},
             "D:\\workspace\\MyLocalNuget\\BlazorWebAssembly\\": {},
             "D:\\workspace\\MyLocalNuget\\ClassLibrary\\": {},
             "https://api.nuget.org/v3/index.json": {},
             "https://nuget.pkg.github.com/codeloverjay30/index.json": {}
           },
           "frameworks": {
             "net10.0": {
               "targetAlias": "net10.0",
               "projectReferences": {}
             }
           },
           "warningProperties": {
             "warnAsError": [
               "NU1605"
             ]
           },
           "restoreAuditProperties": {
             "enableAudit": "true",
             "auditLevel": "low",
             "auditMode": "all"
           },
           "SdkAnalysisLevel": "10.0.100"
         },
         "frameworks": {
           "net10.0": {
             "targetAlias": "net10.0",
             "imports": [
               "net461",
               "net462",
               "net47",
               "net471",
               "net472",
               "net48",
               "net481"
             ],
             "assetTargetFallback": true,
             "warn": true,
             "frameworkReferences": {
               "Microsoft.NETCore.App": {
                 "privateAssets": "all"
               }
             },
             "runtimeIdentifierGraphPath": "D:\\DevTools\\dotnet\\sdk\\10.0.103/PortableRuntimeIdentifierGraph.json",
             "packagesToPrune": {
               "Microsoft.CSharp": "(,4.7.32767]",
               "Microsoft.VisualBasic": "(,10.4.32767]",
               "Microsoft.Win32.Primitives": "(,4.3.32767]",
               "Microsoft.Win32.Registry": "(,5.0.32767]",
               "runtime.any.System.Collections": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.any.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.any.System.Globalization": "(,4.3.32767]",
               "runtime.any.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.any.System.IO": "(,4.3.32767]",
               "runtime.any.System.Reflection": "(,4.3.32767]",
               "runtime.any.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.any.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.any.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.any.System.Runtime": "(,4.3.32767]",
               "runtime.any.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.any.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding": "(,4.3.32767]",
               "runtime.any.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.any.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.any.System.Threading.Timer": "(,4.3.32767]",
               "runtime.aot.System.Collections": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tools": "(,4.3.32767]",
               "runtime.aot.System.Diagnostics.Tracing": "(,4.3.32767]",
               "runtime.aot.System.Globalization": "(,4.3.32767]",
               "runtime.aot.System.Globalization.Calendars": "(,4.3.32767]",
               "runtime.aot.System.IO": "(,4.3.32767]",
               "runtime.aot.System.Reflection": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Reflection.Primitives": "(,4.3.32767]",
               "runtime.aot.System.Resources.ResourceManager": "(,4.3.32767]",
               "runtime.aot.System.Runtime": "(,4.3.32767]",
               "runtime.aot.System.Runtime.Handles": "(,4.3.32767]",
               "runtime.aot.System.Runtime.InteropServices": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding": "(,4.3.32767]",
               "runtime.aot.System.Text.Encoding.Extensions": "(,4.3.32767]",
               "runtime.aot.System.Threading.Tasks": "(,4.3.32767]",
               "runtime.aot.System.Threading.Timer": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.debian.9-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.27-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.fedora.28-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.opensuse.42.3-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple": "(,4.3.32767]",
               "runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography": "(,4.3.32767]",
               "runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Http": "(,4.3.32767]",
               "runtime.ubuntu.18.04-x64.runtime.native.System.Net.Security": "(,4.3.32767]",
               "runtime.unix.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Console": "(,4.3.32767]",
               "runtime.unix.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.unix.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.unix.System.Net.Primitives": "(,4.3.32767]",
               "runtime.unix.System.Net.Sockets": "(,4.3.32767]",
               "runtime.unix.System.Private.Uri": "(,4.3.32767]",
               "runtime.unix.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win.Microsoft.Win32.Primitives": "(,4.3.32767]",
               "runtime.win.System.Console": "(,4.3.32767]",
               "runtime.win.System.Diagnostics.Debug": "(,4.3.32767]",
               "runtime.win.System.IO.FileSystem": "(,4.3.32767]",
               "runtime.win.System.Net.Primitives": "(,4.3.32767]",
               "runtime.win.System.Net.Sockets": "(,4.3.32767]",
               "runtime.win.System.Runtime.Extensions": "(,4.3.32767]",
               "runtime.win10-arm-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-arm64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win10-x64-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win10-x86-aot.runtime.native.System.IO.Compression": "(,4.0.32767]",
               "runtime.win7-x64.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7-x86.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "runtime.win7.System.Private.Uri": "(,4.3.32767]",
               "runtime.win8-arm.runtime.native.System.IO.Compression": "(,4.3.32767]",
               "System.AppContext": "(,4.3.32767]",
               "System.Buffers": "(,5.0.32767]",
               "System.Collections": "(,4.3.32767]",
               "System.Collections.Concurrent": "(,4.3.32767]",
               "System.Collections.Immutable": "(,10.0.32767]",
               "System.Collections.NonGeneric": "(,4.3.32767]",
               "System.Collections.Specialized": "(,4.3.32767]",
               "System.ComponentModel": "(,4.3.32767]",
               "System.ComponentModel.Annotations": "(,4.3.32767]",
               "System.ComponentModel.EventBasedAsync": "(,4.3.32767]",
               "System.ComponentModel.Primitives": "(,4.3.32767]",
               "System.ComponentModel.TypeConverter": "(,4.3.32767]",
               "System.Console": "(,4.3.32767]",
               "System.Data.Common": "(,4.3.32767]",
               "System.Data.DataSetExtensions": "(,4.4.32767]",
               "System.Diagnostics.Contracts": "(,4.3.32767]",
               "System.Diagnostics.Debug": "(,4.3.32767]",
               "System.Diagnostics.DiagnosticSource": "(,10.0.32767]",
               "System.Diagnostics.FileVersionInfo": "(,4.3.32767]",
               "System.Diagnostics.Process": "(,4.3.32767]",
               "System.Diagnostics.StackTrace": "(,4.3.32767]",
               "System.Diagnostics.TextWriterTraceListener": "(,4.3.32767]",
               "System.Diagnostics.Tools": "(,4.3.32767]",
               "System.Diagnostics.TraceSource": "(,4.3.32767]",
               "System.Diagnostics.Tracing": "(,4.3.32767]",
               "System.Drawing.Primitives": "(,4.3.32767]",
               "System.Dynamic.Runtime": "(,4.3.32767]",
               "System.Formats.Asn1": "(,10.0.32767]",
               "System.Formats.Tar": "(,10.0.32767]",
               "System.Globalization": "(,4.3.32767]",
               "System.Globalization.Calendars": "(,4.3.32767]",
               "System.Globalization.Extensions": "(,4.3.32767]",
               "System.IO": "(,4.3.32767]",
               "System.IO.Compression": "(,4.3.32767]",
               "System.IO.Compression.ZipFile": "(,4.3.32767]",
               "System.IO.FileSystem": "(,4.3.32767]",
               "System.IO.FileSystem.AccessControl": "(,4.4.32767]",
               "System.IO.FileSystem.DriveInfo": "(,4.3.32767]",
               "System.IO.FileSystem.Primitives": "(,4.3.32767]",
               "System.IO.FileSystem.Watcher": "(,4.3.32767]",
               "System.IO.IsolatedStorage": "(,4.3.32767]",
               "System.IO.MemoryMappedFiles": "(,4.3.32767]",
               "System.IO.Pipelines": "(,10.0.32767]",
               "System.IO.Pipes": "(,4.3.32767]",
               "System.IO.Pipes.AccessControl": "(,5.0.32767]",
               "System.IO.UnmanagedMemoryStream": "(,4.3.32767]",
               "System.Linq": "(,4.3.32767]",
               "System.Linq.AsyncEnumerable": "(,10.0.32767]",
               "System.Linq.Expressions": "(,4.3.32767]",
               "System.Linq.Parallel": "(,4.3.32767]",
               "System.Linq.Queryable": "(,4.3.32767]",
               "System.Memory": "(,5.0.32767]",
               "System.Net.Http": "(,4.3.32767]",
               "System.Net.Http.Json": "(,10.0.32767]",
               "System.Net.NameResolution": "(,4.3.32767]",
               "System.Net.NetworkInformation": "(,4.3.32767]",
               "System.Net.Ping": "(,4.3.32767]",
               "System.Net.Primitives": "(,4.3.32767]",
               "System.Net.Requests": "(,4.3.32767]",
               "System.Net.Security": "(,4.3.32767]",
               "System.Net.ServerSentEvents": "(,10.0.32767]",
               "System.Net.Sockets": "(,4.3.32767]",
               "System.Net.WebHeaderCollection": "(,4.3.32767]",
               "System.Net.WebSockets": "(,4.3.32767]",
               "System.Net.WebSockets.Client": "(,4.3.32767]",
               "System.Numerics.Vectors": "(,5.0.32767]",
               "System.ObjectModel": "(,4.3.32767]",
               "System.Private.DataContractSerialization": "(,4.3.32767]",
               "System.Private.Uri": "(,4.3.32767]",
               "System.Reflection": "(,4.3.32767]",
               "System.Reflection.DispatchProxy": "(,6.0.32767]",
               "System.Reflection.Emit": "(,4.7.32767]",
               "System.Reflection.Emit.ILGeneration": "(,4.7.32767]",
               "System.Reflection.Emit.Lightweight": "(,4.7.32767]",
               "System.Reflection.Extensions": "(,4.3.32767]",
               "System.Reflection.Metadata": "(,10.0.32767]",
               "System.Reflection.Primitives": "(,4.3.32767]",
               "System.Reflection.TypeExtensions": "(,4.3.32767]",
               "System.Resources.Reader": "(,4.3.32767]",
               "System.Resources.ResourceManager": "(,4.3.32767]",
               "System.Resources.Writer": "(,4.3.32767]",
               "System.Runtime": "(,4.3.32767]",
               "System.Runtime.CompilerServices.Unsafe": "(,7.0.32767]",
               "System.Runtime.CompilerServices.VisualC": "(,4.3.32767]",
               "System.Runtime.Extensions": "(,4.3.32767]",
               "System.Runtime.Handles": "(,4.3.32767]",
               "System.Runtime.InteropServices": "(,4.3.32767]",
               "System.Runtime.InteropServices.RuntimeInformation": "(,4.3.32767]",
               "System.Runtime.Loader": "(,4.3.32767]",
               "System.Runtime.Numerics": "(,4.3.32767]",
               "System.Runtime.Serialization.Formatters": "(,4.3.32767]",
               "System.Runtime.Serialization.Json": "(,4.3.32767]",
               "System.Runtime.Serialization.Primitives": "(,4.3.32767]",
               "System.Runtime.Serialization.Xml": "(,4.3.32767]",
               "System.Security.AccessControl": "(,6.0.32767]",
               "System.Security.Claims": "(,4.3.32767]",
               "System.Security.Cryptography.Algorithms": "(,4.3.32767]",
               "System.Security.Cryptography.Cng": "(,5.0.32767]",
               "System.Security.Cryptography.Csp": "(,4.3.32767]",
               "System.Security.Cryptography.Encoding": "(,4.3.32767]",
               "System.Security.Cryptography.OpenSsl": "(,5.0.32767]",
               "System.Security.Cryptography.Primitives": "(,4.3.32767]",
               "System.Security.Cryptography.X509Certificates": "(,4.3.32767]",
               "System.Security.Principal": "(,4.3.32767]",
               "System.Security.Principal.Windows": "(,5.0.32767]",
               "System.Security.SecureString": "(,4.3.32767]",
               "System.Text.Encoding": "(,4.3.32767]",
               "System.Text.Encoding.CodePages": "(,10.0.32767]",
               "System.Text.Encoding.Extensions": "(,4.3.32767]",
               "System.Text.Encodings.Web": "(,10.0.32767]",
               "System.Text.Json": "(,10.0.32767]",
               "System.Text.RegularExpressions": "(,4.3.32767]",
               "System.Threading": "(,4.3.32767]",
               "System.Threading.AccessControl": "(,10.0.32767]",
               "System.Threading.Channels": "(,10.0.32767]",
               "System.Threading.Overlapped": "(,4.3.32767]",
               "System.Threading.Tasks": "(,4.3.32767]",
               "System.Threading.Tasks.Dataflow": "(,10.0.32767]",
               "System.Threading.Tasks.Extensions": "(,5.0.32767]",
               "System.Threading.Tasks.Parallel": "(,4.3.32767]",
               "System.Threading.Thread": "(,4.3.32767]",
               "System.Threading.ThreadPool": "(,4.3.32767]",
               "System.Threading.Timer": "(,4.3.32767]",
               "System.ValueTuple": "(,4.5.32767]",
               "System.Xml.ReaderWriter": "(,4.3.32767]",
               "System.Xml.XDocument": "(,4.3.32767]",
               "System.Xml.XmlDocument": "(,4.3.32767]",
               "System.Xml.XmlSerializer": "(,4.3.32767]",
               "System.Xml.XPath": "(,4.3.32767]",
               "System.Xml.XPath.XDocument": "(,5.0.32767]"
             }
           }
         }
       }
     }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/ReflectionUtilityServices.csproj.nuget.g.props
   ```
   <?xml version="1.0" encoding="utf-8" standalone="no"?>
   <Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
     <PropertyGroup Condition=" '$(ExcludeRestorePackageImports)' != 'true' ">
       <RestoreSuccess Condition=" '$(RestoreSuccess)' == '' ">True</RestoreSuccess>
       <RestoreTool Condition=" '$(RestoreTool)' == '' ">NuGet</RestoreTool>
       <ProjectAssetsFile Condition=" '$(ProjectAssetsFile)' == '' ">$(MSBuildThisFileDirectory)project.assets.json</ProjectAssetsFile>
       <NuGetPackageRoot Condition=" '$(NuGetPackageRoot)' == '' ">D:\DevTools\nuget_cache</NuGetPackageRoot>
       <NuGetPackageFolders Condition=" '$(NuGetPackageFolders)' == '' ">D:\DevTools\nuget_cache;D:\Program Files\Microsoft Visual Studio\Shared\NuGetPackages</NuGetPackageFolders>
       <NuGetProjectStyle Condition=" '$(NuGetProjectStyle)' == '' ">PackageReference</NuGetProjectStyle>
       <NuGetToolVersion Condition=" '$(NuGetToolVersion)' == '' ">7.0.0</NuGetToolVersion>
     </PropertyGroup>
     <ItemGroup Condition=" '$(ExcludeRestorePackageImports)' != 'true' ">
       <SourceRoot Include="D:\DevTools\nuget_cache\" />
       <SourceRoot Include="D:\Program Files\Microsoft Visual Studio\Shared\NuGetPackages\" />
     </ItemGroup>
   </Project>
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/obj/ReflectionUtilityServices.csproj.nuget.g.targets
   ```
   <?xml version="1.0" encoding="utf-8" standalone="no"?>
   <Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003" />
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityService.cs
   ```csharp
   using System.Reflection;

   namespace ReflectionUtilityServices;

   /// <inheritdoc cref="global::ReflectionUtilityServices.IReflectionUtilityService"/>
   public class ReflectionUtilityService : IReflectionUtilityService
   {
       /// <inheritdoc cref="global::ReflectionUtilityServices.IReflectionUtilityService.SetPropertyValue(object, string, object?)"/>
       public void SetPropertyValue(
           object target,
           string propertyName,
           object? value
       )
       {
           if (target is null)
           {
               throw new ArgumentNullException(nameof(target), "Target object cannot be null.");
           }

           if (string.IsNullOrWhiteSpace(propertyName))
           {
               throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
           }

           Type type = target.GetType();

           // 使用 BindingFlags 確保能找到 Public 與 Non-Public 的 Instance 屬性
           PropertyInfo? propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

           if (propertyInfo is null)
           {
               throw new InvalidOperationException($"Property '{propertyName}' was not found on type '{type.FullName}'.");
           }

           if (!propertyInfo.CanWrite)
           {
               throw new InvalidOperationException($"Property '{propertyName}' on type '{type.FullName}' does not have a setter.");
           }

           try
           {
               // 防禦性設計：轉換型別，避免傳入不相符的型別導致底層噴出隱蔽異常
               object? convertedValue = value;
               if (value is not null && !propertyInfo.PropertyType.IsAssignableFrom(value.GetType()))
               {
                   convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
               }

               propertyInfo.SetValue(target, convertedValue, null);
           }
           catch (Exception ex) when (ex is not TargetInvocationException)
           {
               // 攔截並包裝非預期的異常
               throw new InvalidOperationException($"Failed to set value for property '{propertyName}' on type '{type.FullName}'.", ex);
           }
       }

       /// <inheritdoc cref="global::ReflectionUtilityServices.IReflectionUtilityService.GetPropertyValue(object, string)"/>
       public object? GetPropertyValue(
           object target,
           string propertyName
       )
       {
           if (target is null)
           {
               throw new ArgumentNullException(nameof(target), "Target object cannot be null.");
           }

           if (string.IsNullOrWhiteSpace(propertyName))
           {
               throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
           }

           Type type = target.GetType();

           // 使用 BindingFlags 確保不論是 Public 還是 Non-Public 的實例屬性都能被找到
           PropertyInfo? propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

           if (propertyInfo is null)
           {
               throw new InvalidOperationException($"Property '{propertyName}' was not found on type '{type.FullName}'.");
           }

           if (!propertyInfo.CanRead)
           {
               throw new InvalidOperationException($"Property '{propertyName}' on type '{type.FullName}' does not have a getter.");
           }

           try
           {
               // 取得屬性值
               return propertyInfo.GetValue(target, null);
           }
           catch (Exception ex) when (ex is not TargetInvocationException)
           {
               // 防禦性設計：攔截並包裝底層非預期的異常
               throw new InvalidOperationException($"Failed to get value for property '{propertyName}' on type '{type.FullName}'.", ex);
           }
       }
   }
   ```

   ## File: Examples/Ex1/Ex1-1/ReflectionUtilityServices/ReflectionUtilityServices.csproj
   ```
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net10.0</TargetFramework>
       <ImplicitUsings>enable</ImplicitUsings>
       <Nullable>enable</Nullable>
     </PropertyGroup>
   </Project>
   ```

   ## File: GEMINI.md
   ```markdown
   # 角色與核心哲學

   你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。

   # 核心硬性規定 (鐵律，絕不可違反)

   ## 1. 單元測試斷言標準 (最高優先級)

   - 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

   - 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

   - 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

     正例：

     Action act = () => _service.DoSomething();

     act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");


   ## 2. 測試環境之檔案系統與 Mock 副作用防禦

   - 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 當測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或 其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。

   ## 3. 回覆語言

   - 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法 ）保持 C# 原生英文。

   - 針對程式碼的XML註解，請永遠使用英文(US, America)

   ## 4. 程式碼產生

   - 易讀性:針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`

   - 防禦性檢查:針對非單元測試的類別(和其方法)，請務必進行防禦性檢查(詳見`7. 防禦性檢查`)

   - 易維護、易測試:針對涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護、易測試:針對測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，其類別一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護:針對非單元測試的類別(和其方法)，盡可能地使用interface和decoration pattern(不過有例外地，請根據情境判斷之)

   - 簡潔:針對非單元測試的類別(和其方法)，請針對我正在開發的Utility package底下，適當地用facade pattern以減少其他要引用 該utility package的專案底下需要傳入太多引數至建構子或方法

   - 效能:針對我新開發的Utility package，優先使用效能最佳的手段 (詳見`5. 效能最佳之設計`)

   - 針對我新開發的Utility package，優先盡可能地設計出一個參數不多的API (詳見`6. 參數最佳之設計`)

   - 簡潔:針對非單元測試的類別(和其方法)，根據各種的Design Pattern的優缺點和其應用來決定是否要用哪種，若用到的Design Pattern需要使用介面時，則一併附上介面的定義。

   - 高重用性:針對非單元測試的類別(和其方法)，盡可能重用我自己已經開發完的Utility service(位在`~\modules`資料夾底下的專 案)、內建API

   、第三方套件的API

   - 易維護:針對非單元測試的類別，其產生的類別(和其方法、其Property)永遠應該要遵守‵.editorconfig‵的規則(且其掃描機制跟Rolsyn一模一樣)

   - 請使用最新語法完成設計(尤其是針對字串,詳見`8. 字串`)

   ## 5. 效能最佳之設計
   ### API
   包含但不限於

   - `struct`

   - `ReadOnlySpan<T>`, `ReadOnlyMemory<T>`等系列的API

   - `IMemoryCached` 等系列的API 而不是`Dictionary<T>`

   - `Stream`等系列的API

   - `PipelineWriter`,`PipelineReader` 等系列的API

   ### 策略
   包含但不限於

   - 快取機制

   - 增量機制

   - Zero allocation (詳見`ReadOnlySpan<T>`)

   - 減少GC的發生 (詳見`ReadOnlySpan<T>`)

   - 減少堆疊使用 (提示:可以使用`struct`而不是`class`)

   - 避免一次讀取大檔案 可使用`Stream`甚至是`Pipeline` (參考`PipelineWriter`等)


   ## 6. 參數最佳之設計
   ### 策略
   包含但不限於

   - POCO (`class`, `struct`, `record`, `record class` etc)

   - facade pattern (詳見`4. 程式碼產生`的某一條)

   ## 7. 防禦性檢查
   ### 檢查項目
   包含但不限於

   - 判斷傳入的引數的值是否合法 (如是否是null)

   - 根據意圖，判斷即將要回傳的回傳值是否合法

   ### 撰寫模式
   盡可能的

   - 使用.NET本身就有的內建方法(如:`ArgumentNullException.ThrowIfNull(x)`,`ArgumentOutOfRangeException.ThrowIfGreaterThan(counter,4)`)而不是用`throw new`來拋出異常(如:`throw new Excception("")`)

   ## 8. 字串
   ### 撰寫風格

   - 針對字串串接，請使用字串插值 (若可以)

   - 針對字串，請使用Raw String Literals
   ```

   ## File: global.json
   ```json
   {
     "test": {
       "runner": "VSTest"
     },
     "sdk": {
       "version": "10.0.103"
     }
   }
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

   ## File: PreloadPropsEntry.props.entry.config
   ```
   <Project>
     <ImportGroup Condition="'$(IsGlobalCustomBuildEntryLoaded)' == 'true'">
       <Import Project="$(EntryPointOfCustomBuildLogicsForUtilityPackages)" Condition="Exists('$(EntryPointOfCustomBuildLogicsForUtilityPackages)')" />
     </ImportGroup>
   </Project>
   ```

   ## File: repomix.config.json
   ```json
   {
     "$schema": "https://repomix.com/schemas/latest/schema.json",
     "input": {
       "maxFileSize": 52428800
     },
     "output": {
       "filePath": "CSharp-LearningNotes-repomix-output.md",
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
 --- Content End ---
 [File]      : Directory.Build.props
 --- Content Start ---
   <Project>
     <ImportGroup>
       <Import Project="$(MSBuildThisFileDirectory)\PreloadPropsEntry.props.entry.config" Condition="Exists('$(MSBuildThisFileDirectory)\PreloadPropsEntry.props.entry.config')" />
     </ImportGroup>
     <PropertyGroup>
       <Authors>codeloverjay30</Authors>
       <IsClassLibraryPackage>true</IsClassLibraryPackage>
       <IsBlazorWasmPackage>false</IsBlazorWasmPackage>
       <PackageRootPath>$(MSBuildThisFileDirectory)</PackageRootPath>
       <!-- Default path of local NuGet package that will be packed -->
       <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
     </PropertyGroup>
     <Choose>
       <When Condition="'$(IsClassLibraryPackage)' == 'true'">
         <!-- The package is class library or not -->
         <PropertyGroup>
           <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
         </PropertyGroup>
       </When>
       <!-- The package is used for Blazor WebAssembly or not -->
       <When Condition="'$(IsBlazorWasmPackage)' == 'true'">
         <PropertyGroup>
           <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\BlazorWebAssembly</MyNuGetPackageOutputPath>
         </PropertyGroup>
       </When>
       <Otherwise>
         <PropertyGroup>
           <MyNuGetPackageOutputPath>$(PackageRootPath)..\MyLocalNuget\ClassLibrary</MyNuGetPackageOutputPath>
         </PropertyGroup>
       </Otherwise>
     </Choose>
     <PropertyGroup>
       <PackageOutputPath>$(MyNuGetPackageOutputPath)</PackageOutputPath>
       <PackageId>$(MSBuildProjectName)</PackageId>
       <GeneratePackageOnBuild>false</GeneratePackageOnBuild>
       <PackageTags>Utility Service</PackageTags>
       <Description>See README.md</Description>
       <PackageReleaseNotes>See README.md</PackageReleaseNotes>
       <PackageReadmeFile>$(ProjectDir)README.md</PackageReadmeFile>
       <RepositoryUrl>https://github.com/codeloverjay30/utility_packages</RepositoryUrl>
       <RepositoryType>git</RepositoryType>
     </PropertyGroup>
     <!-- define property that belongs to framework at .NET SDK 6.0 -->
     <PropertyGroup>
       <IsAtLeastNet6>false</IsAtLeastNet6>
       <IsAtLeastNet6 Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '6.0'))">true</IsAtLeastNet6>
     </PropertyGroup>
     <!-- define property that belongs to legacy framework -->
     <PropertyGroup>
       <IsLegacyFramework>true</IsLegacyFramework>
       <IsLegacyFramework Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '6.0'))">false</IsLegacyFramework>
     </PropertyGroup>
     <!-- define property that belongs to framework at .NET SDK 8.0 -->
     <PropertyGroup>
       <IsAtLeastNet8>false</IsAtLeastNet8>
       <IsAtLeastNet8 Condition="'$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([MSBuild]::VersionGreaterThanOrEquals($(TargetFrameworkVersion), '8.0'))">true</IsAtLeastNet8>
     </PropertyGroup>
     <Choose>
         <When Condition="'$(IsAtLeastNet6)'=='true'">
           <PropertyGroup>
             <ImplicitUsings>enable</ImplicitUsings>
             <Nullable>enable</Nullable>
           </PropertyGroup>
         </When>
         <Otherwise>
           <PropertyGroup>
             <ImplicitUsings>disable</ImplicitUsings>
             <Nullable>disable</Nullable>
           </PropertyGroup>
         </Otherwise>
     </Choose>
     <PropertyGroup>
       <!-- set is as packable by default -->
       <IsPackable>true</IsPackable>

       <!-- If the project contains "Test", "Demo", or "Sample", set it as unpackable -->
       <IsTestProject Condition="$(MSBuildProjectName.Contains('Test'))">true</IsTestProject>
       <IsDemoProject Condition="$(MSBuildProjectName.Contains('Demo'))">true</IsDemoProject>
       <IsSampleProject Condition="$(MSBuildProjectName.Contains('Sample'))">true</IsSampleProject>

       <!-- indicates that project is experimental -->
       <IsExperimental Condition="$(MSBuildProjectDirectory.Contains('experiments'))">true</IsExperimental>

       <!-- indicates that project is stable -->
       <IsStable Condition="$(MSBuildProjectDirectory.Contains('modules'))">true</IsStable>

       <!-- indicates that project is stable and is module -->
       <IsModule Condition="$(MSBuildProjectDirectory.Contains('modules'))">true</IsModule>

       <!-- indicates that project is stable class library -->
       <IsClassLibrary Condition="'$(IsTestProject)' == 'true' Or '$(IsDemoProject)' == 'true' Or '$(IsSampleProject)' == 'true'">false</IsClassLibrary>

       <!-- set it as packable iff it is a class library and it is stable. Otherwise, set is unpackable -->
       <IsPackable Condition="('$(IsStable)' != 'true' And '$(IsModule)' != 'true') Or ('$(IsClassLibrary)' == 'false')">false</IsPackable>
     </PropertyGroup>
     <PropertyGroup Condition="'$(IsModule)' == 'true' And ( '$(IsClassLibrary)' == 'true' Or '$(IsTestProject)' == 'true')">
       <!-- build target path -->
       <OutputPath>$(SolutionDir)\artifacts\$(Configuration)\</OutputPath>
     </PropertyGroup>
     <!-- is it class library and is .NET SDK 8.0+ -->
     <PropertyGroup Condition="'$(IsAtLeastNet8)' == 'true' And '$(IsClassLibrary)' == 'true'">
       <!-- set the build target to put into artifacts path(see`<ArtifactsPath>`), indicating VSCode launcher (`.vscode/launch.json`) uses build target which placed in `<ArtifactsPath>`-->
       <UseArtifactsOutput>true</UseArtifactsOutput>
       <!-- artifacts path -->
       <ArtifactsPath>$(PackageRootPath).artifacts</ArtifactsPath>
     </PropertyGroup>
     <ItemGroup>
       <None Condition="'$(IsPackable)' == 'true'" Include="$(ProjectDir)README.md" Pack="true" PackagePath="\" />
     </ItemGroup>
   </Project>
 --- Content End ---
 [File]      : Directory.Build.targets
 --- Content Start ---
   <Project>
     <Target Name="RemoveVersions" BeforeTargets="CollectPackageReferences">
       <ItemGroup>
         <PackageReference Update="@(PackageReference)"
                         Version=""
                         Condition="'%(Identity)' != 'NETStandard.Library' And !$([System.String]::Copy('%(Identity)').StartsWith('Microsoft.Extensions.'))" />
       </ItemGroup>
     </Target>

     <!-- The Target is triggered once NuGet completes to restore -->
     <Target Name="ValidateCompatibilityPackageVersions" AfterTargets="_CheckForUnsupportedTargetFramework">

       <!-- Get all `<PackageVersion>`  -->
       <ItemGroup>
         <!-- If `NETStandard.Library package` is specified but its `Version` attribute is NOT specified or with incorrect value, then setting this tag and thus throws an error -->
         <InvalidCompats Include="@(PackageVersion)"
                         Condition="'%(Identity)' == 'NETStandard.Library' And '%(Version)' == ''" />
       </ItemGroup>

       <!-- if `InvalidCompats` is set, throws an error -->
       <Error Text="【架構警告】偵測到相容性套件 NETStandard.Library 未在中央明確指定版本號！請至 Directory.Packages.props 補上。"
              Code="ARCH001"
              Condition="'@(InvalidCompats)' != ''" />
     </Target>
   </Project>
 --- Content End ---
 [File]      : Directory.Packages.props
 --- Content Start ---
   <Project>
        <PropertyGroup>
          <NoWarn>$(NoWarn);NU1507</NoWarn>
        </PropertyGroup>
     <PropertyGroup>
       <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
       <CentralPackageTransitivePinningEnabled>true</CentralPackageTransitivePinningEnabled>
     </PropertyGroup>
     <!-- If one didn't specify the package version, treat it as errors instead of warnings -->
     <PropertyGroup>
       <WarningsAsErrors>$(WarningsAsErrors);NU1008;NU1605</WarningsAsErrors>
     </PropertyGroup>
     <!-- Let projetcs can be compiled in .NET Framework (older version) when not installed -->
     <ItemGroup>
       <GlobalPackageVersion Include="NETStandard.Library" Version="2.0.3" PrivateAssets="all" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive"/>
     </ItemGroup>
     <ItemGroup>
       <!-- This applies to ALL projects in the repository automatically -->
       <GlobalPackageVersion Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.3" PrivateAssets="all" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
     </ItemGroup>
     <!-- Use .ASP .NET Core Framework -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.AspNetCore.App.Ref" Version="10.0.5" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.TeamFoundationServer.ExtendedClient" Version="20.256.2" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.Extensions.TimeProvider.Testing" Version="10.6.0" />
     </ItemGroup>
     <!-- Mock File System -->
     <ItemGroup>
       <!-- abstraction of File system -->
       <PackageVersion Include="System.IO.Abstractions" Version="22.1.1" />
       <!-- Mock File System -->
       <PackageVersion Include="System.IO.Abstractions.TestingHelpers" Version="22.1.1" />
     </ItemGroup>
     <!-- abstraction of System Process -->
     <ItemGroup>
       <PackageVersion Include="System.Diagnostics.Abstractions" Version="1.0.9" />
     </ItemGroup>
     <!-- abstraction of System Process (used by older test framework such as `xUnit2`) -->
     <ItemGroup>
       <PackageVersion Include="Testably.Abstractions.FileSystem.Interface" Version="10.2.0" />
     </ItemGroup>
     <!-- abstraction of Environment class -->
     <ItemGroup>
       <PackageVersion Include="EnvironmentAbstractions" Version="5.0.0" />
       <!-- Mock Environment -->
       <PackageVersion Include="EnvironmentAbstractions.TestHelpers" Version="5.0.0" />
     </ItemGroup>
     <!-- Official utility packages of command line -->
     <ItemGroup>
       <PackageVersion Include="System.CommandLine" Version="2.0.5" />
     </ItemGroup>
     <!-- `MSBuild`, simulating `MSBuild` behaviour -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Build" Version="18.4.0" />
     </ItemGroup>
     <!-- Locator of `MSBuild` -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Build.Locator" Version="1.11.2" />
     </ItemGroup>
     <!-- Official utility packages about accessing version of NuGet package or .NET framework -->
     <ItemGroup>
       <!-- accessing version of  NuGet package -->
       <PackageVersion Include="NuGet.Protocol" Version="7.3.1" />
       <!-- accessing version of  .NET framework -->
       <PackageVersion Include="NuGet.Frameworks" Version="7.3.1" />
     </ItemGroup>
     <!-- Use traditional mode `VSTest` -->
     <!-- `NUnit` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="NUnit" Version="4.3.2" />
       <PackageVersion Include="NUnit3TestAdapter" Version="6.2.0" />
       <PackageVersion Include="NUnit.Analyzers" Version="4.7.0" />
     </ItemGroup>
     <!-- `xUnit.v3` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="coverlet.collector" Version="6.0.4" />
       <PackageVersion Include="xunit.v3" Version="3.2.2" />
       <PackageVersion Include="xunit.v3.mtp-v2" Version="3.2.2" />
       <PackageVersion Include="xunit.v3.extensibility.core" Version="3.2.2" />
     </ItemGroup>
     <!-- package that checks the class or property contains specific words, it uses `xUnit.v2` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="TngTech.ArchUnitNET.xUnit" Version="0.13.3" />
     </ItemGroup>
     <!-- `xUnit.v2` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="xunit" Version="2.9.3" />
       <PackageVersion Include="xunit.runner.visualstudio" Version="3.1.4" />
     </ItemGroup>
     <!-- Use modern mode `MTP` -->
     <!-- use `MSTest` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="MSTest.TestAdapter" Version="3.2.2" />
       <PackageVersion Include="MSTest.TestFramework" Version="3.2.2" />
       <PackageVersion Include="Microsoft.Testing.Extensions.VSTestBridge" Version="2.1.0" />
     </ItemGroup>
     <!-- use `NUnit` using `MTP` as Test Framework -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.TestPlatform" Version="18.4.0" />
       <PackageVersion Include="Microsoft.Testing.Platform.MSBuild" Version="2.2.1" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.Testing.Platform" Version="2.2.1" />
     </ItemGroup>
     <!-- packages that makes the assertions be more fluent -->
     <ItemGroup>
       <PackageVersion Include="FluentAssertions" Version="8.9.0" />
       <PackageVersion Include="FluentValidation" Version="12.1.1" />
     </ItemGroup>
     <!-- packages that mock the interface and virtual class -->
     <ItemGroup>
       <PackageVersion Include="Moq" Version="4.20.72" />
     </ItemGroup>
     <!-- packages that checks the AST of class (including, check a class contains property named `property1`) -->
     <ItemGroup>
       <PackageVersion Include="NSubstitute" Version="5.3.0" />
     </ItemGroup>
     <!-- Rolsyn -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.CodeAnalysis.CSharp" Version="5.3.0" />
       <PackageVersion Include="Microsoft.CodeAnalysis.CSharp.Workspaces" Version="5.3.0" />
     </ItemGroup>
     <!-- Rolsyn used for tests -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.CodeAnalysis.CSharp.SourceGenerators.Testing" Version="1.1.3" />
     </ItemGroup>
     <!-- Roslynator -->
     <ItemGroup>
       <PackageVersion Include="Roslynator.Analyzers" Version="4.15.0"/>
     </ItemGroup>
     <!-- SonarAnalyzer -->
     <ItemGroup>
       <PackageVersion Include="SonarAnalyzer.CSharp" Version="10.25.0.139117" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Microsoft.Bcl.AsyncInterfaces" Version="10.0.7" />
       <PackageVersion Include="System.ClientModel" Version="1.10.0" />
       <PackageVersion Include="System.Memory.Data" Version="10.0.3" />
     </ItemGroup>
      <!-- logger `ILogger` -->
      <ItemGroup>
           <PackageVersion Include="Microsoft.Extensions.Logging" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Logging.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Options" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.DependencyInjection" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Configuration.Abstractions" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Primitives" Version="[11.0.0-preview.2.26159.112]" />
           <PackageVersion Include="Microsoft.Extensions.Hosting" Version="10.0.3" />
           <PackageVersion Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.3" />
       </ItemGroup>

      <!-- Azure -->
     <ItemGroup>
       <PackageVersion Include="Azure.ResourceManager" Version="1.14.0" />
       <PackageVersion Include="Azure.Identity" Version="1.18.0" />
       <PackageVersion Include="Azure.Core" Version="1.51.1" />
       <PackageVersion Include="Microsoft.Extensions.Configuration" Version="10.0.3" />
       <PackageVersion Include="Microsoft.Extensions.Configuration.AzureAppConfiguration" Version="8.5.0" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="System.Management" Version="10.0.2" />
     </ItemGroup>
     <!-- Registry key on Windows OS -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Win32.Registry" Version="5.0.0" />
     </ItemGroup>
     <!-- ACL on Windows OS -->
     <ItemGroup>
       <PackageVersion Include="System.Security.AccessControl" Version="6.0.1" />
     </ItemGroup>
     <!-- lock -->
     <ItemGroup>
       <PackageVersion Include="AsyncKeyedLock" Version="8.0.2" />
     </ItemGroup>
     <!-- EF Core -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.EntityFrameworkCore" Version="10.0.3" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="StackExchange.Redis" Version="2.11.0" />
     </ItemGroup>
     <!-- `Serilog` -->
     <ItemGroup>
       <PackageVersion Include="Serilog" Version="4.3.1" />
       <PackageVersion Include="Serilog.Settings.Configuration" Version="10.0.0" />
       <PackageVersion Include="Serilog.Enrichers.Environment" Version="3.0.1" />
       <PackageVersion Include="Serilog.Exceptions" Version="8.4.0" />
       <PackageVersion Include="Serilog.Enrichers.Demystifier" Version="1.0.3" />
       <PackageVersion Include="Serilog.Expressions" Version="5.0.0" />
       <PackageVersion Include="Serilog.Sinks.File" Version="7.0.0" />
       <PackageVersion Include="Serilog.Sinks.Console" Version="6.1.1" />
       <PackageVersion Include="Serilog.Extensions.Hosting" Version="10.0.0" />
       <PackageVersion Include="Serilog.Extensions.Logging" Version="10.0.0" />
     </ItemGroup>
     <!-- `Nuke` -->
     <ItemGroup>
       <PackageVersion Include="Nuke.Common" Version="10.1.0" />
     </ItemGroup>
     <!-- `PostSharp` -->
     <ItemGroup>
       <PackageVersion Include="PostSharp" Version="2026.0.6" />
       <PackageVersion Include="PostSharp.Patterns.Diagnostics" Version="2026.0.6" />
     </ItemGroup>
     <!-- `Appium.WebDriver` -->
     <ItemGroup>
       <PackageVersion Include="Appium.WebDriver" Version="8.1.0" />
       <PackageVersion Include="Selenium.WebDriver" Version="4.36.0" />
     </ItemGroup>
     <!-- `Polly` -->
     <ItemGroup>
       <PackageVersion Include="Polly" Version="8.6.6" />
       <PackageVersion Include="Polly.Core" Version="8.6.6" />
       <PackageVersion Include="Polly.RateLimiting" Version="8.6.6" />
       <PackageVersion Include="Polly.Extensions" Version="8.6.6" />
     </ItemGroup>
     <!-- other packages -->
     <ItemGroup>
       <PackageVersion Include="MiniExcel" Version="2.0.0-preview.2" />
       <PackageVersion Include="QRCoder" Version="1.7.0" />
       <PackageVersion Include="Otp.NET" Version="1.4.1" />
       <PackageVersion Include="Spectre.Console" Version="0.54.0" />
       <PackageVersion Include="MediatR" Version="14.0.0" />
     </ItemGroup>
     <!-- about command line (CLI)  -->
     <ItemGroup>
       <PackageVersion Include="CliWrap" Version="3.10.1" />
     </ItemGroup>
      <!-- about encoding -->
     <ItemGroup>
       <PackageVersion Include="System.Text.Encoding.CodePages" Version="10.0.7" />
     </ItemGroup>
     <!-- about sanitization -->
     <ItemGroup>
       <PackageVersion Include="System.Text.Encodings.Web" Version="10.0.7" />
     </ItemGroup>
     <!-- about file system globbing -->
     <ItemGroup>
       <PackageVersion Include="Microsoft.Extensions.FileSystemGlobbing" Version="10.0.7" />
     </ItemGroup>
     <!-- F# compiler and F# core library -->
     <ItemGroup>
       <PackageVersion Include="FSharp.Compiler.Service" Version="43.12.203" />
       <PackageVersion Include="FSharp.Core" Version="10.1.203" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="Dumpify" Version="0.7.0" />
     </ItemGroup>
     <!-- About AhoCorasick algorithm -->
     <ItemGroup>
       <PackageVersion Include="NReco.Text.AhoCorasickDoubleArrayTrie" Version="1.1.1" />
     </ItemGroup>
     <!-- My developed NuGet packages -->
     <!-- -->
     <ItemGroup>
       <PackageVersion Include="AiUtility.AiBaseUtilityServices" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="AiUtility.ToolKits" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="AiUtility.GeminiKits" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="AiUtility.GeminiUtilityServices" Version="[3.0.0-preview-1.0.0]" />
     </ItemGroup>
     <!-- My developed NuGet packages -->
     <!-- Utility services about `enum` -->
     <ItemGroup>
       <PackageVersion Include="EnumUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <!-- my custom Attributes in data annotations that are commonly used -->
     <ItemGroup>
       <PackageVersion Include="CustomDataAnnotations" Version="2.4.0" />
     </ItemGroup>
     <!-- About type (System.Type) -->
     <ItemGroup>
       <PackageVersion Include="TypeConstants" Version="2.1.0" />
       <PackageVersion Include="MimeTypeConstants" Version="3.0.0" />
     </ItemGroup>
     <!-- Extension services that extends existing Exception (or its subclass) and defined my customized exceptions -->
     <ItemGroup>
       <PackageVersion Include="ExceptionsUtilityServices" Version="1.1.0" />
     </ItemGroup>
     <!-- My developed NuGet packages -->
     <!-- About Expression Tree-->
     <ItemGroup>
       <PackageVersion Include="ExpressionTreeUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <!-- About caching Reflection-->
     <ItemGroup>
       <PackageVersion Include="ReflectionUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="LogNameUtilityFactories" Version="[2.0.0-preview-1.0.0]" />
     </ItemGroup>
       <PackageVersion Include="RegexUtilityServices" Version="2.0.0" />
       <PackageVersion Include="AssemblyUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="ExceptionHandlingUtilityServices" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="ExceptionFactories" Version="2.0.0" />
       <PackageVersion Include="ExceptionWrappers" Version="[3.0.0-preview-1.0.0]" />
       <PackageVersion Include="FileStreamUtilityServices" Version="2.1.0" />
       <PackageVersion Include="JsonUtilityServices" Version="2.0.0" />
       <PackageVersion Include="LoggerFactoryUtilityServices" Version="[5.0.0-preview-1.0.0]" />
       <PackageVersion Include="TaskUtilityServices" Version="2.0.0" />
       <PackageVersion Include="ThreadLevelLockingUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="TypeUtilityServices" Version="2.0.0" />
       <PackageVersion Include="SerilogHelperServices" Version="2.0.0" />
       <PackageVersion Include="CoordinateUtilityServices" Version="2.0.0" />
       <PackageVersion Include="TransversalUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="AzureUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="ListItemUitilityServices" Version="2.0.0" />
       <PackageVersion Include="MathUtilityServices" Version="2.0.0" />
       <PackageVersion Include="StringUtilityServices" Version="2.0.0" />
       <PackageVersion Include="NetRuntimeUtilityServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="ILoggerBuilderFactoryServices" Version="[2.0.0-preview-1.0.0]" />
       <PackageVersion Include="LoggingCodeTemplateGenerators" Version="2.0.0" />
       <PackageVersion Include="IOOperationUtilityServices" Version="[5.0.0-preview-1.0.0]" />
       <PackageVersion Include="EntriesAndTheirContentsServices" Version="[1.0.0-preview-1.0.0]" />
       <PackageVersion Include="SortingUtilityServices" Version="1.0.1" />
       <PackageVersion Include="CommonModels" Version="2.1.0" />
       <PackageVersion Include="ConsoleUtilityServices" Version="1.0.0" />
       <PackageVersion Include="FileCategorizationUtilityServices" Version="2.0.0" />
     </ItemGroup>
     <ItemGroup>
       <!-- Get or check OS Environment-->
       <PackageVersion Include="EnvironmentUtilityServices" Version="3.0.0" />
       <!-- Get or check specific drive -->
       <PackageVersion Include="DriveInfoUtilityServices" Version="1.0.0-preview-1.0.0" />
       <!-- Utility services about symbolic links (based on low-level API)-->
       <PackageVersion Include="SymbolicLinkUtilityServices" Version="1.0.0-preview-1.0.0" />
     </ItemGroup>
     <!-- About cli -->
     <ItemGroup>
         <!-- wrapper or utility service based on `CliWrap` package -->
         <PackageVersion Include="CliUtilityServices" Version="[4.0.0-preview-1.0.0]" />
     </ItemGroup>
     <ItemGroup>
       <PackageVersion Include="ProjectsVersionUtilityServices" Version="1.0.0" />
       <PackageVersion Include="SolutionUtilityServices" Version="[5.0.0-preview-1.0.0]" />
       <PackageVersion Include="PaketUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- check which programming languages are used -->
     <ItemGroup>
       <PackageVersion Include="ProgrammingLanguageUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- discovering script -->
     <ItemGroup>
       <PackageVersion Include="ScriptDiscoveryUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- Utility services that are shared (or commonly used) -->
     <ItemGroup>
       <PackageVersion Include="SharedUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- About Span or its related classes -->
     <ItemGroup>
       <PackageVersion Include="SpanUtilityServices" Version="[3.0.0-preview-1.0.0]" />
     </ItemGroup>
     <!-- Utility services that wraps searching file (similar to behavior of File Explorer) -->
     <ItemGroup>
       <PackageVersion Include="FileExplorerUtilityServices" Version="[1.0.0-preview-1.0.0]" />
     </ItemGroup>
     <!-- Wrapper of decimal to fraction (my custom defined class named `Fraction`) -->
     <ItemGroup>
       <PackageVersion Include="FractionsUtilityServices" Version="1.0.0" />
     </ItemGroup>
     <!-- Utility services that move one or more apps in Windows -->
     <ItemGroup>
       <PackageVersion Include="WindowsAppUtilityServices" Version="[4.0.0-preview-1.0.0]" />
     </ItemGroup>
   </Project>
 --- Content End ---
 [File]      : GEMINI.md
 --- Content Start ---
   # 角色與核心哲學

   你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。

   # 核心硬性規定 (鐵律，絕不可違反)

   ## 1. 單元測試斷言標準 (最高優先級)

   - 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

   - 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

   - 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

     正例：

     Action act = () => _service.DoSomething();

     act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");


   ## 2. 測試環境之檔案系統與 Mock 副作用防禦

   - 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 當測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或 其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。

   ## 3. 回覆語言

   - 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法 ）保持 C# 原生英文。

   - 針對程式碼的XML註解，請永遠使用英文(US, America)

   ## 4. 程式碼產生

   - 易讀性:針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`

   - 防禦性檢查:針對非單元測試的類別(和其方法)，請務必進行防禦性檢查(詳見`7. 防禦性檢查`)

   - 易維護、易測試:針對涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護、易測試:針對測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，其類別一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

   - 易維護:針對非單元測試的類別(和其方法)，盡可能地使用interface和decoration pattern(不過有例外地，請根據情境判斷之)

   - 簡潔:針對非單元測試的類別(和其方法)，請針對我正在開發的Utility package底下，適當地用facade pattern以減少其他要引用 該utility package的專案底下需要傳入太多引數至建構子或方法

   - 效能:針對我新開發的Utility package，優先使用效能最佳的手段 (詳見`5. 效能最佳之設計`)

   - 針對我新開發的Utility package，優先盡可能地設計出一個參數不多的API (詳見`6. 參數最佳之設計`)

   - 簡潔:針對非單元測試的類別(和其方法)，根據各種的Design Pattern的優缺點和其應用來決定是否要用哪種，若用到的Design Pattern需要使用介面時，則一併附上介面的定義。

   - 高重用性:針對非單元測試的類別(和其方法)，盡可能重用我自己已經開發完的Utility service(位在`~\modules`資料夾底下的專 案)、內建API

   、第三方套件的API

   - 易維護:針對非單元測試的類別，其產生的類別(和其方法、其Property)永遠應該要遵守‵.editorconfig‵的規則(且其掃描機制跟Rolsyn一模一樣)

   - 請使用最新語法完成設計(尤其是針對字串,詳見`8. 字串`)

   ## 5. 效能最佳之設計
   ### API
   包含但不限於

   - `struct`

   - `ReadOnlySpan<T>`, `ReadOnlyMemory<T>`等系列的API

   - `IMemoryCached` 等系列的API 而不是`Dictionary<T>`

   - `Stream`等系列的API

   - `PipelineWriter`,`PipelineReader` 等系列的API

   ### 策略
   包含但不限於

   - 快取機制

   - 增量機制

   - Zero allocation (詳見`ReadOnlySpan<T>`)

   - 減少GC的發生 (詳見`ReadOnlySpan<T>`)

   - 減少堆疊使用 (提示:可以使用`struct`而不是`class`)

   - 避免一次讀取大檔案 可使用`Stream`甚至是`Pipeline` (參考`PipelineWriter`等)


   ## 6. 參數最佳之設計
   ### 策略
   包含但不限於

   - POCO (`class`, `struct`, `record`, `record class` etc)

   - facade pattern (詳見`4. 程式碼產生`的某一條)

   ## 7. 防禦性檢查
   ### 檢查項目
   包含但不限於

   - 判斷傳入的引數的值是否合法 (如是否是null)

   - 根據意圖，判斷即將要回傳的回傳值是否合法

   ### 撰寫模式
   盡可能的

   - 使用.NET本身就有的內建方法(如:`ArgumentNullException.ThrowIfNull(x)`,`ArgumentOutOfRangeException.ThrowIfGreaterThan(counter,4)`)而不是用`throw new`來拋出異常(如:`throw new Excception("")`)

   ## 8. 字串
   ### 撰寫風格

   - 針對字串串接，請使用字串插值 (若可以)

   - 針對字串，請使用Raw String Literals

 --- Content End ---
 [File]      : global.json
 --- Content Start ---
   {
     "test": {
       "runner": "VSTest"
     },
     "sdk": {
       "version": "10.0.103"
     }
   }
 --- Content End ---
 [File]      : nuget.config
 --- Content Start ---
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
 --- Content End ---
 [File]      : PreloadPropsEntry.props.entry.config
 --- Content Start ---
   <Project>
     <ImportGroup Condition="'$(IsGlobalCustomBuildEntryLoaded)' == 'true'">
       <Import Project="$(EntryPointOfCustomBuildLogicsForUtilityPackages)" Condition="Exists('$(EntryPointOfCustomBuildLogicsForUtilityPackages)')" />
     </ImportGroup>
   </Project>
 --- Content End ---
 [File]      : repomix.config.json
 --- Content Start ---
   {
     "$schema": "https://repomix.com/schemas/latest/schema.json",
     "input": {
       "maxFileSize": 52428800
     },
     "output": {
       "filePath": "CSharp-LearningNotes-repomix-output.md",
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
 --- Content End ---
PS D:\workspace\Scripts>
````

## File: GEMINI.md
````markdown
# 角色與核心哲學

你是一位極度嚴謹的 .NET 進階軟體架構師。你崇尚防禦性程式設計（Defensive Programming）、乾淨架構（Clean Architecture）與單一職責原則（SRP）。你寫出的程式碼必須考慮到底層非預期異常的攔截與還原。

# 核心硬性規定 (鐵律，絕不可違反)

## 1. 單元測試斷言標準 (最高優先級)

- 絕對禁止使用 xUnit 的原生斷言方法（例如：Assert.True, Assert.Throws, Assert.Equal）。

- 所有單元測試的斷言、值驗證、異常拋出測試，一律、必須、永遠使用 `FluentAssertions` 套件。

- 測試異常時，必須使用 Action 攔截並驗證真實的 Exception Message。

  正例：

  Action act = () => _service.DoSomething();

  act.Should().Throw<InvalidOperationException>().WithMessage("*預期的關鍵字*");


## 2. 測試環境之檔案系統與 Mock 副作用防禦

- 當測試涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 當測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 在使用 Moq 時，必須主動防禦 `DefaultValue.Mock` 帶來的遞迴/平行時空副作用。如果對象方法內部會點擊 `.FileSystem` 或其他未 Setup 的導覽屬性，必須明確 Setup，嚴防底層噴出隱蔽的 `NotSupportedException` 或 `NullReferenceException`。

## 3. 回覆語言

- 請永遠使用 繁體中文 (台灣，Traditional Chinese) 回覆，但程式碼、專案術語及測試框架方法（如 FluentAssertions 的語法）保持 C# 原生英文。

- 針對程式碼的XML註解，請永遠使用英文(US, America)

## 4. 程式碼產生

- 易讀性:針對非單元測試的類別(和其方法)，永遠在類別(和其方法)的正上方寫`XML註解`

- 防禦性檢查:針對非單元測試的類別(和其方法)，請務必進行防禦性檢查(詳見`7. 防禦性檢查`)

- 易維護、易測試:針對涉及 IO 或檔案系統時，一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 易維護、易測試:針對測試涉及跟生產環境有關時(舉例:判斷是否被掛載Debugger)，其類別一律使用 `System.IO.Abstractions`或其他可以被Mock的介面。

- 易維護:針對非單元測試的類別(和其方法)，盡可能地使用interface和decoration pattern(不過有例外地，請根據情境判斷之)

- 簡潔:針對非單元測試的類別(和其方法)，請針對我正在開發的Utility package底下，適當地用facade pattern以減少其他要引用該utility package的專案底下需要傳入太多引數至建構子或方法

- 效能:針對我新開發的Utility package，優先使用效能最佳的手段 (詳見`5. 效能最佳之設計`)

- 針對我新開發的Utility package，優先盡可能地設計出一個參數不多的API (詳見`6. 參數最佳之設計`)

- 簡潔:針對非單元測試的類別(和其方法)，根據各種的Design Pattern的優缺點和其應用來決定是否要用哪種，若用到的Design Pattern需要使用介面時，則一併附上介面的定義。

- 高重用性:針對非單元測試的類別(和其方法)，盡可能重用我自己已經開發完的Utility service(位在`~\modules`資料夾底下的專案)、內建API

、第三方套件的API

- 易維護:針對非單元測試的類別，其產生的類別(和其方法、其Property)永遠應該要遵守‵.editorconfig‵的規則(且其掃描機制跟Rolsyn一模一樣)

- 請使用最新語法完成設計(尤其是針對字串,詳見`8. 字串`)

## 5. 效能最佳之設計
### API
包含但不限於

- `struct`

- `ReadOnlySpan<T>`, `ReadOnlyMemory<T>`等系列的API

- `IMemoryCached` 等系列的API 而不是`Dictionary<T>`

- `Stream`等系列的API

- `PipelineWriter`,`PipelineReader` 等系列的API

### 策略
包含但不限於

- 快取機制

- 增量機制

- Zero allocation (詳見`ReadOnlySpan<T>`)

- 減少GC的發生 (詳見`ReadOnlySpan<T>`)

- 減少堆疊使用 (提示:可以使用`struct`而不是`class`)

- 避免一次讀取大檔案 可使用`Stream`甚至是`Pipeline` (參考`PipelineWriter`等)


## 6. 參數最佳之設計
### 策略
包含但不限於

- POCO (`class`, `struct`, `record`, `record class` etc)

- facade pattern (詳見`4. 程式碼產生`的某一條)

## 7. 防禦性檢查
### 檢查項目
包含但不限於

- 判斷傳入的引數的值是否合法 (如是否是null)

- 根據意圖，判斷即將要回傳的回傳值是否合法

### 撰寫模式
盡可能的

- 使用.NET本身就有的內建方法(如:`ArgumentNullException.ThrowIfNull(x)`,`ArgumentOutOfRangeException.ThrowIfGreaterThan(counter,4)`)而不是用`throw new`來拋出異常(如:`throw new Excception("")`)

## 8. 字串
### 撰寫風格

- 針對字串串接，請使用字串插值 (若可以)

- 針對字串，請使用Raw String Literals
````

## File: global.json
````json
{
  "test": {
    "runner": "VSTest"
  },
  "sdk": {
    "version": "10.0.103"
  }
}
````

## File: nuget.config
````
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
````

## File: PreloadPropsEntry.props.entry.config
````
<Project>
  <ImportGroup Condition="'$(IsGlobalCustomBuildEntryLoaded)' == 'true'">
    <Import Project="$(EntryPointOfCustomBuildLogicsForUtilityPackages)" Condition="Exists('$(EntryPointOfCustomBuildLogicsForUtilityPackages)')" />
  </ImportGroup>
</Project>
````

## File: repomix.config.json
````json
{
  "$schema": "https://repomix.com/schemas/latest/schema.json",
  "input": {
    "maxFileSize": 52428800
  },
  "output": {
    "filePath": "CSharp-LearningNotes-repomix-output.md",
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
````
