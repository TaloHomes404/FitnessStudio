using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
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
                PopupTitle.Text = $"{category} Exercises";

                // Clear previous content
                ExerciseContent.Children.Clear();

                // Add exercises based on category
                switch (category)
                {
                    case "Back":
                        AddExerciseItem("Seated Back Stretch", "2 minutes",
                            "Improves spinal alignment and relieves tension.",
                            "1. Sit upright on a chair with your feet flat on the floor.\n2. Place your hands on your knees.\n3. Gently arch your back, pushing your chest forward.\n4. Hold for 5 seconds, then return to the starting position.\n5. Repeat for 2 minutes.");
                        AddExerciseItem("Wall Angels", "3 minutes",
                            "Strengthens upper back and improves posture.",
                            "1. Stand with your back against a wall, feet about 6 inches away.\n2. Raise your arms to form a 'W' shape with your elbows bent at 90 degrees.\n3. Slowly slide your arms up the wall, keeping contact with the wall.\n4. Return to the 'W' position.\n5. Repeat for 3 minutes.");
                        AddExerciseItem("Cat-Cow Stretch", "2 minutes",
                            "Increases flexibility in the spine.",
                            "1. Get on all fours with your hands under your shoulders and knees under your hips.\n2. Arch your back upwards (cat pose) and hold for 5 seconds.\n3. Lower your back and lift your head (cow pose).\n4. Repeat for 2 minutes.");
                        AddExerciseItem("Thoracic Extension", "3 minutes",
                            "Improves thoracic spine mobility.",
                            "1. Sit on a chair with your feet flat on the floor.\n2. Place your hands behind your head.\n3. Gently arch your upper back over the chair.\n4. Hold for 5 seconds, then return to the starting position.\n5. Repeat for 3 minutes.");
                        break;
                    case "Eyes":
                        AddExerciseItem("20-20-20 Rule", "Ongoing",
                            "Reduces eye strain from screen use.",
                            "1. Every 20 minutes, look at something 20 feet away.\n2. Focus on that object for 20 seconds.\n3. Return to your screen.\n4. Repeat throughout the day.");
                        AddExerciseItem("Eye Rolling", "1 minute",
                            "Relaxes eye muscles.",
                            "1. Sit or stand comfortably.\n2. Slowly roll your eyes in a clockwise direction for 30 seconds.\n3. Reverse direction and roll your eyes counterclockwise for 30 seconds.");
                        AddExerciseItem("Focus Shifting", "2 minutes",
                            "Improves eye focus flexibility.",
                            "1. Hold your thumb about 10 inches in front of your face.\n2. Focus on your thumb for 5 seconds.\n3. Shift your focus to an object about 10 feet away.\n4. Alternate focus between your thumb and the distant object for 2 minutes.");
                        break;
                    case "Neck":
                        AddExerciseItem("Neck Tilts", "2 minutes",
                            "Relieves neck tension.",
                            "1. Sit or stand with your back straight.\n2. Gently tilt your head to the right, bringing your ear towards your shoulder.\n3. Hold for 5 seconds, then return to the center.\n4. Repeat on the left side.\n5. Alternate sides for 2 minutes.");
                        AddExerciseItem("Chin Tucks", "2 minutes",
                            "Aligns the head with the spine.",
                            "1. Sit or stand with your back straight.\n2. Gently tuck your chin towards your chest.\n3. Hold for 5 seconds, then return to the starting position.\n4. Repeat for 2 minutes.");
                        AddExerciseItem("Neck Rotations", "2 minutes",
                            "Increases neck mobility.",
                            "1. Sit or stand with your back straight.\n2. Slowly rotate your head to the right, looking over your shoulder.\n3. Hold for 5 seconds, then return to the center.\n4. Repeat on the left side.\n5. Alternate sides for 2 minutes.");
                        AddExerciseItem("Shoulder Blade Squeeze", "1 minute",
                            "Strengthens upper back and neck.",
                            "1. Sit or stand with your back straight.\n2. Squeeze your shoulder blades together.\n3. Hold for 5 seconds, then release.\n4. Repeat for 1 minute.");
                        break;
                    case "FullBody":
                        AddExerciseItem("Mountain Pose", "1 minute",
                            "Improves overall posture.",
                            "1. Stand tall with your feet together.\n2. Engage your core and keep your arms at your sides.\n3. Hold this position for 1 minute, focusing on your posture.");
                        AddExerciseItem("Child's Pose", "2 minutes",
                            "Stretches the entire back and relieves tension.",
                            "1. Kneel on the floor with your big toes touching.\n2. Sit back on your heels and stretch your arms forward.\n3. Hold this position for 2 minutes, breathing deeply.");
                        AddExerciseItem("Standing Forward Bend", "2 minutes",
                            "Stretches hamstrings and back.",
                            "1. Stand with your feet hip-width apart.\n2. Bend forward from your hips, keeping your back straight.\n3. Let your hands hang towards the floor.\n4. Hold for 2 minutes, then slowly return to standing.");
                        AddExerciseItem("Plank", "1 minute",
                            "Strengthens core and improves posture.",
                            "1. Start in a push-up position with your body in a straight line.\n2. Hold this position for 1 minute, keeping your core engaged.");
                        AddExerciseItem("Standing Side Stretch", "2 minutes",
                            "Stretches the sides of the body.",
                            "1. Stand tall with your feet together.\n2. Raise your right arm overhead and lean to the left.\n3. Hold for 10 seconds, then switch sides.\n4. Alternate sides for 2 minutes.");
                        break;
                    case "Wrists":
                        AddExerciseItem("Wrist Flexor Stretch", "1 minute",
                            "Stretches the wrist flexor muscles.",
                            "1. Extend your right arm in front of you with your palm facing up.\n2. Use your left hand to gently pull your fingers back.\n3. Hold for 15 seconds, then switch arms.\n4. Repeat for 1 minute.");
                        AddExerciseItem("Wrist Extensor Stretch", "1 minute",
                            "Stretches the wrist extensor muscles.",
                            "1. Extend your right arm in front of you with your palm facing down.\n2. Use your left hand to gently push your fingers down.\n3. Hold for 15 seconds, then switch arms.\n4. Repeat for 1 minute.");
                        AddExerciseItem("Wrist Circles", "1 minute",
                            "Improves wrist mobility.",
                            "1. Extend your arms in front of you.\n2. Rotate your wrists in a clockwise direction for 30 seconds.\n3. Reverse direction and rotate counterclockwise for 30 seconds.");
                        break;
                    case "Legs":
                        AddExerciseItem("Standing Hamstring Stretch", "1 minute",
                            "Stretches the hamstrings.",
                            "1. Stand with your feet hip-width apart.\n2. Bend forward from your hips, keeping your legs straight.\n3. Let your hands hang towards the floor.\n4. Hold for 1 minute, then slowly return to standing.");
                        AddExerciseItem("Quadriceps Stretch", "1 minute",
                            "Stretches the quadriceps.",
                            "1. Stand on your right leg and pull your left heel towards your buttocks.\n2. Hold for 15 seconds, then switch legs.\n3. Repeat for 1 minute.");
                        AddExerciseItem("Calf Raises", "2 minutes",
                            "Strengthens calf muscles.",
                            "1. Stand with your feet hip-width apart.\n2. Rise onto your toes, then slowly lower back down.\n3. Repeat for 2 minutes.");
                        AddExerciseItem("Seated Leg Crosses", "2 minutes",
                            "Improves hip flexibility.",
                            "1. Sit on the floor with your legs extended.\n2. Cross your right leg over your left, then switch.\n3. Alternate legs for 2 minutes.");
                        break;
                }

                // Show the popup and overlay
                ExercisePopup.Visibility = Visibility.Visible;
                PopupOverlay.Visibility = Visibility.Visible;
            }
        }

        private void AddExerciseItem(string title, string duration, string benefits, string instructions)
        {
            // Create exercise item container
            Border container = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                CornerRadius = new CornerRadius(10),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 0, 10),
                Padding = new Thickness(15)
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Exercise details
            StackPanel detailsPanel = new StackPanel();
            TextBlock titleBlock = new TextBlock
            {
                Text = title,
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock durationBlock = new TextBlock
            {
                Text = $"Duration: {duration}",
                FontSize = 14,
                Foreground = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock benefitsBlock = new TextBlock
            {
                Text = $"Benefits: {benefits}",
                FontSize = 14,
                Foreground = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock instructionsBlock = new TextBlock
            {
                Text = $"Instructions:\n{instructions}",
                FontSize = 14,
                Foreground = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap
            };

            detailsPanel.Children.Add(titleBlock);
            detailsPanel.Children.Add(durationBlock);
            detailsPanel.Children.Add(benefitsBlock);
            detailsPanel.Children.Add(instructionsBlock);

            Grid.SetColumn(detailsPanel, 0);
            grid.Children.Add(detailsPanel);

            container.Child = grid;
            ExerciseContent.Children.Add(container);
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            ExercisePopup.Visibility = Visibility.Collapsed;
            PopupOverlay.Visibility = Visibility.Collapsed;
        }
    }
}