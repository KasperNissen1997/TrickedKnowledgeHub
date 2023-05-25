using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrickedKnowledgeHub.ViewModel;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace TrickedKnowledgeHub.Command.CreateExerciseWindowCommand
{
    public class SelectMaterialCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (parameter is CreateExerciseWindowViewVM vm)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            var vm = (CreateExerciseWindowViewVM)parameter;

            OpenFileDialog openFileDialog = new();
            openFileDialog.DefaultExt = ".docx";
            // openFileDialog.Filter = "CSV (Comma delimited)|*.csv";
            openFileDialog.Filter = ".docx (Microsoft Word)|*.docx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                Trace.WriteLine("Chose file " + openFileDialog.FileName);
                vm.Material = File.ReadAllBytes(openFileDialog.FileName);

                vm.FileName = Path.GetFileName(openFileDialog.FileName);
            }

        }
    }
}
