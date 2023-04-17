using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class LearningObjectiveRepository : Repository
    {
        private List<LearningObjective> learningObjectives = new List<LearningObjective>();

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public void Create(string title)
        {
            learningObjectives.Add(new LearningObjective(title));
        }

        public LearningObjective Retrive(string title)
        {
            foreach (LearningObjective learningObjective in learningObjectives)
            {
                if (learningObjective.Title == title)
                {
                    return learningObjective;
                }
            }

            throw new ArgumentException($"No learningObjective with title {title} found.");
        }

        public LearningObjective RetriveAll()
        {
            foreach (LearningObjective learningObjective in learningObjectives)
            {
                return new LearningObjective(learningObjective.Title);
            }

            throw new ArgumentException($"No more learningObjectives could be found  ");
        }

    }
}
