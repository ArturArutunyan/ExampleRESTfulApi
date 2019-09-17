﻿// <auto-generated />
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.DocumentCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DocumentName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DocumentCards");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DocumentName = "Main Document"
                        });
                });

            modelBuilder.Entity("DAL.Models.DocumentCardRoles", b =>
                {
                    b.Property<int>("DocumentCardId");

                    b.Property<int>("RoleId");

                    b.HasKey("DocumentCardId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("DocumentCardRoles");

                    b.HasData(
                        new
                        {
                            DocumentCardId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserName = "Artur"
                        },
                        new
                        {
                            Id = 2,
                            UserName = "Igor"
                        },
                        new
                        {
                            Id = 3,
                            UserName = "Lena"
                        },
                        new
                        {
                            Id = 4,
                            UserName = "Vladimir"
                        });
                });

            modelBuilder.Entity("DAL.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("DAL.Models.DocumentCardRoles", b =>
                {
                    b.HasOne("DAL.Models.DocumentCard", "DocumentCard")
                        .WithMany("DocumentCardRoles")
                        .HasForeignKey("DocumentCardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Role", "Role")
                        .WithMany("DocumentCardRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.UserRole", b =>
                {
                    b.HasOne("DAL.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
