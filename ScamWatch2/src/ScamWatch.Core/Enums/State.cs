namespace ScamWatch.Core.Enums;

public enum State
{
    ACT,
    NSW,
    NT,
    QLD,
    SA,
    TAS,
    VIC,
    WA,
    OS
}

public static class StateExtensions
{
    public static string GetString(this State me)
    {
        return me switch
        {
            State.ACT => "Australian Capital Territory",
            State.NSW => "New South Wales",
            State.NT => "Northern Territory",
            State.QLD => "Queensland",
            State.SA => "South Australia",
            State.TAS => "Tasmania",
            State.VIC => "Victoria",
            State.WA => "Western Australia",
            State.OS => "Outside of Australia",
        };
    }
}
