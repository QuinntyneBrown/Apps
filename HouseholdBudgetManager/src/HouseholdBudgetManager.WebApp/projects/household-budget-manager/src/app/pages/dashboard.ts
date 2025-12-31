import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BudgetService, ExpenseService, IncomeService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>account_balance_wallet</mat-icon>
            <mat-card-title>Budgets</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ (budgets$ | async)?.length || 0 }} budgets</p>
            <p class="dashboard__substat">{{ activeBudgetsCount }} active</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/budgets">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>money_off</mat-icon>
            <mat-card-title>Expenses</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ totalExpenses | currency }}</p>
            <p class="dashboard__substat">{{ (expenses$ | async)?.length || 0 }} transactions</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/expenses">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>attach_money</mat-icon>
            <mat-card-title>Incomes</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ totalIncomes | currency }}</p>
            <p class="dashboard__substat">{{ (incomes$ | async)?.length || 0 }} transactions</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/incomes">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--highlight">
          <mat-card-header>
            <mat-icon mat-card-avatar>savings</mat-icon>
            <mat-card-title>Net Balance</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat" [class.positive]="netBalance >= 0" [class.negative]="netBalance < 0">
              {{ netBalance | currency }}
            </p>
            <p class="dashboard__substat">{{ netBalance >= 0 ? 'Surplus' : 'Deficit' }}</p>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 1.5rem;
    }
    .dashboard__title {
      margin-bottom: 1.5rem;
    }
    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 1.5rem;
    }
    .dashboard__card {
      mat-icon[mat-card-avatar] {
        font-size: 40px;
        width: 40px;
        height: 40px;
      }
    }
    .dashboard__stat {
      font-size: 1.5rem;
      font-weight: 500;
      margin: 0.5rem 0;
    }
    .dashboard__stat.positive {
      color: #4caf50;
    }
    .dashboard__stat.negative {
      color: #f44336;
    }
    .dashboard__substat {
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }
  `]
})
export class Dashboard implements OnInit {
  private _budgetService = inject(BudgetService);
  private _expenseService = inject(ExpenseService);
  private _incomeService = inject(IncomeService);

  budgets$ = this._budgetService.budgets$;
  expenses$ = this._expenseService.expenses$;
  incomes$ = this._incomeService.incomes$;

  activeBudgetsCount = 0;
  totalExpenses = 0;
  totalIncomes = 0;
  netBalance = 0;

  ngOnInit(): void {
    this._budgetService.getAll().subscribe(budgets => {
      this.activeBudgetsCount = budgets.filter(b => b.status === 1).length;
    });
    this._expenseService.getAll().subscribe(expenses => {
      this.totalExpenses = expenses.reduce((sum, e) => sum + e.amount, 0);
      this.updateNetBalance();
    });
    this._incomeService.getAll().subscribe(incomes => {
      this.totalIncomes = incomes.reduce((sum, i) => sum + i.amount, 0);
      this.updateNetBalance();
    });
  }

  private updateNetBalance(): void {
    this.netBalance = this.totalIncomes - this.totalExpenses;
  }
}
