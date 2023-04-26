using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TrickedKnowledgeHub.Model.Repo
{
    public abstract class Repository : IRepository
    {
        private IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        protected SqlConnection GetConnection()
        {
            string? ConnectionString = config.GetConnectionString("MyDBConnection");

            SqlConnection connection = new SqlConnection(ConnectionString);
            
            return connection;
        }

        public abstract void Load();
    }
}
