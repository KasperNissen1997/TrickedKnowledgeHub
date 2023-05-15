namespace TrickedKnowledgeHub.Model
{
    public class FocusPoint
    {
        public string Title { get; set; }
        public LearningObjective Parent { get; set; }

        public FocusPoint(string title, LearningObjective parent)
        {
            Title = title;
            Parent = parent;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Parent: {Parent.ID}";
        }

        /// <summary>
        /// Compares the current instance with <paramref name="obj"/>. <br/>
        /// Specifically overriden to enable precise comparison of different <see cref="FocusPoint"/> objects.
        /// </summary>
        /// <param name="obj">The object that this should be compared with.</param>
        /// <returns>
        /// If <paramref name="obj"/> is of type <see cref="FocusPoint"/>, then <see langword="true"/> if the <see cref="FocusPoint.Title"/>s and <see cref="FocusPoint.Parent"/>s of the two match. If not, then <see langword="false"/><br/>
        /// Otherwise, it uses the default comparer.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is FocusPoint otherFocusPoint)
            {
                if (!Title.Equals(otherFocusPoint.Title))
                    return false;

                if (!Parent.Equals(otherFocusPoint.Parent))
                    return false;

                return true;
            }

            return base.Equals(obj);
        }
    }
}
