using Microsoft.EntityFrameworkCore;
using SystemFH.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SystemFH.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Learning> Learnings { get; set; }
        public DbSet<PersonLearning> PeopleLearning { get; set; }
        public DbSet<PersonFeedback> PeopleFeedback { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ActualStatus> ActualStatus { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Circle> Circles { get; set; }
        public DbSet<TypeConsultor> TypeConsultors { get; set; }
        public DbSet<DayTime> DayTime { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<CashManager> CashManager { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActualStatus>()
                    .HasOne(e => e.Person)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonFeedback>()
                    .HasOne(e => e.Person)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonLearning>()
                    .HasOne(e => e.Person)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Category> ClientProject { get; set; }

        public DbSet<Enterprise> Enterprise { get; set; }

        public DbSet<CostCenter> CostCenter { get; set; }

        public DbSet<Account> Account { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
