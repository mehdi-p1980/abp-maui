using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Acme.BookStore.MauiClient.Auth;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.MauiClient.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged, ITransientDependency
    {
        private readonly AuthService _authService;
        private bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new Command(async () => await ExecuteLoginCommand(), () => !IsBusy);
        }

        private async Task ExecuteLoginCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ((Command)LoginCommand).ChangeCanExecute(); // Disable button

            bool success = await _authService.LoginAsync();

            if (success)
            {
                // Navigate to the main part of the app
                // This assumes AppShell is the main page after login
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                // Show error message
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Could not log in. Please try again.", "OK");
            }

            IsBusy = false;
            ((Command)LoginCommand).ChangeCanExecute(); // Re-enable button
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
