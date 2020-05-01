using Microsoft.EntityFrameworkCore;

namespace TodoList.Api.DataAccess
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
        { }

        public DbSet <TodoTask> TodoTask { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTask>(entity =>
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd()
            );

            modelBuilder.Entity<TodoTask>(entity =>
                entity.Property(e => e.Description)
                    .IsRequired()
            );
        }
    }
}
