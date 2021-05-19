using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SeparateScheduleIntoExternalAndInternalModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "Schedules",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityLookupId",
                table: "Schedules",
                type: "int",
                nullable: false);

            migrationBuilder.RenameColumn(
                name: "Availability",
                table: "Schedules",
                newName: "ExternalId");

            migrationBuilder.CreateTable(
                name: "AvailabilityLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityLookups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AvailabilityLookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Free" });

            migrationBuilder.InsertData(
                table: "AvailabilityLookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Busy" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_AvailabilityLookupId",
                table: "Schedules",
                column: "AvailabilityLookupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AvailabilityLookups_AvailabilityLookupId",
                table: "Schedules",
                column: "AvailabilityLookupId",
                principalTable: "AvailabilityLookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("UPDATE [dbo].[Schedules] SET AvailabilityLookupId = 1 WHERE Availability = 1");
            migrationBuilder.Sql("UPDATE [dbo].[Schedules] SET AvailabilityLookupId = 2 WHERE Availability = 0");

            migrationBuilder.DropColumn("Schedules", "Availability");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AvailabilityLookups_AvailabilityLookupId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "AvailabilityLookups");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_AvailabilityLookupId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "AvailabilityLookupId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ExternalId",
                table: "Schedules",
                newName: "Availability");
        }
    }
}
