/// Hints
/// add-migration name -outputdir "C:\Users\ArutyunyanAV\source\repos\ExampleRESTfulApi\DAL\EF\Migrations"

using DAL.Enums;
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
        public DbSet<ContractDocument> ContractDocuments { get; set; }

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

            #region Data for init

            var users = new User[]
                    {
                        new User() { Id = 1, UserName = "Artur" },
                        new User() { Id = 2, UserName = "Igor" },
                        new User() { Id = 3, UserName = "Lena" },
                        new User() { Id = 4, UserName = "Vladimir" }
                    };
            var roles = new Role[]
                {
                    new Role() { Id = 1, RoleName = "Admin" },
                    new Role() { Id = 2, RoleName = "User" }
                };
            var contractDocument = new ContractDocument[]
                {
                    new ContractDocument() { Id = 1, DocumentName = "Contract Document" }
                };
            var userRoles = new UserRole[]
                    {
                        new UserRole() { UserId = users[0].Id, RoleId = roles[0].Id },
                        new UserRole() { UserId = users[1].Id, RoleId = roles[1].Id },
                        new UserRole() { UserId = users[2].Id, RoleId = roles[1].Id },
                        new UserRole() { UserId = users[3].Id, RoleId = roles[1].Id },
                    };
    
            var documentContractRoles = new DocumentContractRole[]
                {
                    new DocumentContractRole() { RoleId = roles[0].Id, ContractDocumentId = contractDocument[0].Id }
                };

            #endregion

            #region Building identity entities

            builder.Entity<User>().Property(u => u.UserName).IsRequired();
            builder.Entity<User>().HasData(users);


            builder.Entity<Role>().Property(r => r.RoleName).IsRequired();
            builder.Entity<Role>().HasData(roles);


            builder.Entity<ContractDocument>().Property(c => c.DocumentName).IsRequired();
            builder.Entity<ContractDocument>().HasData(contractDocument);


            // Many-to-many: User <---> Role
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<UserRole>().HasData(userRoles);
            builder.Entity<UserRole>().Property(ur => ur.RoleId).HasDefaultValue(Roles.User);
            //----------------------------------------------------------------------


            //// Many-to-many: Role <---> DocumentContract 
            builder.Entity<DocumentContractRole>().HasKey(dr => new { dr.RoleId, dr.ContractDocumentId });

            builder.Entity<DocumentContractRole>()
                .HasOne(dr => dr.Role)
                .WithMany(r => r.DocumentContractRoles)
                .HasForeignKey(dr => dr.RoleId);

            builder.Entity<DocumentContractRole>()
                .HasOne(dr => dr.ContractDocument)
                .WithMany(d => d.DocumentRoles)
                .HasForeignKey(dr => dr.ContractDocumentId);

            builder.Entity<DocumentContractRole>().HasData(documentContractRoles);
            #endregion
        }
    }
}
