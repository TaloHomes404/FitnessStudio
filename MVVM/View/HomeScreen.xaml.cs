using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel;

namespace FitnessStudio.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        public HomeScreen()
        {
            InitializeComponent();
            this.DataContext = new HomeViewModel();
        }
    }
}
