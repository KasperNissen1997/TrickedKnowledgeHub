using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
using Microsoft.Data.SqlClient;

namespace RepositoryTestProject
{
    [TestClass]
    public class EmployeeRepoTester
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        List<Employee> employees1 = new List<Employee>();
        List<Employee> employees2 = new List<Employee>();

        [TestInitialize]

        public void TestInitialize()
        {
            // create new items
            employeeRepository.Create("Nikolai", "nikolai@gmail.com", "nikolaikiller123", "donthackmeplz1", EmployeeType.Coach);
            employeeRepository.Create("Jonathan", "jonathan@gmail.com", "jonathantheman", "donthackmeplz2", EmployeeType.Coach);
            employeeRepository.Create("Kasper", "kasper@gmail.com", "kasperthemaster", "donthackmeplz3", EmployeeType.Coach);
            employeeRepository.Create("Casper", "casper@gmail.com", "cappertheslapper", "donthackmeplz4", EmployeeType.Coach);
            employeeRepository.Create("Oguz", "oguz@gmail.com", "oguztheboguz", "donthackmeplz5", EmployeeType.Coach);
            employeeRepository.Create("Aleksander", "aleksander@gmail.com", "aleksalamander", "donthackmeplz6", EmployeeType.Coach);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // drop all items
            SqlConnection con = new SqlConnection("Server=10.56.8.36; Database=P3_DB_2023_06; User Id=P3_PROJECT_USER_06; Password=OPENDB_06; TrustServerCertificate=true");
            {

                con.Open();
                for (int i = 0; i < 6; i++)
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM EMPLOYEE WHERE Password = 'donthackmeplz{i}'");
                    cmd.ExecuteNonQuery();
                }

            }

        }


        [TestMethod]
        public void TestRetrieve()
        {
            employees2.Add(employeeRepository.Retrieve("kasper@gmail.com"));

            Assert.AreEqual("kasper@gmail.com, Kasper, kasperthemaster, donthackmeplz3, Coach", employees2[0]);
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            employees1 = employeeRepository.RetrieveAll();

            Assert.AreEqual("Nikolai, nikolai@gmail.com, nikolaikiller123, donthackmeplz1, Coach", employees1[0]);


        }
    }
}