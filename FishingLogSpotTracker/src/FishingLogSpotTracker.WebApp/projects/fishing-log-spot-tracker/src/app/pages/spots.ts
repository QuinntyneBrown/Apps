import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatGridListModule } from '@angular/material/grid-list';
import { SpotsService } from '../services';
import { Spot, LocationType } from '../models';

@Component({
  selector: 'app-spots',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatGridListModule],
  template: `
    <div class="spots">
      <div class="spots__header">
        <h1 class="spots__title">Fishing Spots</h1>
        <button mat-raised-button color="primary" (click)="onCreateSpot()">
          <mat-icon>add</mat-icon>
          New Spot
        </button>
      </div>

      <div class="spots__grid">
        <mat-card *ngFor="let spot of spots$ | async" class="spots__card">
          <mat-card-header>
            <mat-card-title class="spots__card-title">
              {{ spot.name }}
              <mat-icon *ngIf="spot.isFavorite" class="spots__favorite">star</mat-icon>
            </mat-card-title>
            <mat-card-subtitle>{{ getLocationTypeName(spot.locationType) }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <div class="spots__card-info">
              <p *ngIf="spot.waterBodyName">
                <mat-icon>water</mat-icon>
                <span>{{ spot.waterBodyName }}</span>
              </p>
              <p *ngIf="spot.latitude && spot.longitude">
                <mat-icon>location_on</mat-icon>
                <span>{{ spot.latitude }}, {{ spot.longitude }}</span>
              </p>
              <p *ngIf="spot.description">
                <mat-icon>description</mat-icon>
                <span>{{ spot.description }}</span>
              </p>
            </div>
            <mat-chip-set>
              <mat-chip>{{ spot.tripCount }} trips</mat-chip>
            </mat-chip-set>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button (click)="onEditSpot(spot)">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="onDeleteSpot(spot)">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>
      </div>

      <div *ngIf="(spots$ | async)?.length === 0" class="spots__empty">
        <mat-card>
          <mat-card-content>
            <div class="spots__empty-content">
              <mat-icon>place</mat-icon>
              <p>No fishing spots yet. Add your first spot!</p>
              <button mat-raised-button color="primary" (click)="onCreateSpot()">
                Add Spot
              </button>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .spots {
      padding: 24px;
    }

    .spots__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .spots__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .spots__grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
      gap: 24px;
    }

    .spots__card-title {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    .spots__favorite {
      color: #ffc107;
      font-size: 20px;
      width: 20px;
      height: 20px;
    }

    .spots__card-info {
      margin-bottom: 16px;
    }

    .spots__card-info p {
      display: flex;
      align-items: center;
      gap: 8px;
      margin: 8px 0;
      color: rgba(0, 0, 0, 0.7);
    }

    .spots__card-info mat-icon {
      font-size: 18px;
      width: 18px;
      height: 18px;
      color: rgba(0, 0, 0, 0.54);
    }

    .spots__empty {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 400px;
    }

    .spots__empty-content {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 48px;
      text-align: center;
    }

    .spots__empty-content mat-icon {
      font-size: 72px;
      width: 72px;
      height: 72px;
      color: rgba(0, 0, 0, 0.26);
      margin-bottom: 16px;
    }

    .spots__empty-content p {
      margin: 0 0 24px 0;
      color: rgba(0, 0, 0, 0.6);
    }
  `]
})
export class Spots implements OnInit {
  spots$ = this.spotsService.spots$;

  constructor(private spotsService: SpotsService) {}

  ngOnInit(): void {
    this.spotsService.getSpots().subscribe();
  }

  getLocationTypeName(type: LocationType): string {
    return LocationType[type];
  }

  onCreateSpot(): void {
    // TODO: Open dialog to create spot
    console.log('Create spot');
  }

  onEditSpot(spot: Spot): void {
    // TODO: Open dialog to edit spot
    console.log('Edit spot', spot);
  }

  onDeleteSpot(spot: Spot): void {
    if (confirm(`Are you sure you want to delete "${spot.name}"?`)) {
      this.spotsService.deleteSpot(spot.spotId).subscribe();
    }
  }
}
