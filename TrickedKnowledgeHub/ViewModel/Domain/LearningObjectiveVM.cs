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

        /// <summary>
        /// Compares <see cref="LearningObjectiveVM"/> objects. If <paramref name="obj"/> is of type <see cref="LearningObjectiveVM"/> then the base <see cref="LearningObjective.Equals(object?)"/> method is used.
        /// </summary>
        /// <param name="obj">The object that this should be compared with.</param>
        /// <returns><see langword="true"/> if the <see cref="LearningObjectiveVM"/> instances represent the same <see cref="LearningObjective"/>, otherwise <see langword="false"/>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is LearningObjectiveVM otherLearningObjectiveVM)
                return Source.Equals(otherLearningObjectiveVM.Source);

            return base.Equals(obj);
        }
    }
}
