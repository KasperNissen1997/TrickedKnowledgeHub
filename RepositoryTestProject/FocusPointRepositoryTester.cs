using Microsoft.Data.SqlClient;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;

namespace RepositoryTestProject
{
    [TestClass]
    public class FocusPointRepositoryTester
    {
        private static string connectionString = "Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true";

        private GameRepository gameRepo = RepositoryManager.TestGameRepository;
        private LearningObjectiveRepository learningObjectiveRepo = RepositoryManager.TestLearningObjectiveRepository;
        private FocusPointRepository focusPointRepo = RepositoryManager.TestFocusPointRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initilize the DB with a bunch of FocusPoints.
            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                SqlCommand cmd = new("INSERT INTO GAME (G_TITLE) VALUES " +
                    "('CS:GO'), " +
                    "('Valorant');", con);
                cmd.ExecuteNonQuery();

                cmd = new("INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES " +
                    "('Aim', 'CS:GO'), " +
                    "('Utility usage', 'CS:GO'), " +
                    "('Aim', 'Valorant'), " +
                    "('Team composition', 'Valorant');", con);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO FOCUSPOINT (F_Title, LO_ID) VALUES " +
                    "('Spray', 1), " +
                    "('Peek', 1), " +
                    "('Tap', 1), " +
                    "('Reaction time', 1), " +
                    "('Smokes (D2)', 2), " +
                    "('Pop flashes (D2)', 2)," +
                    "('Spray', 3), " +
                    "('Tap', 3), " +
                    "('Roles and Responsibility', 4);";
                cmd.ExecuteNonQuery();
            }

            gameRepo.Reset();
            learningObjectiveRepo.Reset();
            focusPointRepo.Reset();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Remove all FocusPoint rows in the DB.
            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                SqlCommand cmd = new("DELETE FROM FOCUSPOINT;", con);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM LEARNINGOBJECTIVE;";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM GAME;";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DBCC CHECKIDENT ('LEARNINGOBJECTIVE', RESEED, 0);";
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void Test_Load() 
        { 
            // Arrange

            // Act

            // Assert
            Assert.IsNotNull(focusPointRepo);
        }

        [TestMethod]
        public void Test_Create()
        {
            // Arrange
            string title = "Raw aim";
            LearningObjective valorantAimLearningObjective = learningObjectiveRepo.Retrieve(3);

            // Act
            FocusPoint rawAimFocusPoint = focusPointRepo.Create(title, valorantAimLearningObjective);

            // Assert
            Assert.AreEqual("Title: Raw aim, Parent: 3", rawAimFocusPoint.ToString());
        }

        [TestMethod]
        public void Test_Create_IsAssociatedToFocusPoint()
        {
            // Arrange
            string title = "Raw aim";
            LearningObjective csgoAimLearningObjective = learningObjectiveRepo.Retrieve(1);

            // Act
            FocusPoint newFocusPoint = focusPointRepo.Create(title, csgoAimLearningObjective);

            // Assert
            Assert.AreEqual("ID: 1, Title: Aim, Parent: CS:GO, FocusPoints: Peek, Reaction time, Spray, Tap, Raw aim", csgoAimLearningObjective.ToString());
        }

        [TestMethod]
        public void Test_Retrieve()
        {
            // Arrange
            string title = "Pop flashes (D2)";

            // Act
            FocusPoint popFlashesD2FocusPoint = focusPointRepo.Retrieve(title);

            // Assert
            Assert.AreEqual("Title: Pop flashes (D2), Parent: 2", popFlashesD2FocusPoint.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Retrieve_WrongID()
        {
            // Arrange
            int invalidID = 0;

            // Act
            learningObjectiveRepo.Retrieve(invalidID);

            // Assert
        }

        [TestMethod]
        public void Test_RetrieveAll()
        {
            // Arrange
            List<LearningObjective> retrievedLearningObjectives;

            // Act
            retrievedLearningObjectives = learningObjectiveRepo.RetrieveAll();

            // Assert
            Assert.IsNotNull(retrievedLearningObjectives);
        }

        [TestMethod]
        public void Test_RetrieveAll_CorrectAmount()
        {
            // Arrange
            List<FocusPoint> retrievedFocusPoints;

            // Act
            retrievedFocusPoints = focusPointRepo.RetrieveAll();

            // Assert
            Assert.AreEqual(9, retrievedFocusPoints.Count);
        }
    }
}
