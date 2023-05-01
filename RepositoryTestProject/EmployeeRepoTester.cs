using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;

namespace RepositoryTestProject
{
    [TestClass]
    public class EmployeeRepoTester
    {
        EmployeeRepository employeeRepository = RepositoryManager.TestEmployeeRepository;

        string connectionString = "Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true";

        [TestInitialize]
        public void TestInitialize()
        {
            // create new items
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.TYPE VALUES('Coach'), ('GameCoordinator'), ('Administrator')", con);

                cmd.ExecuteNonQuery();

                cmd = new ("INSERT INTO EMPLOYEE VALUES " +
                "('nikolai@gmail.com',    'Nikolai',    'nikolaikiller123', 'donthackmeplz1', 'Coach'), " +
                "('jonathan@gmail.com',   'Jonathan',   'jonathantheman',   'donthackmeplz2', 'Coach'), " +
                "('kasper@gmail.com',     'Kasper',     'kasperthemaster',  'donthackmeplz3', 'Coach'), " +
                "('casper@gmail.com',     'Casper',     'cappertheslapper', 'donthackmeplz4', 'Coach'), " +
                "('oguz@gmail.com',       'Oguz',       'oguztheboguz',     'donthackmeplz5', 'Coach'), " +
                "('aleksander@gmail.com', 'Aleksander', 'aleksalamander',   'donthackmeplz6', 'Coach')", con);

                cmd.ExecuteNonQuery();


            }

            employeeRepository.Reset();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            // drop all items
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM EMPLOYEE", con);

                cmd.ExecuteNonQuery();

                cmd = new("DELETE FROM TYPE", con);

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        [TestMethod]
        public void Test_Create()
        {
            //Arrange

            //Act
            Employee newEmp = employeeRepository.Create("nils@gmail.com", "Nils", "nilstheboss", "donthackmeplz7", EmployeeType.Coach);

            //Assert
            Assert.IsNotNull(newEmp);
            Assert.AreEqual("nils@gmail.com, Nils, nilstheboss, donthackmeplz7, Coach", newEmp.ToString());
        }

        [TestMethod]
        public void Test_Retrieve()
        {
            //Arrange
            string email = "nikolai@gmail.com";

            //Act
            Employee retrievedEmployee = employeeRepository.Retrieve(email);

            //Assert
            Assert.IsNotNull(retrievedEmployee);
            Assert.AreEqual(retrievedEmployee.Mail, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Retrieve_Wrong_Mail()
        {
            //Arrange
            string invalidMail = "wrong@gmail.com";

            //Act
            employeeRepository.Retrieve(invalidMail);

            //Assert
        }

        [TestMethod]
        public void Test_RetrieveAll()
        {
            // Arrange
            List<Employee> retrievedEmployees;

            // Act
            retrievedEmployees = employeeRepository.RetrieveAll();

            // Assert
            Assert.IsNotNull(retrievedEmployees);
            Assert.AreEqual(retrievedEmployees.Count, 6);
        }

        [TestMethod]
        public void Test_Load()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(employeeRepository);
        }
    }
}






