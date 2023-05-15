using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using TrickedKnowledgeHub.Model;
using Microsoft.Office.Interop.Word;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Command.CreateExerciseWindowCommand;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.ViewModel
{
    public class CreateExerciseWindowViewVM : INotifyPropertyChanged
    {
        private EmployeeVM activeUser;
        public EmployeeVM ActiveUser
        {
            get
            {
                return activeUser;
            }

            set
            {
                activeUser = value;
                OnPropertyChanged(nameof(ActiveUser));
            }
        }

        public List<Rating> Ratings { get; set; }

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
        private string? _description;
        public string? Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
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
        private byte[] _material;
        public byte[] Material
        {
            get
            {
                return _material;
            }

            set
            {
                _material = value;
                OnPropertyChanged(nameof(Material));
            }
        }

        public MainWindowViewVM MainWindowViewVM { get; set; }

        #region Commands
        public CreateExerciseCommand CreateExerciseCommand { get; set; } = new();
        public SelectMaterialCommand SelectMaterialCommand { get; set; } = new();
        #endregion

        public CreateExerciseWindowViewVM()
        {
            AvailableGames = new();
            AvailableLearningObjectives = new();
            AvailableFocusPoints = new();

            foreach (Game game in RepositoryManager.GameRepository.RetrieveAll())
            {
                AvailableGames.Add(new(game));
            }
            Game gameBlank = new("Blank");
            GameVM gameBlankVM = new(gameBlank);
            AvailableGames.Add(gameBlankVM);

            foreach (LearningObjective learningObjective in RepositoryManager.LearningObjectiveRepository.RetrieveAll())
                if (learningObjective.Parent == null)
                    AvailableLearningObjectives.Add(new(learningObjective));

            foreach (FocusPoint focusPoint in RepositoryManager.FocusPointRepository.RetrieveAll())
                if (focusPoint.Parent.Parent == null)
                    AvailableFocusPoints.Add(new(focusPoint));

            Ratings = Rating.GetValues<Rating>().ToList();
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }


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
