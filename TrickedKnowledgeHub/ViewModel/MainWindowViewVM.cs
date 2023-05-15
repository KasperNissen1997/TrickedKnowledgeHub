using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.Command.MainWindowCommand;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel.Domain;
using TrickedKnowledgeHub.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Navigation;

namespace TrickedKnowledgeHub.ViewModel
{
    public class MainWindowViewVM : INotifyPropertyChanged
    {
        public EmployeeVM ActiveUser { get; set; }

        public CreateExerciseWindowViewVM CreateExerciseWindowVM { get; set; }

        private ObservableCollection<ExerciseVM> _exerciseVM;
        public ObservableCollection<ExerciseVM> ExerciseVMs
        {
            get
            {
                return _exerciseVM;
            }
            set
            {
                _exerciseVM = value;
                OnPropertyChanged(nameof(ExerciseVMs));
                OnPropertyChanged(nameof(VisibleExercises));
            }
        }

        /// <summary>
        /// This is the collection of <see cref="ExerciseVM"/>s that the user can see in the UI. <br/>
        /// It is essentially a filtered copy of <see cref="ExerciseVMs"/>, using <see cref="FilterExercises(IEnumerable{ExerciseVM})"/>.
        /// </summary>
        public ObservableCollection<ExerciseVM> VisibleExercises
        {
            get
            {
                return FilterExercises(ExerciseVMs);
            }
        }

        private ExerciseVM _selectedExerciseVM;
        public ExerciseVM SelectedExerciseVM
        {
            get
            {
                return _selectedExerciseVM;
            }

            set
            {

                if (value != null)
                {
                    _selectedExerciseVM = value;
                    OnPropertyChanged(nameof(SelectedExerciseVM));

                    ExercisePageVM.SelectedExercise = _selectedExerciseVM;
                }
            }
        }
        private ExercisePageVM _exercisePageVM;

        public ExercisePageVM ExercisePageVM
        {
            get
            {
                if (_exercisePageVM == null)
                {
                    _exercisePageVM= new ExercisePageVM();
                }
                return _exercisePageVM;
            }
            set
            {
                _exercisePageVM = value;
                OnPropertyChanged(nameof(ExercisePageVM));
            }
        }

        private ObservableCollection<GameVM> _availableGames;
        /// <summary>
        /// The games that the system can fetch from the database. Whichever game is selected here is used when filtering the exercises, see <see cref="SelectedGameFilter"/>.
        /// </summary>
        public ObservableCollection<GameVM> AvailableGames
        {
            get
            {
                return _availableGames;
            }

            set
            {
                _availableGames = value;
                OnPropertyChanged(nameof(AvailableGames));
            }
        }
        private GameVM? _selectedGameFilter;
        /// <summary>
        /// The selected game, that is being used when filtering exercises.
        /// </summary>
        public GameVM? SelectedGameFilter
        {
            get
            {
                return _selectedGameFilter;
            }

            set
            {
                _selectedGameFilter = value;
                OnPropertyChanged(nameof(SelectedGameFilter));
                OnPropertyChanged(nameof(VisibleExercises));

                if (SelectedLearningObjectiveFilter != null && !SelectedGameFilter.Objectives.Contains(SelectedLearningObjectiveFilter))
                    SelectedLearningObjectiveFilter = null;

                AvailableLearningObjectives = SelectedGameFilter.Objectives;
            }
        }
        private ObservableCollection<LearningObjectiveVM> _availableLearningObjectives;
        public ObservableCollection<LearningObjectiveVM> AvailableLearningObjectives
        {
            get
            {
                return _availableLearningObjectives;
            }

            set
            {
                _availableLearningObjectives = value;
                OnPropertyChanged(nameof(AvailableLearningObjectives));
            }
        }
        private LearningObjectiveVM _selectedLearningObjectiveFilter;
        public LearningObjectiveVM SelectedLearningObjectiveFilter
        {
            get
            {
                return _selectedLearningObjectiveFilter;
            }

            set
            {
                _selectedLearningObjectiveFilter = value;
                OnPropertyChanged(nameof(SelectedLearningObjectiveFilter));
                OnPropertyChanged(nameof(VisibleExercises));

                if (SelectedLearningObjectiveFilter != null)
                    AvailableFocusPoints = SelectedLearningObjectiveFilter.FocusPointVMs;
                else
                    AvailableFocusPoints = new ObservableCollection<FocusPointVM>();
            }
        }
        private ObservableCollection<FocusPointVM> _availableFocusPoints;
        public ObservableCollection<FocusPointVM> AvailableFocusPoints
        {
            get
            {
                return _availableFocusPoints;
            }

            set
            {
                _availableFocusPoints = value;
                OnPropertyChanged(nameof(AvailableFocusPoints));
            }
        }
        private FocusPointVM _selectedFocusPointFilter;
        public FocusPointVM SelectedFocusPointFilter
        {
            get
            {
                return _selectedFocusPointFilter;
            }

            set
            {
                _selectedFocusPointFilter = value;
                OnPropertyChanged(nameof(SelectedFocusPointFilter));
                OnPropertyChanged(nameof(VisibleExercises));
            }
        }

