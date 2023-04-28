using TrickedKnowledgeHub;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
using System.Security.Cryptography.Xml;
using Microsoft.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Documents;

namespace TestEmployeeRepo
{
    [TestClass]
    public class ExerciseRepoTest
    {
        private string connectionString = "Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true;";

        ExerciseRepository exerciseRepository = new ExerciseRepository(true);

        int id = 1;
        string title = "exerciseTitle";
        string description = "exerciseDesc";
        Byte[] material = new byte[10];
        DateTime timestamp = DateTime.Now;
        Employee employee = new Employee("NameTest", "test@test.com", "NickTest", "PassTest", EmployeeType.Coach);
        Game game = new Game("CS:GO");


        [TestInitialize] 
        public void TestInitialize() 
        {
            //Initialize database with exercises
            using(SqlConnection con = new(connectionString))
            {
                con.Open();

                //These statements deletes all data from all tables
                SqlCommand cmd = new("EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?';", con);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'SET QUOTED_IDENTIFIER ON; DELETE FROM ?';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'";
                cmd.ExecuteNonQuery();



                //Reset identity for LEARNINGOBJECTIVE
                cmd.CommandText = "DBCC CHECKIDENT ('LEARNINGOBJECTIVE', RESEED, 0);";
                cmd.ExecuteNonQuery();

                //Reset identity for EXERCISE
                cmd.CommandText = "DBCC CHECKIDENT ('EXERCISE', RESEED, 0);";
                cmd.ExecuteNonQuery();



                //Insert type
                cmd.CommandText = "INSERT INTO TYPE (Type) VALUES " +
                    "('Coach')";
                cmd.ExecuteNonQuery();

                //Insert Employee
                cmd.CommandText = "INSERT INTO EMPLOYEE (Mail, Name, Nickname, Password, Type) VALUES " +
                    "('test@test.com', 'NameTest', 'NickTest', 'PassTest', 'Coach')";
                cmd.ExecuteNonQuery();

                //Insert Game
                cmd.CommandText = "INSERT INTO GAME (G_Title) VALUES " +
                    "('CS:GO')";
                cmd.ExecuteNonQuery();

                //Insert Rating
                cmd.CommandText = "INSERT INTO RATING (Value) VALUES " +
                    "(1)";
                cmd.ExecuteNonQuery();

                //Insert Learningobjective
                cmd.CommandText = "INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES " +
                    "('Aim', 'CS:GO'), " +
                    "('Utility Usage', 'CS:GO')";
                cmd.ExecuteNonQuery();

                //Insert Focuspoint
                cmd.CommandText = "INSERT INTO FOCUSPOINT (F_Title, LO_ID) VALUES " +
                    "('Spray', 1), " +
                    "('Smokes (D2)', 2)";
                cmd.ExecuteNonQuery();

                //Insert exercises
                cmd.CommandText = "INSERT INTO EXERCISE (Title, Description, Material, Timestamp, Mail, G_Title, Value) VALUES " +
                    "('TitleTest', 'DescTest', 1101010, '26-04-2023 20:00:00', 'test@test.com', 'CS:GO', 1), " +
                    "('Aim Control', 'How to aim like', 1011010, '25-04-2023 19:15:34', 'test@test.com', 'CS:GO', 1)";
                cmd.ExecuteNonQuery();

                //Insert Exercise_focuspoint
                cmd.CommandText = "INSERT INTO EXERCISE_FOCUSPOINT (E_ID, F_Title, LO_ID) VALUES " +
                    "(1, 'Spray', 1), " +
                    "(2, 'Smokes (D2)', 2)";
                cmd.ExecuteNonQuery();
            }

            exerciseRepository.Reset();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            //Remove Exercises from database
            using(SqlConnection con = new(connectionString))
            {
                con.Open();

                //These statements deletes all data from all tables
                SqlCommand cmd = new("EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?';", con);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'SET QUOTED_IDENTIFIER ON; DELETE FROM ?';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'";
                cmd.ExecuteNonQuery();

                //Reset identity for LEARNINGOBJECTIVE
                cmd.CommandText = "DBCC CHECKIDENT ('LEARNINGOBJECTIVE', RESEED, 0);";
                cmd.ExecuteNonQuery();

                //Reset identity for EXERCISE
                cmd.CommandText = "DBCC CHECKIDENT ('EXERCISE', RESEED, 0);";
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestLoad()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(exerciseRepository);
        }

        [TestMethod]
        public void Test_Create()
        {
            //Arrange
            LearningObjective learningObjective = new LearningObjective(1, "Aim", game);
            FocusPoint focusPoint = new FocusPoint("Spray", learningObjective);
            Exercise ex1 = new(id, title, description, material, timestamp, employee, game, focusPoint, Rating.Begynder);

            //Act
            Exercise exercise = exerciseRepository.Create(ex1);

            //Assert
            Assert.AreEqual(ex1.ToString(), exercise.ToString());
        }

        [TestMethod]
        public void Test_Retrieve()
        {
            //Arrange
            int Id = 1;

            //Act
            Exercise exercise = exerciseRepository.Retrieve(Id);

            //Assert
            Assert.AreEqual("1, TitleTest, DescTest, System.Byte[], 26-04-2023 20:00:00, test@test.com, NameTest, NickTest, PassTest, Coach, Title: CS:GO, LearningObjectives: Aim, Utility Usage, Spray, Begynder", exercise.ToString());
        }

        [TestMethod]
        public void Test_RetrieveAll()
        {
            //Arrange
            List<Exercise> retrievedExercises;

            //Act
            retrievedExercises = exerciseRepository.RetrieveAll();

            //Assert
            Assert.IsNotNull(retrievedExercises);
        }

        [TestMethod]
        public void Test_RetrieveAll_CorrectAmount()
        {
            //Arrange
            List<Exercise> retrievedExercises;

            //Act
            retrievedExercises = exerciseRepository.RetrieveAll();

            //Assert
            Assert.AreEqual(2, retrievedExercises.Count);
        }
    }
}