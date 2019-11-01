/// Hints
/// add-migration name -outputdir "C:\Users\ArutyunyanAV\source\repos\ExampleRESTfulApi\DAL\DAO\EF\Migrations"

using DAL.Entities.Documents;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DAL.DAO.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        DbSet<ContractDocument> contractDocuments { get; set; }

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
            private IHostingEnvironment _env;
     
            public ApplicationDbContextFactory()
            {
               
            }
            public ApplicationDbContextFactory(IHostingEnvironment env)
            {
                _env = env;
            }

            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                    //.AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true) // Эта строка вызывает ошибку, т.к. обьект env == null
                    //.AddEnvironmentVariables()
                    .Build();

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);

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
            var roles = new ApplicationRole[]
                {
                    new ApplicationRole() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                    new ApplicationRole() { Id = Guid.NewGuid(), Name = "Customer", NormalizedName = "CUSTOMER" }
                };
            var contractDocument = new ContractDocument[]
                {
                    new ContractDocument() { Guid = Guid.NewGuid(), Title = "Titul2005", DocumentName = "titul contract" },
                    new ContractDocument() { Guid = Guid.NewGuid(), Title = "Road-pro", DocumentName = "Road-pro contract" }
                };

            #endregion

            #region Building identity entities 
            builder.Entity<ApplicationUser>()
                .Property(u => u.Id)
                .HasDefaultValueSql("newsequentialid()"); // генерация guid

            builder.Entity<ApplicationRole>()
               .Property(u => u.Id)
               .HasDefaultValueSql("newsequentialid()");

            builder.Entity<ApplicationRole>().HasData(roles);
            builder.Entity<ContractDocument>().HasData(contractDocument);
            #endregion
        }
    }
}
