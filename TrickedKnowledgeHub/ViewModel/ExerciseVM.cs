using System;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class ExerciseVM
    {
        private Exercise source;

        public string Title { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public DateTime Timestamp { get; set; }

        public EmployeeVM Author { get; set; }
        public GameVM? Game { get; set; }
        public FocusPointVM? FocusPoint { get; set; }
        public Rating Rating { get; set; }

        public ExerciseVM(Exercise source)
        {
            this.source = source;

            Title = source.Title;
            Description = source.Description;
            Material = source.Material;
            Timestamp = source.Timestamp;

            Author = new(source.Author);

            // Handle a possible null reference
            if (source.Game != null)
                Game = new(source.Game);
            else
                Game = null;

            // Handle a possible null reference
            if (source.FocusPoint != null)
                FocusPoint = new(source.FocusPoint);
            else
                FocusPoint = null;

            Rating = source.Rating;
        }
    }
}
