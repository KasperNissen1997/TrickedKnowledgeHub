using System.Collections.ObjectModel;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class GameVM
    {
        private Game source;

        public string Title { get; set; }

        public ObservableCollection<LearningObjective> Objectives { get; set; }

        public GameVM(Game source)
        {
            this.source = source;

            Title = source.Title;

            Objectives = new(source.LearningObjectives);
        }
    }
}
