namespace BuildSystem.Tests;

public static class BooleanExtension
{
    /// <summary>
    /// Let <paramref name="value"/> with type <see cref="global::System.Boolean"> represent to <see cref="global::System.String"/>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string? ToLiteralString(
        this bool? value
    )
    {
        return value.HasValue ? (value.Value == true ? "true" : "false") : null;
    }

    public static string ToLiteralString(
        this bool value
    )
    {
        return value == true ? "true" : "false";
    }
}
