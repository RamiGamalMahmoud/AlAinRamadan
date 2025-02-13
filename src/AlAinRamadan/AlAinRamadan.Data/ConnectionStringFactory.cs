using AlAinRamadan.Core.Abstraction;
using Microsoft.Data.Sqlite;

namespace AlAinRamadan.Data
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        public ConnectionStringFactory(string path)
        {
            _path = path;
        }

        public string GetConnectionString()
        {
            SqliteConnectionStringBuilder sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder();
            sqliteConnectionStringBuilder.DataSource = _path;
#if DEBUG
            //sqliteConnectionStringBuilder.DataSource = ".\\DB\\al_ain_ramadan.dev.db";
#else
#endif

            return sqliteConnectionStringBuilder.ConnectionString;
        }

        private string _path;
    }
}
