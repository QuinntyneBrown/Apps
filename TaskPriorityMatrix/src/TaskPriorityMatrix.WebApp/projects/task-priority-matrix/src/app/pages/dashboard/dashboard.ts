import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PriorityTask, Urgency, Importance, TaskStatus, Category } from '../../models';
import { PriorityTaskService, CategoryService } from '../../services';
import { TaskCard } from '../../components/task-card';
import { CreateTaskDialog } from '../../components/create-task-dialog';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, TaskCard],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _taskService = inject(PriorityTaskService);
  private _categoryService = inject(CategoryService);
  private _dialog = inject(MatDialog);

  tasks$ = this._taskService.tasks$;
  categories$ = this._categoryService.categories$;

  doFirstTasks$!: Observable<PriorityTask[]>;
  scheduleTasks$!: Observable<PriorityTask[]>;
  delegateTasks$!: Observable<PriorityTask[]>;
  eliminateTasks$!: Observable<PriorityTask[]>;

  ngOnInit(): void {
    this._taskService.getAll().subscribe();
    this._categoryService.getAll().subscribe();

    // Filter tasks by quadrant
    this.doFirstTasks$ = this.tasks$.pipe(
      map(tasks => tasks.filter(t =>
        t.urgency === Urgency.Urgent && t.importance === Importance.Important && t.status !== TaskStatus.Completed
      ))
    );

    this.scheduleTasks$ = this.tasks$.pipe(
      map(tasks => tasks.filter(t =>
        t.urgency === Urgency.NotUrgent && t.importance === Importance.Important && t.status !== TaskStatus.Completed
      ))
    );

    this.delegateTasks$ = this.tasks$.pipe(
      map(tasks => tasks.filter(t =>
        t.urgency === Urgency.Urgent && t.importance === Importance.NotImportant && t.status !== TaskStatus.Completed
      ))
    );

    this.eliminateTasks$ = this.tasks$.pipe(
      map(tasks => tasks.filter(t =>
        t.urgency === Urgency.NotUrgent && t.importance === Importance.NotImportant && t.status !== TaskStatus.Completed
      ))
    );
  }

  onCreateTask(): void {
    this.categories$.subscribe(categories => {
      const dialogRef = this._dialog.open(CreateTaskDialog, {
        width: '500px',
        data: { categories }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._taskService.create(result).subscribe();
        }
      });
    });
  }

  onEditTask(task: PriorityTask): void {
    this.categories$.subscribe(categories => {
      const dialogRef = this._dialog.open(CreateTaskDialog, {
        width: '500px',
        data: { task, categories }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._taskService.update({
            priorityTaskId: task.priorityTaskId,
            ...result,
            status: task.status
          }).subscribe();
        }
      });
    });
  }

  onDeleteTask(taskId: string): void {
    if (confirm('Are you sure you want to delete this task?')) {
      this._taskService.delete(taskId).subscribe();
    }
  }

  onStatusChange(event: { task: PriorityTask; status: TaskStatus }): void {
    this._taskService.updateStatus(event.task, event.status).subscribe();
  }
}
