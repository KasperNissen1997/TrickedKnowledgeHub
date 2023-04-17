using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class CreateExerciseWindowViewVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Rating SelectedRating { get; set; }
        public List<Rating> Ratings { get; set; }
        public LearningObjectiveVM LearningObjective { get; set; }
        public GameVM Game { get; set; }
        public FocusPointVM FocusPoint { get; set; }
        public string Material { get; set; }

        public CreateExerciseWindowViewVM()
        {
            Ratings = Rating.GetValues<Rating>().ToList();
        }
    }
}
