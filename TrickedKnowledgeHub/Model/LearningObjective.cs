using System.Collections.Generic;
using System.Text;

namespace TrickedKnowledgeHub.Model
{
    public class LearningObjective
    {
        public int ID { get; }
        public string Title { get; set; }

        public List<FocusPoint> FocusPoints = new List<FocusPoint>();

        public LearningObjective(int id, string title)
        {
            ID = id;

            Title = title;
        }

        /// <summary>
        /// Provides a string representation of the <see cref="LearningObjective"/> instance.
        /// </summary>
        /// <returns>A string representation of the <see cref="LearningObjective"/> instance.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("ID: " + ID + ", ");
            sb.Append("Title: " + Title + ", ");
            sb.Append("FocusPoints: ");

            // Iterate over each associated FocusPoint, and add them to the string.
            for (int i = 0; i < FocusPoints.Count; i++)
            {
                // If we are at the last associated FocusPoint, then don't add the comma at the end.
                if (i == FocusPoints.Count - 1)
                    sb.Append(FocusPoints[i].Title);
                else
                    sb.Append(FocusPoints[i].Title + ", ");
            }

            return sb.ToString();
        }
    }
}
