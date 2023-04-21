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
    }
}
