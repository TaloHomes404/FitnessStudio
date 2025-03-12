using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy BMICalculatorContent.xaml
    /// </summary>
    public partial class BMICalculatorContent : UserControl
    {
        public BMICalculatorContent()
        {
            InitializeComponent();
            DataContext = new BMICalculatorContentViewModel();
        }

    }
}
