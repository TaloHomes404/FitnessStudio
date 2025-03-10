using System.Windows.Controls;

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
            DataContext = new SleepScheduleContentViewModel();
        }
    }
}
