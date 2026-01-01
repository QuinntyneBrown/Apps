import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { GoalsService, ContributionsService, MilestonesService } from '../services';
import { Goal, Contribution, Milestone, CreateContributionCommand, CreateMilestoneCommand } from '../models';
import { Observable, combineLatest } from 'rxjs';

@Component({
  selector: 'app-contribution-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatDialogModule
  ],
  template: `
    <h2 mat-dialog-title>Add Contribution</h2>
    <mat-dialog-content>
      <div class="contribution-form">
        <mat-form-field appearance="outline" class="contribution-form__field">
          <mat-label>Amount</mat-label>
          <input matInput type="number" [(ngModel)]="command.amount" required>
          <span matPrefix>$&nbsp;</span>
        </mat-form-field>

        <mat-form-field appearance="outline" class="contribution-form__field">
          <mat-label>Date</mat-label>
          <input matInput [matDatepicker]="picker" [(ngModel)]="command.contributionDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="contribution-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput [(ngModel)]="command.notes" rows="3"></textarea>
        </mat-form-field>
      </div>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [mat-dialog-close]="command">Add</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .contribution-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class ContributionFormDialog {
  command: CreateContributionCommand = {
    goalId: '',
    amount: 0,
    contributionDate: new Date().toISOString(),
    notes: ''
  };
}

@Component({
  selector: 'app-milestone-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatDialogModule
  ],
  template: `
    <h2 mat-dialog-title>Add Milestone</h2>
    <mat-dialog-content>
      <div class="milestone-form">
        <mat-form-field appearance="outline" class="milestone-form__field">
          <mat-label>Name</mat-label>
          <input matInput [(ngModel)]="command.name" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="milestone-form__field">
          <mat-label>Target Amount</mat-label>
          <input matInput type="number" [(ngModel)]="command.targetAmount" required>
          <span matPrefix>$&nbsp;</span>
        </mat-form-field>

        <mat-form-field appearance="outline" class="milestone-form__field">
          <mat-label>Target Date</mat-label>
          <input matInput [matDatepicker]="picker" [(ngModel)]="command.targetDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="milestone-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput [(ngModel)]="command.notes" rows="2"></textarea>
        </mat-form-field>
      </div>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [mat-dialog-close]="command">Add</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .milestone-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class MilestoneFormDialog {
  command: CreateMilestoneCommand = {
    goalId: '',
    name: '',
    targetAmount: 0,
    targetDate: new Date().toISOString(),
    notes: ''
  };
}

@Component({
  selector: 'app-goal-details',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule,
    MatTabsModule,
    MatTableModule,
    MatSnackBarModule
  ],
  template: `
    <div class="goal-details" *ngIf="goal$ | async as goal">
      <div class="goal-details__header">
        <button mat-icon-button (click)="goBack()">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="goal-details__title">{{ goal.name }}</h1>
      </div>

      <div class="goal-details__overview">
        <mat-card class="goal-details__card">
          <mat-card-content>
            <div class="goal-details__info">
              <div class="goal-details__info-item">
                <span class="goal-details__info-label">Type</span>
                <span class="goal-details__info-value">{{ getGoalTypeLabel(goal.goalType) }}</span>
              </div>
              <div class="goal-details__info-item">
                <span class="goal-details__info-label">Status</span>
                <span class="goal-details__info-value">{{ getStatusLabel(goal.status) }}</span>
              </div>
              <div class="goal-details__info-item">
                <span class="goal-details__info-label">Target Date</span>
                <span class="goal-details__info-value">{{ goal.targetDate | date:'mediumDate' }}</span>
              </div>
            </div>
            <p class="goal-details__description">{{ goal.description }}</p>
            <div class="goal-details__progress">
              <div class="goal-details__progress-header">
                <span class="goal-details__progress-label">Progress</span>
                <span class="goal-details__progress-percentage">{{ goal.progress }}%</span>
              </div>
              <mat-progress-bar mode="determinate" [value]="goal.progress"></mat-progress-bar>
              <div class="goal-details__progress-amounts">
                <span>\${{ goal.currentAmount | number:'1.2-2' }}</span>
                <span>\${{ goal.targetAmount | number:'1.2-2' }}</span>
              </div>
            </div>
            <div *ngIf="goal.notes" class="goal-details__notes">
              <strong>Notes:</strong> {{ goal.notes }}
            </div>
          </mat-card-content>
        </mat-card>
      </div>

      <mat-tab-group class="goal-details__tabs">
        <mat-tab label="Contributions">
          <div class="goal-details__tab-content">
            <div class="goal-details__tab-header">
              <h2>Contributions</h2>
              <button mat-raised-button color="primary" (click)="openContributionDialog()">
                <mat-icon>add</mat-icon>
                Add Contribution
              </button>
            </div>
            <div *ngIf="(contributions$ | async)?.length; else noContributions">
              <table mat-table [dataSource]="(contributions$ | async) || []" class="goal-details__table">
                <ng-container matColumnDef="date">
                  <th mat-header-cell *matHeaderCellDef>Date</th>
                  <td mat-cell *matCellDef="let contribution">{{ contribution.contributionDate | date:'mediumDate' }}</td>
                </ng-container>

                <ng-container matColumnDef="amount">
                  <th mat-header-cell *matHeaderCellDef>Amount</th>
                  <td mat-cell *matCellDef="let contribution">\${{ contribution.amount | number:'1.2-2' }}</td>
                </ng-container>

                <ng-container matColumnDef="notes">
                  <th mat-header-cell *matHeaderCellDef>Notes</th>
                  <td mat-cell *matCellDef="let contribution">{{ contribution.notes || '-' }}</td>
                </ng-container>

                <ng-container matColumnDef="actions">
                  <th mat-header-cell *matHeaderCellDef>Actions</th>
                  <td mat-cell *matCellDef="let contribution">
                    <button mat-icon-button (click)="deleteContribution(contribution.contributionId)" color="warn">
                      <mat-icon>delete</mat-icon>
                    </button>
                  </td>
                </ng-container>

                <tr mat-header-row *matHeaderRowDef="contributionColumns"></tr>
                <tr mat-row *matRowDef="let row; columns: contributionColumns;"></tr>
              </table>
            </div>
            <ng-template #noContributions>
              <div class="goal-details__empty">
                <mat-icon>payments</mat-icon>
                <p>No contributions yet. Add your first contribution!</p>
              </div>
            </ng-template>
          </div>
        </mat-tab>

        <mat-tab label="Milestones">
          <div class="goal-details__tab-content">
            <div class="goal-details__tab-header">
              <h2>Milestones</h2>
              <button mat-raised-button color="primary" (click)="openMilestoneDialog()">
                <mat-icon>add</mat-icon>
                Add Milestone
              </button>
            </div>
            <div *ngIf="(milestones$ | async)?.length; else noMilestones">
              <table mat-table [dataSource]="(milestones$ | async) || []" class="goal-details__table">
                <ng-container matColumnDef="name">
                  <th mat-header-cell *matHeaderCellDef>Name</th>
                  <td mat-cell *matCellDef="let milestone">{{ milestone.name }}</td>
                </ng-container>

                <ng-container matColumnDef="targetAmount">
                  <th mat-header-cell *matHeaderCellDef>Target Amount</th>
                  <td mat-cell *matCellDef="let milestone">\${{ milestone.targetAmount | number:'1.2-2' }}</td>
                </ng-container>

                <ng-container matColumnDef="targetDate">
                  <th mat-header-cell *matHeaderCellDef>Target Date</th>
                  <td mat-cell *matCellDef="let milestone">{{ milestone.targetDate | date:'mediumDate' }}</td>
                </ng-container>

                <ng-container matColumnDef="status">
                  <th mat-header-cell *matHeaderCellDef>Status</th>
                  <td mat-cell *matCellDef="let milestone">
                    <mat-icon [color]="milestone.isCompleted ? 'primary' : ''">
                      {{ milestone.isCompleted ? 'check_circle' : 'radio_button_unchecked' }}
                    </mat-icon>
                  </td>
                </ng-container>

                <ng-container matColumnDef="actions">
                  <th mat-header-cell *matHeaderCellDef>Actions</th>
                  <td mat-cell *matCellDef="let milestone">
                    <button mat-icon-button (click)="deleteMilestone(milestone.milestoneId)" color="warn">
                      <mat-icon>delete</mat-icon>
                    </button>
                  </td>
                </ng-container>

                <tr mat-header-row *matHeaderRowDef="milestoneColumns"></tr>
                <tr mat-row *matRowDef="let row; columns: milestoneColumns;"></tr>
              </table>
            </div>
            <ng-template #noMilestones>
              <div class="goal-details__empty">
                <mat-icon>emoji_events</mat-icon>
                <p>No milestones yet. Add your first milestone!</p>
              </div>
            </ng-template>
          </div>
        </mat-tab>
      </mat-tab-group>
    </div>
  `,
  styles: [`
    .goal-details {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        display: flex;
        align-items: center;
        gap: 16px;
        margin-bottom: 24px;
      }

      &__title {
        font-size: 32px;
        font-weight: 500;
        margin: 0;
      }

      &__overview {
        margin-bottom: 32px;
      }

      &__card {
        mat-card-content {
          padding: 24px;
        }
      }

      &__info {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 24px;
        margin-bottom: 24px;

        &-item {
          display: flex;
          flex-direction: column;
          gap: 8px;
        }

        &-label {
          font-size: 12px;
          color: rgba(0, 0, 0, 0.6);
          text-transform: uppercase;
          font-weight: 500;
        }

        &-value {
          font-size: 18px;
          font-weight: 500;
        }
      }

      &__description {
        font-size: 16px;
        color: rgba(0, 0, 0, 0.7);
        margin-bottom: 24px;
      }

      &__progress {
        background: #f5f5f5;
        padding: 16px;
        border-radius: 8px;
        margin-bottom: 16px;

        &-header {
          display: flex;
          justify-content: space-between;
          margin-bottom: 8px;
        }

        &-label {
          font-weight: 500;
        }

        &-percentage {
          font-weight: 600;
          color: #3f51b5;
        }

        &-amounts {
          display: flex;
          justify-content: space-between;
          font-weight: 500;
          margin-top: 8px;
        }
      }

      &__notes {
        font-size: 14px;
        color: rgba(0, 0, 0, 0.7);
        padding: 12px;
        background: #f5f5f5;
        border-radius: 4px;
      }

      &__tabs {
        margin-top: 32px;
      }

      &__tab-content {
        padding: 24px;
      }

      &__tab-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;

        h2 {
          margin: 0;
        }
      }

      &__table {
        width: 100%;
        background: white;
      }

      &__empty {
        text-align: center;
        padding: 48px 24px;

        mat-icon {
          font-size: 48px;
          width: 48px;
          height: 48px;
          color: rgba(0, 0, 0, 0.3);
        }

        p {
          font-size: 16px;
          color: rgba(0, 0, 0, 0.6);
          margin: 16px 0;
        }
      }
    }
  `]
})
export class GoalDetails implements OnInit {
  goal$: Observable<Goal | null>;
  contributions$: Observable<Contribution[]>;
  milestones$: Observable<Milestone[]>;
  goalId: string = '';

