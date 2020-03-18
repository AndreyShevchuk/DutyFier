using DutyFier.Core.Entities;
using System.Data.Entity;

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

        public DutyFierContext() : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<DutyRequest>();
            modelBuilder.Entity<Duty>().Ignore(c => c.ExecutorsNames);
            modelBuilder.Entity<Duty>().Ignore(c => c.PositionNames);
            modelBuilder.Entity<Duty>().Ignore(c => c.PositionsNames);
            modelBuilder.Entity<Duty>().Ignore(c => c.PreliminaryAssessments);

            modelBuilder.Ignore<PersonScoreCover>();
            //modelBuilder.Entity<Person>().Ignore(c => c.Score);
            //modelBuilder.Entity<DutyType>()
            //    .HasMany(p => p.Positions)
            //    .WithRequired(p => p.DutyType);
        }
    }
}
