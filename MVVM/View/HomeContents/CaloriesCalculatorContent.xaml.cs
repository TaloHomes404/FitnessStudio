using System.Windows;
using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy CaloriesCalculatorContent.xaml
    /// </summary>
    public partial class CaloriesCalculatorContent : UserControl
    {
        public CaloriesCalculatorContent()
        {
            InitializeComponent();
            DataContext = new CaloriesCalculatorContentViewModel();
        }

        private void CalculateMacrosButton_Click(object sender, RoutedEventArgs e)
        {
            MacrosPopup.IsOpen = true;
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            MacrosPopup.IsOpen = false;
        }

    }
}
