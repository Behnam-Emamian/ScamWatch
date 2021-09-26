using Business.Services;
using Microsoft.Maui.Controls;
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

        private void OnSubmitReportClicked(object sender, EventArgs e)
        {

        }
    }
}
