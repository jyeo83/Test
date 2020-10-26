using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.DOM.Models;

namespace Test.DOM
{
    public class TestContext : IdentityDbContext<User, Role, int>
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<AssistantDailyReport> AssistantDailyReports { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ProjectContractor> ProjectContractors { get; set; }
        public DbSet<ProjectLabor> ProjectLabors { get; set; }
        public DbSet<Labor> Labors { get; set; }
        public DbSet<Trade> Trade { get; set; }
        public DbSet<WorkPerformed> WorkPerformed { get; set; }
        public DbSet<LaborWorkPerformed> LaborWorkPerformed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            //modelBuilder.Entity<IdentityUser>(b => b.ToTable("Users")); // NOT NEEDED: Existing Users table will be modified during migration
            modelBuilder.Entity<Role>(b => b.ToTable("Roles")); // NOT "IdentityRole<int>" but just "Role"
            modelBuilder.Entity<IdentityUserRole<int>>(b => b.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityRoleClaim<int>>(b => b.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserClaim<int>>(b => b.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<int>>(b => b.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityUserToken<int>>(b => b.ToTable("UserTokens"));

            modelBuilder.Entity<User>(buildAction: b =>
            {
                b.ToTable("Users");

                b.Property(u => u.Id).HasColumnName("UserId");

                b.HasMany(u => u.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(u => u.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(u => u.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(u => u.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(buildAction: b =>
            {
                b.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(r => r.RoleClaims)
                    .WithOne(rc => rc.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            // ProjectContractor
            modelBuilder.Entity<ProjectContractor>()
                .HasKey(pc => new { pc.ProjectId, pc.ContractorId });

            modelBuilder.Entity<ProjectContractor>()
                .HasOne(pc => pc.Project)
                .WithMany(p => p.Contractors)
                .HasForeignKey(pc => pc.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectContractor>()
                .HasOne(pc => pc.Contractor)
                .WithMany(p => p.Projects)
                .HasForeignKey(pc => pc.ContractorId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectLabor
            modelBuilder.Entity<ProjectLabor>()
                .HasKey(pl => new { pl.ProjectId, pl.LaborId });

            modelBuilder.Entity<ProjectLabor>()
                .HasOne(pl => pl.Project)
                .WithMany(p => p.Labors)
                .HasForeignKey(pl => pl.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectLabor>()
                .HasOne(pl => pl.Labor)
                .WithMany(l => l.ProjectsWorked)
                .HasForeignKey(pl => pl.LaborId)
                .OnDelete(DeleteBehavior.Cascade);

            // LaborWorkPerformed
            modelBuilder.Entity<LaborWorkPerformed>()
                .HasKey(lwp => new { lwp.WorkPerformedId, lwp.LaborId, lwp.TradeId });

            modelBuilder.Entity<LaborWorkPerformed>()
                .HasOne(lwp => lwp.Labor)
                .WithMany(l => l.LaborWorkPerformed)
                .HasForeignKey(lwp => lwp.LaborId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LaborWorkPerformed>()
                .HasOne(lwp => lwp.WorkPerformed)
                .WithMany(wp => wp.LaborWorkPerformed)
                .HasForeignKey(lwp => lwp.WorkPerformedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LaborWorkPerformed>()
                .HasOne(lwp => lwp.Trade)
                .WithMany(t => t.LaborWorkPerformed)
                .HasForeignKey(lwp => lwp.TradeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
