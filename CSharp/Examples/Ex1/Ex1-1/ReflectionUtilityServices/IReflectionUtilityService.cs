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
