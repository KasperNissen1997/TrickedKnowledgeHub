using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class EmployeeRepository : Repository
    {
        private List<Employee> employees = new();

        public EmployeeRepository(bool isTestRepository = false)
        {
            IsTestRepository = isTestRepository;

            Load();
        }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open(); //Open connection

                SqlCommand cmd = new SqlCommand("SELECT Mail, Name, Nickname, Password, Type  FROM EMPLOYEE", con); //SQL Query run at execution

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) //While reader reads
                    {
                        string mail = reader["Mail"].ToString();
                        string name = reader["Name"].ToString();
                        string nickname = reader["Nickname"].ToString();
                        string password = reader["Password"].ToString();

                        string danishType = reader["Type"].ToString();

                        EmployeeType employeeType;
                        switch (danishType)
                        {
                            case "Underviser":
                                employeeType = EmployeeType.Coach;
                                break;

                            case "Administator":
                                employeeType = EmployeeType.Administrator;
                                break;

                            case "Spilansvarlig":
                                employeeType = EmployeeType.GameCoordinator;
                                break;

                            default:
                                employeeType = EmployeeType.Coach;
                                break;
                        }

                        Employee employee = new(mail, name, nickname, password, employeeType);

                        employees.Add(employee);
                    }
                }
            }
        }

        public void Reset()
        {
            employees.Clear();

            Load();
        }

        public Employee Create(string email, string name, string nickname, string password, EmployeeType type)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open(); //Open connection

                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE (Mail, Name, Nickname, Password, Type) " + //SQL query ran at execution
                                                "VALUES(@Mail, @Name, @Nickname, @Password, @Type)", con); //values references @ in code block below
                
                cmd.Parameters.AddWithValue("@Mail", email); //cmd.Parameters{Get;}.AddWithValue(ParameterName, object value)
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Nickname", nickname);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Type", type.ToString());

                cmd.ExecuteNonQuery();

                Employee employee = new(email, name, nickname, password, type);

                employees.Add(employee);

                return employee;
            }
        }

        /// <summary>
        /// Retrieves the <see cref="Employee"/> where the mail properpty matches <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email of the <see cref="Employee"/> that should be retrieved.</param>
        /// <returns>An <see cref="Employee"/> where the mail property matches <paramref name="email"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if no <see cref="Employee"/> has a mail property that matches with <paramref name="email"/>.</exception>
        public Employee Retrieve(string email)
        {
            // Iterate through all existing employees and search for an employee with a matching mail.
            foreach (Employee employee in employees)
                if (email == employee.Mail)
                    return employee;

            // If we couldn't find any matching employee...
            throw new ArgumentException($"Could not find employee with mail: {email}");
        }

        public List<Employee> RetrieveAll() //Retrieves all Employees, aka returning the whole list
        {
            return new(employees);
        }
    }
}
