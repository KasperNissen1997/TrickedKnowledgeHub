using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;

namespace TrickedKnowledgeHub.Model.Repo
{
    internal class GameRepository : Repository
    {

        private List<Game> games = new List<Game>();

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public void Create(string title)
        {
            games.Add(new Game(title));
        }

        public Game Retrieve(string title)
        {
            foreach (Game game in games)
            {
                if (game.Title == title)
                {
                    return game;
                }
            }

            throw new ArgumentException($"No game with title {title} found.");
        }

        public Game RetriveAll()
        {
            foreach (Game game in games)
            {
                return new Game(game.Title);
            }

            throw new ArgumentException($"No more game could be found  ");
        }






    }
}
