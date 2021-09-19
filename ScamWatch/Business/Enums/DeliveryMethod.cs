namespace Business.Enums;

public enum DeliveryMethod
{
    phone,
    text_message
}

public static class DeliveryMethodExtensions
{
    public static string GetString(this DeliveryMethod me)
    {
        return me switch
        {
            DeliveryMethod.phone => "Phone (Voice)",
            DeliveryMethod.text_message => "Text message",
            _ => "",
        };
    }
}