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
            modelBuilder.Entity<Duty>().Ignore(c => c.ExecutorsPositions);
            modelBuilder.Entity<Duty>().Ignore(c => c.PreliminaryAssessments);
        }
    }
}
