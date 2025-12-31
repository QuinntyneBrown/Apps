import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { WithdrawalStrategyService } from '../services';
import { WithdrawalStrategy, WithdrawalStrategyTypeLabels } from '../models';

@Component({
  selector: 'app-withdrawal-strategies',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="withdrawal-strategies">
      <div class="withdrawal-strategies__header">
        <h1 class="withdrawal-strategies__title">Withdrawal Strategies</h1>
        <button mat-raised-button color="primary" routerLink="/withdrawal-strategies/new" class="withdrawal-strategies__add-btn">
          <mat-icon>add</mat-icon>
          New Strategy
        </button>
      </div>

      <mat-card class="withdrawal-strategies__card">
        <mat-card-content>
          <table mat-table [dataSource]="(strategies$ | async) || []" class="withdrawal-strategies__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let strategy">{{ strategy.name }}</td>
            </ng-container>

            <ng-container matColumnDef="strategyType">
              <th mat-header-cell *matHeaderCellDef>Strategy Type</th>
              <td mat-cell *matCellDef="let strategy">{{ getStrategyTypeLabel(strategy.strategyType) }}</td>
            </ng-container>

            <ng-container matColumnDef="withdrawalRate">
              <th mat-header-cell *matHeaderCellDef>Withdrawal Rate</th>
              <td mat-cell *matCellDef="let strategy">{{ strategy.withdrawalRate * 100 | number:'1.2-2' }}%</td>
            </ng-container>

            <ng-container matColumnDef="annualWithdrawalAmount">
              <th mat-header-cell *matHeaderCellDef>Annual Amount</th>
              <td mat-cell *matCellDef="let strategy">{{ strategy.annualWithdrawalAmount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="adjustForInflation">
              <th mat-header-cell *matHeaderCellDef>Adjust for Inflation</th>
              <td mat-cell *matCellDef="let strategy">
                <mat-icon [class.withdrawal-strategies__adjust-icon--yes]="strategy.adjustForInflation">
                  {{ strategy.adjustForInflation ? 'check_circle' : 'cancel' }}
                </mat-icon>
              </td>
            </ng-container>

            <ng-container matColumnDef="minimumBalance">
              <th mat-header-cell *matHeaderCellDef>Minimum Balance</th>
              <td mat-cell *matCellDef="let strategy">
                {{ strategy.minimumBalance ? (strategy.minimumBalance | currency) : '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let strategy">
                <button mat-icon-button color="primary" [routerLink]="['/withdrawal-strategies', strategy.withdrawalStrategyId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteStrategy(strategy)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div *ngIf="(strategies$ | async)?.length === 0" class="withdrawal-strategies__empty">
            <p>No withdrawal strategies found. Create your first strategy to plan your retirement withdrawals!</p>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .withdrawal-strategies {
      padding: 2rem;
    }

    .withdrawal-strategies__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .withdrawal-strategies__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .withdrawal-strategies__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .withdrawal-strategies__card {
      overflow-x: auto;
    }

    .withdrawal-strategies__table {
      width: 100%;
    }

    .withdrawal-strategies__adjust-icon--yes {
      color: #4caf50;
    }

    .withdrawal-strategies__empty {
      padding: 3rem;
      text-align: center;
      color: #666;
    }
  `]
})
export class WithdrawalStrategies implements OnInit {
  private strategyService = inject(WithdrawalStrategyService);
  private router = inject(Router);

  strategies$ = this.strategyService.strategies$;
  displayedColumns = ['name', 'strategyType', 'withdrawalRate', 'annualWithdrawalAmount', 'adjustForInflation', 'minimumBalance', 'actions'];

  ngOnInit(): void {
    this.strategyService.loadStrategies().subscribe();
  }

  getStrategyTypeLabel(type: number): string {
    return WithdrawalStrategyTypeLabels[type] || 'Unknown';
  }

  deleteStrategy(strategy: WithdrawalStrategy): void {
    if (confirm(`Are you sure you want to delete "${strategy.name}"?`)) {
      this.strategyService.deleteStrategy(strategy.withdrawalStrategyId).subscribe();
    }
  }
}
