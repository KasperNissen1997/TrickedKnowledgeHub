using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    internal class FocusPointRepository : Repository
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM FOCUSPOINT", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FocusPoint focusPoint = new FocusPoint
                            (
                            dr["F_Title"].ToString()                       
                            );

                        _focusPoints.Add(focusPoint);
                    };

                }
            }
        }

        public void Create(string title, LearningObjective parrent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO FOCUSPOINT(F_Title,L_Title)" +
                    "VALUES(@F_Title, @L_Title", con);
                cmd.Parameters.Add("@F_Title", SqlDbType.NVarChar, 50).Value = title;
                cmd.Parameters.Add("@L_Title", SqlDbType.NVarChar, 50).Value = parrent.Title;

                cmd.ExecuteScalar();
            }
        }
        public FocusPoint Retrieve(string title)
        {
            
            foreach (FocusPoint focusPoint in _focusPoints)
            {
                if (title.Equals(focusPoint))
                {
                    return focusPoint;                    
                }
                
            }
            throw new ArgumentException($"no focuspoint found with that tittle{title}");
        } 

        public List<FocusPoint> RetrieveAll()
        {
            return new(_focusPoints);
        }


    }
}
