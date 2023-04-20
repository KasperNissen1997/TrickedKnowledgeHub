using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class EmployeeRepository : Repository
    {
        private List<Employee> employees;

        public EmployeeRepository()
        {
            employees = new();

            Load();
        }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open(); //Open connection

                SqlCommand cmd = new SqlCommand("SELECT Name, Mail, Nickname, Password, Type  FROM EMPLOYEE", con); //SQL Query run at execution

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) //While reader reads
                    {
                        string name = reader["Name"].ToString();
                        string mail = reader["Mail"].ToString();
                        string nickname = reader["Nickname"].ToString();
                        string password = reader["Password"].ToString();

                        string danishType = reader["Type"].ToString();

                        EmployeeType employeeType;
                        switch (danishType)
                        {
                            case "Underviser":
                                employeeType = EmployeeType.Teacher;
                                break;

                            case "Administator":
                                employeeType = EmployeeType.Administrator;
                                break;

                            case "Spilansvarlig":
                                employeeType = EmployeeType.GameCoordinator;
                                break;

                            default:
                                employeeType = EmployeeType.Teacher;
                                break;
                        }

                        Employee employee = new(name, mail, nickname, password, employeeType);

                        employees.Add(employee);
                    }
                }
            }
        }

        public Employee Create(string email, string name, string nickname, string password, EmployeeType type)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open(); //Open connection

                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE (Name, Mail, Nickname, Password, Type) " + //SQL query ran at execution
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

        public Employee Retrieve(string email) //Retrieves Employee based on string parameter
        {
            foreach (Employee employee in employees) //foreach Employee in list
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
            return new(employees);
        }
    }
}
