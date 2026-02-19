import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ApplicationService, CompanyService, InterviewService } from '../services';
import { ApplicationStatus, ApplicationStatusLabels } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Job Search Dashboard</h1>

      <div class="dashboard__stats">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">business</mat-icon>
              Companies
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ (companyService.companies$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Total companies tracked</div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateTo('/companies')">
              View Companies
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">work</mat-icon>
              Applications
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ (applicationService.applications$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Total applications</div>
            <div class="dashboard__card-breakdown">
              <div *ngFor="let status of applicationStatuses" class="dashboard__card-stat">
                <span class="dashboard__card-stat-label">{{ getStatusLabel(status) }}:</span>
                <span class="dashboard__card-stat-value">{{ getApplicationCountByStatus(status) }}</span>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateTo('/applications')">
              View Applications
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">calendar_today</mat-icon>
              Interviews
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ (interviewService.interviews$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Total interviews</div>
            <div class="dashboard__card-breakdown">
              <div class="dashboard__card-stat">
                <span class="dashboard__card-stat-label">Upcoming:</span>
                <span class="dashboard__card-stat-value">{{ getUpcomingInterviewsCount() }}</span>
              </div>
              <div class="dashboard__card-stat">
                <span class="dashboard__card-stat-label">Completed:</span>
                <span class="dashboard__card-stat-value">{{ getCompletedInterviewsCount() }}</span>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateTo('/interviews')">
              View Interviews
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
      font-weight: 500;
    }

    .dashboard__stats {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 24px;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 8px;
      font-size: 18px;
    }

    .dashboard__card-icon {
      color: #1976d2;
    }

    .dashboard__card-value {
      font-size: 48px;
      font-weight: 700;
      color: #1976d2;
      margin: 16px 0 8px 0;
    }

    .dashboard__card-label {
      font-size: 14px;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 16px;
    }

    .dashboard__card-breakdown {
      display: flex;
      flex-direction: column;
      gap: 4px;
      margin-top: 12px;
    }

    .dashboard__card-stat {
      display: flex;
      justify-content: space-between;
      font-size: 13px;
    }

    .dashboard__card-stat-label {
      color: rgba(0, 0, 0, 0.6);
    }

    .dashboard__card-stat-value {
      font-weight: 500;
      color: rgba(0, 0, 0, 0.87);
    }
  `]
})
export class Dashboard implements OnInit {
  applicationService = inject(ApplicationService);
  companyService = inject(CompanyService);
  interviewService = inject(InterviewService);
  private router = inject(Router);

  applicationStatuses = [
    ApplicationStatus.Applied,
    ApplicationStatus.UnderReview,
    ApplicationStatus.PhoneScreen,
    ApplicationStatus.Interviewing,
    ApplicationStatus.OfferReceived
  ];

  ngOnInit(): void {
    this.applicationService.getApplications().subscribe();
    this.companyService.getCompanies().subscribe();
    this.interviewService.getInterviews().subscribe();
  }

  getStatusLabel(status: ApplicationStatus): string {
    return ApplicationStatusLabels[status];
  }

  getApplicationCountByStatus(status: ApplicationStatus): number {
    const applications = this.applicationService['applicationsSubject'].value;
    return applications.filter(app => app.status === status).length;
  }

  getUpcomingInterviewsCount(): number {
    const interviews = this.interviewService['interviewsSubject'].value;
    return interviews.filter(i => !i.isCompleted).length;
  }

  getCompletedInterviewsCount(): number {
    const interviews = this.interviewService['interviewsSubject'].value;
    return interviews.filter(i => i.isCompleted).length;
  }

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
}
