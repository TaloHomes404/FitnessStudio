using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    public partial class CaloriesCalculatorContent : UserControl
    {
        public CaloriesCalculatorContent()
        {
            InitializeComponent();
            DataContext = new CaloriesCalculatorContentViewModel();
        }
    }
}
