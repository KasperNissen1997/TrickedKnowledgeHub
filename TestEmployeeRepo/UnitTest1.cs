using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
namespace TestEmployeeRepo
{
    [TestClass]
    public class UnitTest1
    {
        
        EmployeeRepository employeeRepository = new EmployeeRepository();
        List<Employee> employees = new List<Employee>();
        [TestMethod]
        public void TestRetrieveAll()
        {
            employees = employeeRepository.RetrieveAll();

            Assert.AreEqual("Nikolaj, Nikko@gmail.com, nikko, DDR100best, Teacher", employees[0]);
        }
            
    }
}