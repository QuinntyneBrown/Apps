import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ServiceLogService } from '../services';
import { ServiceLog } from '../models';

@Component({
  selector: 'app-service-logs',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="service-logs">
      <div class="service-logs__header">
        <h1>Service Logs</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Service Log
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(logs$ | async) || []" class="service-logs__table">
          <ng-container matColumnDef="serviceDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let log">{{ log.serviceDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let log">{{ log.description }}</td>
          </ng-container>

          <ng-container matColumnDef="cost">
            <th mat-header-cell *matHeaderCellDef>Cost</th>
            <td mat-cell *matCellDef="let log">{{ log.cost ? (log.cost | currency) : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="laborHours">
            <th mat-header-cell *matHeaderCellDef>Labor Hours</th>
            <td mat-cell *matCellDef="let log">{{ log.laborHours || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="warranty">
            <th mat-header-cell *matHeaderCellDef>Warranty Expires</th>
            <td mat-cell *matCellDef="let log">{{ log.warrantyExpiresAt ? (log.warrantyExpiresAt | date:'mediumDate') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let log">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteLog(log)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .service-logs {
      padding: 1.5rem;
    }
    .service-logs__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .service-logs__table {
      width: 100%;
    }
  `]
})
export class ServiceLogs implements OnInit {
  private _logService = inject(ServiceLogService);

  logs$ = this._logService.logs$;
  displayedColumns = ['serviceDate', 'description', 'cost', 'laborHours', 'warranty', 'actions'];

  ngOnInit(): void {
    this._logService.getAll().subscribe();
  }

  deleteLog(log: ServiceLog): void {
    if (confirm('Are you sure you want to delete this service log?')) {
      this._logService.delete(log.serviceLogId).subscribe();
    }
  }
}
