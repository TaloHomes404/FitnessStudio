﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy MeditationContent.xaml
    /// </summary>
    public partial class MeditationContent : UserControl
    {
        private DispatcherTimer timer;
        private int remainingSeconds;
        private int totalSeconds;
        private bool isRunning = false;

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isMuted = false;

        public MeditationContent()
        {
            InitializeComponent();

            DataContext = new MeditationContentViewModel();

            // Inicjalizacja timera
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            //Inicjalizacja mediaplayera 
            mediaPlayer.Open(new Uri("C:\\Users\\Developer\\source\\FitnessStudio\\Audio\\earth_liborio_conti.mp3", UriKind.Relative));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                UpdateTimerDisplay();
            }
            else
            {
                // Koniec sesji
                timer.Stop();
                isRunning = false;
                StartPauseButton.Content = "Start";
                MessageBox.Show("Sesja medytacji zakończona!", "Koniec", MessageBoxButton.OK);
                MeditationPopup.IsOpen = false;
            }
        }

        private void UpdateTimerDisplay()
        {
            // Aktualizacja licznika czasu
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            TimerDisplay.Text = $"{minutes:D2}:{seconds:D2}";

            // Aktualizacja paska postępu
            double progress = 100 - ((double)remainingSeconds / totalSeconds * 100);
            TimerProgress.Value = progress;
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Prosta animacja kliknięcia karty
            var border = sender as Border;
            if (border != null)
            {
                DoubleAnimation animation = new DoubleAnimation(0.95, TimeSpan.FromMilliseconds(100));
                animation.AutoReverse = true;

                border.RenderTransformOrigin = new Point(0.5, 0.5);
                border.RenderTransform = new ScaleTransform(1, 1);
                border.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                border.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
            }
        }

        private void StartMeditation_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie informacji o medytacji
            var button = sender as Button;
            if (button?.Tag == null) return;

            string[] tagData = button.Tag.ToString().Split('|');
            string title = tagData[0];
            int minutes = int.Parse(tagData[1]);
            string description = tagData[2];

            // Ustawienie danych w popupie
            PopupTitle.Text = title;
            MeditationDescription.Text = description;

            // Przygotowanie timera
            totalSeconds = minutes * 60;
            remainingSeconds = totalSeconds;
            UpdateTimerDisplay();

            // Reset stanu
            isRunning = false;
            StartPauseButton.Content = "Start";

            // Wyświetlenie popupu
            MeditationPopup.IsOpen = true;
        }

        private void StartPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                // Pauza
                timer.Stop();
                mediaPlayer.Pause();
                isRunning = false;
                StartPauseButton.Content = "Wznów";
            }
            else
            {
                // Start/Wznowienie
                timer.Start();
                mediaPlayer.Play();
                isRunning = true;
                StartPauseButton.Content = "Pauza";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Zatrzymanie timera i muzyki
            timer.Stop();
            mediaPlayer.Stop();
            isRunning = false;

            // Reset wartości
            remainingSeconds = totalSeconds;
            UpdateTimerDisplay();
            StartPauseButton.Content = "Start";
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            // Zatrzymanie timera, muzyki i zamknięcie popupu
            timer.Stop();
            mediaPlayer.Stop();
            isRunning = false;
            MeditationPopup.IsOpen = false;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => mediaPlayer.Volume = VolumeSlider.Value;

        private void MuteButton_Click(object sender, MouseButtonEventArgs e)
        {
            isMuted = !isMuted;
            mediaPlayer.IsMuted = isMuted;

            // Pobranie obrazu z Border
            var image = MuteButton.Child as Image;
            if (image != null)
            {
                if (isMuted)
                {
                    image.Source = new BitmapImage(new Uri("/Resources/music_muted_icon.png", UriKind.Relative));
                }
                else
                {
                    image.Source = new BitmapImage(new Uri("/Resources/music_on_icon.png", UriKind.Relative));
                }
            }
        }

    }
}