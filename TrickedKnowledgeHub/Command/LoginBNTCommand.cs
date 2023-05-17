using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;
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
            //if (parameter is LoginViewModel vm)
            //{
            //    Employee employee;

            //    try
            //    {
            //        employee = RepositoryManager.EmployeeRepository.Retrieve(vm.Username);
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.WriteLine($"Error finding employee with username {vm.Username}");
            //        return;
            //    }

            //    if (employee.Password.Equals(vm.Password))
            //    {
            //        Trace.WriteLine("Login succesfull!");

            //        MainWindow mainWindow = new();

            //        if (mainWindow.DataContext is MainWindowViewVM mainWindowVM)
            //            mainWindowVM.ActiveUser = new(employee);

            //        mainWindow.Show();
            //    }
            //}
            //else
            //{
            //    throw new ArgumentException("Wrong input");
            //}
        }
    }
}
