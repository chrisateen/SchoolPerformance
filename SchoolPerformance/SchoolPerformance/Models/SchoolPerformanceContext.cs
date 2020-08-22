using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadData;
using System.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Castle.Core.Internal;

namespace SchoolPerformance.Models
{
    public class SchoolPerformanceContext : DbContext
    {
        private DbContextOptions<SchoolPerformanceContext> _options;

        public SchoolPerformanceContext(DbContextOptions<SchoolPerformanceContext> options) : base(options)
        {
            _options = options;
            
        }

        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolContextual> SchoolContextual { get; set; }
        public virtual DbSet<SchoolDetails> SchoolDetails { get; set; }
        public virtual DbSet<SchoolResult> SchoolResult { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Specify primary key in School
            modelBuilder.Entity<School>()
                .HasKey(c => new { c.URN });

            //Specify primary key in SchoolDetails
            modelBuilder.Entity<SchoolDetails>()
                .HasKey(c => new { c.URN });

            //Specify that there is a composite keys in SchoolResult and SchoolContextual
            modelBuilder.Entity<SchoolResult>()
                .HasKey(c => new { c.URN, c.ACADEMICYEAR});

            modelBuilder.Entity<SchoolContextual>()
                .HasKey(c => new { c.URN, c.ACADEMICYEAR });

            //Specify the one to one relationship between school and school details
            modelBuilder.Entity<School>()
                .HasOne<SchoolDetails>(s => s.SchoolDetails)
                .WithOne(s => s.School)
                .HasForeignKey<SchoolDetails>( s => s.URN);

            //Specify the one to many relationship between school and school contextual
            modelBuilder.Entity<School>()
                .HasMany<SchoolContextual>(s => s.SchoolContextuals)
                .WithOne(s => s.School)
                .HasForeignKey(s => s.URN);

            //Specify the one to many relationship between school and school result
            modelBuilder.Entity<School>()
                .HasMany<SchoolResult>(s => s.SchoolResults)
                .WithOne(s => s.School)
                .HasForeignKey(s => s.URN);

                //Seed data
                modelBuilder.Seed(2019);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}
