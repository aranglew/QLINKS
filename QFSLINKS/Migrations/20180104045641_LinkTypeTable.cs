using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFSLINKS.Migrations
{
    public partial class LinkTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.AddColumn<int>(
                name: "SDR_QFS_DatauTopicUserID",
                table: "SDR_QFS_Data",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SDR_QFS_LinkType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LinkType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDR_QFS_LinkType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SDR_QFS_Data_SDR_QFS_DatauTopicUserID",
                table: "SDR_QFS_Data",
                column: "SDR_QFS_DatauTopicUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_SDR_QFS_Data_SDR_QFS_DataU_SDR_QFS_DatauTopicUserID",
                table: "SDR_QFS_Data",
                column: "SDR_QFS_DatauTopicUserID",
                principalTable: "SDR_QFS_DataU",
                principalColumn: "TopicUserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SDR_QFS_Data_SDR_QFS_DataU_SDR_QFS_DatauTopicUserID",
                table: "SDR_QFS_Data");

            migrationBuilder.DropTable(
                name: "SDR_QFS_LinkType");

            migrationBuilder.DropIndex(
                name: "IX_SDR_QFS_Data_SDR_QFS_DatauTopicUserID",
                table: "SDR_QFS_Data");

            migrationBuilder.DropColumn(
                name: "SDR_QFS_DatauTopicUserID",
                table: "SDR_QFS_Data");
        }
    }
}
