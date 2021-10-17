using Business.Enums;
using Business.Models;
using Business.Services;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace ScamWatch
{
    public partial class MainPage : ContentPage
    {
        private readonly IDrupalFormService _drupalFormService;

        public MainPage()
        {
            _drupalFormService = ServiceProvider.GetService<IDrupalFormService>();

            InitializeComponent();
        }

        private async void OnSubmitReportClicked(object sender, EventArgs e)
        {
            // TODO: get values from UI

            var report = new Report
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

            // TODO: show progress

            var submission = await _drupalFormService.Process(report);

            if (submission)
            {
                // TODO: show successfull message
            }
            else
            {
                // TODO: show error message
            }

        }
    }
}
