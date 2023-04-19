using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TrickedKnowledgeHub.Model.Repo
{
    public class GameRepository : Repository
    {
        private List<Game> games = new List<Game>();

        public GameRepository() { Load(); }

        public override void Load()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand cmd = new("SELECT * FROM GAME");

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string title = dr["G_Title"].ToString();

                        Game game = new(title);

                        games.Add(game);
                    }
                }
            }
        }

        public Game Create(string title)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new("INSERT INTO GAME G_Title VALUES (@G_Title)");

                cmd.Parameters.AddWithValue("G_Title", title);

                cmd.ExecuteNonQuery();
            }

            Game game = new(title);

            games.Add(game);

            return game;
        }

        public Game Retrieve(string title)
        {
            foreach (Game game in games)
                if (game.Title == title)
                    return game;

            throw new ArgumentException($"No game with title {title} found.");
        }

        public List<Game> RetriveAll()
        {
            return new(games);
        }
    }
}
