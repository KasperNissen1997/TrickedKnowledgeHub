﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TrickedKnowledgeHub.Model.Persistence
{
    /// <summary>
    /// This repository streamlines all DB communcation associated with the creation, retrieval, updating and deletion of all <see cref="FocusPoint"/> instances. <br/>
    /// The child <see cref="Repository"/> classes are the only places from which DB communication occurs. <br/>
    /// <br/>
    /// All child <see cref="Repository"/> classes are accessed from <see cref="RepositoryManager"/>.
    /// </summary>
    public class FocusPointRepository : Repository
    {
        private List<FocusPoint> focusPoints = new();

        public FocusPointRepository(bool isTestRepository = false) 
        {
            IsTestRepository = isTestRepository;

            // Load all the focus points from the DB.
            Load();
        }

        /// <summary>
        /// Loads the data from the DB into this class.
        /// </summary>
        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                // Prepare the SQL query that we send to the DB.
                SqlCommand cmd = new SqlCommand("SELECT * FROM FOCUSPOINT", con);

                // Query the DB.
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    // As long as we can read more rows of data from the DB, we continue parsing the data.
                    while (dr.Read()) 
                    {
                        string title = dr["F_Title"].ToString();
                        int learningObjectiveID = int.Parse(dr["LO_ID"].ToString());

                        LearningObjective parent;

                        if (IsTestRepository)
                            parent = RepositoryManager.TestLearningObjectiveRepository.Retrieve(learningObjectiveID);
                        else
                            parent = RepositoryManager.LearningObjectiveRepository.Retrieve(learningObjectiveID);

                        FocusPoint focusPoint = new(title, parent);

                        parent.FocusPoints.Add(focusPoint);
                        focusPoints.Add(focusPoint);
                    }
                }
            }
        }

        public void Reset()
        {
            focusPoints.Clear();

            Load();
        }

        /// <summary>
        /// Creates a new <see cref="FocusPoint"/> instance, and stores it in the DB.
        /// </summary>
        /// <param name="title">The title fo the <see cref="FocusPoint"/>.</param>
        /// <param name="parent">The <see cref="LearningObjective"/> that this <see cref="FocusPoint"/> is placed beneath.</param>
        /// <returns>A new <see cref="FocusPoint"/> instance.</returns>
        public FocusPoint Create(string title, LearningObjective parent)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                // Prepare the SQL query we are going to send to the DB.
                SqlCommand cmd = new SqlCommand("INSERT INTO FOCUSPOINT(F_Title, LO_ID)" +
                    "VALUES (@F_Title, @LO_ID)", con);
                // Initialize the parameters with the values the DB should receive.
                // We use AddWithValue instead of Add because we are only dealing with strings.
                cmd.Parameters.AddWithValue("@F_Title", title);
                cmd.Parameters.Add("@LO_ID", SqlDbType.Int).Value = parent.ID;

                cmd.ExecuteNonQuery();

                FocusPoint focusPoint = new(title, parent);

                // Create the association between the parenting LearningObjective instance and this new FocusPoint instance.
                parent.FocusPoints.Add(focusPoint);
                focusPoints.Add(focusPoint);

                return focusPoint;
            }
        }

        /// <summary>
        /// Retrieves a <see cref="FocusPoint"/> instance if one exists.
        /// </summary>
        /// <param name="title">The title of the <see cref="FocusPoint"/>.</param>
        /// <returns>
        /// A <see cref="FocusPoint"/> instance with a matching title. <br/>
        /// If no such <see cref="FocusPoint"/> exists, <see langword="null"/> is returned instead.</returns>
        /// <exception cref="ArgumentException"></exception>
        public FocusPoint Retrieve(string title,int LO_ID)
        {
            // Iterate over each FocusPoint instance stored locally, and search for the FocusPoint with a matching title.
            foreach (FocusPoint focusPoint in focusPoints)
                if (title.Equals(focusPoint.Title)&&LO_ID.Equals(focusPoint.Parent.ID))
                    return focusPoint;

            throw new ArgumentException($"No FocusPoint found with title: {title}");
        }

        /// <summary>
        /// Retrieves all of the <see cref="FocusPoint"/> instances.
        /// </summary>
        /// <returns>A collection of all existing <see cref="FocusPoint"/> instances.</returns>
        public List<FocusPoint> RetrieveAll()
        {
            return new(focusPoints);
        }
    }
}
