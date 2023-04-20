using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
namespace TestEmployeeRepo
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        List<Employee> employees1 = new List<Employee>();
        List<Employee> employees2 = new List<Employee>();

        [TestMethod]
        public void TestRetrieve()
        {
            employees2.Add(employeeRepository.Retrieve("Nikko@gmail.dk"));

            Assert.AreEqual("Nikolaj, Nikko@gmail.dk, nikko, DDR100best, Teacher", employees2[0]);
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            employees1 = employeeRepository.RetrieveAll();

            Assert.AreEqual("Nikolaj, Nikko@gmail.dk, nikko, DDR100best, Teacher", employees1[0]);
        }



    }
}