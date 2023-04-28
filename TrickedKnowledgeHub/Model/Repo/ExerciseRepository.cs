using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class ExerciseRepository : Repository
    {
        private List<Exercise> exerciseList = new();

        public ExerciseRepository(bool isTestRepository = false)
        { 
            IsTestRepository = isTestRepository;

            Load(); 
        }


        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EXERCISE INNER JOIN EXERCISE_FOCUSPOINT ON EXERCISE.ID = EXERCISE_FOCUSPOINT.E_ID;", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int ID = Convert.ToInt32(dr["ID"]);
                        string Title = dr["Title"].ToString();
                        string description = dr["Description"].ToString();
                        byte[] Material = (byte[])dr["Material"];
                        DateTime Time = DateTime.Parse(dr["Timestamp"].ToString());
                        string Mail = dr["Mail"].ToString();
                        string G_Title = dr["G_Title"].ToString();
                        Rating rating = (Rating)Enum.Parse(typeof(Rating), dr["Value"].ToString());
                        string F_Title = dr["F_Title"].ToString();

                        Employee associatedEmployee;
                        Game associatedGame;
                        FocusPoint associatedFocusPoint;

                        if (IsTestRepository)
                        {
                            associatedEmployee = RepositoryManager.TestEmployeeRepository.Retrieve(Mail);
                            associatedGame = RepositoryManager.TestGameRepository.Retrieve(G_Title);
                            associatedFocusPoint = RepositoryManager.TestFocusPointRepository.Retrieve(F_Title);
                        }
                        else
                        {
                            associatedEmployee = RepositoryManager.EmployeeRepository.Retrieve(Mail);
                            associatedGame = RepositoryManager.GameRepository.Retrieve(G_Title);
                            associatedFocusPoint = RepositoryManager.FocusPointRepository.Retrieve(F_Title);
                        }

                        Exercise exercise = new(ID, Title, description, Material, Time, associatedEmployee, associatedGame, associatedFocusPoint, rating);

                        exerciseList.Add(exercise);
                    }
                }
            }
        }

        public void Reset()
        {
            exerciseList.Clear();
            Load();
        }

        #region CRUD
        public Exercise Create(Exercise exercise)
        {
            using (SqlConnection con = GetConnection())
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


                SqlCommand command = new SqlCommand("INSERT INTO EXERCISE_FOCUSPOINT (E_ID, F_Title, LO_ID)" + // this code to bind ExerciseID and the selected FocusPoint
                                                "VALUES(@E_ID, @F_Title, @LO_ID)", con);
                command.Parameters.Add("@E_ID", SqlDbType.Int).Value = exercise.ExerciseID;
                command.Parameters.Add("@F_Title", SqlDbType.NVarChar).Value = exercise.FocusPoint.Title;
                command.Parameters.Add("@LO_ID", SqlDbType.Int).Value = exercise.FocusPoint.Parent.ID;
                command.ExecuteNonQuery();

                return exercise;
            }
        }

        public Exercise Retrieve(int id)
        {
            foreach (Exercise ex in exerciseList)
            {
                if(id == ex.ExerciseID)
                {
                    return ex;
                }
            }
            throw new ArgumentException($"No Exercise with id {id} found.");
        }

        public List<Exercise> RetrieveAll()
        {
            return new(exerciseList);
        }

        #endregion
    }
}
