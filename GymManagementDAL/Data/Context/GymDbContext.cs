using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GymManagementDAL.Data.Context
{
    public class GymDbContext(DbContextOptions<GymDbContext> dbContextOptions, IConfiguration configuration)
        : IdentityDbContext<ApplicationUser>(dbContextOptions)
    {
        //8 dbsets with 7 tables (Member , HealthRecord ) the same Table
        public DbSet<Member> Members { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MemberSessionRL> MemberSessionRLs { get; set; }
        public DbSet<Membership> MemberPlanRls { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("GymDbContextConectionString"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(EB =>
            {
                EB.Property(p => p.FirstName)
                .HasColumnType("varChar")
                .HasMaxLength(50);
                EB.Property(p => p.LastName)
                .HasColumnType("varChar")
                .HasMaxLength(50);
            });
        }

    }
}
