using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        //Views values for main content section
        private object? _currentMainContent;
        public object? CurrentMainContent
        {
            get => _currentMainContent;
            set => SetProperty(ref _currentMainContent, value);
        }

        //Navigation commands init
        public ICommand NavigateToHomeCommand { get; private set; }
        public ICommand NavigateToWorkoutLogCommand { get; private set; }
        public ICommand NavigateToToolsCommand { get; private set; }
        public ICommand NavigateToWaterIntakeCommand { get; private set; }
        public ICommand NavigateToSuplementsCommand { get; private set; }
        public ICommand NavigateToStepsCounterCommand { get; private set; }
        public ICommand NavigateToBreathingExercisesCommand { get; private set; }



        // Kolekcja dla kategorii w sidebarze
        public ObservableCollection<SidebarCategory> SidebarCategories { get; set; }

        public HomeViewModel()
        {
            //Default init home page content - HomeMainContent
            CurrentMainContent = new HomeMainContentViewModel();

            //init for commands to change home page content
            NavigateToHomeCommand = new RelayCommand(NavigateToHome);
            NavigateToWorkoutLogCommand = new RelayCommand(NavigateToWorkoutLog);
            NavigateToToolsCommand = new RelayCommand(NavigateToTools);
            NavigateToWaterIntakeCommand = new RelayCommand(NavigateToWaterIntake);
            NavigateToSuplementsCommand = new RelayCommand(NavigateToSuplements);
            NavigateToStepsCounterCommand = new RelayCommand(NavigateToStepsCounter);
            NavigateToBreathingExercisesCommand = new RelayCommand(NavigateToBreathingExercises);

            // Categories sections declaration and buttons asign
            SidebarCategories = new ObservableCollection<SidebarCategory>
            {
                //Kategoria "Dashboard"
                new SidebarCategory
                {
                    CategoryName = "Dashboard",
                    CategoryIcon = new Uri("/FitnessStudio;component/Resources/dashboard_icon.png", UriKind.Relative),
                    Items = new ObservableCollection<SidePanelItem>
                    {
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/home_icon.png", UriKind.Relative),
                            Text = "Home",
                            Command = NavigateToToolsCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/profile_icon.png", UriKind.Relative),
                            Text = "Profile",
                            Command = NavigateToToolsCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/settings_icon.png", UriKind.Relative),
                            Text = "Settings",
                            Command = NavigateToToolsCommand
                        },
                    }
                },

                // Kategoria "Physical Health"
                new SidebarCategory
                {
                    CategoryName = "Physical Health",
                    CategoryIcon = new Uri("/FitnessStudio;component/Resources/physical_health_icon.png", UriKind.Relative),
                    Items = new ObservableCollection<SidePanelItem>
                    {
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/workout_log_icon.png", UriKind.Relative),
                            Text = "Workout Log",
                            Command = NavigateToWorkoutLogCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/progressive_overload_icon.png", UriKind.Relative),
                            Text = "Progressive Overload",
                            Command = NavigateToHomeCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/step_counter_icon.png", UriKind.Relative),
                            Text = "Steps Counter",
                            Command = NavigateToStepsCounterCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/correct_posture_icon.png", UriKind.Relative),
                            Text = "Correct Posture",
                            Command = NavigateToHomeCommand
                        }
                    }
                },
                
                // Kategoria "Mental Health"
                new SidebarCategory
                {
                    CategoryName = "Mental Health",
                    CategoryIcon = new Uri("/FitnessStudio;component/Resources/mental_health_icon.png", UriKind.Relative),
                    Items = new ObservableCollection<SidePanelItem>
                    {
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/breathing_exercises_icon.png", UriKind.Relative),
                            Text = "Breathing Exercises",
                            Command = NavigateToBreathingExercisesCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/meditation_icon.png", UriKind.Relative),
                            Text = "Meditation",
                            Command = NavigateToWaterIntakeCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/stressed_icon.png", UriKind.Relative),
                            Text = "Stress",
                            Command = NavigateToWaterIntakeCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/motivation_icon.png", UriKind.Relative),
                            Text = "Motivation",
                            Command = NavigateToWaterIntakeCommand
                        },
                    }
                },
                
                // Kategoria "Nutrition"
                new SidebarCategory
                {
                    CategoryName = "Nutrition",
                    CategoryIcon = new Uri("/FitnessStudio;component/Resources/nutrition_icon.png", UriKind.Relative),
                    Items = new ObservableCollection<SidePanelItem>
                    {
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/calories_calculator_icon.png", UriKind.Relative),
                            Text = "Calories Calculator",
                            Command = NavigateToToolsCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/bmi_calculator_icon.png", UriKind.Relative),
                            Text = "BMI Calculator",
                            Command = NavigateToToolsCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/suplements_icon.png", UriKind.Relative),
                            Text = "Suplements",
                            Command = NavigateToSuplementsCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/water_icon.png", UriKind.Relative),
                            Text = "Water Intake",
                            Command = NavigateToWaterIntakeCommand
                        },
                    }
                },
                // Kategoria "Recovery & Sleep"
                new SidebarCategory
                {
                    CategoryName = "Recovery & Sleep",
                    CategoryIcon = new Uri("/FitnessStudio;component/Resources/recovery_icon.png", UriKind.Relative),
                    Items = new ObservableCollection<SidePanelItem>
                    {
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/sleep_schedule_icon.png", UriKind.Relative),
                            Text = "Sleep Schedule",
                            Command = NavigateToToolsCommand
                        },
                        new SidePanelItem {
                            IconPath = new Uri("/FitnessStudio;component/Resources/blue_light_icon.png", UriKind.Relative),
                            Text = "Blue Light Reduction",
                            Command = NavigateToToolsCommand
                        },
                    }
                }
            };
        }

        //Change home screen content method
        public void ChangeMainContent(object newContent)
        {
            CurrentMainContent = newContent;
        }

        //Navigation to main sections (change main section)
        private void NavigateToHome()
        {
            CurrentMainContent = new HomeMainContentViewModel();
        }

        private void NavigateToSuplements()
        {
            CurrentMainContent = new SuplementsContentViewModel();
        }

        private void NavigateToWorkoutLog()
        {
            CurrentMainContent = new WorkoutLogContentViewModel();
        }

        private void NavigateToStepsCounter()
        {
            CurrentMainContent = new StepsCounterContentViewModel();
        }

        private void NavigateToBreathingExercises()
        {
            CurrentMainContent = new BreathingExercisesContentViewModel();
        }

        private void NavigateToWaterIntake()
        {
            CurrentMainContent = new WaterIntakeContentViewModel();
        }

        private void NavigateToTools()
        {
            CurrentMainContent = new ToolsContentViewModel();
        }

        // Klasa dla elementu panelu bocznego (przycisk)
        public class SidePanelItem
        {
            public Uri? IconPath { get; set; }
            public string? Text { get; set; }
            public ICommand? Command { get; set; }
        }

        // Klasa dla kategorii w sidebarze
        public class SidebarCategory
        {
            public string? CategoryName { get; set; }
            public Uri? CategoryIcon { get; set; }
            public ObservableCollection<SidePanelItem> Items { get; set; } = new ObservableCollection<SidePanelItem>();
        }
    }
}