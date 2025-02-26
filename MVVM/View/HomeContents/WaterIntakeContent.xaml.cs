using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy WaterIntakeContent.xaml
    /// </summary>
    public partial class WaterIntakeContent : UserControl
    {
        public WaterIntakeContent()
        {
            InitializeComponent();
            DataContext = new WaterIntakeContentViewModel();
        }
    }
}
