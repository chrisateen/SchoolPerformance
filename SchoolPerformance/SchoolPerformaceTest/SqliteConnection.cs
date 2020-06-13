using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest
{
    public class InMemorySqliteConnection : IDisposable
    {
        private readonly string _connectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        public readonly SchoolPerformanceContext _context;

        public readonly ISchoolResultRepository<School> _repository;

        public InMemorySqliteConnection()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<SchoolPerformanceContext>()
                    .UseSqlite(_connection)
                    .Options;
            _context = new SchoolPerformanceContext(options);

            if (_context != null)
            {
                _context.Database.EnsureDeleted();
                
            }

            _context.Database.EnsureCreated();

            _repository = new SchoolResultRepository<School>(_context);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
