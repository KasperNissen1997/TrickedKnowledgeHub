using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
using Microsoft.Data.SqlClient;

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
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new("INSERT INTO EMPLOYEE VALUES " +
                "('Nikolai', 'nikolai@gmail.com', 'nikolaikiller123', 'donthackmeplz1', 'Coach'), " +
                "('Jonathan', 'jonathan@gmail.com', 'jonathantheman', 'donthackmeplz2', 'Coach'), " +
                "('Kasper', 'kasper@gmail.com', 'kasperthemaster', 'donthackmeplz3', 'Coach'), " +
                "('Casper', 'casper@gmail.com', 'cappertheslapper', 'donthackmeplz4', 'Coach'), " +
                "('Oguz', 'oguz@gmail.com', 'oguztheboguz', 'donthackmeplz5', 'Coach'), " +
                "('Aleksander', 'aleksander@gmail.com', 'aleksalamander', 'donthackmeplz6', 'Coach');", con);

                cmd.ExecuteNonQuery();
            }

            employeeRepository.Reset();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            // drop all items
            SqlConnection con = new SqlConnection("Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true");
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM EMPLOYEE'");
                cmd.ExecuteNonQuery();
            }

        }

        [TestMethod]

        public void Test_Create()
        {
            //Arrange

            //Act
            
            //Assert
        }

        [TestMethod]

        public void Test_Retrieve()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]

        public void Test_RetrieveAll()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]

        public void Test_Load()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}