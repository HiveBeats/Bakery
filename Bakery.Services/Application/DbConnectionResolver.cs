using System.Data;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

namespace Bakery.Services.Application
{
    public interface IDbConnectionResolver
    {
        IDbConnection ResolveConnection(string connectionString);
    }

    public class ProductionDbConnectionResolver: IDbConnectionResolver
    {
        public IDbConnection ResolveConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }

    public class TestDbConnectionResolver : IDbConnectionResolver
    {
        public IDbConnection ResolveConnection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}