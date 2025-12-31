import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { DoseSchedule } from '../../models';

@Component({
  selector: 'app-dose-schedule-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './dose-schedule-card.html',
  styleUrl: './dose-schedule-card.scss'
})
export class DoseScheduleCard {
  @Input({ required: true }) schedule!: DoseSchedule;
  @Output() edit = new EventEmitter<DoseSchedule>();
  @Output() remove = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.schedule);
  }

  onDelete(): void {
    this.remove.emit(this.schedule.doseScheduleId);
  }
}
