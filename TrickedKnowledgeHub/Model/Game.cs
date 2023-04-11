using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model
{
    public class Game
    {
        public string Title { get; set; }

        private List<LearningObjective> learningObjectives = new List<LearningObjective>();

        public Game(string title)
        {
            Title = title;
        }

    }
}
