using System;

namespace TrickedKnowledgeHub.Model
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Material {get; set; }
        public DateTime Timestamp { get; set; }

        public Employee Author { get; set; }
        public Game? Game { get; set; }
        public FocusPoint? FocusPoint { get; set; }
        public Rating Rating { get; set; }

        public Exercise(int exerciseID, string title, string description, byte[] material, DateTime timeStamp, Employee author, Game game, FocusPoint focusPoint, Rating rating)
        {
            ExerciseID = exerciseID;
            Title = title;
            Description = description;
            Material = material;
            Timestamp = timeStamp;

            Author = author;
            Game = game;
            FocusPoint = focusPoint;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{ExerciseID}, {Title}, {Description}, {Material}, {Timestamp}, {Author}, {Game}, {FocusPoint}, {Rating}";
        }
    }
}
