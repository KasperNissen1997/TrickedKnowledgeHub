using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TrickedKnowledgeHub.Model.Repo
{
    public abstract class Repository : IRepository
    {
        // TODO: Investigate ways to make this property readonly.
        public bool IsTestRepository { get; set; }

        private IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        protected SqlConnection GetConnection()
        {
            string? connectionString = config.GetConnectionString("Main");

            if (IsTestRepository)
                connectionString = config.GetConnectionString("Test");

            SqlConnection connection = new SqlConnection(connectionString);
            
            return connection;
        }

        public abstract void Load();
    }
}
