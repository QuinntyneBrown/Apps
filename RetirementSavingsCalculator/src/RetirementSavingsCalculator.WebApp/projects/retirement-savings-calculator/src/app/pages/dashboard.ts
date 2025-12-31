import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RetirementScenarioService, ContributionService, WithdrawalStrategyService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Retirement Savings Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">savings</mat-icon>
              Retirement Scenarios
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (scenarios$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Active retirement planning scenarios</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/retirement-scenarios">
              View Scenarios
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">attach_money</mat-icon>
              Contributions
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (contributions$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total contribution records</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/contributions">
              View Contributions
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">trending_down</mat-icon>
              Withdrawal Strategies
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (strategies$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Configured withdrawal strategies</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/withdrawal-strategies">
              View Strategies
            </button>
          </mat-card-actions>
        </mat-card>
      </div>

      <div class="dashboard__summary" *ngIf="(scenarios$ | async) as scenarios">
        <mat-card *ngIf="scenarios.length > 0" class="dashboard__summary-card">
          <mat-card-header>
            <mat-card-title>Recent Scenarios</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__scenario-list">
              <div *ngFor="let scenario of scenarios.slice(0, 5)" class="dashboard__scenario-item">
                <div class="dashboard__scenario-name">{{ scenario.name }}</div>
                <div class="dashboard__scenario-details">
                  <span>Age: {{ scenario.currentAge }}</span>
                  <span>Retirement: {{ scenario.retirementAge }}</span>
                  <span>Years to retirement: {{ scenario.yearsToRetirement }}</span>
                </div>
                <div class="dashboard__scenario-projections">
                  <span>Projected Savings: ${{ scenario.projectedSavings | number:'1.2-2' }}</span>
                  <span>Annual Withdrawal: ${{ scenario.annualWithdrawalNeeded | number:'1.2-2' }}</span>
                </div>
              </div>
            </div>
          </mat-card-content>
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
      font-weight: 500;
      color: #333;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 1.5rem;
      margin-bottom: 2rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .dashboard__card-icon {
      color: #1976d2;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: 600;
      color: #1976d2;
      margin: 1rem 0;
    }

    .dashboard__card-description {
      color: #666;
      margin: 0;
    }

    .dashboard__summary-card {
      margin-top: 2rem;
    }

    .dashboard__scenario-list {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .dashboard__scenario-item {
      padding: 1rem;
      background-color: #f5f5f5;
      border-radius: 4px;
    }

    .dashboard__scenario-name {
      font-size: 1.1rem;
      font-weight: 500;
      margin-bottom: 0.5rem;
    }

    .dashboard__scenario-details,
    .dashboard__scenario-projections {
      display: flex;
      gap: 1.5rem;
      font-size: 0.875rem;
      color: #666;
    }

    .dashboard__scenario-projections {
      margin-top: 0.5rem;
      font-weight: 500;
    }
  `]
})
export class Dashboard implements OnInit {
  private scenarioService = inject(RetirementScenarioService);
  private contributionService = inject(ContributionService);
  private strategyService = inject(WithdrawalStrategyService);

  scenarios$ = this.scenarioService.scenarios$;
  contributions$ = this.contributionService.contributions$;
  strategies$ = this.strategyService.strategies$;

  ngOnInit(): void {
    this.scenarioService.loadScenarios().subscribe();
    this.contributionService.loadContributions().subscribe();
    this.strategyService.loadStrategies().subscribe();
  }
}
