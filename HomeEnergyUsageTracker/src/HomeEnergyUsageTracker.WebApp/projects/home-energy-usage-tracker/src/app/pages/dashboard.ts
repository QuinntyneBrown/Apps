import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { UtilityBillService, UsageService, SavingsTipService } from '../services';
import { UtilityType } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__stats">
        <mat-card class="dashboard__stat-card">
          <mat-card-content class="dashboard__stat-content">
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--primary">receipt</mat-icon>
            <div class="dashboard__stat-details">
              <div class="dashboard__stat-value">{{ (utilityBills$ | async)?.length || 0 }}</div>
              <div class="dashboard__stat-label">Utility Bills</div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="navigateTo('/utility-bills')">View All</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-content class="dashboard__stat-content">
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--accent">show_chart</mat-icon>
            <div class="dashboard__stat-details">
              <div class="dashboard__stat-value">{{ (usages$ | async)?.length || 0 }}</div>
              <div class="dashboard__stat-label">Usage Readings</div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="navigateTo('/usages')">View All</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-content class="dashboard__stat-content">
            <mat-icon class="dashboard__stat-icon dashboard__stat-icon--warn">lightbulb</mat-icon>
            <div class="dashboard__stat-details">
              <div class="dashboard__stat-value">{{ (savingsTips$ | async)?.length || 0 }}</div>
              <div class="dashboard__stat-label">Savings Tips</div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="navigateTo('/savings-tips')">View All</button>
          </mat-card-actions>
        </mat-card>
      </div>

      <div class="dashboard__sections">
        <mat-card class="dashboard__section">
          <mat-card-header>
            <mat-card-title class="dashboard__section-title">Recent Utility Bills</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="(utilityBills$ | async)?.length === 0" class="dashboard__empty">
              No utility bills found. <a (click)="navigateTo('/utility-bills/create')" class="dashboard__link">Add your first bill</a>
            </div>
            <div *ngFor="let bill of (utilityBills$ | async)?.slice(0, 5)" class="dashboard__item">
              <div class="dashboard__item-header">
                <strong>{{ getUtilityTypeName(bill.utilityType) }}</strong>
                <span class="dashboard__item-amount">\${{ bill.amount | number: '1.2-2' }}</span>
              </div>
              <div class="dashboard__item-details">
                {{ bill.billingDate | date: 'short' }}
                <span *ngIf="bill.usageAmount"> - {{ bill.usageAmount }} {{ bill.unit }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__section">
          <mat-card-header>
            <mat-card-title class="dashboard__section-title">Recent Usage Readings</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="(usages$ | async)?.length === 0" class="dashboard__empty">
              No usage readings found. <a (click)="navigateTo('/usages/create')" class="dashboard__link">Add your first reading</a>
            </div>
            <div *ngFor="let usage of (usages$ | async)?.slice(0, 5)" class="dashboard__item">
              <div class="dashboard__item-header">
                <strong>{{ usage.date | date: 'short' }}</strong>
                <span class="dashboard__item-amount">{{ usage.amount | number: '1.2-2' }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__section dashboard__section--full">
          <mat-card-header>
            <mat-card-title class="dashboard__section-title">Energy Savings Tips</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="(savingsTips$ | async)?.length === 0" class="dashboard__empty">
              No savings tips found. <a (click)="navigateTo('/savings-tips/create')" class="dashboard__link">Add your first tip</a>
            </div>
            <div *ngFor="let tip of (savingsTips$ | async)?.slice(0, 3)" class="dashboard__tip">
              <mat-icon class="dashboard__tip-icon">lightbulb</mat-icon>
              <div class="dashboard__tip-content">
                <strong>{{ tip.title }}</strong>
                <p>{{ tip.description }}</p>
              </div>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
      max-width: 1200px;
      margin: 0 auto;

      &__title {
        font-size: 32px;
        margin-bottom: 24px;
      }

      &__stats {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 24px;
        margin-bottom: 32px;
      }

      &__stat-card {
        text-align: center;
      }

      &__stat-content {
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 16px;
        padding: 24px;
      }

      &__stat-icon {
        font-size: 48px;
        width: 48px;
        height: 48px;

        &--primary {
          color: #1976d2;
        }

        &--accent {
          color: #f57c00;
        }

        &--warn {
          color: #fbc02d;
        }
      }

      &__stat-details {
        text-align: left;
      }

      &__stat-value {
        font-size: 32px;
        font-weight: 500;
        line-height: 1;
      }

      &__stat-label {
        color: rgba(0, 0, 0, 0.6);
        margin-top: 4px;
      }

      &__sections {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
        gap: 24px;
      }

      &__section {
        &--full {
          grid-column: 1 / -1;
        }
      }

      &__section-title {
        font-size: 20px;
        margin-bottom: 16px;
      }

      &__empty {
        padding: 32px;
        text-align: center;
        color: rgba(0, 0, 0, 0.6);
      }

      &__link {
        color: #1976d2;
        cursor: pointer;
        text-decoration: underline;

        &:hover {
          color: #1565c0;
        }
      }

      &__item {
        padding: 12px 0;
        border-bottom: 1px solid rgba(0, 0, 0, 0.12);

        &:last-child {
          border-bottom: none;
        }
      }

      &__item-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 4px;
      }

      &__item-amount {
        color: #1976d2;
        font-weight: 500;
      }

      &__item-details {
        font-size: 14px;
        color: rgba(0, 0, 0, 0.6);
      }

      &__tip {
        display: flex;
        gap: 16px;
        padding: 16px 0;
        border-bottom: 1px solid rgba(0, 0, 0, 0.12);

        &:last-child {
          border-bottom: none;
        }
      }

      &__tip-icon {
        color: #fbc02d;
        flex-shrink: 0;
      }

      &__tip-content {
        strong {
          display: block;
          margin-bottom: 4px;
        }

        p {
          margin: 0;
          color: rgba(0, 0, 0, 0.7);
          font-size: 14px;
        }
      }
    }
  `]
})
export class Dashboard implements OnInit {
  utilityBills$ = this.utilityBillService.utilityBills$;
  usages$ = this.usageService.usages$;
  savingsTips$ = this.savingsTipService.savingsTips$;

  constructor(
    private utilityBillService: UtilityBillService,
    private usageService: UsageService,
    private savingsTipService: SavingsTipService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.utilityBillService.getAll().subscribe();
    this.usageService.getAll().subscribe();
    this.savingsTipService.getAll().subscribe();
  }

  getUtilityTypeName(type: UtilityType): string {
    return UtilityType[type];
  }

  navigateTo(path: string): void {
    this.router.navigate([path]);
  }
}
