using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Interaction logic for CorrectPostureContent.xaml
    /// </summary>
    public partial class CorrectPostureContent : UserControl
    {
        public CorrectPostureContent()
        {
            InitializeComponent();

            DataContext = new CorrectPostureContentViewModel();
        }

        private void PostureCircle_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Tag is string category)
            {
                // Update popup title
                PopupTitle.Text = category + " Exercises";

                // Clear previous content
                ExerciseContent.Children.Clear();

                // Add exercises based on category
                switch (category)
                {
                    case "Back":
                        AddExerciseItem("Seated Back Stretch", "2 minutes", "Sit upright and stretch your spine to improve posture");
                        AddExerciseItem("Wall Angels", "3 minutes", "Stand with your back against a wall and perform arm movements");
                        AddExerciseItem("Cat-Cow Stretch", "2 minutes", "Get on all fours and alternate between arching and rounding your back");
                        AddExerciseItem("Thoracic Extension", "3 minutes", "Use a foam roller to extend your thoracic spine");
                        break;
                    case "Eyes":
                        AddExerciseItem("20-20-20 Rule", "Ongoing", "Every 20 minutes, look at something 20 feet away for 20 seconds");
                        AddExerciseItem("Eye Rolling", "1 minute", "Roll your eyes in clockwise and counterclockwise directions");
                        AddExerciseItem("Focus Shifting", "2 minutes", "Switch focus between near and far objects");
                        break;
                    case "Neck":
                        AddExerciseItem("Neck Tilts", "2 minutes", "Gently tilt your head to each side");
                        AddExerciseItem("Chin Tucks", "2 minutes", "Tuck your chin to align your head with your spine");
                        AddExerciseItem("Neck Rotations", "2 minutes", "Slowly rotate your head from side to side");
                        AddExerciseItem("Shoulder Blade Squeeze", "1 minute", "Squeeze your shoulder blades together to support neck posture");
                        break;
                    case "FullBody":
                        AddExerciseItem("Mountain Pose", "1 minute", "Stand tall with proper alignment from feet to head");
                        AddExerciseItem("Child's Pose", "2 minutes", "Stretch the entire back and release tension");
                        AddExerciseItem("Standing Forward Bend", "2 minutes", "Bend forward from the hips to stretch back and hamstrings");
                        AddExerciseItem("Plank", "1 minute", "Build core strength to support better posture");
                        AddExerciseItem("Standing Side Stretch", "2 minutes", "Stretch sides of the body to maintain lateral balance");
                        break;
                    case "Wrists":
                        AddExerciseItem("Wrist Flexor Stretch", "1 minute", "Extend arm and gently pull fingers back");
                        AddExerciseItem("Wrist Extensor Stretch", "1 minute", "Extend arm and gently push fingers down");
                        AddExerciseItem("Wrist Circles", "1 minute", "Rotate wrists in both directions");
                        break;
                    case "Legs":
                        AddExerciseItem("Standing Hamstring Stretch", "1 minute", "Keep legs straight and bend forward from hips");
                        AddExerciseItem("Quadriceps Stretch", "1 minute", "Stand on one leg and pull other heel toward buttocks");
                        AddExerciseItem("Calf Raises", "2 minutes", "Stand on toes and lower back down slowly");
                        AddExerciseItem("Seated Leg Crosses", "2 minutes", "Cross legs while seated to keep hips flexible");
                        break;
                }

                // Show the popup and overlay
                ExercisePopup.Visibility = Visibility.Visible;
                PopupOverlay.Visibility = Visibility.Visible;
            }
        }

        private void AddExerciseItem(string title, string duration, string description)
        {
            // Create exercise item container
            Border container = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(0, 0, 0, 10),
                Padding = new Thickness(15)
            };

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Exercise title with duration
            StackPanel headerPanel = new StackPanel { Orientation = Orientation.Horizontal };
            TextBlock titleBlock = new TextBlock
            {
                Text = title,
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 0, 10, 0)
            };

            TextBlock durationBlock = new TextBlock
            {
                Text = duration,
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(187, 187, 187)),
                VerticalAlignment = VerticalAlignment.Center
            };

            headerPanel.Children.Add(titleBlock);
            headerPanel.Children.Add(durationBlock);

            // Description
            TextBlock descriptionBlock = new TextBlock
            {
                Text = description,
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(204, 204, 204)),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 0)
            };

            grid.Children.Add(headerPanel);
            Grid.SetRow(headerPanel, 0);

            grid.Children.Add(descriptionBlock);
            Grid.SetRow(descriptionBlock, 1);

            container.Child = grid;
            ExerciseContent.Children.Add(container);
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            // Hide the popup and overlay
            ExercisePopup.Visibility = Visibility.Collapsed;
            PopupOverlay.Visibility = Visibility.Collapsed;
        }
    }
}