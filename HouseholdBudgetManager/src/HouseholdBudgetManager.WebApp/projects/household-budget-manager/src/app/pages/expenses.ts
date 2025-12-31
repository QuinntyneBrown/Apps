import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ExpenseService } from '../services';
import { Expense, EXPENSE_CATEGORY_LABELS } from '../models';

@Component({
  selector: 'app-expenses',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="expenses">
      <div class="expenses__header">
        <h1>Expenses</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Expense
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(expenses$ | async) || []" class="expenses__table">
          <ng-container matColumnDef="date">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let expense">{{ expense.expenseDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let expense">{{ expense.description }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let expense">
              <mat-chip>{{ getCategoryLabel(expense.category) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="amount">
            <th mat-header-cell *matHeaderCellDef>Amount</th>
            <td mat-cell *matCellDef="let expense">{{ expense.amount | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="payee">
            <th mat-header-cell *matHeaderCellDef>Payee</th>
            <td mat-cell *matCellDef="let expense">{{ expense.payee || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="recurring">
            <th mat-header-cell *matHeaderCellDef>Recurring</th>
            <td mat-cell *matCellDef="let expense">
              <mat-icon *ngIf="expense.isRecurring" color="primary">repeat</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let expense">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteExpense(expense)">
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
    .expenses {
      padding: 1.5rem;
    }
    .expenses__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .expenses__table {
      width: 100%;
    }
  `]
})
export class Expenses implements OnInit {
  private _expenseService = inject(ExpenseService);

  expenses$ = this._expenseService.expenses$;
  displayedColumns = ['date', 'description', 'category', 'amount', 'payee', 'recurring', 'actions'];

  ngOnInit(): void {
    this._expenseService.getAll().subscribe();
  }

  getCategoryLabel(category: number): string {
    return EXPENSE_CATEGORY_LABELS[category as keyof typeof EXPENSE_CATEGORY_LABELS] || 'Unknown';
  }

  deleteExpense(expense: Expense): void {
    if (confirm(`Are you sure you want to delete "${expense.description}"?`)) {
      this._expenseService.delete(expense.expenseId).subscribe();
    }
  }
}
