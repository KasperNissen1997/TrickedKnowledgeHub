using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrickedKnowledgeHub.Model.Persistence;
using TrickedKnowledgeHub.ViewModel;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.Command.MainWindowCommand
{
    public class DeleteExerciseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (parameter is ExercisePageVM)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            var exercisePageVM = (ExercisePageVM)parameter;

            RepositoryManager.ExerciseRepository.Delete(exercisePageVM.SelectedExercise.Source);
        }
    }
}
