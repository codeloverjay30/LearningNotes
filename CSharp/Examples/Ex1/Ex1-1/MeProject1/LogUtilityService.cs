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
