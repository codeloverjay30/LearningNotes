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
    
