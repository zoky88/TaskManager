import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export interface TaskItem {
  id?: number;
  title: string;
  description?: string;
  status: string;
  dueDateTime: string;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'https://localhost:7178/api/TaskItems';

  constructor(private http: HttpClient) { }

  getTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  getTask(id: number): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  createTask(task: TaskItem): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, task)
      .pipe(catchError(this.handleError));
  }

  updateTask(id: number, task: TaskItem): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, task)
      .pipe(catchError(this.handleError));
  }

  deleteTask(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    // Log error to the console
    console.error('An error occurred:', error);
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}
