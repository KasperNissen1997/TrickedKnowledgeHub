using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class LearningObjectiveRepository : Repository
    {
        private List<LearningObjective> learningObjectives = new();

        public LearningObjectiveRepository(bool isTestRepository = false)
        {
            IsTestRepository = isTestRepository;

            Load();
        }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand cmd = new("SELECT * FROM LEARNINGOBJECTIVE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["ID"].ToString());
                        string title = dr["LO_Title"].ToString();
                        string gameTitle = dr["G_Title"].ToString();

                        LearningObjective learningObjective = new(id, title);
                        learningObjectives.Add(learningObjective);

                        Game associatedGame;

                        if (IsTestRepository)
                            associatedGame = RepositoryManager.TestGameRepository.Retrieve(gameTitle);
                        else
                            associatedGame = RepositoryManager.GameRepository.Retrieve(gameTitle);

                        associatedGame.LearningObjectives.Add(learningObjective);
                    }
                }
            }
        }

        public void Reset()
        {
            learningObjectives.Clear();

            Load();
        }

        public LearningObjective Create(string title, Game game)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES (@LO_Title, @G_Title) SELECT @@IDENTITY", con);

                cmd.Parameters.AddWithValue("@LO_Title", title);
                cmd.Parameters.AddWithValue("@G_Title", game.Title);

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                LearningObjective learningObjective = new(id, title);

                game.LearningObjectives.Add(learningObjective);
                learningObjectives.Add(learningObjective);

                return learningObjective;
            }
        }

        public LearningObjective Retrive(int id)
        {
            foreach (LearningObjective learningObjective in learningObjectives)
                if (learningObjective.ID == id)
                    return learningObjective;

            throw new ArgumentException($"No learningObjective with title {id} found.");
        }

        public List<LearningObjective> RetrieveAll()
        {
            return new(learningObjectives);
        }
    }
}
