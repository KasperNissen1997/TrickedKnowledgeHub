using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class LearningObjectiveRepository : Repository
    {
        private List<LearningObjective> learningObjectives = new();

        public LearningObjectiveRepository(bool isTestRepository)
        {
            IsTestRepository = isTestRepository;

            Load();
        }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand cmd = new("SELECT * FROM LEARNINGSOBJECTIVE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string title = dr["LO_Title"].ToString();
                        string gameTitle = dr["G_Title"].ToString();

                        LearningObjective learningObjective = new(title);
                        learningObjectives.Add(learningObjective);

                        Game associatedGame = RepositoryManager.GameRepository.Retrieve(gameTitle);
                        associatedGame.LearningObjectives.Add(learningObjective);
                    }
                }
            }
        }

        public LearningObjective Create(string title, Game game)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LEARNINGOBJECTIVE (L_Title, G_Title)" + "VALUES(@L_Title, @G_Title)", con);

                cmd.Parameters.AddWithValue("@L_Title", title);
                cmd.Parameters.AddWithValue("@G_Title", game.Title);

                cmd.ExecuteNonQuery();

                LearningObjective learningObjective = new(title);

                game.LearningObjectives.Add(learningObjective);
                learningObjectives.Add(learningObjective);

                return learningObjective;
            }
        }

        public LearningObjective Retrive(string title)
        {
            foreach (LearningObjective learningObjective in learningObjectives)
            {
                if (learningObjective.Title == title)
                {
                    return learningObjective;
                }
            }

            throw new ArgumentException($"No learningObjective with title {title} found.");
        }

        public List<LearningObjective> RetrieveAll()
        {
            return new(learningObjectives);
        }
    }
}