  contributionColumns = ['date', 'amount', 'notes', 'actions'];
  milestoneColumns = ['name', 'targetAmount', 'targetDate', 'status', 'actions'];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private goalsService: GoalsService,
    private contributionsService: ContributionsService,
    private milestonesService: MilestonesService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.goal$ = this.goalsService.selectedGoal$;
    this.contributions$ = this.contributionsService.contributions$;
    this.milestones$ = this.milestonesService.milestones$;
  }

  ngOnInit(): void {
    this.goalId = this.route.snapshot.paramMap.get('id') || '';
    if (this.goalId) {
      combineLatest([
        this.goalsService.getGoalById(this.goalId),
        this.contributionsService.getContributions(this.goalId),
        this.milestonesService.getMilestones(this.goalId)
      ]).subscribe();
    }
  }

  openContributionDialog(): void {
    const dialogRef = this.dialog.open(ContributionFormDialog);
    const instance = dialogRef.componentInstance;
    instance.command.goalId = this.goalId;

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createContribution(result);
      }
    });
  }

  openMilestoneDialog(): void {
    const dialogRef = this.dialog.open(MilestoneFormDialog);
    const instance = dialogRef.componentInstance;
    instance.command.goalId = this.goalId;

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createMilestone(result);
      }
    });
  }

  createContribution(command: CreateContributionCommand): void {
    const formattedCommand = {
      ...command,
      contributionDate: typeof command.contributionDate === 'string'
        ? command.contributionDate
        : new Date(command.contributionDate).toISOString()
    };

    this.contributionsService.createContribution(formattedCommand).subscribe({
      next: () => {
        this.snackBar.open('Contribution added successfully!', 'Close', { duration: 3000 });
        this.goalsService.getGoalById(this.goalId).subscribe();
      },
      error: () => {
        this.snackBar.open('Failed to add contribution. Please try again.', 'Close', { duration: 3000 });
      }
    });
  }

  createMilestone(command: CreateMilestoneCommand): void {
    const formattedCommand = {
      ...command,
      targetDate: typeof command.targetDate === 'string'
        ? command.targetDate
        : new Date(command.targetDate).toISOString()
    };

    this.milestonesService.createMilestone(formattedCommand).subscribe({
      next: () => {
        this.snackBar.open('Milestone added successfully!', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Failed to add milestone. Please try again.', 'Close', { duration: 3000 });
      }
    });
  }

  deleteContribution(contributionId: string): void {
    if (confirm('Are you sure you want to delete this contribution?')) {
      this.contributionsService.deleteContribution(contributionId).subscribe({
        next: () => {
          this.snackBar.open('Contribution deleted successfully!', 'Close', { duration: 3000 });
          this.goalsService.getGoalById(this.goalId).subscribe();
        },
        error: () => {
          this.snackBar.open('Failed to delete contribution. Please try again.', 'Close', { duration: 3000 });
        }
      });
    }
  }

  deleteMilestone(milestoneId: string): void {
    if (confirm('Are you sure you want to delete this milestone?')) {
      this.milestonesService.deleteMilestone(milestoneId).subscribe({
        next: () => {
          this.snackBar.open('Milestone deleted successfully!', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete milestone. Please try again.', 'Close', { duration: 3000 });
        }
      });
    }
  }

  getGoalTypeLabel(type: number): string {
    const labels: { [key: number]: string } = {
      0: 'Savings',
      1: 'Debt Payoff',
      2: 'Investment',
      3: 'Purchase',
      4: 'Emergency',
      5: 'Retirement'
    };
    return labels[type] || 'Unknown';
  }

  getStatusLabel(status: number): string {
    const labels: { [key: number]: string } = {
      0: 'Not Started',
      1: 'In Progress',
      2: 'Completed',
      3: 'Paused',
      4: 'Cancelled'
    };
    return labels[status] || 'Unknown';
  }

  goBack(): void {
    this.router.navigate(['/goals']);
  }
}
