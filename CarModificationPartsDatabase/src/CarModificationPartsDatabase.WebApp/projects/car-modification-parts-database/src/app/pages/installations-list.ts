import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { InstallationsService } from '../services';

@Component({
  selector: 'app-installations-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  template: `
    <div class="installations-list">
      <div class="installations-list__header">
        <h1>Installations</h1>
        <a mat-raised-button color="primary" routerLink="/installations/new">
          <mat-icon>add</mat-icon>
          Add Installation
        </a>
      </div>

      <table mat-table [dataSource]="installations$ | async" class="installations-list__table">
        <ng-container matColumnDef="vehicleInfo">
          <th mat-header-cell *matHeaderCellDef>Vehicle</th>
          <td mat-cell *matCellDef="let installation">{{ installation.vehicleInfo }}</td>
        </ng-container>

        <ng-container matColumnDef="installationDate">
          <th mat-header-cell *matHeaderCellDef>Date</th>
          <td mat-cell *matCellDef="let installation">{{ installation.installationDate | date }}</td>
        </ng-container>

        <ng-container matColumnDef="installedBy">
          <th mat-header-cell *matHeaderCellDef>Installed By</th>
          <td mat-cell *matCellDef="let installation">{{ installation.installedBy || 'N/A' }}</td>
        </ng-container>

        <ng-container matColumnDef="totalCost">
          <th mat-header-cell *matHeaderCellDef>Total Cost</th>
          <td mat-cell *matCellDef="let installation">{{ installation.totalCost | currency }}</td>
        </ng-container>

        <ng-container matColumnDef="isCompleted">
          <th mat-header-cell *matHeaderCellDef>Status</th>
          <td mat-cell *matCellDef="let installation">
            <mat-chip [class.installations-list__chip--completed]="installation.isCompleted" [class.installations-list__chip--pending]="!installation.isCompleted">
              {{ installation.isCompleted ? 'Completed' : 'Pending' }}
            </mat-chip>
          </td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Actions</th>
          <td mat-cell *matCellDef="let installation">
            <a mat-icon-button [routerLink]="['/installations', installation.installationId]">
              <mat-icon>visibility</mat-icon>
            </a>
            <a mat-icon-button [routerLink]="['/installations', installation.installationId, 'edit']">
              <mat-icon>edit</mat-icon>
            </a>
            <button mat-icon-button (click)="deleteInstallation(installation.installationId)" color="warn">
              <mat-icon>delete</mat-icon>
            </button>
          </td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
      </table>
    </div>
  `,
  styles: [`
    .installations-list {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__table {
        width: 100%;
      }

      &__chip--completed {
        background-color: #4caf50 !important;
        color: white;
      }

      &__chip--pending {
        background-color: #ff9800 !important;
        color: white;
      }
    }
  `]
})
export class InstallationsList implements OnInit {
  installations$ = this.installationsService.installations$;
  displayedColumns = ['vehicleInfo', 'installationDate', 'installedBy', 'totalCost', 'isCompleted', 'actions'];

  constructor(private installationsService: InstallationsService) {}

  ngOnInit() {
    this.installationsService.getAll().subscribe();
  }

  deleteInstallation(id: string) {
    if (confirm('Are you sure you want to delete this installation?')) {
      this.installationsService.delete(id).subscribe();
    }
  }
}
