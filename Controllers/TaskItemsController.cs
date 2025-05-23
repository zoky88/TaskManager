﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Controller for managing task items.
    /// Provides endpoints to create, retrieve, update, and delete task items.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly TasksDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemsController"/> class.
        /// </summary>
        /// <param name="context">The database context for task items.</param>
        public TaskItemsController(TasksDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all task items.
        /// </summary>
        /// <returns>A list of all <see cref="TaskItem"/> objects.</returns>
        /// <response code="200">Returns the list of task items.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific task item by its ID.
        /// </summary>
        /// <param name="id">The ID of the task item to retrieve.</param>
        /// <returns>The <see cref="TaskItem"/> object with the specified ID.</returns>
        /// <response code="200">Returns the task item.</response>
        /// <response code="404">If the task item is not found.</response>
        [HttpGet("{id}", Name = "GetTaskItem")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        /// <summary>
        /// Updates an existing task item.
        /// </summary>
        /// <param name="id">The ID of the task item to update.</param>
        /// <param name="taskItem">The updated <see cref="TaskItem"/> object.</param>
        /// <returns>An <see cref="IActionResult"/> indicating whether the update was successful.</returns>
        /// <response code="204">If the update is successful.</response>
        /// <response code="400">If the ID in the request doesn't match the task item's ID.</response>
        /// <response code="404">If the task item is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new task item.
        /// </summary>
        /// <param name="taskItem">The <see cref="TaskItem"/> object to create.</param>
        /// <returns>The newly created <see cref="TaskItem"/> object.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/TaskItems
        ///     {
        ///         "title": "New Task",
        ///         "description": "Optional description",
        ///         "status": "Pending",
        ///         "dueDateTime": "2025-05-01T12:00:00"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created task item.</response>
        /// <response code="400">If the task item is null or invalid.</response>
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            _context.Tasks.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
        }

        /// <summary>
        /// Deletes a specific task item.
        /// </summary>
        /// <param name="id">The ID of the task item to delete.</param>
        /// <returns>The deleted <see cref="TaskItem"/> object.</returns>
        /// <response code="200">Returns the deleted task item.</response>
        /// <response code="404">If the task item is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a task item exists in the database.
        /// </summary>
        /// <param name="id">The ID of the task item.</param>
        /// <returns>True if the task item exists; otherwise, false.</returns>
        private bool TaskItemExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
