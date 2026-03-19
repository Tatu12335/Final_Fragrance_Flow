
using Fragrance_flow_DL_VERSION_.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.InteropServices;
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
       
    }

}
