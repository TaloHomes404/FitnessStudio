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
                new SidePanelItem { Icon="Resources/home_icon.png", Text="Home" },
                new SidePanelItem { Icon="Resources/tools_icon.png", Text="Tools" },
                new SidePanelItem { Icon="Resources/workout_icon.png", Text="Workout \nLog" },
                new SidePanelItem { Icon="Resources/water_intake_icon.png", Text="Water \nIntake" }
            };
        }
    }
    public class SidePanelItem
    {
        public string Icon { get; set; }
        public string Text { get; set; }
    }
}
