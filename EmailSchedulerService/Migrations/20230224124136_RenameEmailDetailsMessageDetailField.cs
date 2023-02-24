using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailSchedulerService.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmailDetailsMessageDetailField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageDetail",
                table: "EmailDetails",
                newName: "Message");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "EmailDetails",
                newName: "MessageDetail");
        }
    }
}
