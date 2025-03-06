using System.Windows;
using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy SuplementsContent.xaml
    /// </summary>
    public partial class SuplementsContent : UserControl
    {
        public SuplementsContent()
        {
            InitializeComponent();
            DataContext = new SuplementsContentViewModel();

            //Initialize popup window with default hidden state
            AddSupplementPopup.IsOpen = false;

        }

        private void AddSupplementButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the popup when the button is clicked
            AddSupplementPopup.IsOpen = true;

            // Focus on the search bar
            SearchSupplementTextBox.Focus();
        }
    }
}
