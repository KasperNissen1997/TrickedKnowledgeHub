using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using TrickedKnowledgeHub.ViewModel;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.Command.MainWindowCommand
{
    public class OpenCreateExerciseViewCmd : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (parameter is MainWindowViewVM)
            {
                return true;
            }
                return false;
        }

        public void Execute(object? parameter)
        {
            var vm = (MainWindowViewVM)parameter;
            try
            {
                vm.CreateExerciseWindowVM.ActiveUser = vm.ActiveUser;

                Create_exercise_window createExerciseWindow = new();
                createExerciseWindow.DataContext = vm.CreateExerciseWindowVM;
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                // Find the Frame control in the MainWindow using its name
                Frame frameExercise = (Frame)mainWindow.FindName("FrameExercise");
                frameExercise.Content= createExerciseWindow;
                frameExercise.Visibility = Visibility.Visible;

            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
