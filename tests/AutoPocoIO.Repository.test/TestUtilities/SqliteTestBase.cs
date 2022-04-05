using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AutoPocoIO.Repository.Test.TestUtilities;

public abstract class SqliteTestBase<TContext> : IDisposable where TContext : DbContext
{
    private readonly DbConnection? _connection;

    protected SqliteTestBase()
    {
        Options = new DbContextOptionsBuilder<TContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

        _connection = RelationalOptionsExtension.Extract(Options).Connection;
    }

    protected DbContextOptions<TContext> Options { get; }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");

        connection.Open();

        return connection;
    }
    public void Dispose() => _connection?.Dispose();
}

