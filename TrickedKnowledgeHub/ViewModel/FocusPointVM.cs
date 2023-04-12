using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class FocusPointVM
    {
        private FocusPoint source;

        public string Title;

        public FocusPointVM(FocusPoint source)
        {
            this.source = source;

            Title = source.Title;
        }
    }
}
