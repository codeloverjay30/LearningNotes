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
