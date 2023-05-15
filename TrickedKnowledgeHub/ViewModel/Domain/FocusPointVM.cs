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

        public override bool Equals(object? obj)
        {
            if (obj is FocusPointVM otherFocusPointVM)
                return Source.Equals(otherFocusPointVM.Source);

            return base.Equals(obj);
        }
    }
}
