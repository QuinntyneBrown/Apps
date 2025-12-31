import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ContributionsService, GoalsService } from '../services';
import { Contribution, Goal } from '../models';
import { Observable, combineLatest } from 'rxjs';
import { map } from 'rxjs/operators';

interface ContributionWithGoal extends Contribution {
  goalName?: string;
}

@Component({
  selector: 'app-contributions',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule
  ],
  template: `
    <div class="contributions">
      <div class="contributions__header">
        <h1 class="contributions__title">Contributions</h1>
      </div>

      <mat-card class="contributions__filters">
        <mat-card-content>
          <div class="contributions__filters-row">
            <mat-form-field appearance="outline" class="contributions__filter">
              <mat-label>From Date</mat-label>
              <input matInput [matDatepicker]="fromPicker" [(ngModel)]="fromDate" (ngModelChange)="applyFilters()">
              <mat-datepicker-toggle matSuffix [for]="fromPicker"></mat-datepicker-toggle>
              <mat-datepicker #fromPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field appearance="outline" class="contributions__filter">
              <mat-label>To Date</mat-label>
              <input matInput [matDatepicker]="toPicker" [(ngModel)]="toDate" (ngModelChange)="applyFilters()">
              <mat-datepicker-toggle matSuffix [for]="toPicker"></mat-datepicker-toggle>
              <mat-datepicker #toPicker></mat-datepicker>
            </mat-form-field>

            <button mat-raised-button (click)="clearFilters()">
              <mat-icon>clear</mat-icon>
              Clear Filters
            </button>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-card class="contributions__content">
        <div *ngIf="(contributionsWithGoals$ | async)?.length; else noContributions">
          <table mat-table [dataSource]="contributionsWithGoals$ | async" class="contributions__table">
            <ng-container matColumnDef="date">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let contribution">{{ contribution.contributionDate | date:'mediumDate' }}</td>
            </ng-container>

            <ng-container matColumnDef="goal">
              <th mat-header-cell *matHeaderCellDef>Goal</th>
              <td mat-cell *matCellDef="let contribution">{{ contribution.goalName || 'Unknown Goal' }}</td>
            </ng-container>

            <ng-container matColumnDef="amount">
              <th mat-header-cell *matHeaderCellDef>Amount</th>
              <td mat-cell *matCellDef="let contribution" class="contributions__amount">
                \${{ contribution.amount | number:'1.2-2' }}
              </td>
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

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div class="contributions__summary">
            <div class="contributions__summary-item">
              <span class="contributions__summary-label">Total Contributions:</span>
              <span class="contributions__summary-value">\${{ getTotalAmount(contributionsWithGoals$ | async) | number:'1.2-2' }}</span>
            </div>
            <div class="contributions__summary-item">
              <span class="contributions__summary-label">Count:</span>
              <span class="contributions__summary-value">{{ (contributionsWithGoals$ | async)?.length || 0 }}</span>
            </div>
          </div>
        </div>

        <ng-template #noContributions>
          <div class="contributions__empty">
            <mat-icon>payments</mat-icon>
            <p>No contributions found.</p>
          </div>
        </ng-template>
      </mat-card>
    </div>
  `,
  styles: [`
    .contributions {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        margin-bottom: 24px;
      }

      &__title {
        font-size: 32px;
        font-weight: 500;
        margin: 0;
      }

      &__filters {
        margin-bottom: 24px;

        &-row {
          display: flex;
          gap: 16px;
          align-items: center;
        }
      }

      &__filter {
        min-width: 200px;
      }

      &__content {
        mat-card-content {
          padding: 0;
        }
      }

      &__table {
        width: 100%;
        background: white;

        th {
          font-weight: 600;
          background: #f5f5f5;
        }
      }

      &__amount {
        font-weight: 600;
        color: #4caf50;
      }

      &__summary {
        display: flex;
        justify-content: flex-end;
        gap: 48px;
        padding: 24px;
        background: #f5f5f5;
        border-top: 1px solid rgba(0, 0, 0, 0.12);

        &-item {
          display: flex;
          flex-direction: column;
          gap: 4px;
        }

        &-label {
          font-size: 12px;
          color: rgba(0, 0, 0, 0.6);
          text-transform: uppercase;
          font-weight: 500;
        }

        &-value {
          font-size: 24px;
          font-weight: 600;
          color: #4caf50;
        }
      }

      &__empty {
        text-align: center;
        padding: 64px 24px;

        mat-icon {
          font-size: 64px;
          width: 64px;
          height: 64px;
          color: rgba(0, 0, 0, 0.3);
        }

        p {
          font-size: 18px;
          color: rgba(0, 0, 0, 0.6);
          margin: 16px 0;
        }
      }
    }
  `]
})
export class Contributions implements OnInit {
  contributions$: Observable<Contribution[]>;
  goals$: Observable<Goal[]>;
  contributionsWithGoals$: Observable<ContributionWithGoal[]>;

  displayedColumns = ['date', 'goal', 'amount', 'notes', 'actions'];
  fromDate: Date | null = null;
  toDate: Date | null = null;

  constructor(
    private contributionsService: ContributionsService,
    private goalsService: GoalsService,
    private snackBar: MatSnackBar
  ) {
    this.contributions$ = this.contributionsService.contributions$;
    this.goals$ = this.goalsService.goals$;

    this.contributionsWithGoals$ = combineLatest([
      this.contributions$,
      this.goals$
    ]).pipe(
      map(([contributions, goals]) => {
        return contributions.map(contribution => ({
          ...contribution,
          goalName: goals.find(g => g.goalId === contribution.goalId)?.name
        }));
      })
    );
  }

  ngOnInit(): void {
    this.goalsService.getGoals().subscribe();
    this.applyFilters();
  }

  applyFilters(): void {
    const fromDateStr = this.fromDate ? new Date(this.fromDate).toISOString() : undefined;
    const toDateStr = this.toDate ? new Date(this.toDate).toISOString() : undefined;

    this.contributionsService.getContributions(undefined, fromDateStr, toDateStr).subscribe();
  }

  clearFilters(): void {
    this.fromDate = null;
    this.toDate = null;
    this.applyFilters();
  }

  deleteContribution(contributionId: string): void {
    if (confirm('Are you sure you want to delete this contribution?')) {
      this.contributionsService.deleteContribution(contributionId).subscribe({
        next: () => {
          this.snackBar.open('Contribution deleted successfully!', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete contribution. Please try again.', 'Close', { duration: 3000 });
        }
      });
    }
  }

  getTotalAmount(contributions: ContributionWithGoal[] | null): number {
    if (!contributions) return 0;
    return contributions.reduce((sum, c) => sum + c.amount, 0);
  }
}
