﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                "('Nikolai',    'nikolai@gmail.com',    'nikolaikiller123', 'donthackmeplz1', 'Coach'), " +
                "('Jonathan',   'jonathan@gmail.com',   'jonathantheman',   'donthackmeplz2', 'Coach'), " +
                "('Kasper',     'kasper@gmail.com',     'kasperthemaster',  'donthackmeplz3', 'Coach'), " +
                "('Casper',     'casper@gmail.com',     'cappertheslapper', 'donthackmeplz4', 'Coach'), " +
                "('Oguz',       'oguz@gmail.com',       'oguztheboguz',     'donthackmeplz5', 'Coach'), " +
                "('Aleksander', 'aleksander@gmail.com', 'aleksalamander',   'donthackmeplz6', 'Coach')", con);

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

                cmd = new("DELETE FROM TYPE");

                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void Test_Create()
        {
            //Arrange

            //Act
            Employee newEmp = employeeRepository.Create("Nils", "nils@gmail.com", "nilstheboss", "donthackmeplz7", EmployeeType.Coach);

            //Assert
            Assert.IsNotNull(newEmp);
            Assert.AreEqual("Mail: nils@gmail.com, Name: Nils, Nickname: nilstheboss, Password: donthackmeplz7, Type: Coach", newEmp.ToString());
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






