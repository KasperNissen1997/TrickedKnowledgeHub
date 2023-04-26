using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class ExerciseRepository : Repository
    {
        private List<Exercise> exerciseList = new();

        public ExerciseRepository()
        { 
            Load(); 
        }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EXERCISE INNER JOIN EXERCISE_FOCUSPOINT ON EXERCISE.ExerciseID = EXERCISE_FOCUSPOINT.ExerciseID;", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string Title = dr["Title"].ToString();
                        string description = dr["Description"].ToString();
                        byte[] Material = (byte[])dr["Material"];
                        DateTime Time = DateTime.Parse(dr["Time"].ToString());
                        string Mail = dr["Mail"].ToString();
                        string G_Title = dr["G_Title"].ToString();
                        Rating rating = (Rating)Enum.Parse(typeof(Rating), dr["Value"].ToString());
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
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EXERCISE (Title, Description, Material, Time, Mail, G_Title, Value)" +
                    "VALUES(@Title, @Description, @Material, @Time, @Mail, @G_Title, @Value)" + "SELECT @@IDENTITY", con);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = exercise.Title;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = exercise.Description;
                cmd.Parameters.Add("@Material", SqlDbType.VarBinary).Value = exercise.Material;
                cmd.Parameters.Add("@Time", SqlDbType.DateTime2).Value = exercise.Timestamp;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = exercise.Author.Mail;
                cmd.Parameters.Add("@G_Title", SqlDbType.NVarChar).Value = exercise.Game.Title;
                cmd.Parameters.Add("@Value", SqlDbType.Int).Value = (int)exercise.Rating;

                exercise.ExerciseID = Convert.ToInt32(cmd.ExecuteScalar());
                exerciseList.Add(exercise);
                SqlCommand command = new SqlCommand("INSERT INTO EXERCISE_FOCUSPOINT (ExerciseID, F_Title)" + // this code to bind IxerciseID and the selected FocusPoint
                                                "VALUES(@ExerciseID, @F_Title)" + "SELECT @@IDENTITY", con);
                command.Parameters.Add("@ExerciseID", SqlDbType.Int).Value = exercise.ExerciseID;
                command.Parameters.Add("@F_Title", SqlDbType.NVarChar).Value = exercise.FocusPoint.Title;
                command.ExecuteNonQuery();

                return exercise;
            }
        }

        public Exercise Retrieve(Exercise exercise)
        {
            foreach (Exercise ex in exerciseList)
            {
                if (exercise.ExerciseID == ex.ExerciseID)
                {
                    return ex;
                }
            }
            return null;
        }

        public Exercise RetrieveAll()
        {
            foreach (Exercise exercise in exerciseList)
            {
                return exercise;
            }
            return null;
        }

        #endregion
    }
}
