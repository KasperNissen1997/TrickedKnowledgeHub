using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class ExerciseRepository : Repository
    {
        private List<Exercise> exerciseList = new();

        public ExerciseRepository() { Load(); }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EXERCISE INNER JOIN EXERCISE_FOCUSPOINT ON EXERCISE.ID = EXERCISE_FOCUSPOINT.E_ID;", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        string Title = dr["Title"].ToString();
                        string description = dr["Description"].ToString();
                        byte[] Material = (byte[]) dr["Material"];
                        DateTime Time = DateTime.Parse(dr["Time"].ToString());
                        string Mail = dr["Mail"].ToString();
                        string G_Title = dr["G_Title"].ToString();
                        Rating rating = (Rating) Enum.Parse(typeof(Rating), dr["Value"].ToString());
                        string F_Title = dr["F_Title"].ToString();

                        Employee associatedEmployee = RepositoryManager.EmployeeRepository.Retrieve(Mail);
                        Game associatedGame = RepositoryManager.GameRepository.Retrieve(G_Title);
                        FocusPoint associatedFocusPoint = RepositoryManager.FocusPointRepository.Retrieve(F_Title);

                        Exercise exercise = new(Title, description, Material, Time, associatedEmployee, associatedGame, associatedFocusPoint, rating);
                        
                        exerciseList.Add(exercise);
                    }
                }
            }
        }

        #region CRUD
        public Exercise Create(Exercise exercise)
        {
            using(SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EXERCISE (Title, Description, Material, Timestamp, Mail, G_Title, Value)" +
                    "VALUES(@Title, @Description, @Material, @Timestamp, @Mail, @G_Title, @Value)" + "SELECT @@IDENTITY", con);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = exercise.Title;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = exercise.Description;
                cmd.Parameters.Add("@Material", SqlDbType.VarBinary).Value = exercise.Material;
                cmd.Parameters.Add("@Timestamp", SqlDbType.DateTime2).Value = exercise.Timestamp;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = exercise.Author.Mail;
                cmd.Parameters.Add("@G_Title", SqlDbType.NVarChar).Value = exercise.Game.Title;
                cmd.Parameters.Add("@Value", SqlDbType.Int).Value = (int) exercise.Rating;

                exercise.ExerciseID = Convert.ToInt32(cmd.ExecuteScalar());
                exerciseList.Add(exercise);
                return exercise;
            }
        }

        public Exercise Retrieve(int id)
        {
            foreach(Exercise ex in exerciseList)
            {
                if(id == ex.ExerciseID)
                {
                    return ex;
                }
            }
            return null;
        }

        public List<Exercise> RetrieveAll()
        {
            return new(exerciseList);
        }

        #endregion
    }
}
