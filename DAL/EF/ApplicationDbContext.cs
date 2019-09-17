/// Hints
/// add-migration name -o DAO/Portal/EF/Migrations

using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<DocumentCard> Cards { get; set; }
        public DbSet<DocumentCardRoles> DocumentCardRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        #region Migrations
        /// <summary>
        /// For Migrations
        /// </summary>
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {

            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DAL"));

                return new ApplicationDbContext(builder.Options);
            }
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.IdentityBuild();
        }
    }

    internal static class ModelCreatorExtensions
    {
        internal static void IdentityBuild(this ModelBuilder builder)
        {
            builder.Entity<User>().Property(u => u.UserName).IsRequired();
            builder.Entity<User>().HasData(
                new User[]
                {
                    new User() { Id = 1, UserName = "Artur" },
                    new User() { Id = 2, UserName = "Igor" },
                    new User() { Id = 3, UserName = "Lena" },
                    new User() { Id = 4, UserName = "Vladimir" }
                });


            builder.Entity<Role>().Property(r => r.RoleName).IsRequired();
            builder.Entity<Role>().HasData(
                new Role[]
                {
                    new Role() { Id = 1, RoleName = "Admin" },
                    new Role() { Id = 2, RoleName = "User" }
                });
            //builder.Entity<UserRole>().Property(u => u.RoleId).HasDefaultValue(2); // just user


            builder.Entity<UserRole>().HasKey(k => new { k.UserId, k.RoleId });

            builder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserRole>()
                .HasOne(r => r.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId);

            builder.Entity<UserRole>().HasData(
                new UserRole[]
                {
                    new UserRole() { UserId = 1, RoleId = 1 },
                    new UserRole() { UserId = 2, RoleId = 2 }
                });


            builder.Entity<DocumentCard>().ToTable("DocumentCards");
            builder.Entity<DocumentCard>().Property(c => c.DocumentName).IsRequired();
            builder.Entity<DocumentCard>().HasData(
                new DocumentCard[]
                {
                    new DocumentCard() { Id = 1, DocumentName = "Main Document" }
                });


            builder.Entity<DocumentCardRoles>().HasKey(k => new { k.DocumentCardId, k.RoleId });
            builder.Entity<DocumentCardRoles>().HasData(
                new DocumentCardRoles[]
                {
                    new DocumentCardRoles() { RoleId = 1, DocumentCardId = 1 }
                });
        }
    }
}
