import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { UsageService } from '../services';

@Component({
  selector: 'app-usages-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="usages-list">
      <mat-card class="usages-list__card">
        <mat-card-header class="usages-list__header">
          <mat-card-title class="usages-list__title">Usage Readings</mat-card-title>
          <button mat-raised-button color="primary" (click)="navigateToCreate()" class="usages-list__add-btn">
            <mat-icon>add</mat-icon>
            Add Reading
          </button>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="usages$ | async" class="usages-list__table">
            <ng-container matColumnDef="date">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let usage">{{ usage.date | date: 'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="amount">
              <th mat-header-cell *matHeaderCellDef>Amount</th>
              <td mat-cell *matCellDef="let usage">{{ usage.amount | number: '1.2-2' }}</td>
            </ng-container>

            <ng-container matColumnDef="utilityBillId">
              <th mat-header-cell *matHeaderCellDef>Utility Bill ID</th>
              <td mat-cell *matCellDef="let usage">{{ usage.utilityBillId.substring(0, 8) }}...</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let usage">
                <button mat-icon-button (click)="navigateToEdit(usage.usageId)" color="primary">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button (click)="delete(usage.usageId)" color="warn">
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
    .usages-list {
      padding: 24px;

      &__card {
        max-width: 1200px;
        margin: 0 auto;
      }

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 16px;
      }

      &__title {
        font-size: 24px;
      }

      &__add-btn {
        margin-left: auto;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class UsagesList implements OnInit {
  displayedColumns: string[] = ['date', 'amount', 'utilityBillId', 'actions'];
  usages$ = this.usageService.usages$;

  constructor(
    private usageService: UsageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.usageService.getAll().subscribe();
  }

  navigateToCreate(): void {
    this.router.navigate(['/usages/create']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/usages/edit', id]);
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this usage reading?')) {
      this.usageService.delete(id).subscribe();
    }
  }
}
