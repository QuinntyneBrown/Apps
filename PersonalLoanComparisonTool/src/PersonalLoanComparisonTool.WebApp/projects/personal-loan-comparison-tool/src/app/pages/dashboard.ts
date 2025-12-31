import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { LoanService, OfferService, PaymentScheduleService } from '../services';

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
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">account_balance</mat-icon>
              Loans
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (loanService.loans$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total loan requests tracked</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/loans" class="dashboard__card-button">
              View Loans
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">local_offer</mat-icon>
              Offers
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (offerService.offers$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total loan offers received</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/offers" class="dashboard__card-button">
              View Offers
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">schedule</mat-icon>
              Payment Schedules
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (paymentScheduleService.paymentSchedules$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total payment schedules</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/payment-schedules" class="dashboard__card-button">
              View Schedules
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
    }

    .dashboard__title {
      margin: 0 0 24px 0;
      font-size: 32px;
      font-weight: 400;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 24px;
    }

    .dashboard__card {
      height: 100%;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 8px;
      font-size: 20px;
    }

    .dashboard__card-icon {
      color: #1976d2;
    }

    .dashboard__card-count {
      font-size: 48px;
      font-weight: 300;
      color: #1976d2;
      margin: 16px 0;
    }

    .dashboard__card-description {
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .dashboard__card-button {
      width: 100%;
    }
  `]
})
export class Dashboard implements OnInit {
  loanService = inject(LoanService);
  offerService = inject(OfferService);
  paymentScheduleService = inject(PaymentScheduleService);

  ngOnInit(): void {
    this.loanService.getAll().subscribe();
    this.offerService.getAll().subscribe();
    this.paymentScheduleService.getAll().subscribe();
  }
}
