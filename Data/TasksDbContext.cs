using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    /// <summary>
    /// Represents the database context for managing tasks in the application.
    /// </summary>
    public class TasksDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public TasksDbContext(DbContextOptions<TasksDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet representing the collection of task items in the database.
        /// </summary>
        public DbSet<TaskItem> Tasks { get; set; }
    }
}

