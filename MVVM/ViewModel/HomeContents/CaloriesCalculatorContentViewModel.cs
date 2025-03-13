using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public class CaloriesCalculatorContentViewModel : ObservableObject, INotifyDataErrorInfo
    {
        // Stałe dla obliczeń BMR (równanie Harris-Benedict)
        private const double MALE_BASE = 66.5;           // stała bazowa dla mężczyzn
        private const double FEMALE_BASE = 655.1;        // stała bazowa dla kobiet
        private const double MALE_WEIGHT_FACTOR = 13.75; // mnożnik dla wagi mężczyzn (kg)
        private const double FEMALE_WEIGHT_FACTOR = 9.563; // mnożnik dla wagi kobiet (kg)
        private const double MALE_HEIGHT_FACTOR = 5.0;   // mnożnik dla wzrostu mężczyzn (cm)
        private const double FEMALE_HEIGHT_FACTOR = 1.85; // mnożnik dla wzrostu kobiet (cm)
        private const double MALE_AGE_FACTOR = 6.75;     // mnożnik dla wieku mężczyzn
        private const double FEMALE_AGE_FACTOR = 4.676;  // mnożnik dla wieku kobiet

        // Mnożniki aktywności (indeksy: 0 – siedzący, 1 – lekka aktywność, 2 – umiarkowana, 3 – intensywna, 4 – sportowiec)
        private readonly double[] ActivityMultipliers = { 1.2, 1.375, 1.55, 1.725, 1.9 };

        // Właściwości danych osobowych
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                SetProperty(ref _age, value);
                ValidateAge();
                Recalculate();
            }
        }

        private bool _isFemale;
        public bool IsFemale
        {
            get => !_isMale;
            set
            {
                _isMale = !value;
                OnPropertyChanged(nameof(IsMale));
                Recalculate();
            }
        }


        private bool _isMale = true;
        public bool IsMale
        {
            get => _isMale;
            set
            {
                SetProperty(ref _isMale, value);
                Recalculate();
            }
        }

        private double _weight;
        public double Weight
        {
            get => _weight;
            set
            {
                SetProperty(ref _weight, value);
                ValidateWeight();
                Recalculate();
            }
        }

        // Określenie jednostki dla wagi – true: kg, false: lbs
        private bool _isKg = true;
        public bool IsKg
        {
            get => _isKg;
            set
            {
                SetProperty(ref _isKg, value);
                Recalculate();
            }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                ValidateHeight();
                Recalculate();
            }
        }

        // Określenie jednostki dla wzrostu – true: cm, false: in
        private bool _isCm = true;
        public bool IsCm
        {
            get => _isCm;
            set
            {
                SetProperty(ref _isCm, value);
                Recalculate();
            }
        }

        private int _activityLevelIndex = 1;
        public int ActivityLevelIndex
        {
            get => _activityLevelIndex;
            set
            {
                SetProperty(ref _activityLevelIndex, value);
                Recalculate();
            }
        }

        // Wyniki obliczeń
        private int _tdee;
        public int TDEE
        {
            get => _tdee;
            set => SetProperty(ref _tdee, value);
        }

        private int _weightGainCalories;
        public int WeightGainCalories
        {
            get => _weightGainCalories;
            set => SetProperty(ref _weightGainCalories, value);
        }

        private int _mildWeightGainCalories;
        public int MildWeightGainCalories
        {
            get => _mildWeightGainCalories;
            set => SetProperty(ref _mildWeightGainCalories, value);
        }

        private int _weightLossCalories;
        public int WeightLossCalories
        {
            get => _weightLossCalories;
            set => SetProperty(ref _weightLossCalories, value);
        }

        private int _significantWeightLossCalories;
        public int SignificantWeightLossCalories
        {
            get => _significantWeightLossCalories;
            set => SetProperty(ref _significantWeightLossCalories, value);
        }

        // Właściwości obliczeń makroskładników
        private int _selectedCalorieGoalIndex = 2; // Domyślnie: utrzymanie
        public int SelectedCalorieGoalIndex
        {
            get => _selectedCalorieGoalIndex;
            set
            {
                SetProperty(ref _selectedCalorieGoalIndex, value);
                CalculateMacros();
            }
        }

        // Opcje dla proporcji białka (g na funt)
        public ObservableCollection<double> ProteinRatioOptions { get; } = new ObservableCollection<double>();

        // Opcje dla proporcji węglowodanów (g na funt)
        public ObservableCollection<double> CarbRatioOptions { get; } = new ObservableCollection<double>();

        private double _proteinRatio = 0.9; // g na funt
        public double ProteinRatio
        {
            get => _proteinRatio;
            set
            {
                _proteinRatio = value;
                SetProperty(ref _proteinRatio, value);
                CalculateMacros();
                OnPropertyChanged();
            }
        }

        private double _carbRatio = 1.0; // g na funt
        public double CarbRatio
        {
            get => _carbRatio;
            set
            {
                _carbRatio = value;
                SetProperty(ref _carbRatio, value);
                CalculateMacros();
                OnPropertyChanged();
            }
        }

        private double _proteinGrams;
        public double ProteinGrams
        {
            get => _proteinGrams;
            set => SetProperty(ref _proteinGrams, value);
        }

        private double _carbGrams;
        public double CarbGrams
        {
            get => _carbGrams;
            set => SetProperty(ref _carbGrams, value);
        }

        private double _fatGrams;
        public double FatGrams
        {
            get => _fatGrams;
            set => SetProperty(ref _fatGrams, value);
        }

        private int _proteinCalories;
        public int ProteinCalories
        {
            get => _proteinCalories;
            set => SetProperty(ref _proteinCalories, value);
        }

        private int _carbCalories;
        public int CarbCalories
        {
            get => _carbCalories;
            set => SetProperty(ref _carbCalories, value);
        }

        private int _fatCalories;
        public int FatCalories
        {
            get => _fatCalories;
            set => SetProperty(ref _fatCalories, value);
        }

        private double _proteinPercentage;
        public double ProteinPercentage
        {
            get => _proteinPercentage;
            set => SetProperty(ref _proteinPercentage, value);
        }

        private double _carbPercentage;
        public double CarbPercentage
        {
            get => _carbPercentage;
            set => SetProperty(ref _carbPercentage, value);
        }

        private double _fatPercentage;
        public double FatPercentage
        {
            get => _fatPercentage;
            set => SetProperty(ref _fatPercentage, value);
        }

        private bool _macrosPopupIsOpen;
        public bool MacrosPopupIsOpen
        {
            get => _macrosPopupIsOpen;
            set => SetProperty(ref _macrosPopupIsOpen, value);
        }

        // Implementacja walidacji – INotifyDataErrorInfo
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // Komendy
        public IRelayCommand ShowMacrosPopupCommand { get; }
        public IRelayCommand CloseMacrosPopupCommand { get; }

        public IRelayCommand CalculateTDEECommand { get; }

        public CaloriesCalculatorContentViewModel()
        {

            // Inicjalizacja opcji dla proporcji białka i węglowodanów
            InitializeRatioOptions();

            CalculateTDEECommand = new RelayCommand(Recalculate, () => !HasErrors);

            ShowMacrosPopupCommand = new RelayCommand(() =>
            {
                CalculateMacros();
                MacrosPopupIsOpen = true;
            }, () => TDEE > 0);

            CloseMacrosPopupCommand = new RelayCommand(() => MacrosPopupIsOpen = false);
        }

        // Inicjalizacja opcji dla proporcji białka i węglowodanów
        private void InitializeRatioOptions()
        {
            // Opcje dla proporcji białka (g na funt) od 0.8 do 1.5
            for (double i = 0.8; i <= 1.5; i += 0.1)
            {
                ProteinRatioOptions.Add(Math.Round(i, 1));
            }

            // Opcje dla proporcji węglowodanów (g na funt) od 0.5 do 3.0
            for (double i = 0.5; i <= 3.0; i += 0.5)
            {
                CarbRatioOptions.Add(Math.Round(i, 1));
            }
        }


        // Metoda przeliczająca TDEE oraz kalorie dla różnych celów
        private void Recalculate()
        {
            if (!CanCalculateTDEE())
                return;

            // Konwersja do systemu metrycznego, jeśli potrzebne
            double weightInKg = IsKg ? Weight : Weight * 0.453592;
            double heightInCm = IsCm ? Height : Height * 2.54;

            // Obliczenie BMR wg równania Harris-Benedict
            double bmr;
            if (IsMale)
            {
                bmr = MALE_BASE + (MALE_WEIGHT_FACTOR * weightInKg) + (MALE_HEIGHT_FACTOR * heightInCm) - (MALE_AGE_FACTOR * Age);
            }
            else
            {
                bmr = FEMALE_BASE + (FEMALE_WEIGHT_FACTOR * weightInKg) + (FEMALE_HEIGHT_FACTOR * heightInCm) - (FEMALE_AGE_FACTOR * Age);
            }

            double activityMultiplier = ActivityMultipliers[Math.Clamp(ActivityLevelIndex, 0, ActivityMultipliers.Length - 1)];
            TDEE = (int)Math.Round(bmr * activityMultiplier);

            // Obliczenie kaloryczności dla różnych celów
            WeightGainCalories = (int)Math.Round(TDEE * 1.2);       // +20%
            MildWeightGainCalories = (int)Math.Round(TDEE * 1.1);     // +10%
            WeightLossCalories = (int)Math.Round(TDEE * 0.9);         // -10%
            SignificantWeightLossCalories = (int)Math.Round(TDEE * 0.8); // -20%

            CalculateMacros();
            ShowMacrosPopupCommand.NotifyCanExecuteChanged();
        }

        private bool CanCalculateTDEE()
        {
            return Age > 0 && Weight > 0 && Height > 0 && !HasErrors;
        }

        // Obliczenie makroskładników na podstawie wybranego celu kalorycznego
        // Obliczenie makroskładników na podstawie wybranego celu kalorycznego
        private void CalculateMacros()
        {
            int selectedCalories = SelectedCalorieGoalIndex switch
            {
                0 => WeightGainCalories,
                1 => MildWeightGainCalories,
                2 => TDEE,
                3 => WeightLossCalories,
                4 => SignificantWeightLossCalories,
                _ => TDEE
            };

            if (selectedCalories <= 0)
                return;

            // Konwersja wagi na funty dla obliczeń makro (1 kg = 2.20462 funta)
            double weightInLbs = IsKg ? Weight * 2.20462 : Weight;

            // Obliczanie białka
            ProteinGrams = Math.Round(weightInLbs * ProteinRatio, 1);
            ProteinCalories = (int)(ProteinGrams * 4); // 4 kalorie na gram białka

            // Obliczanie węglowodanów
            CarbGrams = Math.Round(weightInLbs * CarbRatio, 1);
            CarbCalories = (int)(CarbGrams * 4); // 4 kalorie na gram węglowodanów

            // Pozostałe kalorie przydzielamy tłuszczom
            int remainingCalories = selectedCalories - ProteinCalories - CarbCalories;

            // Upewniamy się, że pozostałe kalorie są dodatnie
            if (remainingCalories < 0)
            {
                // Jeśli suma białka i węglowodanów przekracza docelową kaloryczność,
                // korygujemy wartości węglowodanów, aby było miejsce na minimalne tłuszcze
                int minimumFatCalories = (int)(weightInLbs * 0.3 * 9); // Minimum 0.3g tłuszczu na funt
                int availableForCarbsAndProtein = selectedCalories - minimumFatCalories;

                // Próba zachowania proporcji białka i węglowodanów
                double totalRatio = ProteinRatio + CarbRatio;
                double proteinProportion = ProteinRatio / totalRatio;

                // Nowe wartości dla białka
                int newProteinCalories = (int)(availableForCarbsAndProtein * proteinProportion);
                ProteinGrams = Math.Round(newProteinCalories / 4.0, 1);
                ProteinCalories = (int)(ProteinGrams * 4);

                // Nowe wartości dla węglowodanów
                CarbCalories = availableForCarbsAndProtein - ProteinCalories;
                CarbGrams = Math.Round(CarbCalories / 4.0, 1);

                remainingCalories = selectedCalories - ProteinCalories - CarbCalories;
            }

            // Obliczanie tłuszczów
            FatGrams = Math.Round(remainingCalories / 9.0, 1); // 9 kalorii na gram tłuszczu
            FatCalories = (int)(FatGrams * 9);

            // Obliczanie procentowych udziałów
            ProteinPercentage = Math.Round((double)ProteinCalories / selectedCalories * 100, 2);
            CarbPercentage = Math.Round((double)CarbCalories / selectedCalories * 100, 2);
            FatPercentage = 100 - ProteinPercentage - CarbPercentage;

            // Korekta, aby upewnić się, że suma wynosi dokładnie 100%
            if (Math.Abs(ProteinPercentage + CarbPercentage + FatPercentage - 100) > 0.1)
            {
                FatPercentage = Math.Round(100 - ProteinPercentage - CarbPercentage, 2);
            }
        }

        // Walidacja wieku
        private void ValidateAge()
        {
            ClearErrors(nameof(Age));
            if (Age <= 0)
                AddError(nameof(Age), "Wiek musi być większy od 0");
            else if (Age > 120)
                AddError(nameof(Age), "Wiek musi być mniejszy niż 120");
        }

        // Walidacja wagi
        private void ValidateWeight()
        {
            ClearErrors(nameof(Weight));
            if (Weight <= 0)
                AddError(nameof(Weight), "Waga musi być większa od 0");
            else
            {
                double min = IsKg ? 30 : 66;
                double max = IsKg ? 300 : 660;
                if (Weight < min)
                    AddError(nameof(Weight), $"Waga musi wynosić przynajmniej {min} {(IsKg ? "kg" : "lbs")}");
                else if (Weight > max)
                    AddError(nameof(Weight), $"Waga musi być mniejsza niż {max} {(IsKg ? "kg" : "lbs")}");
            }
        }

        // Walidacja wzrostu
        private void ValidateHeight()
        {
            ClearErrors(nameof(Height));

            double min = IsCm ? 100 : 39; // 100 cm lub 39 cali
            double max = IsCm ? 250 : 98; // 250 cm lub 98 cali

            if (Height <= 0)
            {
                AddError(nameof(Height), "Wzrost musi być większy od 0");
            }
            else if (Height < min)
            {
                AddError(nameof(Height), $"Wzrost musi wynosić przynajmniej {min} {(IsCm ? "cm" : "in")}");
            }
            else if (Height > max)
            {
                AddError(nameof(Height), $"Wzrost musi być mniejszy niż {max} {(IsCm ? "cm" : "in")}");
            }

            // Odświeżenie UI po zmianie jednostki
            OnPropertyChanged(nameof(IsCm));
        }

        // Implementacja INotifyDataErrorInfo
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
                return null;
            return _errors[propertyName];
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}