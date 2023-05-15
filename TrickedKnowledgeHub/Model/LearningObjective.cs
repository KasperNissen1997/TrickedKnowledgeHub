using System.Collections.Generic;
using System.Text;

namespace TrickedKnowledgeHub.Model
{
    public class LearningObjective
    {
        public int ID { get; }

        public string Title { get; set; }
        public Game? Parent { get; set; }

        public List<FocusPoint> FocusPoints = new List<FocusPoint>();

        public LearningObjective(int id, string title, Game? parent)
        {
            ID = id;

            Title = title;
            Parent = parent;
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
            sb.Append("Parent: " + Parent.Title + ", ");
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

        /// <summary>
        /// Compares the current instance with <paramref name="obj"/>. <br/>
        /// Specifically overriden to enable precise comparison of different <see cref="LearningObjective"/> objects.
        /// </summary>
        /// <param name="obj">The object that this should be compared with.</param>
        /// <returns>
        /// If <paramref name="obj"/> is of type <see cref="LearningObjective"/>, then <see langword="true"/> if the <see cref="LearningObjective.ID"/>s of the two match. If not, then <see langword="false"/><br/>
        /// Otherwise, it uses the default comparer.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is LearningObjective otherLearningObjective)
                return ID.Equals(otherLearningObjective.ID);

            return base.Equals(obj);
        }
    }
}
