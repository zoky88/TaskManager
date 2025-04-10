using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using Xunit;

namespace TaskManager.Tests
{
    public class TaskItemTests
    {
        // Helper method to create an in-memory database context for tests
        private TasksDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TasksDbContext(options);
        }

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
