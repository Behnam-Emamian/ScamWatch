namespace Business.Enums;

public enum Gender
{
    male,
    female,
    not_specified
}

public static class GenderExtensions
{
    public static string GetString(this Gender me)
    {
        return me switch
        {
            Gender.male => "Male",
            Gender.female => "Female",
            Gender.not_specified => "X (Indeterminate/Intersex/Unspecified)",
        };
    }
}