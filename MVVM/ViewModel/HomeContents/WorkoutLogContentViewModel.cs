using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessStudio.MVVM.Model;

namespace FitnessStudio.MVVM.ViewModel.HomeContents
{
    public class WorkoutLogContentViewModel : ObservableObject
    {
        #region Właściwości

        // Kontroluje widoczność okna dialogowego
        private bool _isAddWorkoutDialogOpen;
        public bool IsAddWorkoutDialogOpen
        {
            get { return _isAddWorkoutDialogOpen; }
            set { _isAddWorkoutDialogOpen = value; OnPropertyChanged(); }
        }

        // Przechowuje nowy lub edytowany trening
        private WorkoutSession _newWorkout;
        public WorkoutSession NewWorkout
        {
            get { return _newWorkout; }
            set { _newWorkout = value; OnPropertyChanged(); }
        }

        // Kolekcja sesji treningowych (historia treningów)
        public ObservableCollection<WorkoutSession> WorkoutSessions { get; set; }

        // Lista dostępnych ćwiczeń do wyboru
        public ObservableCollection<string>? AvailableExercises { get; set; }

        //Kolekcja itemów dla prawego sidebara
        public ObservableCollection<HomeViewModel.RightSidebarItem> RightSidebarItems { get; }
        = new ObservableCollection<HomeViewModel.RightSidebarItem>();

        #endregion

        #region Polecenia (Commands)

        // Polecenie otwierające okno dialogowe dodawania treningu
        public ICommand? OpenAddWorkoutCommand { get; set; }

        // Polecenie anulujące dodawanie treningu
        public ICommand? CancelAddWorkoutCommand { get; set; }

        // Polecenie zapisujące nowy trening
        public ICommand? SaveWorkoutCommand { get; set; }

        // Polecenie dodające nowe ćwiczenie do listy
        public ICommand? AddExerciseCommand { get; set; }

        // Polecenie usuwające ćwiczenie z listy
        public ICommand? RemoveExerciseCommand { get; set; }

        // Polecenie edytujące istniejący trening
        public ICommand? EditWorkoutCommand { get; set; }

        // Polecenie usuwające istniejący trening
        public ICommand? DeleteWorkoutCommand { get; set; }

        #endregion

        // Konstruktor
        public WorkoutLogContentViewModel()
        {
            // Inicjalizacja kolekcji
            WorkoutSessions = new ObservableCollection<WorkoutSession>();
            InitializeExerciseList();
            // Inicjalizacja zawartości sidebara
            RightSidebarItems.Add(new HomeViewModel.RightSidebarItem
            {
                Title = "workout log",
                Description = "Spróbuj metody 4-7-8: Wdychaj 4 sekundy, wstrzymaj 7, wydychaj 8",
                ImagePath = new Uri("/FitnessStudio;component/Resources/supplements_border_img.jpg", UriKind.Relative)
            });

            RightSidebarItems.Add(new HomeViewModel.RightSidebarItem
            {
                Title = "workout log",
                Description = "Słuchaj dźwięków natury przez minimum 15 minut dziennie",
                ImagePath = new Uri("/FitnessStudio;component/Resources/supplements_border_img.jpg", UriKind.Relative)
            });

            // Dodanie przykładowych danych
            AddSampleWorkout();

            // Inicjalizacja poleceń
            SetupCommands();

        }

        #region Metody pomocnicze

        // Inicjalizacja listy dostępnych ćwiczeń
        private void InitializeExerciseList()
        {
            AvailableExercises = new ObservableCollection<string>
            {
                // Podstawowe ćwiczenia siłowe
                "Bench Press", "Squat", "Deadlift", "Pull-up", "Push-up",
                "Barbell Row", "Overhead Press", "Bicep Curl", "Tricep Extension",
                
                // Ćwiczenia na maszynie
                "Lat Pulldown", "Leg Press", "Leg Extension", "Leg Curl",
                "Chest Fly", "Lateral Raise", "Front Raise", "Face Pull", 
                
                // Inne ćwiczenia
                "Calf Raise", "Plank", "Crunch", "Russian Twist"
            };
        }

