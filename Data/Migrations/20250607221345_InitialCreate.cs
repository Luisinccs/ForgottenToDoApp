using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgottenToDoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    EsRepetitiva = table.Column<bool>(type: "INTEGER", nullable: false),
                    FrecuenciaRepeticion = table.Column<int>(type: "INTEGER", nullable: false),
                    UltimaFechaCompletada = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProximaFechaRepeticion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    GrupoId = table.Column<string>(type: "TEXT", nullable: true),
                    Prioridad = table.Column<int>(type: "INTEGER", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "INTEGER", nullable: true),
                    EsDescartable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskGroups_Nombre",
                table: "TaskGroups",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_GrupoId",
                table: "ToDoTasks",
                column: "GrupoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskGroups");

            migrationBuilder.DropTable(
                name: "ToDoTasks");
        }
    }
}
