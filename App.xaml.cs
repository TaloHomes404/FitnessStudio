using System.Windows;
using FitnessStudio.MVVM.ViewModel;

namespace FitnessStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Show();

            if (mainWindow.DataContext is MainViewModel viewModel)
            {
                // Uruchamiamy sekwencję ładowania
                await viewModel.InitializeApplicationAsync();
            }
        }
    }

}
