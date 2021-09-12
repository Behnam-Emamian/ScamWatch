using Business.Enums;
using Business.Models;
using Business.Services;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace Business.Tests.Services;

public class SubmitDrupalFormServiceTest
{
    private readonly HttpClient _client;

    public SubmitDrupalFormServiceTest()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("<html><body><input name='form_build_id' value='form-MfFACf1XpqL-35Ikfi-fmTx-mkgg5n2_cHHvSihP6PU' /></body></html>")
            });

        _client = new HttpClient(mockMessageHandler.Object);
    }

    [Fact]
    public async Task GivenAScamSMS_SubmitAReport()
    {
        var drupalForm = new SubmitDrupalFormService(_client);
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
            TargetPhoneNumber = "0487654321",
            TargetState = State.NSW
        };
        var submission = await drupalForm.Process(report);

        Assert.True(submission);
    }

    [Fact]
    public async Task GivenAScamCall_SubmitAReport()
    {
        var drupalForm = new SubmitDrupalFormService(_client);
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
