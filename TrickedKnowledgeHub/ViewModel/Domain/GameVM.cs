using System.Collections.ObjectModel;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel.Domain
{
    public class GameVM
    {
        public Game Source { get; }

        public string Title { get; set; }

        public ObservableCollection<LearningObjectiveVM> Objectives { get; set; }

        public GameVM(Game source)
        {
            this.Source = source;

            Title = source.Title;

            Objectives = new();

            foreach (LearningObjective learningObjective in source.LearningObjectives)
                Objectives.Add(new(learningObjective));
        }
    }
}
