using System.Windows;
using System.Windows.Controls;
using FitnessStudio.MVVM.Model;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
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
            EditSupplementPopup.IsOpen = false;
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

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz kliknięty przycisk
            Button button = sender as Button;
            if (button != null)
            {
                // Pobierz suplement z kontekstu danych przycisku
                Supplement supplement = button.DataContext as Supplement;
                if (supplement != null)
                {
                    // Ustaw wybrany suplement w ViewModel
                    _viewModel.SelectedSupplement = supplement;

                    // Otwórz popup do edycji
                    EditSupplementPopup.IsOpen = true;
                }
            }
        }

        private void EverydayCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedSupplement != null)
            {
                _viewModel.SelectedSupplement.IsMonday = true;
                _viewModel.SelectedSupplement.IsTuesday = true;
                _viewModel.SelectedSupplement.IsWednesday = true;
                _viewModel.SelectedSupplement.IsThursday = true;
                _viewModel.SelectedSupplement.IsFriday = true;
                _viewModel.SelectedSupplement.IsSaturday = true;
                _viewModel.SelectedSupplement.IsSunday = true;
            }
        }

        private void DaysTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel != null)
            {
                var tabControl = sender as TabControl;
                if (tabControl != null)
                {
                    var selectedTab = tabControl.SelectedItem as TabItem;
                    if (selectedTab != null)
                    {
                        _viewModel.SelectedDay = selectedTab.Header.ToString();
                    }
                }
            }
        }

        private void EverydayCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedSupplement != null)
            {
                _viewModel.SelectedSupplement.IsMonday = false;
                _viewModel.SelectedSupplement.IsTuesday = false;
                _viewModel.SelectedSupplement.IsWednesday = false;
                _viewModel.SelectedSupplement.IsThursday = false;
                _viewModel.SelectedSupplement.IsFriday = false;
                _viewModel.SelectedSupplement.IsSaturday = false;
                _viewModel.SelectedSupplement.IsSunday = false;
            }
        }

        private void DeleteSupplement_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedSupplement != null)
            {
                _viewModel.DeleteSupplement(_viewModel.SelectedSupplement);
                EditSupplementPopup.IsOpen = false;
            }
        }

        private void SaveSupplement_Click(object sender, RoutedEventArgs e)
        {
            // Zamknij popup
            EditSupplementPopup.IsOpen = false;
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e)
        {
            // Zamknij popup
            EditSupplementPopup.IsOpen = false;
        }
    }
}