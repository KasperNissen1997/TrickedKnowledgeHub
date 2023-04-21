using System;
using System.Windows.Input;
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.Command.CreateExerciseWindowCommand
{
    public class CreateExerciseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (parameter is CreateExerciseWindowViewVM vm)
            {
                if (String.IsNullOrEmpty(vm.Title))
                    return false;

                if (vm.SelectedFocusPoint == null)
                    return false;

                if (vm.Material == null)
                    return false;

                return true;
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is CreateExerciseWindowViewVM vm)
            {
                // Create the exercise with the supplied information. 
            }

            throw new NotImplementedException();
        }
    }
}
