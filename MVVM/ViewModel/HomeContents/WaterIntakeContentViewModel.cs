using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public partial class WaterIntakeContentViewModel : ObservableObject
    {
        private const int MaxDailyIntake = 2500;

        [ObservableProperty]
        private int _currentIntake = 0;

        [ObservableProperty]
        private WaterAmount _selectedWaterAmount;

        public string CurrentIntakeDisplay => $"{CurrentIntake} / {MaxDailyIntake}ml";

        public ObservableCollection<WaterAmount> WaterAmounts { get; }

        // Nowa lista nawodnień
        public ObservableCollection<HydrationEntry> HydrationHistory { get; } = new();

        public WaterIntakeContentViewModel()
        {
            WaterAmounts = new ObservableCollection<WaterAmount>
            {
                new WaterAmount(120, "/Resources/cup_icon.png", "Kubek - 120ml"),
                new WaterAmount(250, "/Resources/mug_icon.png", "Filiżanka - 250ml"),
                new WaterAmount(500, "/Resources/water_bottle_icon.png", "Butelka - 500ml"),
                new WaterAmount(1500, "/Resources/big_water_bottle_icon.png", "Duża butelka - 1500ml")
            };

            SelectedWaterAmount = WaterAmounts[1];
        }

        [RelayCommand]
        private void IncreaseIntake()
        {
            int amountToAdd = SelectedWaterAmount.Amount;
            CurrentIntake = Math.Min(MaxDailyIntake, CurrentIntake + amountToAdd);

            // Dodaj wpis do historii nawodnień
            HydrationHistory.Insert(0, new HydrationEntry
            {
                Amount = amountToAdd,
                Timestamp = DateTime.Now,
                IconPath = SelectedWaterAmount.IconPath
            });

            OnPropertyChanged(nameof(CurrentIntakeDisplay));
        }

        [RelayCommand]
        private void DecreaseIntake()
        {
            int amountToSubtract = SelectedWaterAmount.Amount;
            CurrentIntake = Math.Max(0, CurrentIntake - amountToSubtract);
            OnPropertyChanged(nameof(CurrentIntakeDisplay));
        }
    }

    // Klasa reprezentująca wpis nawodnienia
    public class HydrationEntry
    {
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public required string IconPath { get; set; }
    }

    public class WaterAmount
    {
        public int Amount { get; }
        public string IconPath { get; }
        public string Description { get; }

        // Właściwość wyświetlana w ComboBox
        public string DisplayText => Description;

        public WaterAmount(int amount, string iconPath, string description)
        {
            Amount = amount;
            IconPath = iconPath;
            Description = description;
        }
    }

}