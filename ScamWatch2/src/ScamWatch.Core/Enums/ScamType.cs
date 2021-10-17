namespace ScamWatch.Core.Enums;

public enum ScamType
{
    IdentityTheft = 30
}

public static class ScamTypeExtensions
{
    public static string GetString(this ScamType me)
    {
        return me switch
        {
            ScamType.IdentityTheft => "Identity Theft",
        };
    }
}
