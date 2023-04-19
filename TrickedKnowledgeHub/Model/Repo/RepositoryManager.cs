using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model.Repo
{
    public static class RepositoryManager
    {
        public static GameRepository GameRepository { get; set; } = new();
        public static LearningObjectiveRepository LearningObjectiveRepository { get; set; } = new();
        public static FocusPointRepository FocusPointRepository { get; set; } = new();
        public static EmployeeRepository EmployeeRepository { get; set; } = new();
        public static ExerciseRepository ExerciseRepository { get; set; } = new();
    }
}
