namespace BuildSystem.Tests;

public static class DummyFileTemplates
{
    public const string CSPROJ_FORMAT_TEMPLATE = @"
        <Project Sdk='Microsoft.NET.Sdk'>
            <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
            </PropertyGroup>
        </Project>";
}
