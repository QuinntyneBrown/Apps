import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PartsService, ModificationsService, InstallationsService } from '../services';

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
            <mat-card-title>Parts</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat">{{ (parts$ | async)?.length || 0 }}</div>
            <p>Total parts in database</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/parts">View Parts</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title>Modifications</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat">{{ (modifications$ | async)?.length || 0 }}</div>
            <p>Total modifications available</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/modifications">View Modifications</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title>Installations</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stat">{{ (installations$ | async)?.length || 0 }}</div>
            <p>Total installations logged</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/installations">View Installations</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;

      &__title {
        margin-bottom: 2rem;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 2rem;
      }

      &__card {
        mat-card-content {
          padding: 1.5rem 0;
        }

        mat-card-actions {
          padding: 0 1rem 1rem;
        }
      }

      &__stat {
        font-size: 3rem;
        font-weight: bold;
        color: var(--primary-color, #3f51b5);
      }
    }
  `]
})
export class Dashboard implements OnInit {
  parts$ = this.partsService.parts$;
  modifications$ = this.modificationsService.modifications$;
  installations$ = this.installationsService.installations$;

  constructor(
    private partsService: PartsService,
    private modificationsService: ModificationsService,
    private installationsService: InstallationsService
  ) {}

  ngOnInit() {
    this.partsService.getAll().subscribe();
    this.modificationsService.getAll().subscribe();
    this.installationsService.getAll().subscribe();
  }
}
