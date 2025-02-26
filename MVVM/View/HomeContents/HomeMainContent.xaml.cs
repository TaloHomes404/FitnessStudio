using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy HomeMainContent.xaml
    /// </summary>
    public partial class HomeMainContent : UserControl
    {
        public HomeMainContent()
        {
            InitializeComponent();
            DataContext = new HomeMainContentViewModel();
        }
    }
}
