using System.Windows;
using System.Windows.Controls;
using FitnessStudio.MVVM.Model;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy SuplementsContent.xaml
    /// </summary>
    public partial class SuplementsContent : UserControl
    {
        private SuplementsContentViewModel _viewModel;

        public SuplementsContent()
        {
            InitializeComponent();
            _viewModel = new SuplementsContentViewModel();
            DataContext = _viewModel;

            // Initialize popup window with default hidden state
            AddSupplementPopup.IsOpen = false;
        }

        private void AddSupplementButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the popup when the button is clicked
            AddSupplementPopup.IsOpen = true;

            // Focus on the search bar
            SearchSupplementTextBox.Focus();
        }

        private void AddSupplementToUser_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz kliknięty przycisk
            Button button = sender as Button;
            if (button != null)
            {
                // Pobierz suplement z kontekstu danych przycisku
                Supplement supplement = button.DataContext as Supplement;
                if (supplement != null)
                {
                    // Dodaj suplement do listy użytkownika
                    _viewModel.AddSupplement(supplement);

                    // Zamknij popup
                    AddSupplementPopup.IsOpen = false;
                }
            }
        }
    }
}