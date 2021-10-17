using Business.Enums;
using Business.Models;
using Business.Services;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Business.Tests.Services;

public class DrupalFormServiceTest
{
    private readonly HttpClient _client;

    public DrupalFormServiceTest()
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

        //_client = new HttpClient(mockMessageHandler.Object);
        _client = new HttpClient();
    }

    [Fact]
    public async Task GivenAScamSMS_SubmitAReport()
    {
        var drupalForm = new DrupalFormService(_client);
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
        var report2 = new Report
        {
            ScamType = ScamType.IdentityTheft,
            DeliveryMethod = DeliveryMethod.phone,
            FirstContactDate = DateOnly.FromDateTime(DateTime.Now),
            Description = @"I have received a call trying to get my infomation",
            ScammerPhoneNumber = "0482707695",
            TargetEmail = "ben@ictace.com",
            TargetFirstName = "Behnam",
            TargetGender = Gender.male,
            TargetLastName = "Emamian",
            TargetPhoneNumber = "0488812388",
            TargetState = State.NSW
        };
        var submission = await drupalForm.Process(report2);

        Assert.True(submission);
    }

    [Fact]
    public async Task GivenAScamCall_SubmitAReport()
    {
        var drupalForm = new DrupalFormService(_client);
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
