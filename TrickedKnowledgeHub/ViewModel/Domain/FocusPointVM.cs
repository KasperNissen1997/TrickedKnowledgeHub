using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel.Domain
{
    public class FocusPointVM
    {
        public FocusPoint Source { get; }

        public string Title { get; set; }

        public FocusPointVM(FocusPoint source)
        {
            this.Source = source;

            Title = source.Title;
        }

        /// <summary>
        /// Compares <see cref="FocusPointVM"/> objects. If <paramref name="obj"/> is of type <see cref="FocusPointVM"/> then the base <see cref="FocusPoint.Equals(object?)"/> method is used.
        /// </summary>
        /// <param name="obj">The object that this should be compared with.</param>
        /// <returns><see langword="true"/> if the <see cref="FocusPointVM"/> instances represent the same <see cref="FocusPoint"/>, otherwise <see langword="false"/>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is FocusPointVM otherFocusPointVM)
                return Source.Equals(otherFocusPointVM.Source);

            return base.Equals(obj);
        }
    }
}
