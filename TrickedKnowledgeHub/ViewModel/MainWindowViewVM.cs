using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.Command.MainWindowCommand;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub.ViewModel
{
    public class MainWindowViewVM
    {
        public EmployeeVM ActiveUser { get; set; }

        public Create_exercise_window create_exercise_window;
        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();

        public MainWindowViewVM()
        {
            ActiveUser = new(RepositoryManager.EmployeeRepository.Retrieve("Nikko@gmail.dk"));

            create_exercise_window = new Create_exercise_window();
        }
    }
}
