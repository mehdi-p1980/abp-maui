using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.MauiClient;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AppShell : Shell
using Acme.BookStore.MauiClient.Pages;

{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(UsersPage), typeof(UsersPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }
}
