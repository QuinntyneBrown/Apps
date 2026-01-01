import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MilestonesService, GoalsService } from '../services';
import { Milestone, Goal } from '../models';
import { Observable, combineLatest } from 'rxjs';
import { map } from 'rxjs/operators';

interface MilestoneWithGoal extends Milestone {
  goalName?: string;
}

@Component({
  selector: 'app-milestones',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    MatSnackBarModule
  ],
  template: `
    <div class="milestones">
      <div class="milestones__header">
        <h1 class="milestones__title">Milestones</h1>
      </div>

      <mat-card class="milestones__filters">
        <mat-card-content>
          <div class="milestones__filters-row">
            <mat-form-field appearance="outline" class="milestones__filter">
              <mat-label>Filter by Status</mat-label>
              <mat-select [(ngModel)]="filterCompleted" (ngModelChange)="applyFilters()">
                <mat-option [value]="null">All</mat-option>
                <mat-option [value]="false">Pending</mat-option>
                <mat-option [value]="true">Completed</mat-option>
              </mat-select>
            </mat-form-field>

            <button mat-raised-button (click)="clearFilters()">
              <mat-icon>clear</mat-icon>
              Clear Filters
            </button>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-card class="milestones__content">
        <div *ngIf="(milestonesWithGoals$ | async)?.length; else noMilestones">
          <table mat-table [dataSource]="(milestonesWithGoals$ | async) || []" class="milestones__table">
            <ng-container matColumnDef="status">
              <th mat-header-cell *matHeaderCellDef>Status</th>
              <td mat-cell *matCellDef="let milestone">
                <mat-icon [color]="milestone.isCompleted ? 'primary' : ''" class="milestones__status-icon">
                  {{ milestone.isCompleted ? 'check_circle' : 'radio_button_unchecked' }}
                </mat-icon>
              </td>
            </ng-container>

            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let milestone">
                <div class="milestones__name">
                  <span class="milestones__name-text">{{ milestone.name }}</span>
                  <span class="milestones__goal-name">{{ milestone.goalName || 'Unknown Goal' }}</span>
                </div>
              </td>
            </ng-container>

            <ng-container matColumnDef="targetAmount">
              <th mat-header-cell *matHeaderCellDef>Target Amount</th>
              <td mat-cell *matCellDef="let milestone" class="milestones__amount">
                \${{ milestone.targetAmount | number:'1.2-2' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="targetDate">
              <th mat-header-cell *matHeaderCellDef>Target Date</th>
              <td mat-cell *matCellDef="let milestone">{{ milestone.targetDate | date:'mediumDate' }}</td>
            </ng-container>

            <ng-container matColumnDef="completedDate">
              <th mat-header-cell *matHeaderCellDef>Completed Date</th>
              <td mat-cell *matCellDef="let milestone">
                {{ milestone.completedDate ? (milestone.completedDate | date:'mediumDate') : '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="notes">
              <th mat-header-cell *matHeaderCellDef>Notes</th>
              <td mat-cell *matCellDef="let milestone">{{ milestone.notes || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let milestone">
                <button mat-icon-button (click)="deleteMilestone(milestone.milestoneId)" color="warn">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"
                [class.milestones__row--completed]="row.isCompleted"></tr>
          </table>

          <div class="milestones__summary">
            <div class="milestones__summary-item">
              <span class="milestones__summary-label">Total Milestones:</span>
              <span class="milestones__summary-value">{{ (milestonesWithGoals$ | async)?.length || 0 }}</span>
            </div>
            <div class="milestones__summary-item">
              <span class="milestones__summary-label">Completed:</span>
              <span class="milestones__summary-value">{{ getCompletedCount(milestonesWithGoals$ | async) }}</span>
            </div>
            <div class="milestones__summary-item">
              <span class="milestones__summary-label">Pending:</span>
              <span class="milestones__summary-value">{{ getPendingCount(milestonesWithGoals$ | async) }}</span>
            </div>
          </div>
        </div>

        <ng-template #noMilestones>
          <div class="milestones__empty">
            <mat-icon>emoji_events</mat-icon>
            <p>No milestones found.</p>
          </div>
        </ng-template>
      </mat-card>
    </div>
  `,
  styles: [`
    .milestones {
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

      &__row--completed {
        background-color: rgba(76, 175, 80, 0.05);
      }

      &__status-icon {
        font-size: 24px;
        width: 24px;
        height: 24px;
      }

      &__name {
        display: flex;
        flex-direction: column;
        gap: 4px;

        &-text {
          font-weight: 500;
        }
      }

      &__goal-name {
        font-size: 12px;
        color: rgba(0, 0, 0, 0.6);
      }

      &__amount {
        font-weight: 600;
        color: #ff9800;
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
          color: #3f51b5;
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
export class Milestones implements OnInit {
  milestones$: Observable<Milestone[]>;
  goals$: Observable<Goal[]>;
  milestonesWithGoals$: Observable<MilestoneWithGoal[]>;

  displayedColumns = ['status', 'name', 'targetAmount', 'targetDate', 'completedDate', 'notes', 'actions'];
  filterCompleted: boolean | null = null;

  constructor(
    private milestonesService: MilestonesService,
    private goalsService: GoalsService,
    private snackBar: MatSnackBar
  ) {
    this.milestones$ = this.milestonesService.milestones$;
    this.goals$ = this.goalsService.goals$;

    this.milestonesWithGoals$ = combineLatest([
      this.milestones$,
      this.goals$
    ]).pipe(
      map(([milestones, goals]) => {
        return milestones.map(milestone => ({
          ...milestone,
          goalName: goals.find(g => g.goalId === milestone.goalId)?.name
        }));
      })
    );
  }

  ngOnInit(): void {
    this.goalsService.getGoals().subscribe();
    this.applyFilters();
  }

  applyFilters(): void {
    this.milestonesService.getMilestones(undefined, this.filterCompleted ?? undefined).subscribe();
  }

  clearFilters(): void {
    this.filterCompleted = null;
    this.applyFilters();
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

  getCompletedCount(milestones: MilestoneWithGoal[] | null): number {
    if (!milestones) return 0;
    return milestones.filter(m => m.isCompleted).length;
  }

  getPendingCount(milestones: MilestoneWithGoal[] | null): number {
    if (!milestones) return 0;
    return milestones.filter(m => !m.isCompleted).length;
  }
}
