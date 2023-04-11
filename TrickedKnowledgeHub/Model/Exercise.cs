using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model
{
    public class Exercise
    {
        public string Title { get; set; }
        public string Descrription { get; set; }
        public string Material {get; set; }
        public DateTime TimeStamp { get; set; }

        public Employee Author { get; set; }
        public FocusPoint FocusPoint { get; set; }
        public Rating Rating { get; set; }
        public Game Game { get; set; }

        public Exercise(Employee author, string title, string description, string material, Game game, FocusPoint focusPoint)
        {
            Author = author;
            Title = title;
            Descrription = description;
            Material = material;
            Game = game;
            FocusPoint = focusPoint;
        }
        
        public Exercise(Employee author, string title, string description, string material, FocusPoint focusPoint)
        {
            Author = author;
            Title = title;
            Descrription = description;
            Material = material;
            FocusPoint = focusPoint;
        }
        
    }
}
