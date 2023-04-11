using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model
{
    public class LearningObjective
    {
        public string Title { get; set; }

        private List<FocusPoint> focusPoints = new List<FocusPoint>();


        public LearningObjective(string title)
        {
            Title = title;
        }


    }
}
