using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessStudio.MVVM.View;

namespace FitnessStudio.MVVM.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private object? currentView;
        public object CurrentView
        {
            get => currentView!;  //zmienic to potem jak bedzie pewnosc
            set => SetProperty(ref currentView, value);
        }

        public MainViewModel()
        {
            // Ustawiamy domyślny widok – RegisterScreen
            CurrentView = new RegisterScreen();
        }


        [RelayCommand]
        public void ChangeView(string viewName)
        {
            switch (viewName)
            {
                case "Home":
                    CurrentView = new HomeScreen();
                    break;
                case "Register":
                    CurrentView = new RegisterScreen();
                    break;
                case "Splash":
                    CurrentView = new SplashScreen();
                    break;
                // Dodaj kolejne przypadki dla innych widoków
                default:
                    throw new System.ArgumentException("Nieznany widok");
            }

        }
    }
}
