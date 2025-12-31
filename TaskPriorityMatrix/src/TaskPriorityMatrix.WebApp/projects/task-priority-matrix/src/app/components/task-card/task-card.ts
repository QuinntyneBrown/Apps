import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { PriorityTask, Urgency, Importance, TaskStatus } from '../../models';

@Component({
  selector: 'app-task-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './task-card.html',
  styleUrl: './task-card.scss'
})
export class TaskCard {
  @Input({ required: true }) task!: PriorityTask;
  @Output() delete = new EventEmitter<string>();
  @Output() edit = new EventEmitter<PriorityTask>();
  @Output() statusChange = new EventEmitter<{ task: PriorityTask; status: TaskStatus }>();

  Urgency = Urgency;
  Importance = Importance;
  TaskStatus = TaskStatus;

  onDelete(): void {
    this.delete.emit(this.task.priorityTaskId);
  }

  onEdit(): void {
    this.edit.emit(this.task);
  }

  onStatusChange(status: TaskStatus): void {
    this.statusChange.emit({ task: this.task, status });
  }

  getQuadrant(): string {
    if (this.task.urgency === Urgency.Urgent && this.task.importance === Importance.Important) {
      return 'Do First';
    } else if (this.task.urgency === Urgency.NotUrgent && this.task.importance === Importance.Important) {
      return 'Schedule';
    } else if (this.task.urgency === Urgency.Urgent && this.task.importance === Importance.NotImportant) {
      return 'Delegate';
    } else {
      return 'Eliminate';
    }
  }

  getQuadrantColor(): string {
    if (this.task.urgency === Urgency.Urgent && this.task.importance === Importance.Important) {
      return 'warn';
    } else if (this.task.urgency === Urgency.NotUrgent && this.task.importance === Importance.Important) {
      return 'primary';
    } else if (this.task.urgency === Urgency.Urgent && this.task.importance === Importance.NotImportant) {
      return 'accent';
    } else {
      return '';
    }
  }
}
