using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using TrickedKnowledgeHub.Command.MainWindowCommand;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.ViewModel
{
    public class ExercisePageVM : INotifyPropertyChanged
    {
        public DownloadMaterialCommand DownloadMaterialCommand { get; set; } = new();
        public DeleteExerciseCommand DeleteExerciseCommand { get; set; } = new();


        private ExerciseVM _selectedExercise;
        public ExerciseVM SelectedExercise
        {
            get
            {
                return _selectedExercise;
            }
            set
            {
                _selectedExercise = value;
                OnPropertyChanged(nameof(SelectedExercise));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
