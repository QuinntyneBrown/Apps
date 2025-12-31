import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { IncomeService } from '../services';
import { Income } from '../models';

@Component({
  selector: 'app-incomes',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="incomes">
      <div class="incomes__header">
        <h1>Incomes</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Income
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(incomes$ | async) || []" class="incomes__table">
          <ng-container matColumnDef="date">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let income">{{ income.incomeDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let income">{{ income.description }}</td>
          </ng-container>

          <ng-container matColumnDef="source">
            <th mat-header-cell *matHeaderCellDef>Source</th>
            <td mat-cell *matCellDef="let income">{{ income.source }}</td>
          </ng-container>

          <ng-container matColumnDef="amount">
            <th mat-header-cell *matHeaderCellDef>Amount</th>
            <td mat-cell *matCellDef="let income">{{ income.amount | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="recurring">
            <th mat-header-cell *matHeaderCellDef>Recurring</th>
            <td mat-cell *matCellDef="let income">
              <mat-icon *ngIf="income.isRecurring" color="primary">repeat</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let income">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteIncome(income)">
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
    .incomes {
      padding: 1.5rem;
    }
    .incomes__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .incomes__table {
      width: 100%;
    }
  `]
})
export class Incomes implements OnInit {
  private _incomeService = inject(IncomeService);

  incomes$ = this._incomeService.incomes$;
  displayedColumns = ['date', 'description', 'source', 'amount', 'recurring', 'actions'];

  ngOnInit(): void {
    this._incomeService.getAll().subscribe();
  }

  deleteIncome(income: Income): void {
    if (confirm(`Are you sure you want to delete "${income.description}"?`)) {
      this._incomeService.delete(income.incomeId).subscribe();
    }
  }
}
