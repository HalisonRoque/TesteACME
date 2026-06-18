using Microsoft.Data.Sqlite;
using System.Data;

namespace AcmeClinic.Infrastructure.Context;

public class DbSQLiteContext
{
    private readonly string _context;

    public DbSQLiteContext(string context)
    {
        _context = context;
    }

    public IDbConnection Create()
    {
        return new SqliteConnection(_context);
    }
}