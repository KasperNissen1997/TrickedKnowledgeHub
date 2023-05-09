using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.ViewModel
{
    public class ExercisePageVM: INotifyPropertyChanged
    {
        private ExerciseVM _selectedExercise;
        public ExerciseVM SelectedExercise
        {
            get { 
                return _selectedExercise; 
                }
            set {
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
