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
