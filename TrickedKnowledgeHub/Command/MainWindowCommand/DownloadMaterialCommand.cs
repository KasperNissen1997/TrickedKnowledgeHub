using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;

namespace TrickedKnowledgeHub.Command.MainWindowCommand
{
    public class DownloadMaterialCommand : ICommand
    {
        // TODO: 
        // Use the correct filename - should be in ExerciseViewModel in latest dev version
        // Use the correct folder - you can't write to the users folder, get the path to the downloads folder some other way 😎
        // Take the viewModel as the parameter, and then only take the material of the selected exercise
        // CanExecute should always return true
        // In future iterations, prompt the user for where to save the material 

        public event EventHandler CanExecuteChanged;

        private Exercise exercise;

        public ExerciseRepository exerciseRepository = RepositoryManager.ExerciseRepository; //Needed for calling methods in repo

        byte[] selectedByte; //Field for material

        string fileName; //File name

        public DownloadMaterialCommand(Exercise exercise)
        {
            this.exercise = exercise;
        }

        public bool CanExecute(object parameter) //Makes sure the command cannot execute in case there is no exercise being passed
        {
            if (this.exercise == null) { return false; }
            else
            {
                return true;
            }

        }

        public void Execute(object parameter)
        {
            // Cast the parameter to the Exercise type
            Exercise exercise = parameter as Exercise;

            // Download the material for the specific exercise
            selectedByte = exerciseRepository.GetMaterial(exercise.ExerciseID); //selectedByte becomes the byte given from the exercise id.

            if (selectedByte != null)
            {
                fileName = exercise.Title;

                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads"; //Get the path to the user's Downloads folder


                string filePath = Path.Combine(downloadsPath, fileName + ".docx"); //Combine the path with the specified file name to make a new file.

                File.WriteAllBytes(filePath, selectedByte); //Creates file on specified path and name, using specified byte
            }
        }
    }
}
