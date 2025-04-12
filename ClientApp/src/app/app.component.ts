import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TasksComponent } from './components/tasks/tasks.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    TasksComponent
  ],
  template: '<app-tasks></app-tasks>',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TaskManager';
}
