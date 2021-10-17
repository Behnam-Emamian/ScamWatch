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
            _ = builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    _ = fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            _ = builder.Services.AddSingleton<HttpClient, HttpClient>();
            _ = builder.Services.AddSingleton<IDrupalFormService, DrupalFormService>();

            return builder.Build();
        }
    }
}