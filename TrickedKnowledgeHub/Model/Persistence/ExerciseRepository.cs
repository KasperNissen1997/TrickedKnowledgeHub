using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace TrickedKnowledgeHub.Model.Persistence
{
    public class ExerciseRepository : Repository
    {
        private ObservableCollection<Exercise> exerciseList = new();

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
                        string? G_Title = dr["G_Title"].ToString();
                        string? ratingString = dr["Value"].ToString();
                        Rating? rating = null;
                        if (!string.IsNullOrEmpty(ratingString)) { rating = (Rating)Enum.Parse(typeof(Rating), ratingString); } //If the ratingString is not null then set the rating to the converted Enum type
                        string F_Title = dr["F_Title"].ToString();
                        int LO_ID = Int32.Parse(dr["LO_ID"].ToString());

                        Employee associatedEmployee;
                        Game? associatedGame;
                        FocusPoint associatedFocusPoint;

                        if (IsTestRepository)
                        {
                            associatedEmployee = RepositoryManager.TestEmployeeRepository.Retrieve(Mail);
                            associatedGame = RepositoryManager.TestGameRepository.Retrieve(G_Title);
                            associatedFocusPoint = RepositoryManager.TestFocusPointRepository.Retrieve(F_Title, LO_ID);
                        }
                        else
                        {
                            associatedEmployee = RepositoryManager.EmployeeRepository.Retrieve(Mail);
                            associatedGame = RepositoryManager.GameRepository.Retrieve(G_Title);
                            associatedFocusPoint = RepositoryManager.FocusPointRepository.Retrieve(F_Title, LO_ID);
                        }

                        Exercise exercise = new(ID, Title, description, Material, Time, associatedEmployee, associatedGame, associatedFocusPoint, rating);

                        exerciseList.Add(exercise);
                    }
                }
            }
        }

        public byte[] GetMaterial(int id)
        {

            byte[] material;

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"SELECT MATERIAL FROM EXERCISE WHERE ID = @ID", con);
                cmd.Parameters.Add(@"ID", SqlDbType.Int).Value = id; //Adds parametized value to query
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        material = (byte[])dr["Material"];

                        return material; //What happens if this is null?
                    }
                    throw new ArgumentException($"Exercise with given id: {id}, can not be found."); //Not sure if this exception handling is good enough
                }
            }
        }

        public void Reset()
        {
            exerciseList.Clear();

            Load();
        }

        #region CRUD
        public Exercise Create(string title, string description, byte[] material, DateTime timestamp, Employee author, Game? game, FocusPoint focusPoint, Rating? rating)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EXERCISE (Title, Description, Material, Timestamp, Mail, G_Title, Value)" +
                    "VALUES(@Title, @Description, @Material, @Timestamp, @Mail, @G_Title, @Value) SELECT @@IDENTITY", con);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
                cmd.Parameters.Add("@Material", SqlDbType.VarBinary).Value = material;
                cmd.Parameters.Add("@Timestamp", SqlDbType.DateTime2).Value = timestamp;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = author.Mail;   
                if (game == null)
                {
                    cmd.Parameters.Add("@G_Title", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@G_Title", SqlDbType.NVarChar).Value = game.Title;
                }

                if (rating  == 0 || rating == null)
                {
                    cmd.Parameters.Add("@Value", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Value", SqlDbType.Int).Value = (int)rating;
                }

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                Exercise exercise = new(id, title, description, material, timestamp, author, game, focusPoint, rating);

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
                if (id == ex.ExerciseID)
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



        public void Delete(Exercise exercise)
        {
            using(SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM EXERCISE_FOCUSPOINT WHERE E_ID = @E_ID", con);
                cmd.Parameters.Add("@E_ID", SqlDbType.Int).Value = exercise.ExerciseID;
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM EXERCISE WHERE ID = @ID", con);
                cmd2.Parameters.Add("@ID", SqlDbType.Int).Value = exercise.ExerciseID;
                cmd2.ExecuteNonQuery();
            }
            exerciseList.Remove(exercise);
        }

        #endregion
    }
}
