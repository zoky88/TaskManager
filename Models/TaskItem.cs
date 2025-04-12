using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    /// <summary>
    /// Represents a task item with details such as title, description, status, and due date.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the task item.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the task item.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the task item.
        /// </summary>
        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the due date and time for the task item.
        /// </summary>
        [Required(ErrorMessage = "Due date and time are required.")]
        public DateTime DueDateTime { get; set; }
    }
}
