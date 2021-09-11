using Business.Enums;
using Business.Models;
using Xunit;

namespace Business.Tests;

public class DrupalFormTest
{
    [Fact]
    public async Task GivenAScamSMS_SubmitAReport()
    {
        var drupalForm = new DrupalForm();
        var report = new Report
        {
            ScamType = ScamType.IdentityTheft,
            DeliveryMethod = DeliveryMethod.text_message,
            FirstContactDate = DateOnly.Parse("9/9/2021"),
            ScammerPhoneNumber = "0412345678",
            Description = @"I have received this sms:

bla bla bla"
        };
        await drupalForm.Process(report);
    }

    [Fact]
    public async Task GivenAScamCall_SubmitAReport()
    {
    }
}