        // TODO: What is this being used for?
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public List<Rating> Ratings { get; set; }
        private Rating _selectedRating;
        public Rating SelectedRatingFilter
        {
            get
            {
                return _selectedRating;
            }

            set
            {
                _selectedRating = value;
                OnPropertyChanged(nameof(SelectedRatingFilter));
                OnPropertyChanged(nameof(VisibleExercises));
            }
        }

        #region Commands
        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();
        #endregion

        public MainWindowViewVM()
        {
            //You need to enter the email of a active user in the database, so be sure the email is in the database.
            ActiveUser = new(RepositoryManager.EmployeeRepository.Retrieve("nikolai@gmail.com"));

            CreateExerciseWindowVM = new();
            AvailableGames = new();
            AvailableLearningObjectives = new();
            AvailableFocusPoints = new();

            foreach (Game game in RepositoryManager.GameRepository.RetrieveAll())
                AvailableGames.Add(new(game));

            Ratings = Rating.GetValues<Rating>().ToList();

            CreateExerciseWindowVM.MainWindowViewVM = this;

            ExerciseVMs = new();
            foreach (Exercise exercise in RepositoryManager.ExerciseRepository.RetrieveAll())
                ExerciseVMs.Add(new ExerciseVM(exercise));
        }

        /// <summary>
        /// Filters all <see cref="ExerciseVM"/>s within <paramref name="availableExercises"/>.
        /// </summary>
        /// <param name="availableExercises">The collection that should be filtered. Should ideally be all available exercises.</param>
        /// <returns>A filtered collection of <see cref="ExerciseVM"/>s.</returns>
        private ObservableCollection<ExerciseVM> FilterExercises(IEnumerable<ExerciseVM> availableExercises)
        {
            // If a game has been selected in the MainWindow, we only include exercises associated with that game.
            // Currently we don't support filtering for only exercises with no associated game.
            if (SelectedGameFilter != null)
                availableExercises = availableExercises
                    .Where(exercise => exercise.Game != null)
                    .Where(exercise => exercise.Game.Equals(SelectedGameFilter)).ToList();

            // If a learning objective has been selected for filtering, we only include exercises associated with that learning objective.
            // We still dont support filtering for any null values.
            if (SelectedLearningObjectiveFilter != null)
                availableExercises = availableExercises
                    .Where(exercise => exercise.LearningObjective != null)
                    .Where(exercise => exercise.LearningObjective.Equals(SelectedLearningObjectiveFilter)).ToList();

            // Same as above, but for focus points.
            if (SelectedFocusPointFilter != null)
                availableExercises = availableExercises
                    .Where(exercise => exercise.FocusPoint != null)
                    .Where(exercise => exercise.FocusPoint.Equals(SelectedFocusPointFilter)).ToList();

            // And finally for ratings. Here we know the rating is never null, since an enum value will always have some value associated.
            // When no rating is selected, the rating is set to be 0. This also means that we, like the other places, don't supoprt filtering for no rating associated.
            if (SelectedRatingFilter != 0)
                availableExercises = availableExercises
                    .Where(exercise => exercise.Rating.Equals(SelectedRatingFilter)).ToList();

            // Return a new ObservableCollection<ExerciseVM> instead of a List<ExerciseVM>.
            return new(availableExercises);
        }

        #region OnChanged Events
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
