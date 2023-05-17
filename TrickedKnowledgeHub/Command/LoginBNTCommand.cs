using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.Commands
{
    public class LoginBNTCommand : ICommand
    {
            
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {CommandManager.RequerySuggested -= value; }
        }



        public bool CanExecute(object? parameter)
        {
            if(parameter is LoginViewModel vm)
            {
                bool lightCheck = false;

                if (!string.IsNullOrEmpty(vm.Password) && !string.IsNullOrEmpty(vm.Username))
                   lightCheck = true;
                CommandManager.InvalidateRequerySuggested();

                return lightCheck;
            }
            else
            {
                return false;
                
            } 
        }

        public void Execute(object? parameter)
        {
            if (parameter is LoginViewModel vm)
            {
                vm.Username = "hejsa";
            }
            else
            {
                throw new ArgumentException("Wrong input");
            }





        }





    }
}
