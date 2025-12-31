import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AccountService, HoldingService, TransactionService, DividendService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Portfolio Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card dashboard__card--highlight">
          <mat-card-header>
            <mat-icon mat-card-avatar>account_balance_wallet</mat-icon>
            <mat-card-title>Total Portfolio Value</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ totalValue | currency }}</p>
            <p class="dashboard__substat" [class.positive]="totalGainLoss >= 0" [class.negative]="totalGainLoss < 0">
              {{ totalGainLoss >= 0 ? '+' : '' }}{{ totalGainLoss | currency }} ({{ totalGainLossPercent | number:'1.2-2' }}%)
            </p>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>account_balance</mat-icon>
            <mat-card-title>Accounts</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ (accounts$ | async)?.length || 0 }}</p>
            <p class="dashboard__substat">{{ activeAccountsCount }} active</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/accounts">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>pie_chart</mat-icon>
            <mat-card-title>Holdings</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ (holdings$ | async)?.length || 0 }}</p>
            <p class="dashboard__substat">Investment positions</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/holdings">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>payments</mat-icon>
            <mat-card-title>Dividends</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ totalDividends | currency }}</p>
            <p class="dashboard__substat">Total received</p>
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
    .dashboard__substat {
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }
    .dashboard__substat.positive {
      color: #4caf50;
    }
    .dashboard__substat.negative {
      color: #f44336;
    }
  `]
})
export class Dashboard implements OnInit {
  private _accountService = inject(AccountService);
  private _holdingService = inject(HoldingService);
  private _dividendService = inject(DividendService);

  accounts$ = this._accountService.accounts$;
  holdings$ = this._holdingService.holdings$;

  activeAccountsCount = 0;
  totalValue = 0;
  totalCostBasis = 0;
  totalGainLoss = 0;
  totalGainLossPercent = 0;
  totalDividends = 0;

  ngOnInit(): void {
    this._accountService.getAll().subscribe(accounts => {
      this.activeAccountsCount = accounts.filter(a => a.isActive).length;
    });
    this._holdingService.getAll().subscribe(holdings => {
      this.totalValue = holdings.reduce((sum, h) => sum + h.marketValue, 0);
      this.totalCostBasis = holdings.reduce((sum, h) => sum + h.costBasis, 0);
      this.totalGainLoss = this.totalValue - this.totalCostBasis;
      this.totalGainLossPercent = this.totalCostBasis > 0 ? (this.totalGainLoss / this.totalCostBasis) * 100 : 0;
    });
    this._dividendService.getAll().subscribe(dividends => {
      this.totalDividends = dividends.reduce((sum, d) => sum + d.totalAmount, 0);
    });
  }
}
