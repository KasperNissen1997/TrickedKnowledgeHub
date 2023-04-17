using System.Collections.Generic;

namespace TrickedKnowledgeHub.Model
{
    public class LearningObjective
    {
        public string Title { get; set; }

        public List<FocusPoint> FocusPoints = new List<FocusPoint>();

        public LearningObjective(string title)
        {
            Title = title;
        }
    }
}
