import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { SessionService } from '../../services';
import { Session } from '../../models';

@Component({
  selector: 'app-sessions',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatSnackBarModule
  ],
  templateUrl: './sessions.html',
  styleUrl: './sessions.scss'
})
export class Sessions implements OnInit {
  sessions$: Observable<Session[]>;

  // For demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private sessionService: SessionService,
    private snackBar: MatSnackBar
  ) {
    this.sessions$ = this.sessionService.sessions$;
  }

  ngOnInit(): void {
    this.loadSessions();
  }

  loadSessions(): void {
    this.sessionService.getByUserId(this.userId).subscribe();
  }

  endSession(session: Session): void {
    this.sessionService.endSession(session.sessionId).subscribe({
      next: () => {
        this.snackBar.open('Session ended', 'Close', { duration: 2000 });
      }
    });
  }

  deleteSession(session: Session): void {
    if (confirm('Are you sure you want to delete this session?')) {
      this.sessionService.delete(session.sessionId).subscribe({
        next: () => {
          this.snackBar.open('Session deleted', 'Close', { duration: 2000 });
        }
      });
    }
  }

  isActive(session: Session): boolean {
    return !session.endTime;
  }
}
