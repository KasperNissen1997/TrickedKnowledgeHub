using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.Command.MainWindowCommand
{
    public class OpenCreateExerciseViewCmd : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainWindowViewVM vm)
            {
                vm.CreateExerciseWindowVM.ActiveUser = vm.ActiveUser;

                Create_exercise_window createExerciseWindow = new();
                createExerciseWindow.DataContext = vm.CreateExerciseWindowVM;
                createExerciseWindow.Show();
            }
            else
                throw new NotImplementedException();
        }
    }
}
