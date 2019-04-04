using Microsoft.EntityFrameworkCore;

using ToDoList.Models;

namespace ToDoList.Data
{
    public class Context : DbContext
    {
            public DbSet<ToDo> Tasks { get; set; }

            public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
