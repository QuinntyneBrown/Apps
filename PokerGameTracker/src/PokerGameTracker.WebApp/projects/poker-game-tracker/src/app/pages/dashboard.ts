import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BankrollService, SessionService, HandService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <div class="dashboard__container">
        <h1 class="dashboard__title">Dashboard</h1>

        <div class="dashboard__cards">
          <mat-card class="dashboard__card">
            <mat-card-header>
              <mat-card-title class="dashboard__card-title">
                <mat-icon class="dashboard__card-icon">account_balance_wallet</mat-icon>
                Bankrolls
              </mat-card-title>
            </mat-card-header>
            <mat-card-content>
              <div class="dashboard__card-content">
                <div class="dashboard__stat">
                  <span class="dashboard__stat-label">Total Records:</span>
                  <span class="dashboard__stat-value">{{ (bankrolls$ | async)?.length || 0 }}</span>
                </div>
                <div class="dashboard__stat" *ngIf="latestBankroll">
                  <span class="dashboard__stat-label">Latest Amount:</span>
                  <span class="dashboard__stat-value">\${{ latestBankroll.amount | number:'1.2-2' }}</span>
                </div>
              </div>
            </mat-card-content>
            <mat-card-actions>
              <a mat-button color="primary" routerLink="/bankrolls">View All</a>
            </mat-card-actions>
          </mat-card>

          <mat-card class="dashboard__card">
            <mat-card-header>
              <mat-card-title class="dashboard__card-title">
                <mat-icon class="dashboard__card-icon">casino</mat-icon>
                Sessions
              </mat-card-title>
            </mat-card-header>
            <mat-card-content>
              <div class="dashboard__card-content">
                <div class="dashboard__stat">
                  <span class="dashboard__stat-label">Total Sessions:</span>
                  <span class="dashboard__stat-value">{{ (sessions$ | async)?.length || 0 }}</span>
                </div>
                <div class="dashboard__stat">
                  <span class="dashboard__stat-label">Active Sessions:</span>
                  <span class="dashboard__stat-value">{{ activeSessions }}</span>
                </div>
              </div>
            </mat-card-content>
            <mat-card-actions>
              <a mat-button color="primary" routerLink="/sessions">View All</a>
            </mat-card-actions>
          </mat-card>

          <mat-card class="dashboard__card">
            <mat-card-header>
              <mat-card-title class="dashboard__card-title">
                <mat-icon class="dashboard__card-icon">style</mat-icon>
                Hands
              </mat-card-title>
            </mat-card-header>
            <mat-card-content>
              <div class="dashboard__card-content">
                <div class="dashboard__stat">
                  <span class="dashboard__stat-label">Total Hands:</span>
                  <span class="dashboard__stat-value">{{ (hands$ | async)?.length || 0 }}</span>
                </div>
                <div class="dashboard__stat">
                  <span class="dashboard__stat-label">Hands Won:</span>
                  <span class="dashboard__stat-value">{{ handsWon }}</span>
                </div>
              </div>
            </mat-card-content>
            <mat-card-actions>
              <a mat-button color="primary" routerLink="/hands">View All</a>
            </mat-card-actions>
          </mat-card>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;

      &__container {
        max-width: 1400px;
        margin: 0 auto;
      }

      &__title {
        margin: 0 0 24px;
        font-size: 32px;
        font-weight: 400;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 24px;
      }

      &__card {
        &-title {
          display: flex;
          align-items: center;
          gap: 8px;
        }

        &-icon {
          color: #1976d2;
        }

        &-content {
          padding: 16px 0;
        }
      }

      &__stat {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 8px 0;

        &-label {
          font-size: 14px;
          color: rgba(0, 0, 0, 0.6);
        }

        &-value {
          font-size: 20px;
          font-weight: 500;
        }
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private bankrollService = inject(BankrollService);
  private sessionService = inject(SessionService);
  private handService = inject(HandService);

  bankrolls$ = this.bankrollService.bankrolls$;
  sessions$ = this.sessionService.sessions$;
  hands$ = this.handService.hands$;

  latestBankroll: any = null;
  activeSessions = 0;
  handsWon = 0;

  ngOnInit() {
    this.bankrollService.getBankrolls().subscribe(bankrolls => {
      if (bankrolls.length > 0) {
        this.latestBankroll = bankrolls.reduce((latest, current) =>
          new Date(current.recordedDate) > new Date(latest.recordedDate) ? current : latest
        );
      }
    });

    this.sessionService.getSessions().subscribe(sessions => {
      this.activeSessions = sessions.filter(s => !s.endTime).length;
    });

    this.handService.getHands().subscribe(hands => {
      this.handsWon = hands.filter(h => h.wasWon).length;
    });
  }
}
