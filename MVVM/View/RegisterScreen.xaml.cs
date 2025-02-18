using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FitnessStudio.MVVM.View
{
    public partial class RegisterScreen : UserControl
    {
        public RegisterScreen()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Używamy FindResource do pobrania storyboardu
            Storyboard sb = (Storyboard)this.FindResource("SlideTransition");
            sb.Begin();
        }
    }
}