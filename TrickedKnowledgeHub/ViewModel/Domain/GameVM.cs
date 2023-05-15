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

        /// <summary>
        /// Compares <see cref="GameVM"/> objects. If <paramref name="obj"/> is of type <see cref="GameVM"/> then the base <see cref="Game.Equals(object?)"/> method is used.
        /// </summary>
        /// <param name="obj">The object that this should be compared with.</param>
        /// <returns>
        /// <see langword="true"/> if the <see cref="GameVM"/> instances represent the same <see cref="Game"/>, otherwise <see langword="false"/>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is GameVM otherGameVM)
                return Source.Equals(otherGameVM.Source);

            return base.Equals(obj);
        }
    }
}
