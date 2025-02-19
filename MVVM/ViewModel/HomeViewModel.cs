using System.Collections.ObjectModel;
using FitnessStudio.MVVM.Model;

namespace FitnessStudio.MVVM.ViewModel
{
    public class HomeViewModel
    {
        public ObservableCollection<SidePanelItem> SidePanelItems { get; set; }

        public HomeViewModel()
        {
            SidePanelItems = new ObservableCollection<SidePanelItem>
        {
            new SidePanelItem { Icon = "🏠", Text = "Home" },
            new SidePanelItem { Icon = "🛠️", Text = "Tools" },
            new SidePanelItem { Icon = "📖", Text = "Workout Log" },
            new SidePanelItem { Icon = "💧", Text = "Water Intake" }
        };
        }
    }
}
