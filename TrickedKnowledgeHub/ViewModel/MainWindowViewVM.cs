using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.Command.MainWindowCommand;

namespace TrickedKnowledgeHub.ViewModel
{
    public class MainWindowViewVM
    {
        public Create_exercise_window create_exercise_window;
        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();

        public MainWindowViewVM()
        {
            create_exercise_window = new Create_exercise_window();
        }
    }
}
