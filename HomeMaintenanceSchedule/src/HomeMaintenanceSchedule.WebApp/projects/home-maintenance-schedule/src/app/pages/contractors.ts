import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ContractorService } from '../services';
import { Contractor } from '../models';

@Component({
  selector: 'app-contractors',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="contractors">
      <div class="contractors__header">
        <h1>Contractors</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Contractor
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(contractors$ | async) || []" class="contractors__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let contractor">{{ contractor.name }}</td>
          </ng-container>

          <ng-container matColumnDef="specialty">
            <th mat-header-cell *matHeaderCellDef>Specialty</th>
            <td mat-cell *matCellDef="let contractor">{{ contractor.specialty || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="phone">
            <th mat-header-cell *matHeaderCellDef>Phone</th>
            <td mat-cell *matCellDef="let contractor">{{ contractor.phoneNumber || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="email">
            <th mat-header-cell *matHeaderCellDef>Email</th>
            <td mat-cell *matCellDef="let contractor">{{ contractor.email || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="rating">
            <th mat-header-cell *matHeaderCellDef>Rating</th>
            <td mat-cell *matCellDef="let contractor">{{ contractor.rating ? contractor.rating + '/5' : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let contractor">
              <mat-chip [color]="contractor.isActive ? 'primary' : 'warn'">
                {{ contractor.isActive ? 'Active' : 'Inactive' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let contractor">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteContractor(contractor)">
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
    .contractors {
      padding: 1.5rem;
    }
    .contractors__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .contractors__table {
      width: 100%;
    }
  `]
})
export class Contractors implements OnInit {
  private _contractorService = inject(ContractorService);

  contractors$ = this._contractorService.contractors$;
  displayedColumns = ['name', 'specialty', 'phone', 'email', 'rating', 'status', 'actions'];

  ngOnInit(): void {
    this._contractorService.getAll().subscribe();
  }

  deleteContractor(contractor: Contractor): void {
    if (confirm(`Are you sure you want to delete "${contractor.name}"?`)) {
      this._contractorService.delete(contractor.contractorId).subscribe();
    }
  }
}
