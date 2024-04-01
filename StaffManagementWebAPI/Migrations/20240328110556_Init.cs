using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace StaffManagementWebAPI.Migrations
{
	public partial class Init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Staff",
				columns: table => new
				{
					StaffId = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
					FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
					Gender = table.Column<int>(type: "int", maxLength: 1, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Staff", x => x.StaffId);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Staff");
		}
	}
}
