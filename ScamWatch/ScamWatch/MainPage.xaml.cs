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
            this.firstName.Text = await SecureStorage.GetAsync("FirstName");
            this.lastName.Text = await SecureStorage.GetAsync("LastName");
            this.email.Text = await SecureStorage.GetAsync("Email");
            this.phoneNumber.Text = await SecureStorage.GetAsync("PhoneNumber"); ;
        }
        private void OnSubmitReportClicked(object sender, EventArgs e)
        {
            SecureStorage.SetAsync("FirstName", this.firstName.Text);
            SecureStorage.SetAsync("lastName", this.lastName.Text);
            SecureStorage.SetAsync("Email", this.email.Text);
            SecureStorage.SetAsync("PhoneNumber", this.phoneNumber.Text);
            SecureStorage.SetAsync("Gender", this.gender.Title);
            SecureStorage.SetAsync("State", this.state.Title);
        }
    }
}
