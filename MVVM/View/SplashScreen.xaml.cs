using FitnessStudio.MVVM.ViewModel;
using MahApps.Metro.Controls;

namespace FitnessStudio.MVVM.View
{
    public partial class SplashScreen : MetroWindow
    {
        public SplashScreen()
        {
            InitializeComponent(); // To naprawi błąd "InitializeComponent"
            DataContext = new SplashViewModel(); // Podłącz ViewModel
        }
    }
}