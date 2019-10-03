using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class DocumentInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contractDocuments",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    DocumentName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractDocuments", x => x.Guid);
                });

            migrationBuilder.InsertData(
                table: "contractDocuments",
                columns: new[] { "Guid", "DocumentName", "Title" },
                values: new object[] { new Guid("ce9b1600-e87d-400a-bddb-15a2b4288210"), "titul contract", "Titul2005" });

            migrationBuilder.InsertData(
                table: "contractDocuments",
                columns: new[] { "Guid", "DocumentName", "Title" },
                values: new object[] { new Guid("3adeadd8-ea4b-4641-a3a8-17967cd80e9d"), "Road-pro contract", "Road-pro" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractDocuments");
        }
    }
}
