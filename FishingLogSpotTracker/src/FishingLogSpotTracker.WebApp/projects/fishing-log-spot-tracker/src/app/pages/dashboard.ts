import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { TripsService, SpotsService, CatchesService } from '../services';
import { Trip, Spot, Catch } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule, RouterLink],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__stats">
        <mat-card class="dashboard__stat-card">
          <mat-card-content>
            <div class="dashboard__stat-icon">
              <mat-icon>directions_boat</mat-icon>
            </div>
            <h2 class="dashboard__stat-value">{{ (trips$ | async)?.length || 0 }}</h2>
            <p class="dashboard__stat-label">Total Trips</p>
            <a mat-button color="primary" routerLink="/trips">View All</a>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-content>
            <div class="dashboard__stat-icon">
              <mat-icon>place</mat-icon>
            </div>
            <h2 class="dashboard__stat-value">{{ (spots$ | async)?.length || 0 }}</h2>
            <p class="dashboard__stat-label">Fishing Spots</p>
            <a mat-button color="primary" routerLink="/spots">View All</a>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-content>
            <div class="dashboard__stat-icon">
              <mat-icon>phishing</mat-icon>
            </div>
            <h2 class="dashboard__stat-value">{{ (catches$ | async)?.length || 0 }}</h2>
            <p class="dashboard__stat-label">Total Catches</p>
            <a mat-button color="primary" routerLink="/catches">View All</a>
          </mat-card-content>
        </mat-card>
      </div>

      <div class="dashboard__recent">
        <mat-card>
          <mat-card-header>
            <mat-card-title>Recent Trips</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__recent-list" *ngIf="(trips$ | async) as trips">
              <div *ngFor="let trip of trips.slice(0, 5)" class="dashboard__recent-item">
                <div>
                  <strong>{{ trip.spotName || 'Unknown Spot' }}</strong>
                  <p>{{ trip.tripDate | date:'short' }}</p>
                </div>
                <span class="dashboard__recent-badge">{{ trip.catchCount }} catches</span>
              </div>
              <p *ngIf="trips.length === 0" class="dashboard__empty">No trips yet</p>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
    }

    .dashboard__title {
      margin: 0 0 24px 0;
      font-size: 32px;
      font-weight: 500;
    }

    .dashboard__stats {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 24px;
      margin-bottom: 24px;
    }

    .dashboard__stat-card {
      text-align: center;
    }

    .dashboard__stat-icon {
      display: flex;
      justify-content: center;
      margin-bottom: 16px;
    }

    .dashboard__stat-icon mat-icon {
      font-size: 48px;
      width: 48px;
      height: 48px;
      color: #3f51b5;
    }

    .dashboard__stat-value {
      margin: 0;
      font-size: 48px;
      font-weight: 500;
      color: #3f51b5;
    }

    .dashboard__stat-label {
      margin: 8px 0 16px 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .dashboard__recent-list {
      display: flex;
      flex-direction: column;
      gap: 12px;
    }

    .dashboard__recent-item {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 12px;
      border: 1px solid rgba(0, 0, 0, 0.12);
      border-radius: 4px;
    }

    .dashboard__recent-item strong {
      display: block;
      margin-bottom: 4px;
    }

    .dashboard__recent-item p {
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
      font-size: 14px;
    }

    .dashboard__recent-badge {
      background-color: #3f51b5;
      color: white;
      padding: 4px 12px;
      border-radius: 12px;
      font-size: 14px;
    }

    .dashboard__empty {
      text-align: center;
      color: rgba(0, 0, 0, 0.6);
      padding: 24px;
    }
  `]
})
export class Dashboard implements OnInit {
  trips$ = this.tripsService.trips$;
  spots$ = this.spotsService.spots$;
  catches$ = this.catchesService.catches$;

  constructor(
    private tripsService: TripsService,
    private spotsService: SpotsService,
    private catchesService: CatchesService
  ) {}

  ngOnInit(): void {
    this.tripsService.getTrips().subscribe();
    this.spotsService.getSpots().subscribe();
    this.catchesService.getCatches().subscribe();
  }
}
