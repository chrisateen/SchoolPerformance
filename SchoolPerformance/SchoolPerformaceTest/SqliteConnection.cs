using LoadData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;

namespace SchoolPerformanceTest
{
    /// <summary>
    /// Creates connection to sqlite database for testing
    /// </summary>
    public class InMemorySqliteConnection : IDisposable
    {
        private readonly string _connectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        public readonly SchoolPerformanceContext _context;

        public InMemorySqliteConnection()
        {
            //Creates and open a connection to the sqlite Db
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            //Create instance of SchoolPerformanceContext with options included
            var options = new DbContextOptionsBuilder<SchoolPerformanceContext>()
                    .UseSqlite(_connection)
                    .Options;
            _context = new SchoolPerformanceContext(options);

           
            //// Drop the database if it exists
            if (_context != null)
            {
                _context.Database.EnsureDeleted();
                
            }

            //Create the database
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
