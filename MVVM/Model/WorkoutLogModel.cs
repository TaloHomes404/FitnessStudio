using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;


namespace FitnessStudio.MVVM.Model
{
    public class WorkoutSession : ObservableObject
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Exercise> _exercises;
        public ObservableCollection<Exercise> Exercises
        {
            get { return _exercises; }
            set { _exercises = value; OnPropertyChanged(); }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNotes));
            }
        }

        public bool HasNotes => !string.IsNullOrWhiteSpace(Notes);

        public WorkoutSession Clone()
        {
            var clone = new WorkoutSession
            {
                Id = this.Id,
                Name = this.Name,
                Date = this.Date,
                Notes = this.Notes,
                Exercises = new ObservableCollection<Exercise>()
            };

            foreach (var exercise in this.Exercises)
            {
                clone.Exercises.Add(new Exercise
                {
                    Name = exercise.Name,
                    Sets = exercise.Sets,
                    Reps = exercise.Reps,
                    Weight = exercise.Weight,
                    RPE = exercise.RPE
                });
            }

            return clone;
        }
    }

    public class Exercise : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private int _sets;
        public int Sets
        {
            get { return _sets; }
            set { _sets = value; OnPropertyChanged(); }
        }

        private string _reps;
        public string Reps
        {
            get { return _reps; }
            set { _reps = value; OnPropertyChanged(); }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged(); }
        }

        private int _rpe;
        public int RPE
        {
            get { return _rpe; }
            set { _rpe = value; OnPropertyChanged(); }
        }
    }
}