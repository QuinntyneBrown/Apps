import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MortgageService, PaymentService, RefinanceScenarioService } from '../services';

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
            <mat-icon mat-card-avatar class="dashboard__card-icon">home</mat-icon>
            <mat-card-title>Active Mortgages</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ activeMortgagesCount }}</div>
            <p class="dashboard__card-description">Total active mortgage accounts</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/mortgages" class="dashboard__card-action">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon">account_balance</mat-icon>
            <mat-card-title>Total Balance</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ totalBalance | currency }}</div>
            <p class="dashboard__card-description">Combined current balance</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/mortgages" class="dashboard__card-action">View Details</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon">payment</mat-icon>
            <mat-card-title>Payments</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ paymentsCount }}</div>
            <p class="dashboard__card-description">Total payment records</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/payments" class="dashboard__card-action">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon">assessment</mat-icon>
            <mat-card-title>Refinance Scenarios</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ refinanceScenariosCount }}</div>
            <p class="dashboard__card-description">Saved refinance scenarios</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/refinance-scenarios" class="dashboard__card-action">View All</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
    }

    .dashboard__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 400;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-icon {
      font-size: 40px;
      width: 40px;
      height: 40px;
      color: #3f51b5;
    }

    .dashboard__card-value {
      font-size: 2.5rem;
      font-weight: 500;
      color: #3f51b5;
      margin: 1rem 0;
    }

    .dashboard__card-description {
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }

    .dashboard__card-action {
      color: #3f51b5;
    }
  `]
})
export class Dashboard implements OnInit {
  private mortgageService = inject(MortgageService);
  private paymentService = inject(PaymentService);
  private refinanceScenarioService = inject(RefinanceScenarioService);

  activeMortgagesCount = 0;
  totalBalance = 0;
  paymentsCount = 0;
  refinanceScenariosCount = 0;

  ngOnInit(): void {
    this.loadDashboardData();
  }

  private loadDashboardData(): void {
    this.mortgageService.getMortgages().subscribe(mortgages => {
      this.activeMortgagesCount = mortgages.filter(m => m.isActive).length;
      this.totalBalance = mortgages.reduce((sum, m) => sum + m.currentBalance, 0);
    });

    this.paymentService.getPayments().subscribe(payments => {
      this.paymentsCount = payments.length;
    });

    this.refinanceScenarioService.getRefinanceScenarios().subscribe(scenarios => {
      this.refinanceScenariosCount = scenarios.length;
    });
  }
}
