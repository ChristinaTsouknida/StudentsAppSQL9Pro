using Microsoft.Data.SqlClient;

namespace StudentsAppSqlDB9Pro.Core
{
    public class DBHelper
    {
        private readonly string? _connectionString;

        public DBHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            // configuration["ConnectionStrings:DefaultConnection"]
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
