using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Persistence;
using TrickedKnowledgeHub.ViewModel;
using Microsoft.Win32;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.Command.MainWindowCommand
{
    public class DownloadMaterialCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is ExercisePageVM)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var exercisePage = (ExercisePageVM) parameter;
            SaveFileDialog saveFileDialog = new(); //New dialog

            //Initial directory equals the specified folder path given from GetFolderPath method which returns a string of a path
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            saveFileDialog.DefaultExt = ".docx";

            if (saveFileDialog.ShowDialog() == true)
            {   //material equals the byte returned from the GetMaterial method using ExerciseID from the selected Exercise
                byte[] material = RepositoryManager.ExerciseRepository.GetMaterial(exercisePage.SelectedExercise.Source.ExerciseID);

                string filePath = saveFileDialog.FileName + "";

                File.WriteAllBytes(filePath, material); //Creates file on specified path and name, using specified byte
            }

        }
    }
}
