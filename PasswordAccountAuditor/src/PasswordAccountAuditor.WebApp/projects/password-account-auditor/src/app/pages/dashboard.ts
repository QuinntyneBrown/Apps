import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService, BreachAlertService, SecurityAuditService } from '../services';
import { Account, BreachAlert, SecurityAudit, AlertStatus, BreachSeverity, SecurityLevel } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card dashboard__card--accounts">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon">account_circle</mat-icon>
            <mat-card-title>Total Accounts</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ (totalAccounts$ | async) || 0 }}</div>
            <div class="dashboard__card-subtitle">Active: {{ (activeAccounts$ | async) || 0 }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/accounts" color="primary">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--alerts">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon dashboard__card-icon--warning">warning</mat-icon>
            <mat-card-title>Breach Alerts</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ (totalBreachAlerts$ | async) || 0 }}</div>
            <div class="dashboard__card-subtitle">Unresolved: {{ (unresolvedAlerts$ | async) || 0 }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/breach-alerts" color="warn">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--audits">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon dashboard__card-icon--accent">security</mat-icon>
            <mat-card-title>Security Audits</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ (totalSecurityAudits$ | async) || 0 }}</div>
            <div class="dashboard__card-subtitle">Completed: {{ (completedAudits$ | async) || 0 }}</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/security-audits" color="accent">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--security">
          <mat-card-header>
            <mat-icon mat-card-avatar class="dashboard__card-icon">shield</mat-icon>
            <mat-card-title>Security Overview</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stats">
              <div class="dashboard__stat">
                <span class="dashboard__stat-label">High Security:</span>
                <span class="dashboard__stat-value">{{ (highSecurityAccounts$ | async) || 0 }}</span>
              </div>
              <div class="dashboard__stat">
                <span class="dashboard__stat-label">Low Security:</span>
                <span class="dashboard__stat-value">{{ (lowSecurityAccounts$ | async) || 0 }}</span>
              </div>
              <div class="dashboard__stat">
                <span class="dashboard__stat-label">Critical Alerts:</span>
                <span class="dashboard__stat-value">{{ (criticalAlerts$ | async) || 0 }}</span>
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
      color: rgba(0, 0, 0, 0.87);
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      height: 100%;
    }

    .dashboard__card-icon {
      font-size: 40px;
      width: 40px;
      height: 40px;
      display: flex;
      align-items: center;
      justify-content: center;
      color: #3f51b5;
    }

    .dashboard__card-icon--warning {
      color: #f44336;
    }

    .dashboard__card-icon--accent {
      color: #ff4081;
    }

    .dashboard__card-value {
      font-size: 3rem;
      font-weight: 500;
      line-height: 1;
      margin: 1rem 0 0.5rem 0;
    }

    .dashboard__card-subtitle {
      color: rgba(0, 0, 0, 0.6);
      font-size: 0.875rem;
    }

    .dashboard__card-stats {
      display: flex;
      flex-direction: column;
      gap: 0.75rem;
      margin-top: 1rem;
    }

    .dashboard__stat {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0.5rem;
      background-color: rgba(0, 0, 0, 0.02);
      border-radius: 4px;
    }

    .dashboard__stat-label {
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .dashboard__stat-value {
      font-size: 1.25rem;
      font-weight: 500;
    }

    @media (max-width: 768px) {
      .dashboard {
        padding: 1rem;
      }

      .dashboard__cards {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private accountService = inject(AccountService);
  private breachAlertService = inject(BreachAlertService);
  private securityAuditService = inject(SecurityAuditService);

  totalAccounts$!: Observable<number>;
  activeAccounts$!: Observable<number>;
  highSecurityAccounts$!: Observable<number>;
  lowSecurityAccounts$!: Observable<number>;
  totalBreachAlerts$!: Observable<number>;
  unresolvedAlerts$!: Observable<number>;
  criticalAlerts$!: Observable<number>;
  totalSecurityAudits$!: Observable<number>;
  completedAudits$!: Observable<number>;

  ngOnInit() {
    this.accountService.getAccounts().subscribe();
    this.breachAlertService.getBreachAlerts().subscribe();
    this.securityAuditService.getSecurityAudits().subscribe();

    this.totalAccounts$ = this.accountService.accounts$.pipe(
      map(accounts => accounts.length)
    );

    this.activeAccounts$ = this.accountService.accounts$.pipe(
      map(accounts => accounts.filter(a => a.isActive).length)
    );

    this.highSecurityAccounts$ = this.accountService.accounts$.pipe(
      map(accounts => accounts.filter(a => a.securityLevel === SecurityLevel.High).length)
    );

    this.lowSecurityAccounts$ = this.accountService.accounts$.pipe(
      map(accounts => accounts.filter(a => a.securityLevel === SecurityLevel.Low).length)
    );

    this.totalBreachAlerts$ = this.breachAlertService.breachAlerts$.pipe(
      map(alerts => alerts.length)
    );

    this.unresolvedAlerts$ = this.breachAlertService.breachAlerts$.pipe(
      map(alerts => alerts.filter(a => a.status !== AlertStatus.Resolved && a.status !== AlertStatus.Dismissed).length)
    );

    this.criticalAlerts$ = this.breachAlertService.breachAlerts$.pipe(
      map(alerts => alerts.filter(a => a.severity === BreachSeverity.Critical).length)
    );

    this.totalSecurityAudits$ = this.securityAuditService.securityAudits$.pipe(
      map(audits => audits.length)
    );

    this.completedAudits$ = this.securityAuditService.securityAudits$.pipe(
      map(audits => audits.filter(a => a.status === 2).length)
    );
  }
}
