using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    public partial class BreathingExercisesContent : UserControl
    {
        // Zmienne dla animacji oddychania
        private Storyboard breathingAnimation;
        private Popup breathingPopup;
        private Ellipse breathingCircle;
        private TextBlock breathingStateText;
        private TextBlock breathingInstructionsText;
        private Button startButton;
        private Button stopButton;
        private TextBlock exerciseTitleText;

        public BreathingExercisesContent()
        {
            InitializeComponent();
            DataContext = new BreathingExercisesContentViewModel();

            // Set the middle card as active by default
            SetActiveCard(MiddleCard);

            // Inicjalizacja popupu oddychania
            InitializeBreathingPopup();
        }

        // Click functionality to select card
        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border clickedCard)
            {
                SetActiveCard(clickedCard);
            }
        }

        private void SetActiveCard(Border activeCard)
        {
            // Reset all cards
            LeftCard.Effect = TryFindResource("BackgroundBlur") as Effect;
            MiddleCard.Effect = TryFindResource("BackgroundBlur") as Effect;
            RightCard.Effect = TryFindResource("BackgroundBlur") as Effect;

            Panel.SetZIndex(LeftCard, 1);
            Panel.SetZIndex(MiddleCard, 1);
            Panel.SetZIndex(RightCard, 1);

            // Set active card
            activeCard.Effect = TryFindResource("CardShadow") as Effect;
            Panel.SetZIndex(activeCard, 2);
        }

        private void InitializeBreathingPopup()
        {
            // Tworzenie popupu
            breathingPopup = new Popup
            {
                StaysOpen = true,
                PlacementTarget = Application.Current.MainWindow,
                Placement = PlacementMode.Center,
                AllowsTransparency = true,
                HorizontalOffset = 0,
                VerticalOffset = 0
            };

            // Tworzenie głównego kontenera
            Border popupBorder = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(20),
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Width = 400,
                Height = 500
            };

            // Siatka do rozmieszczenia elementów
            Grid popupGrid = new Grid();
            popupGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            popupGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            popupGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Nagłówek - nazwa ćwiczenia
            exerciseTitleText = new TextBlock
            {
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = Brushes.Black
            };
            Grid.SetRow(exerciseTitleText, 0);

            // Panel zawartości (zapobieganie przesuwaniu)
            Grid breathingGrid = new Grid
            {
                Width = 200,
                Height = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Koło oddychania
            breathingCircle = new Ellipse
            {
                Width = 100,
                Height = 100,
                Fill = Brushes.LightSkyBlue,
                Stroke = Brushes.SteelBlue,
                StrokeThickness = 2
            };

            breathingGrid.Children.Add(breathingCircle);

            // Tekst statusu oddychania
            breathingStateText = new TextBlock
            {
                FontSize = 18,
                FontStyle = FontStyles.Italic,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = Brushes.Black
            };

            // Tekst instrukcji
            breathingInstructionsText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.Black
            };

            // Ustalony layout (zapobiega powiększaniu okna)
            StackPanel contentPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            contentPanel.Children.Add(breathingGrid);
            contentPanel.Children.Add(breathingStateText);
            contentPanel.Children.Add(breathingInstructionsText);
            Grid.SetRow(contentPanel, 1);

            // Panel przycisków
            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Przyciski
            startButton = new Button
            {
                Content = "Start",
                Width = 80,
                Margin = new Thickness(5)
            };
            startButton.Click += (s, e) => StartBreathingAnimation();

            stopButton = new Button
            {
                Content = "Stop",
                Width = 80,
                Margin = new Thickness(5)
            };
            stopButton.Click += (s, e) => StopBreathingAnimation();

            Button closeButton = new Button
            {
                Content = "Zamknij",
                Width = 80,
                Margin = new Thickness(5)
            };
            closeButton.Click += (s, e) => CloseBreathingPopup();

            // Dodanie przycisków
            buttonPanel.Children.Add(startButton);
            buttonPanel.Children.Add(stopButton);
            buttonPanel.Children.Add(closeButton);
            Grid.SetRow(buttonPanel, 2);

            // Dodanie elementów do siatki
            popupGrid.Children.Add(exerciseTitleText);
            popupGrid.Children.Add(contentPanel);
            popupGrid.Children.Add(buttonPanel);

            // Dodanie siatki do popupu
            popupBorder.Child = popupGrid;
            breathingPopup.Child = popupBorder;

        }

        // Metoda dla otwierania popupu box breathing
        private void ShowBoxBreathingPopup()
        {
            exerciseTitleText.Text = "Box Breathing";
            breathingStateText.Text = "Gotowy do rozpoczęcia";
            breathingInstructionsText.Text = "1. Wdech przez nos (4 sekundy)\n2. Zatrzymaj oddech (4 sekundy)\n3. Wydech przez usta (4 sekundy)\n4. Zatrzymaj oddech (4 sekundy)";

            // Utworzenie animacji box breathing
            CreateBoxBreathingAnimation();

            // Otwarcie popupu
            breathingPopup.IsOpen = true;
        }

        // Metoda dla otwierania popupu 4-7-8 breathing
        private void Show478BreathingPopup()
        {
            exerciseTitleText.Text = "Calming 4-7-8";
            breathingStateText.Text = "Gotowy do rozpoczęcia";
            breathingInstructionsText.Text = "1. Wdech przez nos (4 sekundy)\n2. Zatrzymaj oddech (7 sekund)\n3. Wydech przez usta (8 sekund)";

            // Utworzenie animacji 4-7-8 breathing
            Create478BreathingAnimation();

            // Otwarcie popupu
            breathingPopup.IsOpen = true;
        }

        // Metoda dla otwierania popupu pursed-lip breathing
        private void ShowPursedLipBreathingPopup()
        {
            exerciseTitleText.Text = "Pursed-Lip Breathing";
            breathingStateText.Text = "Gotowy do rozpoczęcia";
            breathingInstructionsText.Text = "1. Wdech przez nos (2 sekundy)\n2. Powolny wydech przez zaciśnięte usta (4 sekundy)";

            // Utworzenie animacji pursed-lip breathing
            CreatePursedLipBreathingAnimation();

            // Otwarcie popupu
            breathingPopup.IsOpen = true;
        }

        // Tworzenie animacji box breathing (4-4-4-4)
        private void CreateBoxBreathingAnimation()
        {
            breathingAnimation = new Storyboard();

            // Animacja dla wdechu (powiększenie)
            DoubleAnimation inhaleAnimation = new DoubleAnimation
            {
                From = 100,
                To = 150,
                Duration = TimeSpan.FromSeconds(4),
                AutoReverse = false
            };
            Storyboard.SetTarget(inhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(inhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(inhaleAnimation);

            // Druga animacja dla wysokości, aby zachować proporcje
            DoubleAnimation inhaleHeightAnimation = new DoubleAnimation
            {
                From = 100,
                To = 150,
                Duration = TimeSpan.FromSeconds(4),
                AutoReverse = false
            };
            Storyboard.SetTarget(inhaleHeightAnimation, breathingCircle);
            Storyboard.SetTargetProperty(inhaleHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(inhaleHeightAnimation);

            // Animacja dla zatrzymania oddechu po wdechu
            DoubleAnimation holdInhaleAnimation = new DoubleAnimation
            {
                From = 150,
                To = 150,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(4)
            };
            Storyboard.SetTarget(holdInhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(holdInhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(holdInhaleAnimation);

            // Animacja dla wydechu (zmniejszenie)
            DoubleAnimation exhaleAnimation = new DoubleAnimation
            {
                From = 150,
                To = 100,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(8)
            };
            Storyboard.SetTarget(exhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(exhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(exhaleAnimation);

            // Animacja wysokości dla wydechu
            DoubleAnimation exhaleHeightAnimation = new DoubleAnimation
            {
                From = 150,
                To = 100,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(8)
            };
            Storyboard.SetTarget(exhaleHeightAnimation, breathingCircle);
            Storyboard.SetTargetProperty(exhaleHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(exhaleHeightAnimation);

            // Animacja dla zatrzymania oddechu po wydechu
            DoubleAnimation holdExhaleAnimation = new DoubleAnimation
            {
                From = 100,
                To = 100,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(12)
            };
            Storyboard.SetTarget(holdExhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(holdExhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(holdExhaleAnimation);

            // Ustawienie powtarzania animacji
            breathingAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Dodanie animacji zmiany tekstu
            StringAnimationUsingKeyFrames textAnimation = new StringAnimationUsingKeyFrames();
            textAnimation.Duration = TimeSpan.FromSeconds(16);
            textAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Klatki kluczowe dla tekstu
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Wdech", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Zatrzymaj", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4))));
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Wydech", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8))));
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Zatrzymaj", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(12))));

            Storyboard.SetTarget(textAnimation, breathingStateText);
            Storyboard.SetTargetProperty(textAnimation, new PropertyPath(TextBlock.TextProperty));
            breathingAnimation.Children.Add(textAnimation);
        }

        // Tworzenie animacji 4-7-8 breathing
        private void Create478BreathingAnimation()
        {
            breathingAnimation = new Storyboard();

            // Animacja dla wdechu (powiększenie)
            DoubleAnimation inhaleAnimation = new DoubleAnimation
            {
                From = 100,
                To = 150,
                Duration = TimeSpan.FromSeconds(4),
                AutoReverse = false
            };
            Storyboard.SetTarget(inhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(inhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(inhaleAnimation);

            // Druga animacja dla wysokości, aby zachować proporcje
            DoubleAnimation inhaleHeightAnimation = new DoubleAnimation
            {
                From = 100,
                To = 150,
                Duration = TimeSpan.FromSeconds(4),
                AutoReverse = false
            };
            Storyboard.SetTarget(inhaleHeightAnimation, breathingCircle);
            Storyboard.SetTargetProperty(inhaleHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(inhaleHeightAnimation);

            // Animacja dla zatrzymania oddechu
            DoubleAnimation holdAnimation = new DoubleAnimation
            {
                From = 150,
                To = 150,
                Duration = TimeSpan.FromSeconds(7),
                BeginTime = TimeSpan.FromSeconds(4)
            };
            Storyboard.SetTarget(holdAnimation, breathingCircle);
            Storyboard.SetTargetProperty(holdAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(holdAnimation);

            // Animacja dla wydechu (zmniejszenie)
            DoubleAnimation exhaleAnimation = new DoubleAnimation
            {
                From = 150,
                To = 100,
                Duration = TimeSpan.FromSeconds(8),
                BeginTime = TimeSpan.FromSeconds(11)
            };
            Storyboard.SetTarget(exhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(exhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(exhaleAnimation);

            // Animacja wysokości dla wydechu
            DoubleAnimation exhaleHeightAnimation = new DoubleAnimation
            {
                From = 150,
                To = 100,
                Duration = TimeSpan.FromSeconds(8),
                BeginTime = TimeSpan.FromSeconds(11)
            };
            Storyboard.SetTarget(exhaleHeightAnimation, breathingCircle);
            Storyboard.SetTargetProperty(exhaleHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(exhaleHeightAnimation);

            // Ustawienie powtarzania animacji
            breathingAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Dodanie animacji zmiany tekstu
            StringAnimationUsingKeyFrames textAnimation = new StringAnimationUsingKeyFrames();
            textAnimation.Duration = TimeSpan.FromSeconds(19);
            textAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Klatki kluczowe dla tekstu
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Wdech (4)", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Zatrzymaj (7)", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4))));
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Wydech (8)", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(11))));

            Storyboard.SetTarget(textAnimation, breathingStateText);
            Storyboard.SetTargetProperty(textAnimation, new PropertyPath(TextBlock.TextProperty));
            breathingAnimation.Children.Add(textAnimation);
        }

        // Tworzenie animacji pursed-lip breathing
        private void CreatePursedLipBreathingAnimation()
        {
            breathingAnimation = new Storyboard();

            // Animacja dla wdechu (powiększenie)
            DoubleAnimation inhaleAnimation = new DoubleAnimation
            {
                From = 100,
                To = 140,
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = false
            };
            Storyboard.SetTarget(inhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(inhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(inhaleAnimation);

            // Druga animacja dla wysokości, aby zachować proporcje
            DoubleAnimation inhaleHeightAnimation = new DoubleAnimation
            {
                From = 100,
                To = 140,
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = false
            };
            Storyboard.SetTarget(inhaleHeightAnimation, breathingCircle);
            Storyboard.SetTargetProperty(inhaleHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(inhaleHeightAnimation);

            // Animacja dla wydechu (zmniejszenie)
            DoubleAnimation exhaleAnimation = new DoubleAnimation
            {
                From = 140,
                To = 100,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(2)
            };
            Storyboard.SetTarget(exhaleAnimation, breathingCircle);
            Storyboard.SetTargetProperty(exhaleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            breathingAnimation.Children.Add(exhaleAnimation);

            // Animacja wysokości dla wydechu
            DoubleAnimation exhaleHeightAnimation = new DoubleAnimation
            {
                From = 140,
                To = 100,
                Duration = TimeSpan.FromSeconds(4),
                BeginTime = TimeSpan.FromSeconds(2)
            };
            Storyboard.SetTarget(exhaleHeightAnimation, breathingCircle);
            Storyboard.SetTargetProperty(exhaleHeightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            breathingAnimation.Children.Add(exhaleHeightAnimation);

            // Ustawienie powtarzania animacji
            breathingAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Dodanie animacji zmiany tekstu
            StringAnimationUsingKeyFrames textAnimation = new StringAnimationUsingKeyFrames();
            textAnimation.Duration = TimeSpan.FromSeconds(6);
            textAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Klatki kluczowe dla tekstu
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Wdech przez nos", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            textAnimation.KeyFrames.Add(new DiscreteStringKeyFrame("Powolny wydech przez zaciśnięte usta", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2))));

            Storyboard.SetTarget(textAnimation, breathingStateText);
            Storyboard.SetTargetProperty(textAnimation, new PropertyPath(TextBlock.TextProperty));
            breathingAnimation.Children.Add(textAnimation);
        }

        // Uruchamianie animacji oddychania
        private void StartBreathingAnimation()
        {
            breathingAnimation.Begin();
        }

        // Zatrzymywanie animacji oddychania
        private void StopBreathingAnimation()
        {
            breathingAnimation.Stop();
            breathingStateText.Text = "Gotowy do rozpoczęcia";
        }

        // Zamykanie popupu
        private void CloseBreathingPopup()
        {
            StopBreathingAnimation();
            breathingPopup.IsOpen = false;
        }

        // Metoda wywoływana przy ładowaniu kontrolki - podpięcie przycisków
        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Podłączenie obsługi przycisków Start Now
            Button boxButton = LeftCard.FindName("BoxBreathingButton") as Button;
            Button calmingButton = MiddleCard.FindName("CalmingBreathingButton") as Button;
            Button pursedLipButton = RightCard.FindName("PursedLipBreathingButton") as Button;

            if (boxButton != null)
                boxButton.Click += (s, args) => ShowBoxBreathingPopup();

            if (calmingButton != null)
                calmingButton.Click += (s, args) => Show478BreathingPopup();

            if (pursedLipButton != null)
                pursedLipButton.Click += (s, args) => ShowPursedLipBreathingPopup();
        }
    }
}