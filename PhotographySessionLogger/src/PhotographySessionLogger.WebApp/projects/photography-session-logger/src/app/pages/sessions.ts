import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { SessionService } from '../services';
import { SessionTypeLabels } from '../models';

@Component({
  selector: 'app-sessions',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="sessions">
      <div class="sessions__header">
        <h1 class="sessions__title">Photography Sessions</h1>
        <a mat-raised-button color="primary" routerLink="/sessions/new" class="sessions__add-btn">
          <mat-icon>add</mat-icon>
          New Session
        </a>
      </div>

      <mat-card class="sessions__card">
        <mat-card-content>
          <table mat-table [dataSource]="(sessions$ | async) || []" class="sessions__table">
            <ng-container matColumnDef="title">
              <th mat-header-cell *matHeaderCellDef>Title</th>
              <td mat-cell *matCellDef="let session">{{ session.title }}</td>
            </ng-container>

            <ng-container matColumnDef="sessionType">
              <th mat-header-cell *matHeaderCellDef>Type</th>
              <td mat-cell *matCellDef="let session">{{ getSessionTypeLabel(session.sessionType) }}</td>
            </ng-container>

            <ng-container matColumnDef="sessionDate">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let session">{{ session.sessionDate | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="location">
              <th mat-header-cell *matHeaderCellDef>Location</th>
              <td mat-cell *matCellDef="let session">{{ session.location || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="client">
              <th mat-header-cell *matHeaderCellDef>Client</th>
              <td mat-cell *matCellDef="let session">{{ session.client || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="photoCount">
              <th mat-header-cell *matHeaderCellDef>Photos</th>
              <td mat-cell *matCellDef="let session">{{ session.photoCount }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let session">
                <button mat-icon-button color="primary" [routerLink]="['/sessions', session.sessionId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteSession(session.sessionId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .sessions {
      padding: 2rem;
    }

    .sessions__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .sessions__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .sessions__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .sessions__card {
      width: 100%;
    }

    .sessions__table {
      width: 100%;
    }
  `]
})
export class Sessions implements OnInit {
  private readonly sessionService = inject(SessionService);

  sessions$ = this.sessionService.sessions$;
  displayedColumns = ['title', 'sessionType', 'sessionDate', 'location', 'client', 'photoCount', 'actions'];

  ngOnInit(): void {
    this.sessionService.getAll().subscribe();
  }

  getSessionTypeLabel(type: number): string {
    return SessionTypeLabels[type] || 'Unknown';
  }

  deleteSession(id: string): void {
    if (confirm('Are you sure you want to delete this session?')) {
      this.sessionService.delete(id).subscribe();
    }
  }
}
