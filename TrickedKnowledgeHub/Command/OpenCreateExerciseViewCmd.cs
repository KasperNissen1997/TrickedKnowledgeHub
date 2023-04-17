﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.Command
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
                vm.create_exercise_window.Show();
            }
            else
                throw new NotImplementedException();
        }
    }
}