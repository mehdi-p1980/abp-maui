using Acme.BookStore.MauiClient.Auth;
using Acme.BookStore.MauiClient.Pages; // Ensure LoginPage is in this namespace or adjust
using Microsoft.Maui.Controls;

namespace Acme.BookStore.MauiClient;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}

    protected override async void OnStart()
    {
        base.OnStart();

        // Resolve AuthService from the DI container
        var authService = IPlatformApplication.Current.Services.GetService<AuthService>();

        if (authService != null && !await authService.IsUserLoggedInAsync())
        {
            // Using MainThread.BeginInvokeOnMainThread to ensure UI updates happen on the main thread,
            // especially important during app startup.
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // It's generally better to navigate to a route that is NOT part of the main shell
                // when the app starts and requires login. Consider a modal page or a different shell.
                // For this example, we'll navigate to a route within the current shell.
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            });
        }
        // If user is logged in, AppShell will navigate to its default route.
    }
}
