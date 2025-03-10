using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;
namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy SleepScheduleContent.xaml
    /// </summary>
    public partial class SleepScheduleContent : UserControl
    {
        public SleepScheduleContent()
        {
            InitializeComponent();
            DataContext = new SleepSchedulerContentViewModel();
        }
    }
}
