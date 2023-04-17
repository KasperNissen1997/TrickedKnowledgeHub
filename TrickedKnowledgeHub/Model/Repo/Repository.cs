using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model.Repo
{
    public abstract class Repository : IRepository
    {

        private IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        //protected string? ConnectionString = config.GetConnectionString("MyDBConnection");

        public SqlConnection GetConnection()
        {
            string cs = config["Data:DefaultConnection:ConnectionString"];
            SqlConnection connection = new SqlConnection(cs);
            return connection;
        }


        public abstract void Load();
    }
}
