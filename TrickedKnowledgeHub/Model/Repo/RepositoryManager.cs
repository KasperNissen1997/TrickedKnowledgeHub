using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model.Repo
{
    public static class RepositoryManager
    {
        // Main repositories.
        public static GameRepository GameRepository { get; set; } = new();
        public static LearningObjectiveRepository LearningObjectiveRepository { get; set; } = new();
        public static FocusPointRepository FocusPointRepository { get; set; } = new();
        public static EmployeeRepository EmployeeRepository { get; set; } = new();
        public static ExerciseRepository ExerciseRepository { get; set; } = new();

        // Test repositories.
        public static GameRepository TestGameRepository { get; set; } = new(true);
        public static LearningObjectiveRepository TestLearningObjectiveRepository { get; set; } = new(true);
        public static FocusPointRepository TestFocusPointRepository { get; set; } = new(true);
        public static EmployeeRepository TestEmployeeRepository { get; set; } = new(true);
        public static ExerciseRepository TestExerciseRepository { get; set; } = new(true);
    }
}
