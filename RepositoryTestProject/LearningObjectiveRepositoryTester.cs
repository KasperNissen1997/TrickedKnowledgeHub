﻿using Microsoft.Data.SqlClient;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Persistence;

namespace RepositoryTestProject
{
    [TestClass]
    public class LearningObjectiveRepositoryTester
    {
        private static string connectionString = "Server=10.56.8.36; Database=DB_2023_35; User Id=STUDENT_35; Password=OPENDB_35; TrustServerCertificate=true";

        private GameRepository gameRepo = RepositoryManager.TestGameRepository;
        private LearningObjectiveRepository learningObjectiveRepo = RepositoryManager.TestLearningObjectiveRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initilize the DB with a bunch of LearningObjectives.
            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                SqlCommand cmd = new("INSERT INTO GAME (G_TITLE) VALUES " +
                    "('CS:GO'), " +
                    "('Valorant'), " +
                    "('FIFA'), " +
                    "('Rocket League');", con);
                cmd.ExecuteNonQuery();

                cmd = new("INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES " +
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

            gameRepo.Reset();
            learningObjectiveRepo.Reset();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Remove all LearningObjective rows in the DB.
            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                SqlCommand cmd = new("DELETE FROM LEARNINGOBJECTIVE;", con);
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
            Assert.AreEqual("ID: 10, Title: Utility usage, Parent: Valorant, FocusPoints: ", utilityUsageLearningObjective.ToString());
        }

        [TestMethod]
        public void Test_Create_IsAssociatedToGame()
        {
            // Arrange
            string title = "Utility usage";
            Game valorantGame = gameRepo.Retrieve("Valorant");

            // Act
            learningObjectiveRepo.Create(title, valorantGame);

            // Assert
            Assert.AreEqual("Title: Valorant, LearningObjectives: Aim, Team composition, Utility usage", valorantGame.ToString());
        }

        [TestMethod]
        public void Test_Retrieve()
        {
            // Arrange
            int passingLearningObjectiveID = 6;

            // Act
            LearningObjective passingLearningObjective = learningObjectiveRepo.Retrieve(passingLearningObjectiveID);

            // Assert
            Assert.AreEqual("ID: 6, Title: Passing, Parent: FIFA, FocusPoints: ", passingLearningObjective.ToString());
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
            List<LearningObjective> retrievedLearningObjectives;

            // Act
            retrievedLearningObjectives = learningObjectiveRepo.RetrieveAll();

            // Assert
            Assert.AreEqual(9, retrievedLearningObjectives.Count);
        }
    }
}
