using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    /// <summary>
    /// This repository streamlines all DB communcation associated with the creation, retrieval, updating and deletion of all <see cref="FocusPoint"/> instances. <br/>
    /// The child <see cref="Repository"/> classes are the only places from which DB communication occurs. <br/>
    /// <br/>
    /// All child <see cref="Repository"/> classes are accessed from <see cref="RepositoryManager"/>.
    /// </summary>
    public class FocusPointRepository : Repository
    {
        private List<FocusPoint> _focusPoints = new();

        public FocusPointRepository() 
        {
            Load(); // when the repository is called the load methode runs and makes the list with the focus points
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
                        string learningObjectiveTitle = dr["LO_Title"].ToString();

                        FocusPoint focusPoint = new(title);
                        _focusPoints.Add(focusPoint);

                        // Retrieve the parent LearningObjective, and associate the newly created FocusPoint.
                        LearningObjective associatedLearningObjective = RepositoryManager.LearningObjectiveRepository.Retrive(learningObjectiveTitle);
                        associatedLearningObjective.FocusPoints.Add(focusPoint);
                    }
                }
            }
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
                SqlCommand cmd = new SqlCommand("INSERT INTO FOCUSPOINT(F_Title,L_Title)" +
                    "VALUES(@F_Title, @L_Title", con);
                // Initialize the parameters with the values the DB should receive.
                // We use AddWithValue instead of Add because we are only dealing with strings.
                cmd.Parameters.AddWithValue("@F_Title", title);
                cmd.Parameters.AddWithValue("@L_Title", parent.Title);

                cmd.ExecuteNonQuery();

                FocusPoint focusPoint = new(title);

                // Create the association between the parenting LearningObjective instance and this new FocusPoint instance.
                parent.FocusPoints.Add(focusPoint);
                _focusPoints.Add(focusPoint);

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
        public FocusPoint Retrieve(string title)
        {
            // Iterate over each FocusPoint instance stored locally, and search for the FocusPoint with a matching title.
            foreach (FocusPoint focusPoint in _focusPoints)
                if (title.Equals(focusPoint.Title))
                    return focusPoint;

            throw new ArgumentException($"No FocusPoint found with title: {title}");
        }

        /// <summary>
        /// Retrieves all of the <see cref="FocusPoint"/> instances.
        /// </summary>
        /// <returns>A collection of all existing <see cref="FocusPoint"/> instances.</returns>
        public List<FocusPoint> RetrieveAll()
        {
            return new(_focusPoints);
        }
    }
}
