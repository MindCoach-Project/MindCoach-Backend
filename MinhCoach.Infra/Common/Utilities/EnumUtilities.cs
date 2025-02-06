namespace MinhCoach.Infra.Common.Utilities;

public static class EnumUtilities
{
    public static string GetEnumAsString<T>()
    {
        var enumValues = Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(v => $"'{v}'")  
            .ToArray();

        return string.Join(", ", enumValues);
    }
}