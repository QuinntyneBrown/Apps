import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Observable } from 'rxjs';
import { SleepSessionService } from '../../services';
import { SleepSession } from '../../models';
import { SleepSessionCard } from '../../components';

@Component({
  selector: 'app-sleep-sessions',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    SleepSessionCard
  ],
  templateUrl: './sleep-sessions.html',
  styleUrl: './sleep-sessions.scss'
})
export class SleepSessions implements OnInit {
  private sleepSessionService = inject(SleepSessionService);

  sleepSessions$!: Observable<SleepSession[]>;

  ngOnInit(): void {
    this.loadSessions();
  }

  loadSessions(): void {
    this.sleepSessions$ = this.sleepSessionService.getSleepSessions();
  }

  onEdit(session: SleepSession): void {
    console.log('Edit session:', session);
    // TODO: Implement edit dialog
  }

  onDelete(sessionId: string): void {
    if (confirm('Are you sure you want to delete this sleep session?')) {
      this.sleepSessionService.deleteSleepSession(sessionId).subscribe({
        next: () => console.log('Session deleted successfully'),
        error: (error) => console.error('Error deleting session:', error)
      });
    }
  }

  onCreate(): void {
    console.log('Create new session');
    // TODO: Implement create dialog
  }
}
