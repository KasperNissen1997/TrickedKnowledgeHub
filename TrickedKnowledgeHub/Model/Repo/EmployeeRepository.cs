using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;

namespace TrickedKnowledgeHub.Model.Repo
{
    internal class EmployeeRepository : Repository
    {
        private List<Employee> employeeList;


        public EmployeeRepository()
        {

        }

        public override void Load()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                employeeList.Clear(); //Clears list before loading, no duplicates.

                con.Open(); //Open connection
                SqlCommand cmd = new SqlCommand("SELECT Name, Mail, Nickname, Password, Type  FROM EMPLOYEE", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee(
                            reader["Name"].ToString(),
                            reader["Mail"].ToString(),
                            reader["Nickname"].ToString(),
                            reader["Password"].ToString(),
                            (EmployeeType)Enum.Parse(typeof(EmployeeType), reader["Type"].ToString())); //Trying to convert string to Enum "Type"
                        employeeList.Add(employee);
                    }
                }
            }
        }

        public void Create(string email, string name, string nickname, string password, EmployeeType type)
        {

        }
        public Retrieve(email : string)
        {

        }
        public RetrieveAll()
        {
            private 
            foreach(Employee emp in employeeList)
            {
                
            }
            return 
        }
}
}
