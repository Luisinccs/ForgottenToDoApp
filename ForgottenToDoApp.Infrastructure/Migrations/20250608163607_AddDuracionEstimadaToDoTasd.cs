using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgottenToDoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDuracionEstimadaToDoTasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DuracionEstimada",
                table: "ToDoTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuracionEstimada",
                table: "ToDoTasks");
        }
    }
}
