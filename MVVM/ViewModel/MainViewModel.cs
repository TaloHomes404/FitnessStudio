using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessStudio.MVVM.View;

namespace FitnessStudio.MVVM.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentView;

        public MainViewModel()
        {
            CurrentView = new SplashScreen();
            _ = InitializeApplicationAsync();
        }

        [RelayCommand]
        public async Task InitializeApplicationAsync()
        {
            await Task.Delay(2500);
            CurrentView = new RegisterScreen();
        }


        [RelayCommand]
        public void NavigateToHome()
        {
            CurrentView = new HomeScreen();
        }

        [RelayCommand]
        public void NavigateToRegister()
        {
            CurrentView = new RegisterScreen();
        }

        [RelayCommand]
        public void ChangeView(string viewName)
        {
            CurrentView = viewName switch
            {
                "Home" => new HomeScreen(),
                "Register" => new RegisterScreen(),
                "Splash" => new SplashScreen(),
                _ => throw new System.ArgumentException("Nieznany widok")
            };
        }
    }
}
