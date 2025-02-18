using CommunityToolkit.Mvvm.ComponentModel;
using FitnessStudio.MVVM.View;

namespace FitnessStudio.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set => SetProperty(ref currentView, value);
        }

        public MainViewModel()
        {
            // Ustawiamy domyślny widok – RegisterScreen
            CurrentView = new RegisterScreen();
        }
    }
}