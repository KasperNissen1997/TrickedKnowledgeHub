using System.Collections.Generic;

namespace TrickedKnowledgeHub.Model
{
    public class Game
    {
        public string Title { get; set; }

        public List<LearningObjective> LearningObjectives { get; set; }

        public Game(string title)
        {
            Title = title;

            LearningObjectives = new();
        }
    }
}
