import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Milestone } from '../../models';

@Component({
  selector: 'app-milestone-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule
  ],
  templateUrl: './milestone-card.html',
  styleUrl: './milestone-card.scss'
})
export class MilestoneCard {
  @Input({ required: true }) milestone!: Milestone;
  @Output() toggleComplete = new EventEmitter<Milestone>();
  @Output() delete = new EventEmitter<string>();

  onToggleComplete(): void {
    this.toggleComplete.emit(this.milestone);
  }

  onDelete(): void {
    if (confirm('Are you sure you want to delete this milestone?')) {
      this.delete.emit(this.milestone.milestoneId);
    }
  }
}
