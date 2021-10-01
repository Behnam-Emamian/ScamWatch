using Business.Enums;
using Business.Models;
using Business.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;

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

        protected override async void OnAppearing()
        {
            gender.ItemsSource = Enum.GetValues(typeof(Gender));
            state.ItemsSource = Enum.GetValues(typeof(State));

            firstName.Text = await SecureStorage.GetAsync("FirstName");
            lastName.Text = await SecureStorage.GetAsync("LastName");
            email.Text = await SecureStorage.GetAsync("Email");
            phoneNumber.Text = await SecureStorage.GetAsync("PhoneNumber");

            state.SelectedItem = (State)Enum.Parse(typeof(State), await SecureStorage.GetAsync("State"));
            gender.SelectedItem = (Gender)Enum.Parse(typeof(Gender), await SecureStorage.GetAsync("Gender"));
        }

        private async void OnSubmitReportClicked(object sender, EventArgs e)
        {
            await SecureStorage.SetAsync("FirstName", firstName.Text);
            await SecureStorage.SetAsync("lastName", lastName.Text);
            await SecureStorage.SetAsync("Email", email.Text);
            await SecureStorage.SetAsync("PhoneNumber", phoneNumber.Text);

            await SecureStorage.SetAsync("Gender", gender.SelectedItem.ToString());
            await SecureStorage.SetAsync("State", state.SelectedItem.ToString());

            var result = await _drupalFormService.Process(new Report
            {
                ScamType = ScamType.IdentityTheft,
                DeliveryMethod = DeliveryMethod.text_message,
                FirstContactDate = DateOnly.FromDateTime(DateTime.Now),
                Description = description.Text,
                ScammerPhoneNumber = scramPhoneNumber.Text,
                TargetEmail = await SecureStorage.GetAsync("Email"),
                TargetFirstName = await SecureStorage.GetAsync("FirstName"),
                TargetGender = (Gender)Enum.Parse(typeof(Gender), await SecureStorage.GetAsync("Gender")),
                TargetLastName = await SecureStorage.GetAsync("LastName"),
                TargetPhoneNumber = await SecureStorage.GetAsync("PhoneNumber"),
                TargetState = (State)Enum.Parse(typeof(State), await SecureStorage.GetAsync("State"))
            });

            if (result)
                await DisplayAlert("Successful", "The report sent", "Ok");
        }
    }
}
