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

            // Access the exercise object
            if (exercise != null)
            {
                // Download the material for the specific exercise
                exercise = exerciseRepository.Retrieve(exercise.ExerciseID);

                if (exercise.Material != null)
                {
                    selectedByte = exercise.Material;

                    fileName = exercise.Title;

                    // Get the path to the user's Downloads folder
                    string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

                    string filePath = Path.Combine(downloadsPath, fileName + ".docx");

                    File.WriteAllBytes(filePath, selectedByte);
                }
            }
        }
    }
}
