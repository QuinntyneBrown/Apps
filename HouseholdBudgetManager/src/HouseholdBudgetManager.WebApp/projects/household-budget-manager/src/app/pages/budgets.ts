import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { BudgetService } from '../services';
import { Budget, BUDGET_STATUS_LABELS } from '../models';

@Component({
  selector: 'app-budgets',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="budgets">
      <div class="budgets__header">
        <h1>Budgets</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Budget
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(budgets$ | async) || []" class="budgets__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let budget">{{ budget.name }}</td>
          </ng-container>

          <ng-container matColumnDef="period">
            <th mat-header-cell *matHeaderCellDef>Period</th>
            <td mat-cell *matCellDef="let budget">{{ budget.period }}</td>
          </ng-container>

          <ng-container matColumnDef="dateRange">
            <th mat-header-cell *matHeaderCellDef>Date Range</th>
            <td mat-cell *matCellDef="let budget">
              {{ budget.startDate | date:'mediumDate' }} - {{ budget.endDate | date:'mediumDate' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="income">
            <th mat-header-cell *matHeaderCellDef>Total Income</th>
            <td mat-cell *matCellDef="let budget">{{ budget.totalIncome | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="expenses">
            <th mat-header-cell *matHeaderCellDef>Total Expenses</th>
            <td mat-cell *matCellDef="let budget">{{ budget.totalExpenses | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let budget">
              <mat-chip [class]="'status--' + budget.status">
                {{ getStatusLabel(budget.status) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let budget">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteBudget(budget)">
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
    .budgets {
      padding: 1.5rem;
    }
    .budgets__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .budgets__table {
      width: 100%;
    }
    .status--0 { background-color: #f5f5f5; }
    .status--1 { background-color: #e8f5e9; }
    .status--2 { background-color: #e3f2fd; }
  `]
})
export class Budgets implements OnInit {
  private _budgetService = inject(BudgetService);

  budgets$ = this._budgetService.budgets$;
  displayedColumns = ['name', 'period', 'dateRange', 'income', 'expenses', 'status', 'actions'];

  ngOnInit(): void {
    this._budgetService.getAll().subscribe();
  }

  getStatusLabel(status: number): string {
    return BUDGET_STATUS_LABELS[status as keyof typeof BUDGET_STATUS_LABELS] || 'Unknown';
  }

  deleteBudget(budget: Budget): void {
    if (confirm(`Are you sure you want to delete "${budget.name}"?`)) {
      this._budgetService.delete(budget.budgetId).subscribe();
    }
  }
}
