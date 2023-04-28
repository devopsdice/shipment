using Npgsql;
using System.Data;

namespace ShipmentService.Repository.Base
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShipmentDBConn");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
