using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using FitnessStudio.MVVM.ViewModel;

namespace FitnessStudio.MVVM.View
{
    public partial class RegisterScreen : UserControl
    {
        public RegisterScreen()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Używamy FindResource do pobrania storyboardu
            Storyboard sb = (Storyboard)this.FindResource("SlideTransition");
            sb.Begin();
        }

        // Przełączanie na panel rejestracji
        private void SwitchToSignUpForm(object sender, MouseButtonEventArgs e)
        {
            SignInFormPanel.Visibility = Visibility.Collapsed;
            SignUpFormPanel.Visibility = Visibility.Visible;
        }

        // Przełączanie na panel logowania
        private void SwitchToSignInForm(object sender, MouseButtonEventArgs e)
        {
            SignUpFormPanel.Visibility = Visibility.Collapsed;
            SignInFormPanel.Visibility = Visibility.Visible;
        }

        private void BackButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            // Używamy FindResource do pobrania storyboardu
            Storyboard sb = (Storyboard)this.FindResource("SlideTransition");
            sb.AutoReverse = true;



            // Wyłącz rozmycie
            SignUpBlur.Radius = 0;

            // Przywróć widoczność panelu logowania
            SignInFormPanel.Visibility = Visibility.Visible;
            SignUpFormPanel.Visibility = Visibility.Collapsed;

            // Przywróć panele na środek
            SignUpTranslateTransform.X = 0;
            SignInTranslateTransform.X = 0;

            sb.Begin();
        }
    }
}