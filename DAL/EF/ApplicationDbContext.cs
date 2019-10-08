/// Hints
/// add-migration name -outputdir "C:\Users\ArutyunyanAV\source\repos\ExampleRESTfulApi\DAL\EF\Migrations"

using DAL.Enums;
using DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ContractDocument> contractDocuments { get; set; }

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

            public ApplicationDbContextFactory(IHostingEnvironment env)
            {
                _env = env;
            }

            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
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

            var contractDocument = new ContractDocument[]
                {
                    new ContractDocument() { Guid = Guid.NewGuid(), Title = "Titul2005", DocumentName = "titul contract" },
                    new ContractDocument() { Guid = Guid.NewGuid(), Title = "Road-pro", DocumentName = "Road-pro contract" }
                };

            #endregion

            #region Building identity entities 
            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            builder.Entity<IdentityUser>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()"); // генерация guid

            builder.Entity<ContractDocument>().HasData(contractDocument);
            #endregion
        }
    }
}
