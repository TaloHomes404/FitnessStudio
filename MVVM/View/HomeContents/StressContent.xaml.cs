using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy StressContent.xaml
    /// </summary>
    public partial class StressContent : UserControl
    {
        // Flag dla stanu muzyki
        private bool isMusicPlaying = false;
        private bool isMuted = false;

        // Animacja dla oddechu
        private Storyboard breathingAnimation;

        // Media player do odtwarzania muzyki
        private MediaPlayer musicPlayer;

        public StressContent()
        {
            InitializeComponent();

            DataContext = new StressContentViewModel();

            // Inicjalizacja daty w dzienniku stresu
            StressDate.SelectedDate = DateTime.Today;

            // Inicjalizacja media playera
            InitializeMediaPlayer();

            // Utworzenie animacji oddechu
            CreateBreathingAnimation();
        }

        // Inicjalizacja odtwarzacza muzyki
        private void InitializeMediaPlayer()
        {
            musicPlayer = new MediaPlayer();

            // Zakładamy, że mamy plik muzyki w Resources
            // W prawdziwej aplikacji należy dostosować ścieżkę lub dodać obsługę błędów
            try
            {
                musicPlayer.Open(new Uri("pack://application:,,,/Resources/relaxation_music.mp3", UriKind.Absolute));
                musicPlayer.Volume = 0.5; // Początkowa głośność 50%
                musicPlayer.MediaEnded += (s, e) =>
                {
                    musicPlayer.Position = TimeSpan.Zero; // Powtarzanie muzyki
                    musicPlayer.Play();
                };
            }
            catch (Exception)
            {
                // Prosta obsługa błędu - w produkcyjnej aplikacji należy ją rozszerzyć
                MessageBox.Show("Nie można załadować pliku muzyki. Upewnij się, że plik istnieje w zasobach aplikacji.");
            }
        }

        // Metoda tworząca animację oddechu
        private void CreateBreathingAnimation()
        {
            breathingAnimation = new Storyboard();

            // Animacja dla wdechu (powiększenie)
            DoubleAnimation expandAnimation = new DoubleAnimation
            {
                From = 100,
                To = 140,
                Duration = TimeSpan.FromSeconds(4),
                AutoReverse = false
            };
            Storyboard.SetTarget(expandAnimation, BreathingCircle);
            Storyboard.SetTargetProperty(expandAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(expandAnimation);

            // Druga animacja dla wysokości, aby zachować proporcje
            DoubleAnimation expandHeightAnimation = new DoubleAnimation
            {
                From = 100,
                To = 140,
                Duration = TimeSpan.FromSeconds(4),
                AutoReverse = false
            };
            Storyboard.SetTarget(expandHeightAnimation, BreathingCircle);
            Storyboard.SetTargetProperty(expandHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(expandHeightAnimation);

            // Animacja dla zatrzymania oddechu
            DoubleAnimation holdAnimation = new DoubleAnimation
            {
                From = 140,
                To = 140,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(4)
            };
            Storyboard.SetTarget(holdAnimation, BreathingCircle);
            Storyboard.SetTargetProperty(holdAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(holdAnimation);

            // Animacja dla wydechu (zmniejszenie)
            DoubleAnimation shrinkAnimation = new DoubleAnimation
            {
                From = 140,
                To = 100,
                Duration = TimeSpan.FromSeconds(8),
                BeginTime = TimeSpan.FromSeconds(8)
            };
            Storyboard.SetTarget(shrinkAnimation, BreathingCircle);
            Storyboard.SetTargetProperty(shrinkAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(shrinkAnimation);

            // Animacja wysokości dla wydechu
            DoubleAnimation shrinkHeightAnimation = new DoubleAnimation
            {
                From = 140,
                To = 100,
                Duration = TimeSpan.FromSeconds(8),
                BeginTime = TimeSpan.FromSeconds(8)
            };
            Storyboard.SetTarget(shrinkHeightAnimation, BreathingCircle);
            Storyboard.SetTargetProperty(shrinkHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(shrinkHeightAnimation);

            // Ustawienie powtarzania animacji
            breathingAnimation.RepeatBehavior = RepeatBehavior.Forever;
        }

        // Obsługa kliknięcia karty
        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Identyfikowanie której karty dotyczy zdarzenie
            if (sender == StressJournalCard)
                ShowStressJournalPopup_Click(sender, e);
            else if (sender == QuickRelaxCard)
                ShowQuickRelaxPopup_Click(sender, e);
        }

        // Wyświetlenie popupu dziennika stresu
        private void ShowStressJournalPopup_Click(object sender, RoutedEventArgs e)
        {
            StressJournalPopup.IsOpen = true;
        }

        // Zamknięcie popupu dziennika stresu
        private void CloseStressJournalPopup_Click(object sender, RoutedEventArgs e)
        {
            StressJournalPopup.IsOpen = false;
        }

        // Zapisanie wpisu w dzienniku stresu
        private void SaveStressJournal_Click(object sender, RoutedEventArgs e)
        {
            // Przykładowa implementacja zapisu wpisu do dziennika stresu
            // W rzeczywistej aplikacji należałoby zapisać dane do bazy lub pliku

            // Zbieramy dane z formularza
            DateTime date = StressDate.SelectedDate ?? DateTime.Today;
            string situation = StressSituation.Text;
            double level = StressLevel.Value;
            string reduction = StressReduction.Text;

            // Walidacja danych
            if (string.IsNullOrWhiteSpace(situation))
            {
                MessageBox.Show("Proszę opisać sytuację stresującą.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // W prawdziwej aplikacji - zapis do bazy/pliku
            // Tutaj tylko komunikat
            MessageBox.Show("Wpis został zapisany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            // Zamknięcie popupu
            StressJournalPopup.IsOpen = false;

            // Czyszczenie pól
            StressSituation.Text = "";
            StressReduction.Text = "";
            StressLevel.Value = 5;
        }

        // Aktualizacja wyświetlania poziomu stresu
        private void StressLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (StressLevelValue != null)
                StressLevelValue.Text = StressLevel.Value.ToString("0");
        }

        // Wyświetlenie popupu szybkiego odstresowania
        private void ShowQuickRelaxPopup_Click(object sender, RoutedEventArgs e)
        {
            QuickRelaxPopup.IsOpen = true;
            // Uruchomienie animacji oddychania
            breathingAnimation.Begin();
        }

        // Zamknięcie popupu szybkiego odstresowania
        private void CloseQuickRelaxPopup_Click(object sender, RoutedEventArgs e)
        {
            // Zatrzymanie animacji i muzyki przed zamknięciem
            breathingAnimation.Stop();
            if (isMusicPlaying)
            {
                musicPlayer.Pause();
                isMusicPlaying = false;
                PlayPauseButton.Content = "▶ Odtwórz";
            }

            QuickRelaxPopup.IsOpen = false;
        }

        // Odtwarzanie/Zatrzymywanie muzyki
        private void PlayPauseMusic_Click(object sender, RoutedEventArgs e)
        {
            if (isMusicPlaying)
            {
                musicPlayer.Pause();
                isMusicPlaying = false;
                PlayPauseButton.Content = "▶ Odtwórz";
            }
            else
            {
                musicPlayer.Play();
                isMusicPlaying = true;
                PlayPauseButton.Content = "⏸ Pauza";
            }
        }

        // Wyciszanie/Wyłączanie wyciszenia muzyki
        private void MuteMusic_Click(object sender, RoutedEventArgs e)
        {
            if (isMuted)
            {
                musicPlayer.Volume = VolumeSlider.Value / 100;
                isMuted = false;
                MuteButton.Content = "🔊 Wycisz";
            }
            else
            {
                musicPlayer.Volume = 0;
                isMuted = true;
                MuteButton.Content = "🔇 Włącz dźwięk";
            }
        }

        // Zmiana głośności muzyki
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VolumeValue != null && musicPlayer != null && !isMuted)
            {
                double volume = VolumeSlider.Value;
                VolumeValue.Text = $"{Math.Round(volume)}%";
                musicPlayer.Volume = volume / 100;
            }
        }
    }
}