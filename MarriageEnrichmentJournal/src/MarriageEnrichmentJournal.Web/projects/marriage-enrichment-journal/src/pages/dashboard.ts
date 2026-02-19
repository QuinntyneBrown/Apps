import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { GratitudeService } from '../services/gratitude.service';
import { JournalEntryService } from '../services/journal-entry.service';
import { ReflectionService } from '../services/reflection.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>
      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">book</mat-icon>
              Journal Entries
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (journalEntryService.journalEntries$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Track your marriage journey</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/journal-entries">View All</a>
            <a mat-raised-button color="primary" routerLink="/journal-entries/create">Add New</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">favorite</mat-icon>
              Gratitudes
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (gratitudeService.gratitudes$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Express your appreciation</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/gratitudes">View All</a>
            <a mat-raised-button color="primary" routerLink="/gratitudes/create">Add New</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">lightbulb</mat-icon>
              Reflections
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (reflectionService.reflections$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Reflect on your relationship</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/reflections">View All</a>
            <a mat-raised-button color="primary" routerLink="/reflections/create">Add New</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
    }

    .dashboard__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 2rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .dashboard__card-icon {
      color: #3f51b5;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: bold;
      color: #3f51b5;
      margin: 1rem 0;
      text-align: center;
    }

    .dashboard__card-description {
      text-align: center;
      color: #666;
    }
  `]
})
export class Dashboard implements OnInit {
  gratitudeService = inject(GratitudeService);
  journalEntryService = inject(JournalEntryService);
  reflectionService = inject(ReflectionService);

  ngOnInit(): void {
    this.gratitudeService.getAll().subscribe();
    this.journalEntryService.getAll().subscribe();
    this.reflectionService.getAll().subscribe();
  }
}
