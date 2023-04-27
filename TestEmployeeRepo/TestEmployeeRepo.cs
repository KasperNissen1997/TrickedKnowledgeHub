using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
using Microsoft.Data.SqlClient;

namespace TestEmployeeRepo
{
    [TestClass]
    public class TestEmployeeRepo
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        List<Employee> employees1 = new List<Employee>();
        List<Employee> employees2 = new List<Employee>();

        //[TestInitialize]

        public void TestInitialize()
        {
            // create new items
            employeeRepository.Create("Nikolai", "nikolai@gmail.com", "nikolaikiller123", "donthackmeplz1", EmployeeType.Teacher);
            employeeRepository.Create("Jonathan", "jonathan@gmail.com", "jonathantheman", "donthackmeplz2", EmployeeType.Teacher);
            employeeRepository.Create("Kasper", "kasper@gmail.com", "kasperthemaster", "donthackmeplz3", EmployeeType.Teacher);
            employeeRepository.Create("Casper", "casper@gmail.com", "cappertheslapper", "donthackmeplz4", EmployeeType.Teacher);
            employeeRepository.Create("Oguz", "oguz@gmail.com", "oguztheboguz", "donthackmeplz5", EmployeeType.Teacher);
            employeeRepository.Create("Aleksander", "aleksander@gmail.com", "aleksalamander", "donthackmeplz6", EmployeeType.Teacher);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // drop all items
            SqlConnection con = new SqlConnection("Server=10.56.8.36; Database=P3_DB_2023_06; User Id=P3_PROJECT_USER_06; Password=OPENDB_06; TrustServerCertificate=true");
            {

                con.Open();
                for( int i = 0; i < 6; i++)
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM EMPLOYEE WHERE Password = 'donthackmeplz{i}'");
                    cmd.ExecuteNonQuery();
                }

            }
            con.Close();

        }


        [TestMethod]
        public void TestRetrieve()
        {
            employees2.Add(employeeRepository.Retrieve("kasper@gmail.com"));

            Assert.AreEqual("kasper@gmail.com, Kasper, kasperthemaster, donthackmeplz3, Teacher", employees2[0]);
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            employees1 = employeeRepository.RetrieveAll();

            Assert.AreEqual("Nikolaj, Nikko@gmail.dk, nikko, DDR100best, Teacher", employees1[0]);
        }
    }
}