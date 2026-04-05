using Microsoft.Data.SqlClient;
using System.Data;

namespace WorkplaceSimulation.Infrastructure.DataAccess;

public class DbSession : IDisposable
{
    public IDbConnection Connection { get; }
    public DbSession(string ConnectionString)
    {
        Connection = new SqlConnection(ConnectionString);
        Connection.Open();
    }
    public void Dispose()
    {
        if (Connection.State != ConnectionState.Closed)
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
