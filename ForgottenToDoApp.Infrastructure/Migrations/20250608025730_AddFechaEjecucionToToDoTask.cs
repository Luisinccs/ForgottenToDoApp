using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgottenToDoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFechaEjecucionToToDoTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "ToDoTasks",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "ToDoTasks");
        }
    }
}
