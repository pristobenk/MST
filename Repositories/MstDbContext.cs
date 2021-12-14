using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MitraSolusiTelematika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitraSolusiTelematika.Repositories
{
    public class MstDbContext:DbContext
    {
        public MstDbContext(DbContextOptions<MstDbContext> options)
    : base(options)
        { }
        public DbSet<KodePos> KodePos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KodePos>().ToTable("KodePos");
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string ConnectionString = configuration.GetConnectionString("Default");

            optionsBuilder.UseSqlServer(ConnectionString);
           
        }


    }
}
