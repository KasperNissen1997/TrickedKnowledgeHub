using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Persistence
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

                        Game? parent;

                        if (IsTestRepository)
                            parent = RepositoryManager.TestGameRepository.Retrieve(gameTitle);
                        else
                            parent = RepositoryManager.GameRepository.Retrieve(gameTitle);


                        LearningObjective learningObjective = new(id, title, parent);
                        
                        if(parent != null)
                        parent.LearningObjectives.Add(learningObjective);

                        learningObjectives.Add(learningObjective);
                    }
                }
            }
        }

        public void Reset()
        {
            learningObjectives.Clear();

            Load();
        }

        public LearningObjective Create(string title, Game? parent)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LEARNINGOBJECTIVE (LO_Title, G_Title) VALUES (@LO_Title, @G_Title) SELECT @@IDENTITY", con);

                cmd.Parameters.AddWithValue("@LO_Title", title);
                cmd.Parameters.AddWithValue("@G_Title", parent.Title);

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                LearningObjective learningObjective = new(id, title, parent);

                parent.LearningObjectives.Add(learningObjective);
                learningObjectives.Add(learningObjective);

                return learningObjective;
            }
        }

        public LearningObjective Retrieve(int id)
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
