using System.Collections.ObjectModel;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class LearningObjectiveVM
    {
        private LearningObjective source;

        public string Title { get; set; }

        public ObservableCollection<FocusPointVM> FocusPointVMs { get; set; }

        public LearningObjectiveVM(LearningObjective source)
        {
            this.source = source;

            Title = source.Title;

            FocusPointVMs = new();

            foreach (FocusPoint focusPoint in source.FocusPoints)
                FocusPointVMs.Add(new(focusPoint));
        }
    }
}
