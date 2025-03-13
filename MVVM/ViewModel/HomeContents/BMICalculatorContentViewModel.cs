using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public class BMICalculatorContentViewModel : ObservableObject
    {
        private string _age;
        private string _weight;
        private string _height;
        private bool _isMale = true;
        private double _bmiResult;
        private string _healthStatus;
        private string _healthDescription;
        private double _bmiIndicatorPosition;
        private Brush _healthStatusBrush;
        private object _selectedWeightUnit;
        private object _selectedHeightUnit;
        private Brush _ageValidationBrush = Brushes.LightGray;
        private Brush _weightValidationBrush = Brushes.LightGray;
        private Brush _heightValidationBrush = Brushes.LightGray;

        public BMICalculatorContentViewModel()
        {
            CalculateBMICommand = new RelayCommand(CalculateBMI, CanCalculateBMI);

            // Inicjalizacja domyślnych jednostek
            SelectedWeightUnit = "kg";
            SelectedHeightUnit = "cm";
        }

        public string Age
        {
            get => _age;
            set
            {
                if (SetProperty(ref _age, value))
                {
                    ValidateAge();
                    if (CalculateBMICommand is RelayCommand relayCommand)
                    {
                        relayCommand.NotifyCanExecuteChanged();
                    }
                }
            }
        }

        public string Weight
        {
            get => _weight;
            set
            {
                if (SetProperty(ref _weight, value))
                {
                    ValidateWeight();
                    if (CalculateBMICommand is RelayCommand relayCommand)
                    {
                        relayCommand.NotifyCanExecuteChanged();
                    }
                }
            }
        }

        public string Height
        {
            get => _height;
            set
            {
                if (SetProperty(ref _height, value))
                {
                    ValidateHeight();
                    if (CalculateBMICommand is RelayCommand relayCommand)
                    {
                        relayCommand.NotifyCanExecuteChanged();
                    }
                }
            }
        }

        public bool IsMale
        {
            get => _isMale;
            set => SetProperty(ref _isMale, value);
        }

        public double BMIResult
        {
            get => _bmiResult;
            set => SetProperty(ref _bmiResult, value);
        }

        public string HealthStatus
        {
            get => _healthStatus;
            set => SetProperty(ref _healthStatus, value);
        }

        public string HealthDescription
        {
            get => _healthDescription;
            set => SetProperty(ref _healthDescription, value);
        }

        public double BMIIndicatorPosition
        {
            get => _bmiIndicatorPosition;
            set => SetProperty(ref _bmiIndicatorPosition, value);
        }

        public Brush HealthStatusBrush
        {
            get => _healthStatusBrush;
            set => SetProperty(ref _healthStatusBrush, value);
        }

        public object SelectedWeightUnit
        {
            get => _selectedWeightUnit;
            set => SetProperty(ref _selectedWeightUnit, value);
        }

        public object SelectedHeightUnit
        {
            get => _selectedHeightUnit;
            set => SetProperty(ref _selectedHeightUnit, value);
        }

        public Brush AgeValidationBrush
        {
            get => _ageValidationBrush;
            set => SetProperty(ref _ageValidationBrush, value);
        }

        public Brush WeightValidationBrush
        {
            get => _weightValidationBrush;
            set => SetProperty(ref _weightValidationBrush, value);
        }

        public Brush HeightValidationBrush
        {
            get => _heightValidationBrush;
            set => SetProperty(ref _heightValidationBrush, value);
        }

        public ICommand CalculateBMICommand { get; }

        private void ValidateAge()
        {
            if (string.IsNullOrEmpty(Age))
            {
                AgeValidationBrush = Brushes.LightGray;
                return;
            }

            if (!int.TryParse(Age, out int ageValue) || ageValue <= 0 || ageValue > 120)
            {
                AgeValidationBrush = Brushes.Red;
            }
            else
            {
                AgeValidationBrush = Brushes.LightGray;
            }
        }

        private void ValidateWeight()
        {
            if (string.IsNullOrEmpty(Weight))
            {
                WeightValidationBrush = Brushes.LightGray;
                return;
            }

            if (!double.TryParse(Weight, out double weightValue) || weightValue <= 0 || weightValue > 500)
            {
                WeightValidationBrush = Brushes.Red;
            }
            else
            {
                WeightValidationBrush = Brushes.LightGray;
            }
        }

        private void ValidateHeight()
        {
            if (string.IsNullOrEmpty(Height))
            {
                HeightValidationBrush = Brushes.LightGray;
                return;
            }

            if (!double.TryParse(Height, out double heightValue) || heightValue <= 0)
            {
                HeightValidationBrush = Brushes.Red;
                return;
            }

            // Dodatkowa walidacja dla cm i inches
            var heightUnit = SelectedHeightUnit?.ToString();
            if (heightUnit == "cm" && (heightValue < 50 || heightValue > 250))
            {
                HeightValidationBrush = Brushes.Red;
            }
            else if (heightUnit == "in" && (heightValue < 20 || heightValue > 100))
            {
                HeightValidationBrush = Brushes.Red;
            }
            else
            {
                HeightValidationBrush = Brushes.LightGray;
            }
        }

        private bool CanCalculateBMI()
        {
            // Sprawdzenie, czy wszystkie pola są wypełnione i poprawne
            return !string.IsNullOrEmpty(Weight) && !string.IsNullOrEmpty(Height) &&
                   SelectedWeightUnit != null && SelectedHeightUnit != null &&
                   WeightValidationBrush != Brushes.Red && HeightValidationBrush != Brushes.Red;
        }

        private void CalculateBMI()
        {
            try
            {
                // Get values
                double weight = double.Parse(Weight);
                double height = double.Parse(Height);
                int age = string.IsNullOrEmpty(Age) ? 30 : int.Parse(Age); // Default age if not provided

                // Convert units
                var weightUnit = SelectedWeightUnit?.ToString();
                var heightUnit = SelectedHeightUnit?.ToString();

                // Convert to standard units (kg and m)
                double weightInKg = weightUnit == "lb" ? weight * 0.453592 : weight;
                double heightInM = heightUnit == "in" ? height * 0.0254 : height / 100;

                // Calculate BMI based on gender
                double bmi;
                if (IsMale)
                {
                    // Standard BMI formula for men
                    bmi = weightInKg / (heightInM * heightInM);
                }
                else
                {
                    // BMI formula adjusted for women               
                    bmi = (weightInKg / (heightInM * heightInM)) * 0.95;
                }

                // Round to 1 decimal place
                BMIResult = Math.Round(bmi, 1);

                // Set BMI indicator position
                SetBMIIndicatorPosition(BMIResult);

                // Update health status
                UpdateHealthStatus(BMIResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating BMI: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetBMIIndicatorPosition(double bmi)
        {


            // Calculate position based on BMI value
            // We need to map the BMI value to a position on the scale
            // The scale is divided into 4 segments (0-18.5, 18.5-25, 25-30, 30-40+)

            // First, clamp BMI to reasonable display range
            double displayBmi = Math.Max(0, Math.Min(bmi, 40));

            // Get the total width of the scale (assume 300px from the layout)
            double scaleWidth = 300;

            // Calculate the position as a percentage of the scale width
            double percentage = 0;
            if (displayBmi < 18.5)
            {
                // Underweight segment (0-18.5)
                percentage = (displayBmi / 18.5) * 25; // 25% of scale width
            }
            else if (displayBmi < 25)
            {
                // Normal segment (18.5-25)
                percentage = 25 + ((displayBmi - 18.5) / 6.5) * 25; // 25% of scale width
            }
            else if (displayBmi < 30)
            {
                // Overweight segment (25-30)
                percentage = 50 + ((displayBmi - 25) / 5) * 25; // 25% of scale width
            }
            else
            {
                // Obese segment (30-40+)
                percentage = 75 + ((displayBmi - 30) / 10) * 25; // 25% of scale width
            }

            // Convert percentage to pixel position
            double position = (percentage / 100) * scaleWidth;

            // Center the triangle on the scale
            BMIIndicatorPosition = position - 150;
        }

        private void UpdateHealthStatus(double bmi)
        {
            if (bmi < 18.5)
            {
                HealthStatus = "Underweight";
                HealthDescription = "You are underweight. Consider consulting with a healthcare professional about healthy weight gain strategies.";
                HealthStatusBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#90CAF9"));
            }
            else if (bmi < 25)
            {
                HealthStatus = "Normal Weight";
                HealthDescription = "Your BMI is within the healthy range. Keep up your healthy habits!";
                HealthStatusBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
            }
            else if (bmi < 30)
            {
                HealthStatus = "Overweight";
                HealthDescription = "You are overweight. Consider adopting a healthier lifestyle with balanced diet and regular exercise.";
                HealthStatusBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107"));
            }
            else
            {
                HealthStatus = "Obese";
                HealthDescription = "Your BMI indicates obesity. It's recommended to consult with a healthcare professional for personalized advice.";
                HealthStatusBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5722"));
            }
        }
    }
}