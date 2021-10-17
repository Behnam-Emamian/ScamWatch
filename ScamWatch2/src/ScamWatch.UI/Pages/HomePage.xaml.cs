using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using ScamWatch.Core.Enums;
using ScamWatch.Core.Models;
using ScamWatch.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScamWatch.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        Report report = new Report
        {
            ScamType = ScamType.IdentityTheft,
            DeliveryMethod = DeliveryMethod.phone,
            FirstContactDate = DateTime.Now,
            Description = @"I have received a call trying to get my infomation",
            ScammerPhoneNumber = "0482707695",
            TargetEmail = "ben@ictace.com",
            TargetFirstName = "Behnam",
            TargetGender = Gender.male,
            TargetLastName = "Emamian",
            TargetPhoneNumber = "0488812388",
            TargetState = State.NSW
        };

        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            }



        }

        int step = 0;
        private async void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            switch (step)
            {
                case 0:
                    await webView.EvaluateJavaScriptAsync("document.getElementById('edit-wizard-next').click()");
                    break;

                case 1:
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-what-type-of-scam-is-it').value={(int)report.ScamType}");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-how-were-you-contacted-by-the-scammer').value='{report.DeliveryMethod}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-when-were-you-first-contacted').value='{report.FirstContactDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-scammer-s-phone-number').value='{report.ScammerPhoneNumber}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-briefly-describe-the-scam').value='{report.Description}'");
                    await webView.EvaluateJavaScriptAsync("document.getElementById('edit-wizard-next').click()");
                    break;

                case 2:
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-im-reporting-as-myself').click()");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-person-targeted-first-name').value='{report.TargetFirstName}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-person-targeted-last-name').value='{report.TargetLastName}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-person-targeted-email').value='{report.TargetEmail}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-person-targeted-gender').value='{report.TargetGender}'");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-person-targeted-state').value='{report.TargetState}'");
                    await webView.EvaluateJavaScriptAsync("document.getElementById('edit-wizard-next').click()");

                    break;

                case 3:
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-would-you-be-willing-to-share-your-story-yes-anonymous').click()");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-share-report-government-yes').click()");
                    await webView.EvaluateJavaScriptAsync($"document.getElementById('edit-share-report-private-sector-yes').click()");
                    // TODO: Disable submit
                    //await webView.EvaluateJavaScriptAsync("document.getElementById('edit-submit').click()");
                    break;
            }
            step++;
        }


    }
}
