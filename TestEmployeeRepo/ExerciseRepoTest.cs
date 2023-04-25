using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
using System.Security.Cryptography.Xml;

namespace TestEmployeeRepo
{
    [TestClass]
    public class ExerciseRepoTest
    {
        Exercise ex1, ex2;
        ExerciseRepository exerciseRepository = new ExerciseRepository();

        
        DateTime dateTime = DateTime.Now;
        Employee employee = new Employee("Oguz","oguz@gmail.com", "oguzthebogus", "donthackmeplz5", EmployeeType.Teacher);
        Game game = new Game("CSGO");
        FocusPoint focusPoint = new FocusPoint("Aim");
        Byte[] bytes = new byte[10];
        

        [TestInitialize] 
        public void Initialize() 
        {
            ex1 = new Exercise("DELETE THIS", "DELETE THIS", bytes, dateTime, employee,
                game, focusPoint, Rating.LetØvet);
            ex2 = new Exercise("DELETE THIS", "DELETE THIS", bytes, dateTime, employee,
                game, focusPoint, Rating.LetØvet);
        }

        List<Exercise> exerciseList = new List<Exercise>();
        List<Exercise> retrieveAllList= new List<Exercise>();



        [TestMethod]
        public void CreateExercise()
        {
            //Act
            exerciseRepository.Create(ex1);

            Exercise temp = exerciseRepository.Retrieve(ex1.ExerciseID);

            //Assert
            Assert.IsNotNull(temp.ExerciseID);
        }

        [TestMethod]
        public void RetrieveExercise()
        {
            //Arrance
            exerciseList.Add(ex1);

            //Act
            exerciseList.Add(exerciseRepository.Retrieve(0));

            //Assert
            Assert.AreEqual("DELETE THIS", exerciseList[0].Title);
        }

        [TestMethod]
        public void RetrieveAllExercises()
        {
            //Arrange
            exerciseList.Add(ex1);
            exerciseList.Add(ex2);

            //Act
            retrieveAllList = exerciseRepository.RetrieveAll();

            //Assert
            Assert.AreEqual(exerciseList.Count, retrieveAllList.Count);
        }
    }
}