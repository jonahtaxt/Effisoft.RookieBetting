using Effisoft.RookieBetting.Infrastructure.Database;
using System.Data.SqlClient;

namespace Effisoft.RookieBetting.SqlDataAccess.Core
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public readonly string _connStringName;

        public DatabaseFactory(string connStringName)
        {
            _connStringName = connStringName;
        }

        public IDatabase Create()
        {
            var database = new Database<SqlConnection>(_connStringName);
            return database;
        }
    }
}
