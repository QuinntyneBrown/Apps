import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { VacationBudgetService, TripService } from '../services';
import { VacationBudget } from '../models';

@Component({
  selector: 'app-budgets',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatProgressBarModule,
    MatSnackBarModule
  ],
  template: `
    <div class="budgets">
      <div class="budgets__header">
        <h1 class="budgets__title">Vacation Budgets</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="budgets__add-btn">
          <mat-icon>add</mat-icon>
          Add Budget
        </button>
      </div>

      <mat-card *ngIf="showForm" class="budgets__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingBudget ? 'Edit Budget' : 'New Budget' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="budgetForm" (ngSubmit)="saveBudget()" class="budgets__form">
            <mat-form-field appearance="outline" class="budgets__form-field">
              <mat-label>Trip</mat-label>
              <mat-select formControlName="tripId" required>
                <mat-option *ngFor="let trip of trips$ | async" [value]="trip.tripId">
                  {{ trip.name }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="budgets__form-field">
              <mat-label>Category</mat-label>
              <mat-select formControlName="category" required>
                <mat-option value="Accommodation">Accommodation</mat-option>
                <mat-option value="Transportation">Transportation</mat-option>
                <mat-option value="Food">Food</mat-option>
                <mat-option value="Activities">Activities</mat-option>
                <mat-option value="Shopping">Shopping</mat-option>
                <mat-option value="Other">Other</mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="budgets__form-field">
              <mat-label>Allocated Amount</mat-label>
              <input matInput type="number" formControlName="allocatedAmount" step="0.01" required>
              <span matPrefix>$&nbsp;</span>
            </mat-form-field>

            <mat-form-field appearance="outline" class="budgets__form-field">
              <mat-label>Spent Amount</mat-label>
              <input matInput type="number" formControlName="spentAmount" step="0.01">
              <span matPrefix>$&nbsp;</span>
            </mat-form-field>

            <div class="budgets__form-actions">
              <button mat-button type="button" (click)="cancelEdit()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!budgetForm.valid">
                {{ editingBudget ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <div class="budgets__list">
        <mat-card *ngFor="let budget of budgets$ | async" class="budgets__card">
          <mat-card-header>
            <mat-card-title>{{ budget.category }}</mat-card-title>
            <mat-card-subtitle>
              ${{ budget.spentAmount || 0 | number:'1.2-2' }} / ${{ budget.allocatedAmount | number:'1.2-2' }}
            </mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <div class="budgets__progress">
              <mat-progress-bar
                mode="determinate"
                [value]="getProgressPercentage(budget)"
                [color]="getProgressColor(budget)">
              </mat-progress-bar>
              <span class="budgets__progress-text">
                {{ getProgressPercentage(budget) | number:'1.0-0' }}%
              </span>
            </div>
            <p class="budgets__remaining">
              <mat-icon [class.budgets__remaining-icon--over]="isOverBudget(budget)">
                {{ isOverBudget(budget) ? 'warning' : 'account_balance_wallet' }}
              </mat-icon>
              {{ isOverBudget(budget) ? 'Over budget by' : 'Remaining' }}:
              ${{ getRemainingAmount(budget) | number:'1.2-2' }}
            </p>
            <p class="budgets__created">
              <mat-icon>schedule</mat-icon>
              Created: {{ budget.createdAt | date:'short' }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="editBudget(budget)">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteBudget(budget.vacationBudgetId)">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .budgets {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__form-card {
        margin-bottom: 2rem;
      }

      &__form {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 1rem;
        margin-top: 1rem;
      }

      &__form-field {
        width: 100%;
      }

      &__form-actions {
        grid-column: 1 / -1;
        display: flex;
        gap: 1rem;
        justify-content: flex-end;
      }

      &__list {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        mat-card-actions {
          display: flex;
          gap: 0.5rem;
        }
      }

      &__progress {
        margin: 1rem 0;
        position: relative;

        mat-progress-bar {
          height: 24px;
          border-radius: 4px;
        }
      }

      &__progress-text {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-weight: 500;
        color: #fff;
        text-shadow: 0 0 2px rgba(0, 0, 0, 0.5);
      }

      &__remaining {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 1rem 0 0.5rem;
        font-weight: 500;
      }

      &__remaining-icon {
        &--over {
          color: #f44336;
        }
      }

      &__created {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 0.5rem 0;
        font-size: 0.875rem;
        color: rgba(0, 0, 0, 0.6);
      }
    }
  `]
})
export class Budgets implements OnInit {
  budgets$ = this.budgetService.budgets$;
  trips$ = this.tripService.trips$;
  budgetForm: FormGroup;
  showForm = false;
  editingBudget: VacationBudget | null = null;

  constructor(
    private budgetService: VacationBudgetService,
    private tripService: TripService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.budgetForm = this.fb.group({
      tripId: ['', Validators.required],
      category: ['', Validators.required],
      allocatedAmount: ['', [Validators.required, Validators.min(0)]],
      spentAmount: ['', Validators.min(0)]
    });
  }

  ngOnInit() {
    this.loadBudgets();
    this.loadTrips();
  }

  loadBudgets() {
    this.budgetService.getBudgets().subscribe();
  }

  loadTrips() {
    const userId = '00000000-0000-0000-0000-000000000001';
    this.tripService.getTrips(userId).subscribe();
  }

  getProgressPercentage(budget: VacationBudget): number {
    const spent = budget.spentAmount || 0;
    return Math.min((spent / budget.allocatedAmount) * 100, 100);
  }

  getProgressColor(budget: VacationBudget): 'primary' | 'accent' | 'warn' {
    const percentage = this.getProgressPercentage(budget);
    if (percentage >= 90) return 'warn';
    if (percentage >= 70) return 'accent';
    return 'primary';
  }

  getRemainingAmount(budget: VacationBudget): number {
    return Math.abs(budget.allocatedAmount - (budget.spentAmount || 0));
  }

  isOverBudget(budget: VacationBudget): boolean {
    return (budget.spentAmount || 0) > budget.allocatedAmount;
  }

  saveBudget() {
    if (this.budgetForm.invalid) return;

    const command = this.budgetForm.value;

    if (this.editingBudget) {
      this.budgetService.updateBudget(this.editingBudget.vacationBudgetId, {
        vacationBudgetId: this.editingBudget.vacationBudgetId,
        ...command
      }).subscribe({
        next: () => {
          this.snackBar.open('Budget updated successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to update budget', 'Close', { duration: 3000 });
        }
      });
    } else {
      this.budgetService.createBudget(command).subscribe({
        next: () => {
          this.snackBar.open('Budget created successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to create budget', 'Close', { duration: 3000 });
        }
      });
    }
  }

  editBudget(budget: VacationBudget) {
    this.editingBudget = budget;
    this.showForm = true;
    this.budgetForm.patchValue(budget);
  }

  deleteBudget(vacationBudgetId: string) {
    if (confirm('Are you sure you want to delete this budget?')) {
      this.budgetService.deleteBudget(vacationBudgetId).subscribe({
        next: () => {
          this.snackBar.open('Budget deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete budget', 'Close', { duration: 3000 });
        }
      });
    }
  }

  cancelEdit() {
    this.showForm = false;
    this.editingBudget = null;
    this.budgetForm.reset();
  }
}
