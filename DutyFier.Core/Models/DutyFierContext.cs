using DutyFier.Core.Entities;
using System.Data.Entity;

//using Microsoft.EntityFrameworkCore;

namespace DutyFier.Core.Models
{
    public class DutyFierContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Executor> Executors { get; set; }
        public DbSet<DaysOfWeekWeight> DaysOfWeekWeights { get; set; }
        public DbSet<PersonDutyFeedback> PersonDutyFeedbacks { get; set; }
        public DbSet<DutyType> DutyTypes { get; set; }
        public DbSet<Duty> Duties { get; set; }


        public DutyFierContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<DutyRequest>();

            modelBuilder.Entity<DutyType>()
                .HasMany(p => p.Positions)
                .WithRequired(p => p.DutyType);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=Duty;Trusted_Connection=True;");
        //    }
        //}

        //public DutyFierContext()
        //{
        //    Database.EnsureCreated();
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Ignore<DutyRequest>();
        //    modelBuilder.Entity<Person>().Ignore(c => c.Score);
        //    //modelBuilder.Entity<DutyType>()
        //    //    .HasMany(p => p.Positions)
        //    //    .WithRequired(p => p.DutyType);
        //}
    }
}
