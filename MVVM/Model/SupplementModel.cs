using System.ComponentModel;

namespace FitnessStudio.MVVM.Model
{
    public class Supplement : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private string _letterCode;
        private string _dosage;
        private bool _isMonday;
        private bool _isTuesday;
        private bool _isWednesday;
        private bool _isThursday;
        private bool _isFriday;
        private bool _isSaturday;
        private bool _isSunday;

        public bool IsEveryday { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public string LetterCode
        {
            get { return _letterCode; }
            set
            {
                if (_letterCode != value)
                {
                    _letterCode = value;
                    OnPropertyChanged(nameof(LetterCode));
                }
            }
        }

        public string Dosage
        {
            get { return _dosage; }
            set
            {
                if (_dosage != value)
                {
                    _dosage = value;
                    OnPropertyChanged(nameof(Dosage));
                }
            }
        }

        public bool IsMonday
        {
            get { return _isMonday; }
            set
            {
                if (_isMonday != value)
                {
                    _isMonday = value;
                    OnPropertyChanged(nameof(IsMonday));
                }
            }
        }

        public bool IsTuesday
        {
            get { return _isTuesday; }
            set
            {
                if (_isTuesday != value)
                {
                    _isTuesday = value;
                    OnPropertyChanged(nameof(IsTuesday));
                }
            }
        }

        public bool IsWednesday
        {
            get { return _isWednesday; }
            set
            {
                if (_isWednesday != value)
                {
                    _isWednesday = value;
                    OnPropertyChanged(nameof(IsWednesday));
                }
            }
        }

        public bool IsThursday
        {
            get { return _isThursday; }
            set
            {
                if (_isThursday != value)
                {
                    _isThursday = value;
                    OnPropertyChanged(nameof(IsThursday));
                }
            }
        }

        public bool IsFriday
        {
            get { return _isFriday; }
            set
            {
                if (_isFriday != value)
                {
                    _isFriday = value;
                    OnPropertyChanged(nameof(IsFriday));
                }
            }
        }

        public bool IsSaturday
        {
            get { return _isSaturday; }
            set
            {
                if (_isSaturday != value)
                {
                    _isSaturday = value;
                    OnPropertyChanged(nameof(IsSaturday));
                }
            }
        }

        public bool IsSunday
        {
            get { return _isSunday; }
            set
            {
                if (_isSunday != value)
                {
                    _isSunday = value;
                    OnPropertyChanged(nameof(IsSunday));
                }
            }
        }

        public Supplement(string name, string description, string letterCode, string dosage)
        {
            Name = name;
            Description = description;
            LetterCode = letterCode;
            Dosage = dosage;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}