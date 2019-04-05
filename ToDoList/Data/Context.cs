using Microsoft.EntityFrameworkCore;

using ToDoList.Models;

namespace ToDoList.Data
{
    public class Context : DbContext
    {
            public DbSet<ToDo> ToDoes { get; set; }

            public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
