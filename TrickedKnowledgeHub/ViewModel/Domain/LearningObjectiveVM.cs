using System.Collections.ObjectModel;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel.Domain
{
    public class LearningObjectiveVM
    {
        public LearningObjective Source { get; }

        public string Title { get; set; }

        public ObservableCollection<FocusPointVM> FocusPointVMs { get; set; }

        public LearningObjectiveVM(LearningObjective source)
        {
            this.Source = source;

            Title = source.Title;

            FocusPointVMs = new();

            foreach (FocusPoint focusPoint in source.FocusPoints)
                FocusPointVMs.Add(new(focusPoint));
        }
    }
}
