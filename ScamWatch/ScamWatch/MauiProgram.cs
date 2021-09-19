using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System.Net.Http;

namespace ScamWatch
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddSingleton<HttpClient, HttpClient>();
            builder.Services.AddSingleton<IDrupalFormService, DrupalFormService>();

            return builder.Build();
        }
    }
}