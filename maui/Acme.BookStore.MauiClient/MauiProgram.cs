using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Acme.BookStore.MauiClient.Auth;
using Acme.BookStore.MauiClient.ViewModels;
using Acme.BookStore.MauiClient.Pages;

namespace Acme.BookStore.MauiClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.ConfigureContainer(new AbpAutofacServiceProviderFactory(new Autofac.ContainerBuilder()));
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        ConfigureConfiguration(builder);

        builder.Services.AddApplication<BookStoreMauiClientModule>(options =>
        {
            options.Services.ReplaceConfiguration(builder.Configuration);
        });

        // Register services and viewmodels
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>(); // Register LoginPage for DI if it's resolved this way

        var app = builder.Build();

        app.Services.GetRequiredService<IAbpApplicationWithExternalServiceProvider>()
            .Initialize(app.Services);

        return app;
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;
        builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
    }
}
