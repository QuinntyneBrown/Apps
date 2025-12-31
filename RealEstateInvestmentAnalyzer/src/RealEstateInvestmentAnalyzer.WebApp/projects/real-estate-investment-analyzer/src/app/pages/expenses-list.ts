import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { ExpenseService } from '../services';
import { Expense } from '../models';

@Component({
  selector: 'app-expenses-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  template: `
    <div class="expenses-list">
      <div class="expenses-list__header">
        <h1 class="expenses-list__title">Expenses</h1>
        <a mat-raised-button color="primary" routerLink="/expenses/new">
          <mat-icon>add</mat-icon>
          Add Expense
        </a>
      </div>

      <mat-card class="expenses-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="expenses" class="expenses-list__table">
            <ng-container matColumnDef="description">
              <th mat-header-cell *matHeaderCellDef>Description</th>
              <td mat-cell *matCellDef="let expense">{{ expense.description }}</td>
            </ng-container>

            <ng-container matColumnDef="category">
              <th mat-header-cell *matHeaderCellDef>Category</th>
              <td mat-cell *matCellDef="let expense">{{ expense.category }}</td>
            </ng-container>

            <ng-container matColumnDef="amount">
              <th mat-header-cell *matHeaderCellDef>Amount</th>
              <td mat-cell *matCellDef="let expense">{{ expense.amount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="date">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let expense">{{ expense.date | date }}</td>
            </ng-container>

            <ng-container matColumnDef="isRecurring">
              <th mat-header-cell *matHeaderCellDef>Recurring</th>
              <td mat-cell *matCellDef="let expense">
                <mat-chip-set>
                  <mat-chip [highlighted]="expense.isRecurring" [class.expenses-list__chip--recurring]="expense.isRecurring">
                    {{ expense.isRecurring ? 'Yes' : 'No' }}
                  </mat-chip>
                </mat-chip-set>
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let expense">
                <div class="expenses-list__actions">
                  <a mat-icon-button color="primary" [routerLink]="['/expenses', expense.expenseId]">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button mat-icon-button color="warn" (click)="deleteExpense(expense.expenseId)">
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
    .expenses-list {
      padding: 2rem;
    }

    .expenses-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .expenses-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .expenses-list__card {
      overflow: auto;
    }

    .expenses-list__table {
      width: 100%;
    }

    .expenses-list__actions {
      display: flex;
      gap: 0.5rem;
    }

    .expenses-list__chip--recurring {
      background-color: #ff9800 !important;
      color: white;
    }
  `]
})
export class ExpensesList implements OnInit {
  private readonly expenseService = inject(ExpenseService);

  expenses: Expense[] = [];
  displayedColumns = ['description', 'category', 'amount', 'date', 'isRecurring', 'actions'];

  ngOnInit(): void {
    this.expenseService.expenses$.subscribe(expenses => {
      this.expenses = expenses;
    });
    this.expenseService.getExpenses().subscribe();
  }

  deleteExpense(id: string): void {
    if (confirm('Are you sure you want to delete this expense?')) {
      this.expenseService.deleteExpense(id).subscribe();
    }
  }
}
