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
            SetAnimValues(sb);
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
            RestartAnimToDefault(sb);

            sb.Begin();

            // Przywróć widoczność panelu logowania
            SignInFormPanel.Visibility = Visibility.Visible;
            SignUpFormPanel.Visibility = Visibility.Collapsed;


        }




        //Helper methods
        private static void RestartAnimToDefault(Storyboard sb)
        {
            var signUpAnimation = sb.Children[0] as DoubleAnimation; // Animacja przesunięcia białego panelu
            var signInAnimation = sb.Children[1] as DoubleAnimation; // Animacja przesunięcia czarnego panelu
            var blurAnimation = sb.Children[2] as DoubleAnimation;   // Animacja rozmycia

            if (signUpAnimation != null && signInAnimation != null && blurAnimation != null)
            {

                signUpAnimation.To = 0; // Przywróć biały panel na środek
                signInAnimation.To = 0;  // Przywróć czarny panel na środek
                blurAnimation.To = 0;    // Wyłącz rozmycie
            }
        }

        private static void SetAnimValues(Storyboard sb)
        {
            var signUpAnimation = sb.Children[0] as DoubleAnimation; // Animacja przesunięcia białego panelu
            var signInAnimation = sb.Children[1] as DoubleAnimation; // Animacja przesunięcia czarnego panelu
            var blurAnimation = sb.Children[2] as DoubleAnimation;   // Animacja rozmycia

            if (signUpAnimation != null && signInAnimation != null && blurAnimation != null)
            {
                // Zmień wartości To na 0 dla wszystkich animacji
                signUpAnimation.To = -320; // Wartość dla którego biały panel przesunie się do lewej
                signInAnimation.To = 320;  // Wartość dla którego czarny panel przesunie się do prawej
                blurAnimation.To = 3;    // Włącz rozmycie
            }
        }
    }
}