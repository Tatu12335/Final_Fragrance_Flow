

using Microsoft.Data.SqlClient;
using System.Data;
namespace fragrance_API.dbcontext
{
    public class Dbcontext
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public Dbcontext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString(Environment.GetEnvironmentVariable("DB_CONNECTION"));
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }

}
