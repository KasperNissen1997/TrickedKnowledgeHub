using System;
using TrickedKnowledgeHub.Command.MainWindowCommand;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel.Domain
{
    public class ExerciseVM
    {
        public Exercise Source { get; }

        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Material { get; set; }
        public DateTime Timestamp { get; set; }

        public EmployeeVM Author { get; set; }
        public GameVM? Game { get; set; }
        public LearningObjectiveVM? LearningObjective { get; set; }
        public FocusPointVM? FocusPoint { get; set; }
        public Rating? Rating { get; set; }

        public DownloadMaterialCommand DownloadMaterialCommand { get; set; } = new();

        public ExerciseVM(Exercise source)
        {
            this.Source = source;

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
            {
                FocusPoint = new(source.FocusPoint);
                LearningObjective = new(source.FocusPoint.Parent);
            }
            else
            {
                FocusPoint = null;
                LearningObjective = null;
            }

            // Handle a possible null reference
            if (source.Rating != null)
            {
                Rating = source.Rating;
            }
            else
            {
                Rating = null;
            }
        }
    }
}
