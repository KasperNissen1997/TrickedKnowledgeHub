using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.Model;
using Microsoft.Data.SqlClient;

namespace RepositoryTestProject
{
    [TestClass]
    public class ExerciseRepoTest
    {
        private string connectionString = "Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true;";

        EmployeeRepository employeeRepo = RepositoryManager.TestEmployeeRepository;
        GameRepository gameRepo = RepositoryManager.TestGameRepository;
        LearningObjectiveRepository learningObjectiveRepo = RepositoryManager.TestLearningObjectiveRepository;
        FocusPointRepository focusPointRepo = RepositoryManager.TestFocusPointRepository;
        ExerciseRepository exerciseRepo = RepositoryManager.TestExerciseRepository;

        [TestInitialize] 
        public void TestInitialize() 
        {
            using(SqlConnection con = new(connectionString))
            {
                con.Open();

                // Insert the Type data.
                SqlCommand cmd = new("INSERT INTO TYPE (Type) VALUES " +
                    "('Coach')", con);
                cmd.ExecuteNonQuery();

                // Insert the dummy Employee data.
                cmd.CommandText = "INSERT INTO EMPLOYEE (Mail, Name, Nickname, Password, Type) VALUES " +
                    "('test@test.com', 'NameTest', 'NickTest', 'PassTest', 'Coach')";
                cmd.ExecuteNonQuery();

                // Insert the dummy Game data.
                cmd.CommandText = "INSERT INTO GAME (G_Title) VALUES " +
                    "('CS:GO')";
                cmd.ExecuteNonQuery();

                // Insert the Rating data.
                cmd.CommandText = "INSERT INTO RATING (Value) VALUES " +
                    "(1)";
                cmd.ExecuteNonQuery();

                // Insert the dummy Learningobjective data.
                cmd.CommandText = "INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES " +
                    "('Aim', 'CS:GO'), " +
                    "('Utility Usage', 'CS:GO')";
                cmd.ExecuteNonQuery();

                // Insert the dummy Focuspoint data.
                cmd.CommandText = "INSERT INTO FOCUSPOINT (F_Title, LO_ID) VALUES " +
                    "('Spray', 1), " +
                    "('Smokes (D2)', 2)";
                cmd.ExecuteNonQuery();

                // Insert the dummy Exercise data.
                cmd.CommandText = "INSERT INTO EXERCISE (Title, Description, Material, Timestamp, Mail, G_Title, Value) VALUES " +
                    "('TitleTest', 'DescTest', 11010100, '26-04-2023 20:00:00', 'test@test.com', 'CS:GO', 1), " +
                    "('Aim Control', 'How to aim like', 10110101, '25-04-2023 19:15:34', 'test@test.com', 'CS:GO', 1)";
                cmd.ExecuteNonQuery();

                // Insert the dummy Exercise_Focuspoint data.
                cmd.CommandText = "INSERT INTO EXERCISE_FOCUSPOINT (E_ID, F_Title, LO_ID) VALUES " +
                    "(1, 'Spray', 1), " +
                    "(2, 'Smokes (D2)', 2)";
                cmd.ExecuteNonQuery();
            }

            employeeRepo.Reset();
            gameRepo.Reset();
            learningObjectiveRepo.Reset();
            focusPointRepo.Reset();
            exerciseRepo.Reset();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            using(SqlConnection con = new(connectionString))
            {
                con.Open();

                // These statements deletes all the data from all the tables in the DB.
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

                // Reset identity for the LEARNINGOBJECTIVE table.
                cmd.CommandText = "DBCC CHECKIDENT ('LEARNINGOBJECTIVE', RESEED, 0);";
                cmd.ExecuteNonQuery();

                // Reset identity for the EXERCISE table.
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
            Assert.IsNotNull(exerciseRepo);
        }

        [TestMethod]
        public void Test_Create()
        {
            //Arrange
            string title = "testExercise";
            string description = "This is my test exercise.";
            byte[] material = new byte[10];
            DateTime timestamp = new(1, 1, 1);
            Employee author = employeeRepo.Retrieve("test@test.com");
            Game game = gameRepo.Retrieve("CS:GO");
            LearningObjective learningObjective = learningObjectiveRepo.Retrieve(1);
            FocusPoint focusPoint = focusPointRepo.Retrieve("Spray");
            Rating rating = Rating.Begynder;

            //Act
            Exercise exercise = exerciseRepo.Create(title, description, material, timestamp, author, game, focusPoint, rating);

            // TODO: Remove this before pushing to main...
            // Fuck you if you think multiple asserts are bad
            // https://softwareengineering.stackexchange.com/questions/7823/is-it-ok-to-have-multiple-asserts-in-a-single-unit-test

            //Assert
            Assert.AreEqual(3, exercise.ExerciseID);
            Assert.AreEqual(title, exercise.Title);
            Assert.AreEqual(description, exercise.Description);
            Assert.AreEqual(material, exercise.Material);
            Assert.AreEqual(timestamp, exercise.Timestamp);
            Assert.AreEqual(author, exercise.Author);
            Assert.AreEqual(game, exercise.Game);
            Assert.AreEqual(focusPoint, exercise.FocusPoint);
            Assert.AreEqual(rating, exercise.Rating);
        }

        [TestMethod]
        public void Test_Retrieve()
        {
            // Arrange
            int id = 1;
            string title = "TitleTest";
            string description = "DescTest";
            // Don't test on the material, as the byte array is a reference type without an intuitive IsEqual method implemented.
            // byte[] material = new byte[8]
            // {
            //     1, 1, 0, 1, 0, 1, 0, 0
            // };
            DateTime timestamp = new(2023, 4, 26, 20, 0, 0);
            Employee author = employeeRepo.Retrieve("test@test.com");
            Game game = gameRepo.Retrieve("CS:GO");
            LearningObjective learningObjective = learningObjectiveRepo.Retrieve(1);
            FocusPoint focusPoint = focusPointRepo.Retrieve("Spray");
            Rating rating = Rating.Begynder;

            // Act
            Exercise exercise = exerciseRepo.Retrieve(id);

            // Assert
            Assert.AreEqual(1, exercise.ExerciseID);
            Assert.AreEqual(title, exercise.Title);
            Assert.AreEqual(description, exercise.Description);
            // Assert.AreEqual(material, exercise.Material);
            Assert.AreEqual(timestamp, exercise.Timestamp);
            Assert.AreEqual(author, exercise.Author);
            Assert.AreEqual(game, exercise.Game);
            Assert.AreEqual(focusPoint, exercise.FocusPoint);
            Assert.AreEqual(rating, exercise.Rating);
        }

        [TestMethod]
        public void Test_RetrieveAll()
        {
            //Arrange
            List<Exercise> retrievedExercises;

            //Act
            retrievedExercises = exerciseRepo.RetrieveAll();

            //Assert
            Assert.IsNotNull(retrievedExercises);
        }

        [TestMethod]
        public void Test_RetrieveAll_CorrectAmount()
        {
            //Arrange
            List<Exercise> retrievedExercises;

            //Act
            retrievedExercises = exerciseRepo.RetrieveAll();

            //Assert
            Assert.AreEqual(2, retrievedExercises.Count);
        }
    }
}