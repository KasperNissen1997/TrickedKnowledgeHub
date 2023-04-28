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
            return $"{Title}";
        }
    }
}
