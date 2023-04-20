using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TrickedKnowledgeHub.Model.Repo
{
    public abstract class Repository : IRepository
    {
        // private IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        protected SqlConnection GetConnection()
        {
            // string cs = config["Data:DefaultConnection:ConnectionString"];
            string cs = "Server=10.56.8.36; Database=P3_DB_2023_06; User Id=P3_PROJECT_USER_06; Password=OPENDB_06; TrustServerCertificate=true";
            
            SqlConnection connection = new SqlConnection(cs);
            
            return connection;
        }

        public abstract void Load();
    }
}
