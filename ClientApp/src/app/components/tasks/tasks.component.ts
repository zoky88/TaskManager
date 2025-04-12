import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService, TaskItem } from '../../services/task.service';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {
  public showSuccessMessage = false;

  tasks: TaskItem[] = [];
  newTask: TaskItem = { title: '', status: '', dueDateTime: new Date().toISOString().slice(0, 16) };
  editedTask: TaskItem | null = null;

  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe(data => {
      this.tasks = data;
    });
  }

  addTask(): void {
    if (this.newTask.title.trim()) {
      // Convert the UI’s local date-time string (yyyy-MM-ddTHH:mm) into a full ISO string.
      // For example, if newTask.dueDateTime = "2025-04-12T19:34", then new Date(...) produces a Date
      // and .toISOString() gives "2025-04-12T19:34:00.000Z".
      const isoDueDate = new Date(this.newTask.dueDateTime).toISOString();
      const taskPayload: TaskItem = {
        ...this.newTask,
        dueDateTime: isoDueDate
      };

      this.taskService.createTask(taskPayload).subscribe({
        next: () => {
          this.loadTasks();
          // Reset newTask for the UI using the proper local format.
          this.newTask = { title: '', status: '', dueDateTime: new Date().toISOString().slice(0, 16) };
        },

        error: err => console.error(err)
      });

      // Show success popup for 3 seconds
      this.showSuccessMessage = true;
      setTimeout(() => {
        this.showSuccessMessage = false;
      }, 3000);
    }
  }


  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe(() => {
      this.loadTasks();
    });
  }

  setEditingTask(task: TaskItem): void {
    this.editedTask = { ...task }; // clone the task for editing
  }

  updateTask(): void {
    if (this.editedTask && this.editedTask.id) {
      this.taskService.updateTask(this.editedTask.id, this.editedTask).subscribe(() => {
        this.loadTasks();
        this.editedTask = null;
      });
    }
  }

  cancelEdit(): void {
    this.editedTask = null;
  }
}
