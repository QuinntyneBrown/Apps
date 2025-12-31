import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { Observable } from 'rxjs';
import { SessionAnalyticsService } from '../services';
import { SessionAnalytics, SessionTypeLabels } from '../models';

@Component({
  selector: 'app-analytics',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDividerModule
  ],
  template: `
    <div class="analytics">
      <div class="analytics__header">
        <h1 class="analytics__title">Session Analytics</h1>
      </div>

      <mat-card class="analytics__generator-card">
        <mat-card-header>
          <mat-card-title>Generate Analytics</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="analyticsForm" (ngSubmit)="generateAnalytics()" class="analytics__form">
            <mat-form-field appearance="outline" class="analytics__form-field">
              <mat-label>User ID</mat-label>
              <input matInput formControlName="userId" placeholder="Enter user ID">
            </mat-form-field>

            <mat-form-field appearance="outline" class="analytics__form-field">
              <mat-label>Start Date</mat-label>
              <input matInput [matDatepicker]="startPicker" formControlName="periodStartDate">
              <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
              <mat-datepicker #startPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field appearance="outline" class="analytics__form-field">
              <mat-label>End Date</mat-label>
              <input matInput [matDatepicker]="endPicker" formControlName="periodEndDate">
              <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
              <mat-datepicker #endPicker></mat-datepicker>
            </mat-form-field>

            <div class="analytics__form-actions">
              <button mat-raised-button color="primary" type="submit" [disabled]="!analyticsForm.valid">
                <mat-icon>analytics</mat-icon>
                Generate Analytics
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <div class="analytics__results">
        <div *ngFor="let analytics of analytics$ | async" class="analytics__result">
          <mat-card class="analytics__result-card">
            <mat-card-header>
              <mat-card-title>
                {{ analytics.periodStartDate | date:'mediumDate' }} - {{ analytics.periodEndDate | date:'mediumDate' }}
              </mat-card-title>
              <mat-card-subtitle>Generated: {{ analytics.createdAt | date:'short' }}</mat-card-subtitle>
            </mat-card-header>
            <mat-card-content>
              <div class="analytics__metrics">
                <div class="analytics__metric-row">
                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--primary">
                      <mat-icon>event</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Total Sessions</div>
                      <div class="analytics__metric-value">{{ analytics.totalSessions }}</div>
                    </div>
                  </div>

                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--accent">
                      <mat-icon>schedule</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Total Focus Time</div>
                      <div class="analytics__metric-value">{{ formatMinutes(analytics.totalFocusMinutes) }}</div>
                    </div>
                  </div>

                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--success">
                      <mat-icon>star</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Average Focus Score</div>
                      <div class="analytics__metric-value">{{ analytics.averageFocusScore?.toFixed(1) || 'N/A' }}</div>
                    </div>
                  </div>

                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--warn">
                      <mat-icon>notifications_off</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Total Distractions</div>
                      <div class="analytics__metric-value">{{ analytics.totalDistractions }}</div>
                    </div>
                  </div>
                </div>

                <mat-divider></mat-divider>

                <div class="analytics__metric-row">
                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--info">
                      <mat-icon>check_circle</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Completion Rate</div>
                      <div class="analytics__metric-value">{{ (analytics.completionRate * 100).toFixed(1) }}%</div>
                    </div>
                  </div>

                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--primary">
                      <mat-icon>trending_up</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Avg Session Duration</div>
                      <div class="analytics__metric-value">{{ analytics.averageSessionDuration.toFixed(1) }} min</div>
                    </div>
                  </div>

                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--warn">
                      <mat-icon>warning</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Avg Distractions</div>
                      <div class="analytics__metric-value">{{ analytics.averageDistractions.toFixed(1) }}</div>
                    </div>
                  </div>

                  <div class="analytics__metric">
                    <div class="analytics__metric-icon analytics__metric-icon--success">
                      <mat-icon>workspace_premium</mat-icon>
                    </div>
                    <div class="analytics__metric-content">
                      <div class="analytics__metric-label">Most Productive Type</div>
                      <div class="analytics__metric-value analytics__metric-value--small">
                        {{ analytics.mostProductiveSessionType !== null ? getSessionTypeLabel(analytics.mostProductiveSessionType) : 'N/A' }}
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </mat-card-content>
          </mat-card>
        </div>

        <div *ngIf="(analytics$ | async)?.length === 0" class="analytics__empty">
          <mat-icon>analytics</mat-icon>
          <p>No analytics generated yet. Use the form above to generate analytics for a specific period.</p>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .analytics {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;
    }

    .analytics__header {
      margin-bottom: 24px;
    }

    .analytics__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .analytics__generator-card {
      margin-bottom: 24px;
    }

    .analytics__form {
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .analytics__form-actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
    }

    .analytics__results {
      display: flex;
      flex-direction: column;
      gap: 24px;
    }

    .analytics__result-card {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;

      ::ng-deep mat-card-title {
        color: white !important;
      }

      ::ng-deep mat-card-subtitle {
        color: rgba(255, 255, 255, 0.8) !important;
      }
    }

    .analytics__metrics {
      padding: 16px 0;
    }

    .analytics__metric-row {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
      gap: 24px;
      margin: 16px 0;
    }

    .analytics__metric {
      display: flex;
      align-items: center;
      gap: 12px;
    }

    .analytics__metric-icon {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 48px;
      height: 48px;
      border-radius: 50%;
      background-color: rgba(255, 255, 255, 0.2);

      mat-icon {
        font-size: 24px;
        width: 24px;
        height: 24px;
        color: white;
      }
    }

    .analytics__metric-content {
      flex: 1;
    }

    .analytics__metric-label {
      font-size: 12px;
      opacity: 0.9;
      text-transform: uppercase;
      letter-spacing: 0.5px;
    }

    .analytics__metric-value {
      font-size: 24px;
      font-weight: 600;
      margin-top: 4px;
    }

    .analytics__metric-value--small {
      font-size: 16px;
    }

    .analytics__empty {
      text-align: center;
      padding: 64px 32px;
      color: rgba(0, 0, 0, 0.54);

      mat-icon {
        font-size: 64px;
        width: 64px;
        height: 64px;
        margin-bottom: 16px;
        opacity: 0.3;
      }

      p {
        font-size: 16px;
      }
    }

    @media (max-width: 768px) {
      .analytics__form {
        grid-template-columns: 1fr;
      }

      .analytics__metric-row {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class Analytics implements OnInit {
  analytics$: Observable<SessionAnalytics[]>;
  analyticsForm: FormGroup;

  constructor(
    private analyticsService: SessionAnalyticsService,
    private fb: FormBuilder
  ) {
    this.analytics$ = this.analyticsService.analytics$;

    const endDate = new Date();
    const startDate = new Date();
    startDate.setDate(startDate.getDate() - 30);

    this.analyticsForm = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      periodStartDate: [startDate, Validators.required],
      periodEndDate: [endDate, Validators.required]
    });
  }

  ngOnInit(): void {
    this.analyticsService.getAnalytics().subscribe();
  }

  generateAnalytics(): void {
    if (this.analyticsForm.valid) {
      const command = {
        userId: this.analyticsForm.value.userId,
        periodStartDate: this.analyticsForm.value.periodStartDate.toISOString(),
        periodEndDate: this.analyticsForm.value.periodEndDate.toISOString()
      };
      this.analyticsService.generateAnalytics(command).subscribe();
    }
  }

  formatMinutes(minutes: number): string {
    const hours = Math.floor(minutes / 60);
    const mins = Math.round(minutes % 60);
    if (hours === 0) return `${mins}m`;
    return `${hours}h ${mins}m`;
  }

  getSessionTypeLabel(type: number): string {
    return SessionTypeLabels[type] || 'Unknown';
  }
}
