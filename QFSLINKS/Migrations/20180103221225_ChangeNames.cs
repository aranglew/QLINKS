using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFSLINKS.Migrations
{
    public partial class ChangeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "SDR_QFS_Data",
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
                    SortOrder = table.Column<decimal>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    TopicID = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDR_QFS_Data", x => x.TID);
                });

            migrationBuilder.CreateTable(
                name: "SDR_QFS_DataU",
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
                    table.PrimaryKey("PK_SDR_QFS_DataU", x => x.TopicUserID);
                });

            migrationBuilder.CreateTable(
                name: "SDR_QFS_Division",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDR_QFS_Division", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SDR_QFS_Data");

            migrationBuilder.DropTable(
                name: "SDR_QFS_DataU");

            migrationBuilder.DropTable(
                name: "SDR_QFS_Division");

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
    }
}
