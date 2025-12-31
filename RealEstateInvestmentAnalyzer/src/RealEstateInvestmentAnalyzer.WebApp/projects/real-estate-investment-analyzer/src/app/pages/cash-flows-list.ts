import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { CashFlowService } from '../services';
import { CashFlow } from '../models';

@Component({
  selector: 'app-cash-flows-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="cash-flows-list">
      <div class="cash-flows-list__header">
        <h1 class="cash-flows-list__title">Cash Flows</h1>
        <a mat-raised-button color="primary" routerLink="/cash-flows/new">
          <mat-icon>add</mat-icon>
          Add Cash Flow
        </a>
      </div>

      <mat-card class="cash-flows-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="cashFlows" class="cash-flows-list__table">
            <ng-container matColumnDef="date">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let cashFlow">{{ cashFlow.date | date }}</td>
            </ng-container>

            <ng-container matColumnDef="income">
              <th mat-header-cell *matHeaderCellDef>Income</th>
              <td mat-cell *matCellDef="let cashFlow" class="cash-flows-list__income">
                {{ cashFlow.income | currency }}
              </td>
            </ng-container>

            <ng-container matColumnDef="expenses">
              <th mat-header-cell *matHeaderCellDef>Expenses</th>
              <td mat-cell *matCellDef="let cashFlow" class="cash-flows-list__expense">
                {{ cashFlow.expenses | currency }}
              </td>
            </ng-container>

            <ng-container matColumnDef="netCashFlow">
              <th mat-header-cell *matHeaderCellDef>Net Cash Flow</th>
              <td mat-cell *matCellDef="let cashFlow"
                  [class.cash-flows-list__net--positive]="cashFlow.netCashFlow >= 0"
                  [class.cash-flows-list__net--negative]="cashFlow.netCashFlow < 0">
                {{ cashFlow.netCashFlow | currency }}
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let cashFlow">
                <div class="cash-flows-list__actions">
                  <a mat-icon-button color="primary" [routerLink]="['/cash-flows', cashFlow.cashFlowId]">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button mat-icon-button color="warn" (click)="deleteCashFlow(cashFlow.cashFlowId)">
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
    .cash-flows-list {
      padding: 2rem;
    }

    .cash-flows-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .cash-flows-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .cash-flows-list__card {
      overflow: auto;
    }

    .cash-flows-list__table {
      width: 100%;
    }

    .cash-flows-list__actions {
      display: flex;
      gap: 0.5rem;
    }

    .cash-flows-list__income {
      color: #4caf50;
      font-weight: 500;
    }

    .cash-flows-list__expense {
      color: #f44336;
      font-weight: 500;
    }

    .cash-flows-list__net--positive {
      color: #4caf50;
      font-weight: 600;
    }

    .cash-flows-list__net--negative {
      color: #f44336;
      font-weight: 600;
    }
  `]
})
export class CashFlowsList implements OnInit {
  private readonly cashFlowService = inject(CashFlowService);

  cashFlows: CashFlow[] = [];
  displayedColumns = ['date', 'income', 'expenses', 'netCashFlow', 'actions'];

  ngOnInit(): void {
    this.cashFlowService.cashFlows$.subscribe(cashFlows => {
      this.cashFlows = cashFlows;
    });
    this.cashFlowService.getCashFlows().subscribe();
  }

  deleteCashFlow(id: string): void {
    if (confirm('Are you sure you want to delete this cash flow record?')) {
      this.cashFlowService.deleteCashFlow(id).subscribe();
    }
  }
}
