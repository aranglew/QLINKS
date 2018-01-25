using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFSLINKS.Migrations
{
    public partial class InititalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    TID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    GroupID = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    SortOrder = table.Column<decimal>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    TopicID = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.TID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    TopicUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Access = table.Column<string>(nullable: true),
                    AccessA = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Division = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    SortOrder = table.Column<decimal>(nullable: false),
                    TopicID = table.Column<int>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true),
                    UserInitials = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserPhone = table.Column<string>(nullable: true),
                    VimsAccess = table.Column<string>(nullable: true),
                    VimsDelegate = table.Column<int>(nullable: false),
                    VimsVisible = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.TopicUserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
