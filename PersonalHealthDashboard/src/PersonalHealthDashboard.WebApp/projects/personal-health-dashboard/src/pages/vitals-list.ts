import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { VitalService } from '../services';
import { Vital, VitalTypeLabels } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-vitals-list',
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
    <div class="vitals-list">
      <mat-card class="vitals-list__card">
        <mat-card-header class="vitals-list__header">
          <mat-card-title class="vitals-list__title">
            <mat-icon class="vitals-list__title-icon">favorite</mat-icon>
            <span>Vitals</span>
          </mat-card-title>
          <button mat-raised-button color="primary" (click)="addVital()" class="vitals-list__add-button">
            <mat-icon>add</mat-icon>
            Add Vital
          </button>
        </mat-card-header>

        <mat-card-content>
          <div *ngIf="vitals$ | async as vitals; else loading" class="vitals-list__content">
            <div *ngIf="vitals.length === 0" class="vitals-list__empty">
              <mat-icon class="vitals-list__empty-icon">inbox</mat-icon>
              <p class="vitals-list__empty-text">No vitals recorded yet</p>
              <button mat-raised-button color="primary" (click)="addVital()">Add Your First Vital</button>
            </div>

            <div *ngIf="vitals.length > 0" class="vitals-list__table-container">
              <table mat-table [dataSource]="vitals" class="vitals-list__table">
                <ng-container matColumnDef="vitalType">
                  <th mat-header-cell *matHeaderCellDef>Type</th>
                  <td mat-cell *matCellDef="let vital">
                    <mat-chip class="vitals-list__chip">{{ getVitalTypeLabel(vital.vitalType) }}</mat-chip>
                  </td>
                </ng-container>

                <ng-container matColumnDef="value">
                  <th mat-header-cell *matHeaderCellDef>Value</th>
                  <td mat-cell *matCellDef="let vital">{{ vital.value }} {{ vital.unit }}</td>
                </ng-container>

                <ng-container matColumnDef="measuredAt">
                  <th mat-header-cell *matHeaderCellDef>Measured At</th>
                  <td mat-cell *matCellDef="let vital">{{ vital.measuredAt | date:'short' }}</td>
                </ng-container>

                <ng-container matColumnDef="source">
                  <th mat-header-cell *matHeaderCellDef>Source</th>
                  <td mat-cell *matCellDef="let vital">{{ vital.source || 'Manual' }}</td>
                </ng-container>

                <ng-container matColumnDef="notes">
                  <th mat-header-cell *matHeaderCellDef>Notes</th>
                  <td mat-cell *matCellDef="let vital" class="vitals-list__notes">{{ vital.notes || '-' }}</td>
                </ng-container>

                <ng-container matColumnDef="actions">
                  <th mat-header-cell *matHeaderCellDef>Actions</th>
                  <td mat-cell *matCellDef="let vital">
                    <button mat-icon-button color="primary" (click)="editVital(vital.vitalId)" class="vitals-list__action-button">
                      <mat-icon>edit</mat-icon>
                    </button>
                    <button mat-icon-button color="warn" (click)="deleteVital(vital.vitalId)" class="vitals-list__action-button">
                      <mat-icon>delete</mat-icon>
                    </button>
                  </td>
                </ng-container>

                <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
                <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="vitals-list__row"></tr>
              </table>
            </div>
          </div>

          <ng-template #loading>
            <div class="vitals-list__loading">
              <mat-spinner></mat-spinner>
            </div>
          </ng-template>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .vitals-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .vitals-list__card {
      width: 100%;
    }

    .vitals-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }

    .vitals-list__title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.5rem;
      margin: 0;
    }

    .vitals-list__title-icon {
      font-size: 1.75rem;
      width: 1.75rem;
      height: 1.75rem;
    }

    .vitals-list__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .vitals-list__content {
      min-height: 200px;
    }

    .vitals-list__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 3rem 1rem;
      text-align: center;
    }

    .vitals-list__empty-icon {
      font-size: 4rem;
      width: 4rem;
      height: 4rem;
      color: rgba(0, 0, 0, 0.3);
      margin-bottom: 1rem;
    }

    .vitals-list__empty-text {
      font-size: 1.125rem;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 1.5rem;
    }

    .vitals-list__table-container {
      overflow-x: auto;
    }

    .vitals-list__table {
      width: 100%;
    }

    .vitals-list__row {
      cursor: pointer;
    }

    .vitals-list__row:hover {
      background-color: rgba(0, 0, 0, 0.04);
    }

    .vitals-list__chip {
      font-size: 0.875rem;
    }

    .vitals-list__notes {
      max-width: 200px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }

    .vitals-list__action-button {
      margin-left: 0.25rem;
    }

    .vitals-list__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 300px;
    }

    @media (max-width: 768px) {
      .vitals-list {
        padding: 1rem;
      }

      .vitals-list__header {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
      }

      .vitals-list__add-button {
        width: 100%;
      }
    }
  `]
})
export class VitalsList implements OnInit {
  private vitalService = inject(VitalService);
  private router = inject(Router);

  vitals$!: Observable<Vital[]>;
  displayedColumns: string[] = ['vitalType', 'value', 'measuredAt', 'source', 'notes', 'actions'];

  ngOnInit(): void {
    this.vitalService.getAll().subscribe();
    this.vitals$ = this.vitalService.vitals$;
  }

  getVitalTypeLabel(vitalType: number): string {
    return VitalTypeLabels[vitalType] || 'Unknown';
  }

  addVital(): void {
    this.router.navigate(['/vitals/new']);
  }

  editVital(id: string): void {
    this.router.navigate(['/vitals/edit', id]);
  }

  deleteVital(id: string): void {
    if (confirm('Are you sure you want to delete this vital?')) {
      this.vitalService.delete(id).subscribe();
    }
  }
}
