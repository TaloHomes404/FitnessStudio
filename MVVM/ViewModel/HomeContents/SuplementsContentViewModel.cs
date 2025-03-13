using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using FitnessStudio.MVVM.Model;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public class SuplementsContentViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Supplement> _allSupplements;
        private ObservableCollection<Supplement> _searchResults;
        private ObservableCollection<Supplement> _userSupplements;
        private string _searchText;
        private Supplement _selectedSupplement;

        //Right Sidebar items declaration list for supplements screen
        public ObservableCollection<HomeViewModel.RightSidebarItem> RightSidebarItems { get; }
        = new ObservableCollection<HomeViewModel.RightSidebarItem>();

        // Właściwości dla tab kontrolki dni tygodnia
        public bool IsMonday { get; set; } = true;
        public bool IsTuesday { get; set; } = false;
        public bool IsWednesday { get; set; } = false;
        public bool IsThursday { get; set; } = false;
        public bool IsFriday { get; set; } = false;
        public bool IsSaturday { get; set; } = false;
        public bool IsSunday { get; set; } = false;

        // Dla tabitems w głównym ekranie
        private string _selectedDay;
        public string SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    OnPropertyChanged(nameof(SelectedDay));
                    FilterSupplementsByDay();
                }
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterSupplements();
                }
            }
        }

        public ObservableCollection<Supplement> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        public ObservableCollection<Supplement> UserSupplements
        {
            get { return _userSupplements; }
            set
            {
                _userSupplements = value;
                OnPropertyChanged(nameof(UserSupplements));
            }
        }

        public Supplement SelectedSupplement
        {
            get { return _selectedSupplement; }
            set
            {
                _selectedSupplement = value;
                OnPropertyChanged(nameof(SelectedSupplement));
            }
        }

        public SuplementsContentViewModel()
        {
            InitializeSupplements();

            // Setting up right sidebar for suplements contentView
            RightSidebarItems.Add(new HomeViewModel.RightSidebarItem
            {
                Title = "Suplementacja kluczem do zdrowia?",
                Description = "test test test test test test test",
                ImagePath = new Uri("/FitnessStudio;component/Resources/SidebarImages/supplements_border_img.jpg", UriKind.Relative)
            });

            _searchResults = new ObservableCollection<Supplement>(_allSupplements);
            _userSupplements = new ObservableCollection<Supplement>();

            // Dodajemy przykładową witaminę D jako już istniejącą
            UserSupplements.Add(new Supplement("Vitamin D", "Once a day", "D", "1 pill daily"));
        }

        private void InitializeSupplements()
        {
            _allSupplements = new ObservableCollection<Supplement>
            {
                new Supplement("Vitamin D3", "Supports immune system", "D", "1 pill daily"),
                new Supplement("Vitamin C", "Antioxidant support", "C", "1-2 pills daily"),
                new Supplement("Magnesium", "Muscle and nerve function", "Mg", "1 pill with meal"),
                new Supplement("Zinc", "Immune system support", "Zn", "1 pill daily"),
                new Supplement("Creatine", "Supports muscle growth", "Cr", "5g daily"),
                new Supplement("Omega-3", "Heart and brain health", "Ω3", "1 capsule daily"),
                new Supplement("Vitamin B12", "Energy metabolism", "B12", "1 pill daily"),
                new Supplement("Iron", "Blood health support", "Fe", "1 pill daily"),
                new Supplement("Calcium", "Bone health", "Ca", "1-2 pills daily"),
                new Supplement("Potassium", "Electrolyte balance", "K", "As directed"),
                new Supplement("Aspirin", "Pain relief", "ASA", "As needed"),
                new Supplement("Protein powder", "Muscle recovery", "PRO", "1 scoop daily"),
                new Supplement("Vitamin E", "Antioxidant properties", "E", "1 capsule daily"),
                new Supplement("Vitamin A", "Vision and immune health", "A", "1 pill daily"),
                new Supplement("Collagen", "Joint and skin health", "COL", "1 scoop daily")
            };
        }

        private void FilterSupplements()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                SearchResults = new ObservableCollection<Supplement>(_allSupplements);
                return;
            }

            var filteredList = _allSupplements
                .Where(s => s.Name.ToLower().Contains(SearchText.ToLower()))
                .ToList();

            SearchResults = new ObservableCollection<Supplement>(filteredList);
        }

        private void FilterSupplementsByDay()
        {
            if (string.IsNullOrEmpty(SelectedDay))
            {
                UserSupplements = new ObservableCollection<Supplement>(_allSupplements);
                return;
            }

            var filteredList = _userSupplements
                .Where(s => s.IsEveryday ||
                           (SelectedDay == "Mon" && s.IsMonday) ||
                           (SelectedDay == "Tue" && s.IsTuesday) ||
                           (SelectedDay == "Wed" && s.IsWednesday) ||
                           (SelectedDay == "Thu" && s.IsThursday) ||
                           (SelectedDay == "Fri" && s.IsFriday) ||
                           (SelectedDay == "Sat" && s.IsSaturday) ||
                           (SelectedDay == "Sun" && s.IsSunday))
                .ToList();

            UserSupplements = new ObservableCollection<Supplement>(filteredList);
        }


        public void AddSupplement(Supplement supplement)
        {
            // Sprawdzamy czy suplement jest już na liście użytkownika
            if (!UserSupplements.Any(s => s.Name == supplement.Name))
            {
                UserSupplements.Add(supplement);
            }
            else
            {
                MessageBox.Show("This supplement is already in your list!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void DeleteSupplement(Supplement supplement)
        {
            if (UserSupplements.Contains(supplement))
            {
                UserSupplements.Remove(supplement);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}