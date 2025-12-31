import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { SleepSession } from '../../models';

@Component({
  selector: 'app-sleep-session-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './sleep-session-card.html',
  styleUrl: './sleep-session-card.scss'
})
export class SleepSessionCard {
  @Input() session!: SleepSession;
  @Output() edit = new EventEmitter<SleepSession>();
  @Output() delete = new EventEmitter<string>();

  getSleepQualityColor(quality: number): string {
    switch (quality) {
      case 4: return 'primary';
      case 3: return 'accent';
      case 2: return 'warn';
      default: return '';
    }
  }

  formatDuration(minutes: number): string {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h ${mins}m`;
  }

  formatTime(dateString: string): string {
    return new Date(dateString).toLocaleTimeString('en-US', {
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    });
  }

  onEdit(): void {
    this.edit.emit(this.session);
  }

  onDelete(): void {
    this.delete.emit(this.session.sleepSessionId);
  }
}
