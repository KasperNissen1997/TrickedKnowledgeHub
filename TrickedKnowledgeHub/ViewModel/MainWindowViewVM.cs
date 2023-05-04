using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.Command.MainWindowCommand;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel.Domain;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class MainWindowViewVM
    {
        public EmployeeVM ActiveUser { get; set; }

        public CreateExerciseWindowViewVM CreateExerciseWindowVM { get; set; }

        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();

        private List<ExerciseVM> _exerciseVM;
        public List<ExerciseVM> ExerciseVMs
        {
            get { return _exerciseVM; }
            set { _exerciseVM = value; }
        }

        public MainWindowViewVM()
        {
            //You need to enter the email of a active user in the database, so be sure the email is in the database.
            ActiveUser = new(RepositoryManager.EmployeeRepository.Retrieve("nikolai@gmail.com"));

            CreateExerciseWindowVM = new();

            ExerciseVMs = new();
            foreach (Exercise exercise in RepositoryManager.ExerciseRepository.RetrieveAll())
                ExerciseVMs.Add(new ExerciseVM(exercise));

        }
    }
}
