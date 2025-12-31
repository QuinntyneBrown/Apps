import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { FocusSessionsService, SessionAnalyticsService } from '../services';
import { FocusSession, SessionAnalytics } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule,
    RouterLink
  ],
  template: `
    <div class="dashboard">
      <div class="dashboard__header">
        <h1 class="dashboard__title">Dashboard</h1>
        <button mat-raised-button color="primary" routerLink="/sessions">
          <mat-icon>add</mat-icon>
          New Session
        </button>
      </div>

      <div class="dashboard__stats">
        <mat-card class="dashboard__stat-card">
          <mat-card-header>
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--primary">timer</mat-icon>
            <mat-card-title>Active Sessions</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat-value">{{ activeSessions.length }}</div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-header>
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--accent">check_circle</mat-icon>
            <mat-card-title>Completed Today</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat-value">{{ completedToday }}</div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-header>
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--warn">notifications_off</mat-icon>
            <mat-card-title>Distractions Today</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat-value">{{ distractionsToday }}</div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-header>
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--success">trending_up</mat-icon>
            <mat-card-title>Focus Score</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat-value">{{ averageScore }}%</div>
          </mat-card-content>
        </mat-card>
      </div>

      <div class="dashboard__content">
        <mat-card class="dashboard__section">
          <mat-card-header>
            <mat-card-title>Active Sessions</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="activeSessions.length === 0" class="dashboard__empty">
              No active sessions
            </div>
            <div *ngFor="let session of activeSessions" class="dashboard__session">
              <div class="dashboard__session-info">
                <h3 class="dashboard__session-name">{{ session.name }}</h3>
                <p class="dashboard__session-meta">
                  Started: {{ session.startTime | date:'short' }} |
                  Duration: {{ session.plannedDurationMinutes }} min
                </p>
              </div>
              <button mat-raised-button color="accent" (click)="completeSession(session.focusSessionId)">
                Complete
              </button>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__section">
          <mat-card-header>
            <mat-card-title>Recent Sessions</mat-card-title>
            <button mat-button routerLink="/sessions">View All</button>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="recentSessions.length === 0" class="dashboard__empty">
              No recent sessions
            </div>
            <div *ngFor="let session of recentSessions" class="dashboard__session">
              <div class="dashboard__session-info">
                <h3 class="dashboard__session-name">{{ session.name }}</h3>
                <p class="dashboard__session-meta">
                  {{ session.startTime | date:'short' }} |
                  Score: {{ session.focusScore || 'N/A' }}
                </p>
              </div>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;
    }

    .dashboard__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .dashboard__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .dashboard__stats {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 16px;
      margin-bottom: 24px;
    }

    .dashboard__stat-card {
      mat-card-header {
        display: flex;
        align-items: center;
        gap: 12px;
      }
    }

    .dashboard__stat-icon {
      font-size: 32px;
      width: 32px;
      height: 32px;
    }

    .dashboard__stat-icon--primary {
      color: #1976d2;
    }

    .dashboard__stat-icon--accent {
      color: #4caf50;
    }

    .dashboard__stat-icon--warn {
      color: #ff9800;
    }

    .dashboard__stat-icon--success {
      color: #8bc34a;
    }

    .dashboard__stat-value {
      font-size: 36px;
      font-weight: 600;
      margin-top: 8px;
    }

    .dashboard__content {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 24px;
    }

    .dashboard__section {
      mat-card-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
      }
    }

    .dashboard__empty {
      padding: 32px;
      text-align: center;
      color: rgba(0, 0, 0, 0.54);
    }

    .dashboard__session {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 16px;
      border-bottom: 1px solid rgba(0, 0, 0, 0.12);
    }

    .dashboard__session:last-child {
      border-bottom: none;
    }

    .dashboard__session-info {
      flex: 1;
    }

    .dashboard__session-name {
      margin: 0 0 4px 0;
      font-size: 16px;
      font-weight: 500;
    }

    .dashboard__session-meta {
      margin: 0;
      font-size: 14px;
      color: rgba(0, 0, 0, 0.54);
    }

    @media (max-width: 768px) {
      .dashboard__content {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class Dashboard implements OnInit {
  sessions$: Observable<FocusSession[]>;
  activeSessions: FocusSession[] = [];
  recentSessions: FocusSession[] = [];
  completedToday = 0;
  distractionsToday = 0;
  averageScore = 0;

  constructor(
    private focusSessionsService: FocusSessionsService,
    private analyticsService: SessionAnalyticsService
  ) {
    this.sessions$ = this.focusSessionsService.sessions$;
  }

  ngOnInit(): void {
    this.loadSessions();
  }

  loadSessions(): void {
    this.focusSessionsService.getSessions().subscribe(sessions => {
      this.activeSessions = sessions.filter(s => !s.isCompleted);
      this.recentSessions = sessions
        .filter(s => s.isCompleted)
        .sort((a, b) => new Date(b.startTime).getTime() - new Date(a.startTime).getTime())
        .slice(0, 5);

      const today = new Date();
      today.setHours(0, 0, 0, 0);

      const todaySessions = sessions.filter(s => {
        const sessionDate = new Date(s.startTime);
        sessionDate.setHours(0, 0, 0, 0);
        return sessionDate.getTime() === today.getTime();
      });

      this.completedToday = todaySessions.filter(s => s.isCompleted).length;
      this.distractionsToday = todaySessions.reduce((sum, s) => sum + s.distractionCount, 0);

      const sessionsWithScore = todaySessions.filter(s => s.focusScore !== null && s.focusScore !== undefined);
      if (sessionsWithScore.length > 0) {
        this.averageScore = Math.round(
          sessionsWithScore.reduce((sum, s) => sum + (s.focusScore || 0), 0) / sessionsWithScore.length
        );
      }
    });
  }

  completeSession(id: string): void {
    this.focusSessionsService.completeSession(id, {}).subscribe(() => {
      this.loadSessions();
    });
  }
}
