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
        public EmployeeVM ActiveUser { get; set; }

        public CreateExerciseWindowViewVM CreateExerciseWindowVM { get; set; }

        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();



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

        public MainWindowViewVM()
        {
            //You need to enter the email of a active user in the database, so be sure the email is in the database.
            ActiveUser = new(RepositoryManager.EmployeeRepository.Retrieve("nikolai@gmail.com"));

            CreateExerciseWindowVM = new();

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
