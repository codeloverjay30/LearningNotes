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
    public const string CSPROJ = "*.csproj";
}