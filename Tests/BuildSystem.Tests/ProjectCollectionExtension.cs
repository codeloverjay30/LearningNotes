using static BuildSystem.Tests.BooleanExtension;

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
        // ArgumentNullException.ThrowIfNullOrWhiteSpace(projectCollectionSettings.DirectoryPackagesPropsPath, nameof(projectCollectionSettings.DirectoryPackagesPropsPath));
        ArgumentNullException.ThrowIfNull(projectCollectionSettings.ShouldStopRecursivelySearchWhenFound, nameof(projectCollectionSettings.ShouldStopRecursivelySearchWhenFound));

        // 建議修改方式：在測試中注入絕對隔離的設定
        var globalProperties = new Dictionary<string, string>
        {
            // 防禦性配置，跳過所有可能會載入全域邏輯的 SDK 掃描
            { "SkipImportWorkloads", "true" },
            { "DisableMSBuildDiagnosticFilters", "true" },
        };

        // 我擔心 directoryPackagesPropsPath的值在if中判斷前被其他執行續改變
        string? directoryPackagesPropsPath = projectCollectionSettings.DirectoryPackagesPropsPath;
        if (
            string.IsNullOrWhiteSpace(directoryPackagesPropsPath)
        )
        {
            // 強制截斷繼承鏈，確保不會向上搜尋或其兄弟的組態 
            // (如:在`D:\workspace\LearningNotes\Tests\BuildSystem.Tests`目錄讀取到`D:\workspace\LearningNotes\CSharp` 目錄的組態)
            globalProperties.Add(
                 "DirectoryPackagesPropsPath",
                 directoryPackagesPropsPath
            ); 
            
            // 針對沒進這個分支這個情況的提醒:
            // 若沒被設定(使用預設值null)或指派為null或empty string, string only consist of whitespace，
            // 則會msbuild不會只找特定的路徑，
            // 而是根據當初載入時專案之其專案設定檔案之內容和msbuild的預設行為(預設行為:由下往上搜尋)搜尋 
            // (更精確地說是`Microsoft.Build.Evaluation`套件內部的實作行為。
            // 若沒意外，詳見下方，則`Microsoft.Build.Evaluation`套件內部的實作行為跟msbuild的行為一致)
            // 警語:
            // 雖然這保持了靈活性，但Microsoft.Build.Evaluation等相關套件有時在極少數的邊界案例中，他的實作行為有時跟msbuild的預設行為不一致。
        }

        string? managePackageVersionsCentrally;
        // 若 ManagePackageVersionsCentrally 不為空才加入設定，若為空則不加入並根據msbuild(透過`ProjectCollection`所用到的IDictionary)找到的為準
        if (
            projectCollectionSettings.ManagePackageVersionsCentrally.HasValue &&
            (managePackageVersionsCentrally = projectCollectionSettings.ManagePackageVersionsCentrally.ToLiteralString()) is not null
        )
        {
            globalProperties.Add(
                "ManagePackageVersionsCentrally",
                managePackageVersionsCentrally
            );
        }
        
        if (projectCollectionSettings.ShouldStopRecursivelySearchWhenFound == false)
        {
            globalProperties.AddRange(
                new Dictionary<string, string>() {
                { "ImportDirectoryBuildProps", "false" },
                { "ImportDirectoryBuildTargets", "false" },
            });
        }

        return globalProperties;
    }
}