using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FitnessStudio.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<SidePanelItem> SidePanelItems { get; set; }

        public HomeViewModel()
        {
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
    }

    public class SidePanelItem
    {
        public Uri IconPath { get; set; }
        public string Text { get; set; }
    }
}