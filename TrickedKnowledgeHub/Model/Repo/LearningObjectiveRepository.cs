using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;

namespace TrickedKnowledgeHub.Model.Repo
{
    internal class LearningObjectiveRepository : Repository
    {
        public string connectionString;

        private List<LearningObjective> learningObjectives = new List<LearningObjective>();

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public void Create(string title, Game game)
        {
            using (SqlConnection con = SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LEARNINGOBJECTIVE (L_Title, G_Title)" + "VALUES(@L_Title, @G_Title)", con)
                
                    cmd.Parameters.AddWithValue("@L_Title", title);
                    cmd.Parameters.AddWithValue("@_Title", title);
                

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

        public LearningObjective RetriveAll()
        {
            foreach (LearningObjective learningObjective in learningObjectives)
            {
                return new LearningObjective(learningObjective.Title);
            }

            throw new ArgumentException($"No more learningObjectives could be found  ");
        }

    }
}
