using System.Windows;
using SplashScreen = FitnessStudio.MVVM.View.SplashScreen;

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

            // Tworzymy i pokazujemy ekran powitalny
            var splash = new SplashScreen();
            splash.Show();

            // Symulacja ładowania (np. inicjalizacja danych)
            //TODO (async funkcje w tle patrzące czy użytkownik jest już zalogowany (kiedyś)

            await Task.Delay(100);

            // Otwieramy główne okno aplikacji
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Zamykamy splash screen
            splash.Close();
        }
    }

}
