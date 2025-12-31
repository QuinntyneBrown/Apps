import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { Goal, GoalCategoryLabels, GoalStatusLabels } from '../../models';

@Component({
  selector: 'app-goal-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatProgressBarModule
  ],
  templateUrl: './goal-card.html',
  styleUrl: './goal-card.scss'
})
export class GoalCard {
  @Input({ required: true }) goal!: Goal;
  @Output() delete = new EventEmitter<string>();

  categoryLabels = GoalCategoryLabels;
  statusLabels = GoalStatusLabels;

  onDelete(): void {
    if (confirm('Are you sure you want to delete this goal?')) {
      this.delete.emit(this.goal.goalId);
    }
  }

  getPriorityLabel(priority: number): string {
    return `Priority ${priority}`;
  }

  getStatusColor(status: number): string {
    const colors: Record<number, string> = {
      0: 'accent',
      1: 'primary',
      2: 'success',
      3: 'warn',
      4: ''
    };
    return colors[status] || '';
  }
}
