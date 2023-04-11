using System;

namespace TrickedKnowledgeHub.Model
{
    public class Exercise
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Material {get; set; }
        public DateTime Timestamp { get; set; }

        public Employee Author { get; set; }
        public Game? Game { get; set; }
        public FocusPoint? FocusPoint { get; set; }
        public Rating Rating { get; set; }

        public Exercise(string title, string description, string material, Employee author, Game game, FocusPoint focusPoint)
        {
            Title = title;
            Description = description;
            Material = material;

            Author = author;
            Game = game;
            FocusPoint = focusPoint;
        }
    }
}
