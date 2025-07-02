using Volo.Abp.DependencyInjection;
using Acme.BookStore.MauiClient.Auth;
using Acme.BookStore.MauiClient.Pages; // Required for nameof(LoginPage)

namespace Acme.BookStore.MauiClient;

public partial class MainPage : ContentPage, ITransientDependency
{
    private readonly AuthService _authService;

    public MainPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Optionally, refresh user info or check login status again
        if (await _authService.IsUserLoggedInAsync())
        {
            // var user = await _authService.GetUserInfoAsync(); // Assuming you add GetUserInfoAsync to AuthService
            // UserInfoLabel.Text = $"Welcome, {user?.Name ?? "User"}!";
            UserInfoLabel.Text = "You are logged in.";
        }
        else
        {
            // This case should ideally not be reached if OnStart() in App.xaml.cs correctly redirects to LoginPage
            UserInfoLabel.Text = "Not logged in.";
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await _authService.LogoutAsync();
        // Navigate to the LoginPage after logout
        // Ensure LoginPage is registered for routing in AppShell.xaml.cs
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private async void OnViewUsersClicked(object sender, EventArgs e)
    {
        // This is just an example navigation, ensure UsersPage is correctly set up
        await Shell.Current.GoToAsync(nameof(UsersPage));
    }
}