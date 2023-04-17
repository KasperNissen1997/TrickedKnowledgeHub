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
            Load();
        }

        public override void Load()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                employeeList.Clear(); //Clears list before loading, no duplicates.

                con.Open(); //Open connection
                SqlCommand cmd = new SqlCommand("SELECT Name, Mail, Nickname, Password, Type  FROM EMPLOYEE", con); //SQL Query run at execution
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) //While reader reads
                    {
                        Employee employee = new Employee(
                            reader["Name"].ToString(), //SQL string is different from C# string, needs to be converted
                            reader["Mail"].ToString(),
                            reader["Nickname"].ToString(),
                            reader["Password"].ToString(),       
                            (EmployeeType)Enum.Parse(typeof(EmployeeType), reader["Type"].ToString())); //convert string to Enum "EmployeeType"
                        employeeList.Add(employee);
                    }
                }
            }
        }

        public void Create(string email, string name, string nickname, string password, EmployeeType type)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open(); //Open connection
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE (Name, Mail, Nickname, Password, Type) " + //SQL query ran at execution
                                                "VALUES(@Mail, @Name, @Nickname, @Password, @Type)", con); //values references @ in code block below
                {

                    cmd.Parameters.AddWithValue("@Mail", email); //cmd.Parameters{Get;}.AddWithValue(ParameterName, object value)
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Nickname", nickname);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Type", type.ToString());
                    
                }
            }
        }
        public Employee Retrieve(string email) //Retrieves Employee based on string parameter
        {
            foreach (Employee employee in employeeList) //foreach Employee in list
            {
                if (email == employee.Mail) //if given string equals selected employee mail
                {
                    return employee; //return selected employee
                }

            }
            throw new ArgumentException($"Could not find employee with mail: {email} ");

        }
        public List<Employee> RetrieveAll() //Retrieves all Employees, aka returning the whole list
        {

            if (employeeList != null) //if list is not empty
            {
                return new(employeeList); //return list
            }
            else
            {
                throw new ArgumentException($"List is empty or not loaded properly"); //exception message

            }
        }
    }
}
