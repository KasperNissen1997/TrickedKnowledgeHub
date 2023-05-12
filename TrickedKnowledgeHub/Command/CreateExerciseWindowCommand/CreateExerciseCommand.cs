using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.Command.CreateExerciseWindowCommand
{
    public class CreateExerciseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

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

                if (vm.Description == null)
                    return false;

                return true;
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is CreateExerciseWindowViewVM vm)
            {
                ExerciseVM exercise;
                
                if (vm.SelectedGame == null)
                {
                    exercise = new ExerciseVM(RepositoryManager.ExerciseRepository.Create(vm.Title, vm.Description, vm.Material, DateTime.Now, vm.ActiveUser.Source, null, vm.SelectedFocusPoint.Source, vm.SelectedRating));
                }
                else
                {
                    exercise = new ExerciseVM(RepositoryManager.ExerciseRepository.Create(vm.Title, vm.Description, vm.Material, DateTime.Now, vm.ActiveUser.Source, vm.SelectedGame.Source, vm.SelectedFocusPoint.Source, vm.SelectedRating));
                }
                vm.MainWindowViewVM.ExerciseVMs.Add(exercise);
            }
            else
                throw new NotImplementedException();

        }
    }
}
