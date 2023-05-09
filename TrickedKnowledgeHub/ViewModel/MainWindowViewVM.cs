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

namespace TrickedKnowledgeHub.ViewModel
{
    public class MainWindowViewVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EmployeeVM ActiveUser { get; set; }

        public CreateExerciseWindowViewVM CreateExerciseWindowVM { get; set; }

        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();

        

        private List<ExerciseVM> _exerciseVM;
        public List<ExerciseVM> ExerciseVMs
        {
            get { return _exerciseVM; }
            set { _exerciseVM = value; }
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

        public List<Rating> Ratings { get; set; }

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
                //It fucking works with the code in learning objective hehe

            }
        }
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
    }
}
