using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public partial class SleepSchedulerContentViewModel : ObservableObject
    {

        //Properties for sleepscheduler vm
        [ObservableProperty]
        private bool isNotificationPopupOpen;

        [ObservableProperty]
        private bool isHolidayPopupOpen;

        [ObservableProperty]
        private bool isAlarmPopupOpen;

        //Commands for sleepscheduler vm
        public RelayCommand ShowNotificationPopupCommand { get; }
        public RelayCommand ShowHolidayPopupCommand { get; }
        public RelayCommand ShowAlarmPopupCommand { get; }

        public SleepSchedulerContentViewModel()
        {
            ShowNotificationPopupCommand = new RelayCommand(() => IsNotificationPopupOpen = !IsNotificationPopupOpen);
            ShowHolidayPopupCommand = new RelayCommand(() => IsHolidayPopupOpen = !IsHolidayPopupOpen);
            ShowAlarmPopupCommand = new RelayCommand(() => IsAlarmPopupOpen = !IsAlarmPopupOpen);

        }

    }
}
