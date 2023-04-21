using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel.Domain
{
    public class FocusPointVM
    {
        private FocusPoint source;

        public string Title { get; set; }

        public FocusPointVM(FocusPoint source)
        {
            this.source = source;

            Title = source.Title;
        }
    }
}
