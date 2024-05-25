using Microsoft.EntityFrameworkCore;
using WebApiTodoList.Data.Models;

namespace WebApiTodoList.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(t =>
            {
                t.ToTable("Todos");
                t.Property(t => t.Titre).HasMaxLength(500);
            });
 
        }


    }
}
