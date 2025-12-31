import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Goal } from '../../models';

@Component({
  selector: 'app-goal-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './goal-card.html',
  styleUrls: ['./goal-card.scss']
})
export class GoalCard {
  @Input() goal!: Goal;
  @Output() edit = new EventEmitter<Goal>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.goal);
  }

  onDelete(): void {
    this.delete.emit(this.goal.goalId);
  }
}
