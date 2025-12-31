import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ResourceService, NoteService, ReadingProgressService } from '../services';
import { Observable, combineLatest, map } from 'rxjs';

interface DashboardStats {
  totalResources: number;
  totalNotes: number;
  inProgressCount: number;
  completedCount: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__stats" *ngIf="stats$ | async as stats">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">library_books</mat-icon>
            <mat-card-title>Total Resources</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.totalResources }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/resources" color="primary">View Resources</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">note</mat-icon>
            <mat-card-title>Total Notes</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.totalNotes }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/notes" color="primary">View Notes</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">schedule</mat-icon>
            <mat-card-title>In Progress</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.inProgressCount }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/progress" color="primary">View Progress</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">check_circle</mat-icon>
            <mat-card-title>Completed</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.completedCount }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/progress" color="primary">View Progress</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
    }

    .dashboard__title {
      margin: 0 0 24px;
      font-size: 32px;
      font-weight: 400;
    }

    .dashboard__stats {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 24px;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-icon {
      color: #1976d2;
      font-size: 32px;
      width: 32px;
      height: 32px;
      margin-right: 16px;
    }

    .dashboard__card-value {
      font-size: 48px;
      font-weight: 300;
      color: #1976d2;
      margin: 16px 0;
    }
  `]
})
export class Dashboard implements OnInit {
  private resourceService = inject(ResourceService);
  private noteService = inject(NoteService);
  private progressService = inject(ReadingProgressService);

  stats$!: Observable<DashboardStats>;

  ngOnInit() {
    this.resourceService.getAll().subscribe();
    this.noteService.getAll().subscribe();
    this.progressService.getAll().subscribe();

    this.stats$ = combineLatest([
      this.resourceService.resources$,
      this.noteService.notes$,
      this.progressService.progress$
    ]).pipe(
      map(([resources, notes, progress]) => ({
        totalResources: resources.length,
        totalNotes: notes.length,
        inProgressCount: progress.filter(p => p.status === 'In Progress').length,
        completedCount: progress.filter(p => p.status === 'Completed').length
      }))
    );
  }
}
