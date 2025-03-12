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
        // Animacja dla oddechu
        private Storyboard breathingAnimation;

        // Media player do odtwarzania muzyki
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isMuted = false;
        private bool isPlaying = false;

        public StressContent()
        {
            InitializeComponent();

            DataContext = new StressContentViewModel();

            // Inicjalizacja daty w dzienniku stresu
            StressDate.SelectedDate = DateTime.Today;

            // Inicjalizacja mediaplayera 
            mediaPlayer.Open(new Uri("C:\\Users\\Developer\\source\\FitnessStudio\\Audio\\earth_liborio_conti.mp3", UriKind.Relative));

            // Domyślna głośność
            mediaPlayer.Volume = 0.5;

            // Utworzenie animacji oddechu
            CreateBreathingAnimation();
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
            mediaPlayer.Stop();
            isPlaying = false;

            QuickRelaxPopup.IsOpen = false;
        }

        // Odtwarzanie/Zatrzymywanie muzyki
        private void PlayPauseMusic_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                // Pauza
                mediaPlayer.Pause();
                isPlaying = false;
                PlayPauseButton.Content = "▶ Odtwórz";
            }
            else
            {
                // Start/Wznowienie
                mediaPlayer.Play();
                isPlaying = true;
                PlayPauseButton.Content = "⏸ Pauza";
            }
        }

        // Wyciszanie/Włączanie dźwięku
        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            isMuted = !isMuted;
            mediaPlayer.IsMuted = isMuted;

            if (isMuted)
            {
                MuteButtonIcon.Text = "🔇";
                MuteButtonText.Text = "Włącz dźwięk";
            }
            else
            {
                MuteButtonIcon.Text = "🔊";
                MuteButtonText.Text = "Wycisz";
            }
        }

        // Zmiana głośności muzyki
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayer != null && !isMuted)
            {
                mediaPlayer.Volume = VolumeSlider.Value;
            }

            // Aktualizuj wyświetlanie wartości głośności
            if (VolumeValue != null)
            {
                VolumeValue.Text = $"{(int)(VolumeSlider.Value * 100)}%";
            }
        }
    }
}