using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FitnessStudio.MVVM.ViewModel
{
    public class RegisterViewModel : ObservableObject
    {
        private bool _isSignInFormVisible = true;

        // Właściwość określająca widoczność panelu logowania
        public bool IsSignInFormVisible
        {
            get => _isSignInFormVisible;
            set
            {
                _isSignInFormVisible = value;
                OnPropertyChanged(nameof(IsSignInFormVisible));
            }
        }

        // Właściwość określająca widoczność panelu rejestracji
        public bool IsSignUpFormVisible => !_isSignInFormVisible;

        // Komenda do przełączania na panel rejestracji
        public ICommand SwitchToSignUpCommand { get; }

        // Komenda do przełączania na panel logowania
        public ICommand SwitchToSignInCommand { get; }

        public RegisterViewModel()
        {
            // Inicjalizacja komend
            SwitchToSignUpCommand = new RelayCommand(SwitchToSignUp);
            SwitchToSignInCommand = new RelayCommand(SwitchToSignIn);
        }

        // Przełączanie na panel rejestracji
        private void SwitchToSignUp()
        {
            IsSignInFormVisible = false;
        }

        // Przełączanie na panel logowania
        private void SwitchToSignIn()
        {
            IsSignInFormVisible = true;
        }
    }
}