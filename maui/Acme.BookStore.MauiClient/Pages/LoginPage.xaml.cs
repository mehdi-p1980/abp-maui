using Microsoft.Maui.Controls;
using Acme.BookStore.MauiClient.ViewModels;
using System;

namespace Acme.BookStore.MauiClient.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _viewModel;

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;

            // Assign the command to the button
            LoginButton.Command = _viewModel.LoginCommand;
        }

        // OnLoginButtonClicked can be removed if you are purely using Command from XAML
        // Or keep it if you need to do something specific in code-behind before/after command execution
        // For this example, we'll assume the command is sufficient.
        // If you had other logic here, you could call _viewModel.LoginCommand.Execute(null);
    }
}
