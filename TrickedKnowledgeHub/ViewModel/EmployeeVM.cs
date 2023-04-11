using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class EmployeeVM
    {
        private Employee source;

        public string Name { get; set; }
        public string Mail { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public EmployeeType Type { get; set; }

        public EmployeeVM(Employee source)
        {
            this.source = source;

            Name = source.Name;
            Mail = source.Mail;
            Nickname = source.Nickname;
            Password = source.Password;
            Type = source.Type;
        }
    }
}
