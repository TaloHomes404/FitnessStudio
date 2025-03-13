using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public class StepsCounterContentViewModel : INotifyPropertyChanged
    {
        private const double METSlowWalk = 2.0, METAverageWalk = 3.0, METFastWalk = 4.0;

        // Basic Properties
        private double _weight = 70;
        public double Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged();
                    WeightHasError = !IsWeightValid();
                }
            }
        }

        private double _height = 170;
        public double Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged();
                    HeightHasError = !IsHeightValid();
                }
            }
        }

        private int _steps = 10000;
        public int Steps
        {
            get => _steps;
            set
            {
                if (_steps != value)
                {
                    _steps = value;
                    OnPropertyChanged();
                    StepsHasError = !IsStepsValid();
                }
            }
        }

        private double _caloriesBurned;
        public double CaloriesBurned
        {
            get => _caloriesBurned;
            set
            {
                if (_caloriesBurned != value)
                {
                    _caloriesBurned = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _caloriesPerStep;
        public double CaloriesPerStep
        {
            get => _caloriesPerStep;
            set
            {
                if (_caloriesPerStep != value)
                {
                    _caloriesPerStep = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weightUnit = "kg";
        public string WeightUnit
        {
            get => _weightUnit;
            set
            {
                if (_weightUnit != value)
                {
                    _weightUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _heightUnit = "cm";
        public string HeightUnit
        {
            get => _heightUnit;
            set
            {
                if (_heightUnit != value)
                {
                    _heightUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        // Speed Properties
        private SpeedLevel _selectedSpeed = SpeedLevel.Average;
        public SpeedLevel SelectedSpeed
        {
            get => _selectedSpeed;
            set
            {
                if (_selectedSpeed != value)
                {
                    _selectedSpeed = value;
                    OnPropertyChanged();
                    UpdateRadioButtons();
                    Calculate();
                }
            }
        }

        private bool _isSlowSelected;
        public bool IsSlowSelected
        {
            get => _isSlowSelected;
            set
            {
                if (_isSlowSelected != value && value)
                {
                    _isSlowSelected = value;
                    _isAverageSelected = false;
                    _isFastSelected = false;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsAverageSelected));
                    OnPropertyChanged(nameof(IsFastSelected));
                    SelectedSpeed = SpeedLevel.Slow;
                }
            }
        }

        private bool _isAverageSelected = true;
        public bool IsAverageSelected
        {
            get => _isAverageSelected;
            set
            {
                if (_isAverageSelected != value && value)
                {
                    _isAverageSelected = value;
                    _isSlowSelected = false;
                    _isFastSelected = false;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsSlowSelected));
                    OnPropertyChanged(nameof(IsFastSelected));
                    SelectedSpeed = SpeedLevel.Average;
                }
            }
        }

        private bool _isFastSelected;
        public bool IsFastSelected
        {
            get => _isFastSelected;
            set
            {
                if (_isFastSelected != value && value)
                {
                    _isFastSelected = value;
                    _isSlowSelected = false;
                    _isAverageSelected = false;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsSlowSelected));
                    OnPropertyChanged(nameof(IsAverageSelected));
                    SelectedSpeed = SpeedLevel.Fast;
                }
            }
        }

        // Validation Properties
        private bool _weightHasError;
        public bool WeightHasError
        {
            get => _weightHasError;
            set
            {
                if (_weightHasError != value)
                {
                    _weightHasError = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _heightHasError;
        public bool HeightHasError
        {
            get => _heightHasError;
            set
            {
                if (_heightHasError != value)
                {
                    _heightHasError = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _stepsHasError;
        public bool StepsHasError
        {
            get => _stepsHasError;
            set
            {
                if (_stepsHasError != value)
                {
                    _stepsHasError = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commands
        public ICommand CalculateCommand { get; }

        public StepsCounterContentViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
            UpdateRadioButtons();
        }

        private void UpdateRadioButtons()
        {
            _isSlowSelected = _selectedSpeed == SpeedLevel.Slow;
            _isAverageSelected = _selectedSpeed == SpeedLevel.Average;
            _isFastSelected = _selectedSpeed == SpeedLevel.Fast;

            OnPropertyChanged(nameof(IsSlowSelected));
            OnPropertyChanged(nameof(IsAverageSelected));
            OnPropertyChanged(nameof(IsFastSelected));
        }

        private bool IsWeightValid() => _weight >= 20 && _weight <= 300;
        private bool IsHeightValid() => _height >= 50 && _height <= 250;
        private bool IsStepsValid() => _steps >= 1 && _steps <= 100000;

        public void Calculate()
        {
            // Validate inputs
            WeightHasError = !IsWeightValid();
            HeightHasError = !IsHeightValid();
            StepsHasError = !IsStepsValid();

            if (WeightHasError || HeightHasError || StepsHasError)
                return;

            double weightInKg = _weightUnit == "kg" ? _weight : _weight * 0.453592;
            double heightInCm = _heightUnit == "cm" ? _height : _height * 2.54;
            double strideLength = heightInCm * 0.42 / 100; // w metrach
            double distanceKm = (_steps * strideLength) / 1000;

            // Użyj aktualnie wybranej prędkości
            double speedKmH = GetSpeedKmH(_selectedSpeed);
            double timeHours = distanceKm / speedKmH;

            // Użyj aktualnie wybranej wartości MET
            double metValue = GetMETValue(_selectedSpeed);
            CaloriesBurned = Math.Round(metValue * weightInKg * timeHours, 1);
            CaloriesPerStep = _steps > 0 ? Math.Round(CaloriesBurned / _steps, 4) : 0;

            OnPropertyChanged(nameof(CaloriesBurned));
            OnPropertyChanged(nameof(CaloriesPerStep));
        }

        private double GetMETValue(SpeedLevel speed) => speed switch
        {
            SpeedLevel.Slow => METSlowWalk,
            SpeedLevel.Average => METAverageWalk,
            SpeedLevel.Fast => METFastWalk,
            _ => METAverageWalk
        };

        private double GetSpeedKmH(SpeedLevel speed) => speed switch
        {
            SpeedLevel.Slow => 3.2,
            SpeedLevel.Average => 4.8,
            SpeedLevel.Fast => 6.4,
            _ => 4.8
        };

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum SpeedLevel
    {
        Slow,
        Average,
        Fast
    }
}