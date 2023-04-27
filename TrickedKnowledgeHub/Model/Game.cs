using System.Collections.Generic;
using System.Text;
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.Model
{
    public class Game
    {
        public string Title { get; set; }

        public List<LearningObjective> LearningObjectives { get; set; }

        public Game(string title)
        {
            Title = title;

            LearningObjectives = new();
        }

        /// <summary>
        /// Provides a string representation of the <see cref="Game"/> instance.
        /// </summary>
        /// <returns>A string representation of the <see cref="Game"/> instance.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("Title: " + Title + ", ");
            sb.Append("LearningObjectives: ");

            // Iterate over each associated LearningObjective, and add them to the string.
            for (int i = 0; i < LearningObjectives.Count; i++)
            {
                // If we are at the last associated LearningObjective, then don't add the comma at the end.
                if (i == LearningObjectives.Count - 1)
                    sb.Append(LearningObjectives[i].Title);
                else
                    sb.Append(LearningObjectives[i].Title + ", ");
            }

            return sb.ToString();
        }
    }
}
