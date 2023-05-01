using Microsoft.Data.SqlClient;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;

namespace RepositoryTestProject
{
    [TestClass]
    public class GameRepositoryTester
    {
        private static string connectionString = "Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true";

        private GameRepository gameRepo = RepositoryManager.TestGameRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initilize the DB with a bunch of Games.
            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                SqlCommand cmd = new("INSERT INTO GAME (G_TITLE) VALUES " +
                    "('CS:GO'), " +
                    "('Valorant'), " +
                    "('FIFA'), " +
                    "('Rocket League');", con);
                cmd.ExecuteNonQuery();
            }

            gameRepo.Reset();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Remove all Game rows in the DB.
            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                SqlCommand cmd = new("DELETE FROM GAME;", con);
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void Test_Load()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsNotNull(gameRepo);
        }

        [TestMethod]
        public void Test_RetrieveAll()
        {
            // Arrange
            List<Game> retrievedGames;

            // Act
            retrievedGames = gameRepo.RetrieveAll();

            // Assert
            Assert.IsNotNull(retrievedGames);
        }

        public void Test_RetrieveAll_CorrectAmount()
        {
            // Arrange
            int correctAmountOfGames = 4;
            List<Game> retrievedGames;

            // Act
            retrievedGames = gameRepo.RetrieveAll();

            // Assert
            Assert.AreEqual(correctAmountOfGames, retrievedGames.Count);
        }

        public void Test_Retrieve()
        {
            // Arrange
            string gameTitle = "FIFA";
            int amountOfLearningObjectives = 0;
            Game retrievedGame;

            // Act
            retrievedGame = gameRepo.Retrieve(gameTitle);

            // Assert
            Assert.AreEqual(gameTitle, retrievedGame.Title);
            Assert.AreEqual(amountOfLearningObjectives, retrievedGame.LearningObjectives.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Retrieve_WrongTitle()
        {
            // Arrange
            string gameTitle = "Minecraft";

            // Act
            gameRepo.Retrieve(gameTitle);

            // Assert
        }
    }
}
