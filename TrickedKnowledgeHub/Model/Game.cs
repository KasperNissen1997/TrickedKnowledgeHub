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

        /// <summary>
        /// Compares the current instance with <paramref name="obj"/>. <br/>
        /// Specifically overriden to enable precise comparison of different <see cref="Game"/> objects.
        /// </summary>
        /// <param name="obj">The object that this should be compared with.</param>
        /// <returns>
        /// If <paramref name="obj"/> is of type <see cref="Game"/>, then <see langword="true"/> if the <see cref="Game.Title"/>s of the two match. If not, then <see langword="false"/><br/>
        /// Otherwise, it uses the default comparer.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is Game otherGame)
                return Title.Equals(otherGame.Title);

            return base.Equals(obj);
        }
    }
}
