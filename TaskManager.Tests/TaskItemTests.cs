using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using Xunit;

namespace TaskManager.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TaskItem"/> class.
    /// </summary>
    public class TaskItemTests
    {
        /// <summary>
        /// Creates an in-memory database context for testing purposes.
        /// </summary>
        /// <returns>A new instance of <see cref="TasksDbContext"/> configured to use an in-memory database.</returns>
        private TasksDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TasksDbContext(options);
        }

        /// <summary>
        /// Verifies that creating a <see cref="TaskItem"/> and saving it to the database sets its ID to a value greater than zero.
        /// </summary>
        [Fact]
        public void CreateTaskItem_SetsId_GreaterThanZero()
        {
            // Arrange
            var dbContext = GetDbContext();
            var task = new TaskItem
            {
                Title = "Test Task",
                Status = "Pending",
                DueDateTime = DateTime.Now.AddDays(1)
            };

            // Act
            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            // Assert
            Assert.True(task.Id > 0, "The task ID should be greater than zero after saving.");
        }
    }
}
