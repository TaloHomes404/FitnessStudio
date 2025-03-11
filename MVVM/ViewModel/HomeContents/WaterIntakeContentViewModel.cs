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

        public ObservableCollection<HomeViewModel.RightSidebarItem> RightSidebarItems { get; }
        = new ObservableCollection<HomeViewModel.RightSidebarItem>();

        [ObservableProperty]
        private double _hydrationProgress;

        public string CurrentIntakeDisplay => $"{CurrentIntake} / {MaxDailyIntake}ml";

        public ObservableCollection<WaterAmount> WaterAmounts { get; }

        // Historia nawodnień
        public ObservableCollection<HydrationEntry> HydrationHistory { get; } = new();

        public WaterIntakeContentViewModel()
        {
            WaterAmounts = new ObservableCollection<WaterAmount>
            {
                new WaterAmount(120, "/Resources/cup_icon.png", "Cup - 120ml"),
                new WaterAmount(250, "/Resources/mug_icon.png", "Mug - 250ml"),
                new WaterAmount(500, "/Resources/water_bottle_icon.png", "Bottle - 500ml"),
                new WaterAmount(1500, "/Resources/big_water_bottle_icon.png", "Large bottle - 1500ml")
            };

            // Inicjalizacja zawartości sidebara
            RightSidebarItems.Add(new HomeViewModel.RightSidebarItem
            {
                Title = "water water",
                Description = "Spróbuj metody 4-7-8: Wdychaj 4 sekundy, wstrzymaj 7, wydychaj 8",
                ImagePath = new Uri("/FitnessStudio;component/Resources/supplements_border_img.jpg", UriKind.Relative)
            });

            RightSidebarItems.Add(new HomeViewModel.RightSidebarItem
            {
                Title = "water water",
                Description = "Słuchaj dźwięków natury przez minimum 15 minut dziennie",
                ImagePath = new Uri("/FitnessStudio;component/Resources/supplements_border_img.jpg", UriKind.Relative)
            });

            SelectedWaterAmount = WaterAmounts[1];
            UpdateHydrationProgress();
        }

        [RelayCommand]
        private void IncreaseIntake()
        {
            int amountToAdd = SelectedWaterAmount.Amount;
            CurrentIntake = Math.Min(MaxDailyIntake, CurrentIntake + amountToAdd);

            // Dodaj wpis do historii nawodnień z unikalnym ID
            var entry = new HydrationEntry
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amountToAdd,
                Timestamp = DateTime.Now,
                IconPath = SelectedWaterAmount.IconPath
            };

            HydrationHistory.Insert(0, entry);

            UpdateHydrationProgress();
            OnPropertyChanged(nameof(CurrentIntakeDisplay));
        }

        [RelayCommand]
        private void DecreaseIntake()
        {
            int amountToSubtract = SelectedWaterAmount.Amount;
            CurrentIntake = Math.Max(0, CurrentIntake - amountToSubtract);
            UpdateHydrationProgress();
            OnPropertyChanged(nameof(CurrentIntakeDisplay));
        }

        [RelayCommand]
        private void RemoveHydrationEntry(string id)
        {
            var entryToRemove = HydrationHistory.FirstOrDefault(e => e.Id == id);
            if (entryToRemove != null)
            {
                // Odejmij wartość z CurrentIntake
                CurrentIntake = Math.Max(0, CurrentIntake - entryToRemove.Amount);

                // Usuń wpis z historii
                HydrationHistory.Remove(entryToRemove);

                UpdateHydrationProgress();
                OnPropertyChanged(nameof(CurrentIntakeDisplay));
            }
        }

        private void UpdateHydrationProgress()
        {
            HydrationProgress = (double)CurrentIntake / MaxDailyIntake;
        }
    }

    // Klasa reprezentująca wpis nawodnienia
    public class HydrationEntry
    {
        public string Id { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public required string IconPath { get; set; }
        public string FormattedTime => Timestamp.ToString("HH:mm");
        public string FormattedDate => Timestamp.ToString("dd.MM.yyyy");
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