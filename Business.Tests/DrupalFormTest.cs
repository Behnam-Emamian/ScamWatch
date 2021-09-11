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
            Description = 
@"I have received this sms:

bla bla bla",
            ScammerPhoneNumber = "0412345678",
            TargetEmail = "test@test.com",
            TargetFirstName = "my first name",
            TargetGender = Gender.male,
            TargetLastName = "my last name",
            TargetPhoneNumber="0487654321",
            TargetState = State.NSW
        };
        var submission = await drupalForm.Process(report);

        Assert.True(submission);
    }

    [Fact]
    public async Task GivenAScamCall_SubmitAReport()
    {
        var drupalForm = new DrupalForm();
        var report = new Report
        {
            ScamType = ScamType.IdentityTheft,
            DeliveryMethod = DeliveryMethod.text_message,
            FirstContactDate = DateOnly.Parse("9/9/2021"),
            Description = @"I have received a call to convised me to give personal information",
            ScammerPhoneNumber = "0412345678",
            TargetEmail = "test@test.com",
            TargetFirstName = "my first name",
            TargetGender = Gender.male,
            TargetLastName = "my last name",
            TargetPhoneNumber = "0487654321",
            TargetState = State.NSW
        };
        var submission = await drupalForm.Process(report);

        Assert.True(submission);
    }
}
