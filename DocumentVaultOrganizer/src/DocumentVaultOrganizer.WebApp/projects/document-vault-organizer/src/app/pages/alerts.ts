import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule } from '@angular/forms';
import { ExpirationAlertService } from '../services';
import { ExpirationAlert } from '../models';

@Component({
  selector: 'app-alerts',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatSlideToggleModule,
    MatSnackBarModule
  ],
  template: `
    <div class="alerts">
      <mat-card class="alerts__card">
        <mat-card-header class="alerts__header">
          <mat-card-title>Expiration Alerts</mat-card-title>
          <mat-slide-toggle
            [(ngModel)]="showOnlyUnacknowledged"
            (change)="onFilterChange()"
            class="alerts__toggle">
            Show only unacknowledged
          </mat-slide-toggle>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="alerts$ | async" class="alerts__table">
            <ng-container matColumnDef="documentId">
              <th mat-header-cell *matHeaderCellDef>Document ID</th>
              <td mat-cell *matCellDef="let alert">{{ alert.documentId }}</td>
            </ng-container>

            <ng-container matColumnDef="alertDate">
              <th mat-header-cell *matHeaderCellDef>Alert Date</th>
              <td mat-cell *matCellDef="let alert">{{ alert.alertDate | date }}</td>
            </ng-container>

            <ng-container matColumnDef="status">
              <th mat-header-cell *matHeaderCellDef>Status</th>
              <td mat-cell *matCellDef="let alert">
                <mat-chip-set>
                  <mat-chip [color]="alert.isAcknowledged ? 'accent' : 'warn'" highlighted>
                    {{ alert.isAcknowledged ? 'Acknowledged' : 'Pending' }}
                  </mat-chip>
                </mat-chip-set>
              </td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created At</th>
              <td mat-cell *matCellDef="let alert">{{ alert.createdAt | date }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let alert">
                <button
                  mat-raised-button
                  color="primary"
                  (click)="acknowledgeAlert(alert.expirationAlertId)"
                  [disabled]="alert.isAcknowledged"
                  class="alerts__action-btn">
                  <mat-icon>check</mat-icon>
                  Acknowledge
                </button>
                <button
                  mat-icon-button
                  color="warn"
                  (click)="deleteAlert(alert.expirationAlertId)"
                  class="alerts__action-btn">
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
    .alerts {
      padding: 2rem;

      &__card {
        margin-bottom: 2rem;
      }

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 1rem;
      }

      &__toggle {
        margin-left: auto;
      }

      &__table {
        width: 100%;
      }

      &__action-btn {
        margin-right: 0.5rem;
      }
    }
  `]
})
export class Alerts implements OnInit {
  alerts$ = this.alertService.alerts$;
  displayedColumns: string[] = ['documentId', 'alertDate', 'status', 'createdAt', 'actions'];
  showOnlyUnacknowledged = false;

  constructor(
    private alertService: ExpirationAlertService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.loadAlerts();
  }

  loadAlerts() {
    this.alertService.getExpirationAlerts(this.showOnlyUnacknowledged ? true : undefined).subscribe();
  }

  onFilterChange() {
    this.loadAlerts();
  }

  acknowledgeAlert(id: string) {
    this.alertService.acknowledgeExpirationAlert(id).subscribe({
      next: () => {
        this.snackBar.open('Alert acknowledged successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error acknowledging alert', 'Close', { duration: 3000 });
      }
    });
  }

  deleteAlert(id: string) {
    if (confirm('Are you sure you want to delete this alert?')) {
      this.alertService.deleteExpirationAlert(id).subscribe({
        next: () => {
          this.snackBar.open('Alert deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting alert', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
