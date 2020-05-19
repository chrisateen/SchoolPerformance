using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Models
{
    public class SchoolPerformanceContext : DbContext
    {
        public SchoolPerformanceContext(DbContextOptions<SchoolPerformanceContext> options) : base(options)
        {

        }

        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolContextual> SchoolContextual { get; set; }
        public virtual DbSet<SchoolDetails> SchoolDetails { get; set; }
        public virtual DbSet<SchoolResult> SchoolResult { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
