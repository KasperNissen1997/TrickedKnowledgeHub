using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class FocusPointRepository : Repository
    {
        private List<FocusPoint> _focusPoints = new();

        public FocusPointRepository(bool isTestRepository = false) 
        {
            IsTestRepository = isTestRepository;

            Load(); // when the repository is called the load methode runs and makes the list with the focus points
        }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM FOCUSPOINT", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) // As long there something to read from the server it runs and save it to the list
                    {
                        string title = dr["F_Title"].ToString();
                        int learningObjectiveID = int.Parse(dr["LO_ID"].ToString());

                        FocusPoint focusPoint = new(title);
                        _focusPoints.Add(focusPoint);

                        LearningObjective associatedLearningObjective = RepositoryManager.LearningObjectiveRepository.Retrive(learningObjectiveID);
                        associatedLearningObjective.FocusPoints.Add(focusPoint);
                    }
                }
            }
        }

        public FocusPoint Create(string title, LearningObjective parent)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO FOCUSPOINT(F_Title,L_Title)" +
                    "VALUES(@F_Title, @L_Title", con); // tells the server what values you insert into.
                cmd.Parameters.AddWithValue("@F_Title", title); //adds the vaule to server.
                cmd.Parameters.AddWithValue("@L_Title", parent.Title);

                cmd.ExecuteNonQuery();

                FocusPoint focusPoint = new(title);

                parent.FocusPoints.Add(focusPoint);
                _focusPoints.Add(focusPoint);

                return focusPoint;
            }
        }

        public FocusPoint Retrieve(string title)
        {
            foreach (FocusPoint focusPoint in _focusPoints)
                if (title.Equals(focusPoint.Title))
                    return focusPoint;

            throw new ArgumentException($"No focuspoint found with title: {title}");
        }

        public List<FocusPoint> RetrieveAll()
        {
            return new(_focusPoints);
        }
    }
}
