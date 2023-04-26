using Microsoft.Data.SqlClient;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;

namespace RepositoryTestProject
{
    [TestClass]
    public class LearningObjectiveRepositoryTester
    {
        private LearningObjectiveRepository learningObjectiveRepo;
        private GameRepository gameRepo;

        [ClassInitialize]
        public void ClassInitilize()
        {
            // Initilize the DB with a bunch of Games.
            using (SqlConnection con = new("Server=10.56.8.36; Database=NOT_SPECIFIED; User Id=NOT_SPECIFIED; Password=NOT_SPECIFIED; TrustServerCertificate=true"))
            {
                con.Open();

                SqlCommand cmd = new("DELETE FROM GAME; DELETE FROM LEARNINGOBJECTIVE;", con);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO GAME (G_TITLE) VALUES " +
                    "('CS:GO'), " +
                    "('Valorant'), " +
                    "('FIFA'), " +
                    "('Rocket League');";

                cmd.ExecuteNonQuery();
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Initilize the DB with a bunch of LearningObjectives.
            using (SqlConnection con = new("Server=10.56.8.36; Database=NOT_SPECIFIED; User Id=NOT_SPECIFIED; Password=NOT_SPECIFIED; TrustServerCertificate=true"))
            {
                con.Open();

                SqlCommand cmd = new("INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES " +
                    "('Aim', 'CS:GO'), " +
                    "('Utility usage', 'CS:GO'), " +
                    "('Map knowledge', 'CS:GO'), " +
                    "('Aim', 'Valorant'), " +
                    "('Team composition', 'Valorant'), " +
                    "('Passing', 'FIFA'), " +
                    "('Offense & Defense', 'FIFA'), " +
                    "('Offensive', 'Rocket League'), " +
                    "('Ball control', 'Rocket League');", con);

                cmd.ExecuteNonQuery();
            }

            learningObjectiveRepo = new();
            gameRepo = new();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Remove all LearningObjective rows in the DB.
            using (SqlConnection con = new("Server=10.56.8.36; Database=P3_DB_2023_06; User Id=P3_PROJECT_USER_06; Password=OPENDB_06; TrustServerCertificate=true"))
            {
                con.Open();

                SqlCommand cmd = new("DELETE FROM LEARNINGOBJECTIVE;", con);

                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void Test_Load() 
        { 
            // Arrange

            // Act

            // Assert
            Assert.IsNotNull(learningObjectiveRepo);
        }

        [TestMethod]
        public void Test_Create()
        {
            // Arrange
            string title = "Utility usage";
            Game valorantGame = gameRepo.Retrieve("Valorant");

            // Act
            LearningObjective utilityUsageLearningObjective = learningObjectiveRepo.Create(title, valorantGame);

            // Assert
            Assert.AreEqual("Title: Utility usage, FocusPoints: ", utilityUsageLearningObjective.ToString());
        }

        [TestMethod]
        public void Test_Create_IsAssociatedToGame()
        {
            // Arrange
            string title = "Utility usage";
            Game valorantGame = gameRepo.Retrieve("Valorant");

            // Act
            LearningObjective utilityUsageLearningObjective = learningObjectiveRepo.Create(title, valorantGame);

            // Assert
            Assert.AreEqual("Title: Valorant, FocusPoints: Aim, Team composition, Utility usage", valorantGame.ToString());
        }

        [TestMethod]
        public void Test_Retrieve()
        {
            // Arrange
            string title = "Passing";

            // Act
            LearningObjective passingLearningObjective = learningObjectiveRepo.Retrive(title);

            // Assert
            Assert.AreEqual("Title: Passing, FocusPoints: ", passingLearningObjective.ToString());
        }

        [TestMethod]
        public void Test_Retrieve_WrongTitle()
        {
            // Arrange
            string title = "Title that does not exist";

            // Act
            LearningObjective passingLearningObjective = learningObjectiveRepo.Retrive(title);

            // Assert
            Assert.IsNull(passingLearningObjective);
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
            List<LearningObjective> retrievedLearningObjectives;

            // Act
            retrievedLearningObjectives = learningObjectiveRepo.RetrieveAll();

            // Assert
            Assert.AreEqual(9, retrievedLearningObjectives.Count);
        }
    }
}
