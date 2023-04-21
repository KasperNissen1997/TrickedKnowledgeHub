using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel.Domain
{
    public class EmployeeVM
    {
        public Employee Source { get; }

        public string Name { get; set; }
        public string Mail { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public EmployeeType Type { get; set; }

        public EmployeeVM(Employee source)
        {
            this.Source = source;

            Name = source.Name;
            Mail = source.Mail;
            Nickname = source.Nickname;
            Password = source.Password;
            Type = source.Type;
        }
    }
}
