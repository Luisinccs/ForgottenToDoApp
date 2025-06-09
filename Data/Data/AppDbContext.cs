// 2025-06-07
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ForgottenToDoApp.Core.Entities; // Necesario para acceder a ToDoTask y TaskGroup

namespace ForgottenToDoApp.Infrastructure.Data;

/// <sum>Contexto de base de datos para la aplicacion ForgottenToDoApp.</sum>
public class AppDbContext : DbContext {
	/// <sum>Conjunto de datos para las tareas pendientes.</sum>
	public DbSet<ToDoTask> ToDoTasks { get; set; }

	/// <sum>Conjunto de datos para los grupos de tareas.</sum>
	public DbSet<TaskGroup> TaskGroups { get; set; }

	/// <sum>Inicializa una nueva instancia de la clase AppDbContext.</sum>
	/// <param name="options">Opciones de configuracion para el contexto de la base de datos.</param>
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	/// <sum>Configura el modelo que se utiliza para crear la base de datos.</sum>
	/// <param name="modelBuilder">Constructor de modelos utilizado para configurar las entidades.</param>
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		// Configuracion para ToDoTask
		modelBuilder.Entity<ToDoTask>().HasKey(t => t.Id);
		// Opcional: Indice para el GrupoId si esperas muchas consultas por grupo
		modelBuilder.Entity<ToDoTask>().HasIndex(t => t.GrupoId);
		// Puedes agregar mas configuraciones aqui si es necesario, ej. configuracion de campos

		// Configuracion para TaskGroup
		modelBuilder.Entity<TaskGroup>().HasKey(g => g.Id);
		// Asegurarse que el nombre del grupo es unico
		modelBuilder.Entity<TaskGroup>().HasIndex(g => g.Nombre).IsUnique();
	}
}