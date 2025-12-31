import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { SessionService } from '../../services';
import { Session } from '../../models';

@Component({
  selector: 'app-sessions',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule
  ],
  templateUrl: './sessions.html',
  styleUrl: './sessions.scss'
})
export class Sessions implements OnInit {
  sessions: Session[] = [];
  displayedColumns: string[] = ['title', 'speaker', 'startTime', 'endTime', 'room', 'plansToAttend'];

  constructor(
    private sessionService: SessionService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadSessions();
  }

  loadSessions(): void {
    this.sessionService.getAll().subscribe({
      next: (sessions) => {
        this.sessions = sessions;
      },
      error: (error) => {
        console.error('Error loading sessions:', error);
        this.snackBar.open('Error loading sessions', 'Close', { duration: 3000 });
      }
    });
  }
}
