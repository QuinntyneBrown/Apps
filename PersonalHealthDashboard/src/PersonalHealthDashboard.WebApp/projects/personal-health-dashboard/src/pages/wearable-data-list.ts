import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { WearableDataService } from '../services';
import { WearableData } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-wearable-data-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatChipsModule
  ],
  template: `
    <div class="wearable-data-list">
      <mat-card class="wearable-data-list__card">
        <mat-card-header class="wearable-data-list__header">
          <mat-card-title class="wearable-data-list__title">
            <mat-icon class="wearable-data-list__title-icon">watch</mat-icon>
            <span>Wearable Data</span>
          </mat-card-title>
          <button mat-raised-button color="primary" (click)="addWearableData()" class="wearable-data-list__add-button">
            <mat-icon>add</mat-icon>
            Add Wearable Data
          </button>
        </mat-card-header>

        <mat-card-content>
          <div *ngIf="wearableData$ | async as wearableData; else loading" class="wearable-data-list__content">
            <div *ngIf="wearableData.length === 0" class="wearable-data-list__empty">
              <mat-icon class="wearable-data-list__empty-icon">inbox</mat-icon>
              <p class="wearable-data-list__empty-text">No wearable data synced yet</p>
              <button mat-raised-button color="primary" (click)="addWearableData()">Add Your First Wearable Data</button>
            </div>

            <div *ngIf="wearableData.length > 0" class="wearable-data-list__table-container">
              <table mat-table [dataSource]="wearableData" class="wearable-data-list__table">
                <ng-container matColumnDef="deviceName">
                  <th mat-header-cell *matHeaderCellDef>Device</th>
                  <td mat-cell *matCellDef="let data">
                    <mat-chip class="wearable-data-list__chip">{{ data.deviceName }}</mat-chip>
                  </td>
                </ng-container>

                <ng-container matColumnDef="dataType">
                  <th mat-header-cell *matHeaderCellDef>Data Type</th>
                  <td mat-cell *matCellDef="let data">{{ data.dataType }}</td>
                </ng-container>

                <ng-container matColumnDef="value">
                  <th mat-header-cell *matHeaderCellDef>Value</th>
                  <td mat-cell *matCellDef="let data">{{ data.value }} {{ data.unit }}</td>
                </ng-container>

                <ng-container matColumnDef="recordedAt">
                  <th mat-header-cell *matHeaderCellDef>Recorded At</th>
                  <td mat-cell *matCellDef="let data">{{ data.recordedAt | date:'short' }}</td>
                </ng-container>

                <ng-container matColumnDef="syncedAt">
                  <th mat-header-cell *matHeaderCellDef>Synced At</th>
                  <td mat-cell *matCellDef="let data">{{ data.syncedAt | date:'short' }}</td>
                </ng-container>

                <ng-container matColumnDef="dataAge">
                  <th mat-header-cell *matHeaderCellDef>Age</th>
                  <td mat-cell *matCellDef="let data">
                    <span class="wearable-data-list__age">{{ data.dataAgeInHours | number:'1.1-1' }}h ago</span>
                  </td>
                </ng-container>

                <ng-container matColumnDef="actions">
                  <th mat-header-cell *matHeaderCellDef>Actions</th>
                  <td mat-cell *matCellDef="let data">
                    <button mat-icon-button color="primary" (click)="editWearableData(data.wearableDataId)" class="wearable-data-list__action-button">
                      <mat-icon>edit</mat-icon>
                    </button>
                    <button mat-icon-button color="warn" (click)="deleteWearableData(data.wearableDataId)" class="wearable-data-list__action-button">
                      <mat-icon>delete</mat-icon>
                    </button>
                  </td>
                </ng-container>

                <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
                <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="wearable-data-list__row"></tr>
              </table>
            </div>
          </div>

          <ng-template #loading>
            <div class="wearable-data-list__loading">
              <mat-spinner></mat-spinner>
            </div>
          </ng-template>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .wearable-data-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .wearable-data-list__card {
      width: 100%;
    }

    .wearable-data-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }

    .wearable-data-list__title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.5rem;
      margin: 0;
    }

    .wearable-data-list__title-icon {
      font-size: 1.75rem;
      width: 1.75rem;
      height: 1.75rem;
    }

    .wearable-data-list__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .wearable-data-list__content {
      min-height: 200px;
    }

    .wearable-data-list__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 3rem 1rem;
      text-align: center;
    }

    .wearable-data-list__empty-icon {
      font-size: 4rem;
      width: 4rem;
      height: 4rem;
      color: rgba(0, 0, 0, 0.3);
      margin-bottom: 1rem;
    }

    .wearable-data-list__empty-text {
      font-size: 1.125rem;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 1.5rem;
    }

    .wearable-data-list__table-container {
      overflow-x: auto;
    }

    .wearable-data-list__table {
      width: 100%;
    }

    .wearable-data-list__row {
      cursor: pointer;
    }

    .wearable-data-list__row:hover {
      background-color: rgba(0, 0, 0, 0.04);
    }

    .wearable-data-list__chip {
      font-size: 0.875rem;
      background-color: #ff9800 !important;
      color: white !important;
    }

    .wearable-data-list__age {
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .wearable-data-list__action-button {
      margin-left: 0.25rem;
    }

    .wearable-data-list__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 300px;
    }

    @media (max-width: 768px) {
      .wearable-data-list {
        padding: 1rem;
      }

      .wearable-data-list__header {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
      }

      .wearable-data-list__add-button {
        width: 100%;
      }
    }
  `]
})
export class WearableDataList implements OnInit {
  private wearableDataService = inject(WearableDataService);
  private router = inject(Router);

  wearableData$!: Observable<WearableData[]>;
  displayedColumns: string[] = ['deviceName', 'dataType', 'value', 'recordedAt', 'syncedAt', 'dataAge', 'actions'];

  ngOnInit(): void {
    this.wearableDataService.getAll().subscribe();
    this.wearableData$ = this.wearableDataService.wearableData$;
  }

  addWearableData(): void {
    this.router.navigate(['/wearable-data/new']);
  }

  editWearableData(id: string): void {
    this.router.navigate(['/wearable-data/edit', id]);
  }

  deleteWearableData(id: string): void {
    if (confirm('Are you sure you want to delete this wearable data?')) {
      this.wearableDataService.delete(id).subscribe();
    }
  }
}
