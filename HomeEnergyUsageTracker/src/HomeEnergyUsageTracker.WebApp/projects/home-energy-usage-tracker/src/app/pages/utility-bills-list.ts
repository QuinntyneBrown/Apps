import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { UtilityBillService } from '../services';
import { UtilityBill, UtilityType } from '../models';

@Component({
  selector: 'app-utility-bills-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="utility-bills-list">
      <mat-card class="utility-bills-list__card">
        <mat-card-header class="utility-bills-list__header">
          <mat-card-title class="utility-bills-list__title">Utility Bills</mat-card-title>
          <button mat-raised-button color="primary" (click)="navigateToCreate()" class="utility-bills-list__add-btn">
            <mat-icon>add</mat-icon>
            Add Bill
          </button>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="utilityBills$ | async" class="utility-bills-list__table">
            <ng-container matColumnDef="utilityType">
              <th mat-header-cell *matHeaderCellDef>Type</th>
              <td mat-cell *matCellDef="let bill">{{ getUtilityTypeName(bill.utilityType) }}</td>
            </ng-container>

            <ng-container matColumnDef="billingDate">
              <th mat-header-cell *matHeaderCellDef>Billing Date</th>
              <td mat-cell *matCellDef="let bill">{{ bill.billingDate | date: 'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="amount">
              <th mat-header-cell *matHeaderCellDef>Amount</th>
              <td mat-cell *matCellDef="let bill">\${{ bill.amount | number: '1.2-2' }}</td>
            </ng-container>

            <ng-container matColumnDef="usageAmount">
              <th mat-header-cell *matHeaderCellDef>Usage</th>
              <td mat-cell *matCellDef="let bill">
                {{ bill.usageAmount ? (bill.usageAmount | number: '1.2-2') + ' ' + (bill.unit || '') : 'N/A' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let bill">
                <button mat-icon-button (click)="navigateToEdit(bill.utilityBillId)" color="primary">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button (click)="delete(bill.utilityBillId)" color="warn">
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
    .utility-bills-list {
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
export class UtilityBillsList implements OnInit {
  displayedColumns: string[] = ['utilityType', 'billingDate', 'amount', 'usageAmount', 'actions'];
  utilityBills$ = this.utilityBillService.utilityBills$;

  constructor(
    private utilityBillService: UtilityBillService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.utilityBillService.getAll().subscribe();
  }

  getUtilityTypeName(type: UtilityType): string {
    return UtilityType[type];
  }

  navigateToCreate(): void {
    this.router.navigate(['/utility-bills/create']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/utility-bills/edit', id]);
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this utility bill?')) {
      this.utilityBillService.delete(id).subscribe();
    }
  }
}
