using Business.Enums;

namespace Business.Models;

public class Report
{
    public ScamType ScamType { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public DateOnly FirstContactDate { get; set; }
    public string ScammerPhoneNumber { get; set; }
    public string? Description { get; set; }
}