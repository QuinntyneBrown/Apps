import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { LeaseService } from '../services';
import { Lease } from '../models';

@Component({
  selector: 'app-leases-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  template: `
    <div class="leases-list">
      <div class="leases-list__header">
        <h1 class="leases-list__title">Leases</h1>
        <a mat-raised-button color="primary" routerLink="/leases/new">
          <mat-icon>add</mat-icon>
          Add Lease
        </a>
      </div>

      <mat-card class="leases-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="leases" class="leases-list__table">
            <ng-container matColumnDef="tenantName">
              <th mat-header-cell *matHeaderCellDef>Tenant</th>
              <td mat-cell *matCellDef="let lease">{{ lease.tenantName }}</td>
            </ng-container>

            <ng-container matColumnDef="monthlyRent">
              <th mat-header-cell *matHeaderCellDef>Monthly Rent</th>
              <td mat-cell *matCellDef="let lease">{{ lease.monthlyRent | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="startDate">
              <th mat-header-cell *matHeaderCellDef>Start Date</th>
              <td mat-cell *matCellDef="let lease">{{ lease.startDate | date }}</td>
            </ng-container>

            <ng-container matColumnDef="endDate">
              <th mat-header-cell *matHeaderCellDef>End Date</th>
              <td mat-cell *matCellDef="let lease">{{ lease.endDate | date }}</td>
            </ng-container>

            <ng-container matColumnDef="securityDeposit">
              <th mat-header-cell *matHeaderCellDef>Security Deposit</th>
              <td mat-cell *matCellDef="let lease">{{ lease.securityDeposit | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="isActive">
              <th mat-header-cell *matHeaderCellDef>Status</th>
              <td mat-cell *matCellDef="let lease">
                <mat-chip-set>
                  <mat-chip [highlighted]="lease.isActive" [class.leases-list__chip--active]="lease.isActive" [class.leases-list__chip--inactive]="!lease.isActive">
                    {{ lease.isActive ? 'Active' : 'Inactive' }}
                  </mat-chip>
                </mat-chip-set>
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let lease">
                <div class="leases-list__actions">
                  <a mat-icon-button color="primary" [routerLink]="['/leases', lease.leaseId]">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button mat-icon-button color="accent" *ngIf="lease.isActive" (click)="terminateLease(lease.leaseId)">
                    <mat-icon>cancel</mat-icon>
                  </button>
                  <button mat-icon-button color="warn" (click)="deleteLease(lease.leaseId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </div>
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
    .leases-list {
      padding: 2rem;
    }

    .leases-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .leases-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .leases-list__card {
      overflow: auto;
    }

    .leases-list__table {
      width: 100%;
    }

    .leases-list__actions {
      display: flex;
      gap: 0.5rem;
    }

    .leases-list__chip--active {
      background-color: #4caf50 !important;
      color: white;
    }

    .leases-list__chip--inactive {
      background-color: #9e9e9e !important;
      color: white;
    }
  `]
})
export class LeasesList implements OnInit {
  private readonly leaseService = inject(LeaseService);

  leases: Lease[] = [];
  displayedColumns = ['tenantName', 'monthlyRent', 'startDate', 'endDate', 'securityDeposit', 'isActive', 'actions'];

  ngOnInit(): void {
    this.leaseService.leases$.subscribe(leases => {
      this.leases = leases;
    });
    this.leaseService.getLeases().subscribe();
  }

  terminateLease(id: string): void {
    if (confirm('Are you sure you want to terminate this lease?')) {
      this.leaseService.terminateLease(id).subscribe();
    }
  }

  deleteLease(id: string): void {
    if (confirm('Are you sure you want to delete this lease?')) {
      this.leaseService.deleteLease(id).subscribe();
    }
  }
}
