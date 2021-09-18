﻿using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Business.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ScamWatch
{
	public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			appBuilder.ConfigureServices(services =>
			{
				services.AddSingleton<HttpClient, HttpClient>();
				services.AddSingleton<IDrupalFormService, DrupalFormService>();
			});
		}
	}
}