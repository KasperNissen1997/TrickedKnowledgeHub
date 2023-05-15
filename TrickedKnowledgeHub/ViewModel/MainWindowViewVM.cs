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

        public ObservableCollection<ExerciseVM> VisibleExercises
        {
            get
            {
                IEnumerable<ExerciseVM> visibleExercises = ExerciseVMs;

                if (SelectedGameFilter != null)
                    visibleExercises = visibleExercises
                        .Where(exercise => exercise.Game != null)
                        .Where(exercise => exercise.Game.Equals(SelectedGameFilter)).ToList();

                if (SelectedLearningObjectiveFilter != null)
                    visibleExercises = visibleExercises.Where(exercise => exercise.LearningObjective == SelectedLearningObjectiveFilter).ToList();

                if (SelectedFocusPointFilter != null)
                    visibleExercises = visibleExercises.Where(exercise => exercise.FocusPoint == SelectedFocusPointFilter).ToList();

                return new(visibleExercises);
            }
        }

        private GameVM _selectedGameFilter;
        public GameVM SelectedGameFilter
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
                SelectedExerciseVM = value;
            }
        }

        private ObservableCollection<GameVM> _availableGames;
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
        private GameVM? _selectedGame;
        public GameVM? SelectedGame
        {
            get
            {
                return _selectedGame;
            }

            set
            {
                _selectedGame = value;
                OnPropertyChanged(nameof(SelectedGame));

                if (SelectedLearningObjective != null && !SelectedGame.Objectives.Contains(SelectedLearningObjective))
                {
                    SelectedLearningObjective = null;
                }

                AvailableLearningObjectives = SelectedGame.Objectives;



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
        private LearningObjectiveVM _selectedLearningObjective;
        public LearningObjectiveVM SelectedLearningObjective
        {
            get
            {
                return _selectedLearningObjective;
            }

            set
            {
                _selectedLearningObjective = value;
                OnPropertyChanged(nameof(SelectedLearningObjective));

                if (SelectedLearningObjective != null)
                {
                    AvailableFocusPoints = SelectedLearningObjective.FocusPointVMs;
                }
                else
                {
                    AvailableFocusPoints = new ObservableCollection<FocusPointVM>();
                }
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
        private FocusPointVM _selectedFocusPoint;
        public FocusPointVM SelectedFocusPoint
        {
            get
            {
                return _selectedFocusPoint;
            }

            set
            {
                _selectedFocusPoint = value;
                OnPropertyChanged(nameof(SelectedFocusPoint));
            }
        }
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
        public Rating SelectedRating
        {
            get
            {
                return _selectedRating;
            }

            set
            {
                _selectedRating = value;
                OnPropertyChanged(nameof(SelectedRating));
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


            ExerciseVMs = new();
            foreach (Exercise exercise in RepositoryManager.ExerciseRepository.RetrieveAll())
                ExerciseVMs.Add(new ExerciseVM(exercise));

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
