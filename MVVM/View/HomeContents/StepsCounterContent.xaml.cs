using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy StepsCounterContent.xaml
    /// </summary>
    public partial class StepsCounterContent : UserControl
    {
        public StepsCounterContent()
        {
            InitializeComponent();
            DataContext = new StepsCounterContentViewModel();
        }
    }
}
