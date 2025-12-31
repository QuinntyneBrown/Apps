import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PropertyService, LeaseService, ExpenseService, CashFlowService } from '../services';

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
            <mat-icon mat-card-avatar class="dashboard__card-icon dashboard__card-icon--properties">home</mat-icon>
            <mat-card-title>Properties</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-value">{{ (propertyService.properties$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-label">Total Properties</p>
            <p class="dashboard__card-metric" *ngIf="totalPropertyValue > 0">
              Total Value: {{ totalPropertyValue | currency }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/properties">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon dashboard__card-icon--leases">assignment</mat-icon>
            <mat-card-title>Leases</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-value">{{ activeLeaseCount }}</p>
            <p class="dashboard__card-label">Active Leases</p>
            <p class="dashboard__card-metric" *ngIf="totalMonthlyRent > 0">
              Monthly Rent: {{ totalMonthlyRent | currency }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/leases">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon dashboard__card-icon--expenses">receipt</mat-icon>
            <mat-card-title>Expenses</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-value">{{ (expenseService.expenses$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-label">Total Expenses</p>
            <p class="dashboard__card-metric" *ngIf="totalExpenses > 0">
              Total Amount: {{ totalExpenses | currency }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/expenses">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon dashboard__card-icon--cash-flows">attach_money</mat-icon>
            <mat-card-title>Cash Flows</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-value">{{ (cashFlowService.cashFlows$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-label">Total Records</p>
            <p class="dashboard__card-metric" *ngIf="totalNetCashFlow !== 0">
              Net Cash Flow: {{ totalNetCashFlow | currency }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/cash-flows">View All</a>
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
      margin: 0 0 2rem;
      font-size: 2rem;
      font-weight: 500;
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
      width: 40px;
      height: 40px;
      display: flex;
      align-items: center;
      justify-content: center;
      border-radius: 50%;
      color: white;
    }

    .dashboard__card-icon--properties {
      background-color: #1976d2;
    }

    .dashboard__card-icon--leases {
      background-color: #388e3c;
    }

    .dashboard__card-icon--expenses {
      background-color: #d32f2f;
    }

    .dashboard__card-icon--cash-flows {
      background-color: #f57c00;
    }

    .dashboard__card-value {
      font-size: 2.5rem;
      font-weight: 500;
      margin: 1rem 0 0.5rem;
      line-height: 1;
    }

    .dashboard__card-label {
      color: rgba(0, 0, 0, 0.6);
      margin: 0 0 0.5rem;
    }

    .dashboard__card-metric {
      margin: 0;
      font-weight: 500;
      color: rgba(0, 0, 0, 0.87);
    }
  `]
})
export class Dashboard implements OnInit {
  readonly propertyService = inject(PropertyService);
  readonly leaseService = inject(LeaseService);
  readonly expenseService = inject(ExpenseService);
  readonly cashFlowService = inject(CashFlowService);

  totalPropertyValue = 0;
  activeLeaseCount = 0;
  totalMonthlyRent = 0;
  totalExpenses = 0;
  totalNetCashFlow = 0;

  ngOnInit(): void {
    this.propertyService.getProperties().subscribe(properties => {
      this.totalPropertyValue = properties.reduce((sum, p) => sum + p.currentValue, 0);
    });

    this.leaseService.getLeases().subscribe(leases => {
      const activeLeases = leases.filter(l => l.isActive);
      this.activeLeaseCount = activeLeases.length;
      this.totalMonthlyRent = activeLeases.reduce((sum, l) => sum + l.monthlyRent, 0);
    });

    this.expenseService.getExpenses().subscribe(expenses => {
      this.totalExpenses = expenses.reduce((sum, e) => sum + e.amount, 0);
    });

    this.cashFlowService.getCashFlows().subscribe(cashFlows => {
      this.totalNetCashFlow = cashFlows.reduce((sum, cf) => sum + cf.netCashFlow, 0);
    });
  }
}
