using ScamWatch.Core.Enums;
using System;

namespace ScamWatch.Core.Models;

public class Report
{
    public ScamType ScamType { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public DateTime FirstContactDate { get; set; }
    public string Description { get; set; }

    public string ScammerPhoneNumber { get; set; }

    public string TargetFirstName { get; set; }
    public string TargetLastName { get; set; }
    public string TargetEmail { get; set; }
    public string TargetPhoneNumber { get; set; }
    public Gender TargetGender { get; set; }
    public State TargetState { get; set; }
}
