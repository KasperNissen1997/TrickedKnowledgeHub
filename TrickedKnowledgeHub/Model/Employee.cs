namespace TrickedKnowledgeHub.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public EmployeeType Type { get; set; }

        public Employee(string name, string mail, string nickname, string password, EmployeeType type)
        {
            Mail = mail;
            Name = name;
            Nickname = nickname;
            Password = password;
            Type = type;
        }
    }
}
