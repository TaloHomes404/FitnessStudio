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
        private object _currentMainContent;
        public object CurrentMainContent
        {
            get { return _currentMainContent; }
            set { _currentMainContent = value; OnPropertyChanged(nameof(CurrentMainContent)); }
        }

        //Navigation commands init
        public ICommand NavigateToHomeCommand { get; private set; }
        public ICommand NavigateToWorkoutLogCommand { get; private set; }

        public ObservableCollection<SidePanelItem> SidePanelItems { get; set; }

        public HomeViewModel()
        {

            //Default init home page content - HomeMainContent
            CurrentMainContent = new HomeMainContentViewModel();

            //init for commands to change home page content
            NavigateToHomeCommand = new RelayCommand(NavigateToHome);
            NavigateToWorkoutLogCommand = new RelayCommand(NavigateToWorkoutLog);



            SidePanelItems = new ObservableCollection<SidePanelItem>
            {
                new SidePanelItem {
                    IconPath = new Uri("/FitnessStudio;component/Resources/home_icon.png", UriKind.Relative),
                    Text = "Home"
                },
                new SidePanelItem {
                    IconPath = new Uri("/FitnessStudio;component/Resources/tools_icon.png", UriKind.Relative),
                    Text = "Tools"
                },
                new SidePanelItem {
                    IconPath = new Uri("/FitnessStudio;component/Resources/workout_log_icon.png", UriKind.Relative),
                    Text = "Workout \nLog"
                },
                new SidePanelItem {
                    IconPath = new Uri("/FitnessStudio;component/Resources/water_intake_icon.png", UriKind.Relative),
                    Text = "Water \nIntake"
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

        private void NavigateToWorkoutLog()
        {
            CurrentMainContent = new WorkoutLogContentViewModel();
        }

    }


    public class SidePanelItem
    {
        public Uri? IconPath { get; set; }
        public string? Text { get; set; }
    }
}