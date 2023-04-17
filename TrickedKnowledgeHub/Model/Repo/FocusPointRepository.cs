using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class FocusPointRepository : Repository
    {
        private List<FocusPoint> _focusPoints;

        public FocusPointRepository() 
        {
            _focusPoints = new List<FocusPoint>();
            Load(); // when the repository is called the load methode runs and makes the list with the focus points
        }

        public override void Load()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT (F_Title) FROM FOCUSPOINT", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) // As long there something to read from the server it runs and save it to the list
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
                    "VALUES(@F_Title, @L_Title", con); // tells the server what values you insert into.
                cmd.Parameters.AddWithValue("@F_Title", title); //adds the vaule to server.
                cmd.Parameters.AddWithValue("@L_Title", parrent.Title);

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

        public List<FocusPoint> RetrieveAll() // not use if there need to be a Exception
        {
            return new(_focusPoints);
        }
    }
}