        // Dodanie przykładowych danych do testów
        private void AddSampleWorkout()
        {
            var sampleWorkout = new WorkoutSession
            {
                Id = 1,
                Name = "Upper Body Workout",
                Date = DateTime.Now.AddDays(-2),
                Exercises = new ObservableCollection<Exercise>
                {
                    new Exercise { Name = "Bench Press", Sets = 3, Reps = "8-10", Weight = 80, RPE = 8 },
                    new Exercise { Name = "Pull-up", Sets = 4, Reps = "6-8", Weight = 0, RPE = 9 },
                    new Exercise { Name = "Overhead Press", Sets = 3, Reps = "8-10", Weight = 50, RPE = 7 }
                },
                Notes = "Felt strong today, might increase bench press weight next session."
            };

            WorkoutSessions.Add(sampleWorkout);
        }

        // Konfiguracja wszystkich poleceń (Commands)
        private void SetupCommands()
        {
            // Otwiera okno dodawania z nowym pustym treningiem
            OpenAddWorkoutCommand = new RelayCommand<object>(_ =>
            {
                NewWorkout = new WorkoutSession
                {
                    Date = DateTime.Now,
                    Exercises = new ObservableCollection<Exercise>()
                };
                IsAddWorkoutDialogOpen = true;
            });

            // Zamyka okno dialogowe bez zapisywania
            CancelAddWorkoutCommand = new RelayCommand<object>(_ =>
            {
                IsAddWorkoutDialogOpen = false;
            });

            // Zapisuje trening i dodaje go do historii
            SaveWorkoutCommand = new RelayCommand<object>(_ =>
            {
                // Sprawdzenie czy trening ma nazwę i co najmniej jedno ćwiczenie
                if (NewWorkout != null && !string.IsNullOrWhiteSpace(NewWorkout.Name) && NewWorkout.Exercises.Any())
                {
                    // Jeśli to nowy trening, ustaw mu ID
                    if (NewWorkout.Id == 0)
                    {
                        if (WorkoutSessions.Any())
                            NewWorkout.Id = WorkoutSessions.Max(ws => ws.Id) + 1;
                        else
                            NewWorkout.Id = 1;

                        // Dodaj na początek listy
                        WorkoutSessions.Insert(0, NewWorkout);
                    }
                    else
                    {
                        // Jeśli to edycja, znajdź i zastąp istniejący trening
                        var existingIndex = WorkoutSessions.ToList().FindIndex(ws => ws.Id == NewWorkout.Id);
                        if (existingIndex >= 0)
                        {
                            WorkoutSessions[existingIndex] = NewWorkout;
                        }
                    }

                    // Zamknij okno dialogowe
                    IsAddWorkoutDialogOpen = false;
                }
            });

            // Dodaje nowe ćwiczenie do treningu
            AddExerciseCommand = new RelayCommand<Exercise>(_ =>
            {
                if (NewWorkout != null)
                {
                    // Dodaj domyślne ćwiczenie
                    NewWorkout.Exercises.Add(new Exercise
                    {
                        Name = AvailableExercises.FirstOrDefault()!,
                        Sets = 3,
                        Reps = "8-12",
                        Weight = 0,
                        RPE = 7
                    });
                }
            });

            // Usuwa ćwiczenie z treningu
            RemoveExerciseCommand = new RelayCommand<Exercise>(exercise =>
            {
                if (NewWorkout != null && exercise is Exercise exerciseToRemove)
                {
                    NewWorkout.Exercises.Remove(exerciseToRemove);
                }
            });

            // Otwiera okno edycji istniejącego treningu
            EditWorkoutCommand = new RelayCommand<WorkoutSession>(workout =>
            {
                if (workout is WorkoutSession sessionToEdit)
                {
                    // Klonuj trening, aby nie modyfikować oryginału podczas edycji
                    NewWorkout = sessionToEdit.Clone();
                    IsAddWorkoutDialogOpen = true;
                }
            });

            // Usuwa trening z historii
            DeleteWorkoutCommand = new RelayCommand<WorkoutSession>(workout =>
            {
                if (workout is WorkoutSession sessionToDelete)
                {
                    WorkoutSessions.Remove(sessionToDelete);
                }
            });
        }

        #endregion
    }
}