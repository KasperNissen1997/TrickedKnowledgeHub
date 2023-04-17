using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class FocusPointRepository : Repository
    {
        private List<FocusPoint> _focusPoints;

        public FocusPointRepository() 
        {
            _focusPoints = new List<FocusPoint>();
            Load();
        }

        public override void Load()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT (F_Title) FROM FOCUSPOINT", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FocusPoint focusPoint = new FocusPoint(dr["F_Title"].ToString());

                        _focusPoints.Add(focusPoint);
                    }
                }
            }
        }

        public void Create(string title, LearningObjective parent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO FOCUSPOINT(F_Title,L_Title)" +
                    "VALUES(@F_Title, @L_Title", con);
                cmd.Parameters.Add("@F_Title", SqlDbType.NVarChar, 50).Value = title;
                cmd.Parameters.Add("@L_Title", SqlDbType.NVarChar, 50).Value = parent.Title;

                cmd.ExecuteNonQuery();
            }
        }

        public FocusPoint Retrieve(string title)
        {
            foreach (FocusPoint focusPoint in _focusPoints)
                if (title.Equals(focusPoint.Title))
                    return focusPoint;

            throw new ArgumentException($"no focuspoint found with that tittle{title}");
        }

        public List<FocusPoint> RetrieveAll()
        {
            return new(_focusPoints);
        }
    }
}
