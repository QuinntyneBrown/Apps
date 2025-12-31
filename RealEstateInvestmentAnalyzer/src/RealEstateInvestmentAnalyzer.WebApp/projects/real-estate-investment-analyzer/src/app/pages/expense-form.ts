import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ExpenseService, PropertyService } from '../services';
import { Property } from '../models';

@Component({
  selector: 'app-expense-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule
  ],
  template: `
    <div class="expense-form">
      <div class="expense-form__header">
        <h1 class="expense-form__title">{{ isEditMode ? 'Edit Expense' : 'Add Expense' }}</h1>
      </div>

      <mat-card class="expense-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="expense-form__form">
            <mat-form-field appearance="outline" class="expense-form__field">
              <mat-label>Property</mat-label>
              <mat-select formControlName="propertyId" required>
                <mat-option *ngFor="let property of properties" [value]="property.propertyId">
                  {{ property.address }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('propertyId')?.hasError('required')">Property is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="expense-form__field">
              <mat-label>Description</mat-label>
              <input matInput formControlName="description" required>
              <mat-error *ngIf="form.get('description')?.hasError('required')">Description is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="expense-form__field">
              <mat-label>Category</mat-label>
              <input matInput formControlName="category" required>
              <mat-error *ngIf="form.get('category')?.hasError('required')">Category is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="expense-form__field">
              <mat-label>Amount</mat-label>
              <input matInput type="number" formControlName="amount" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('amount')?.hasError('required')">Amount is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="expense-form__field">
              <mat-label>Date</mat-label>
              <input matInput [matDatepicker]="datePicker" formControlName="date" required>
              <mat-datepicker-toggle matIconSuffix [for]="datePicker"></mat-datepicker-toggle>
              <mat-datepicker #datePicker></mat-datepicker>
              <mat-error *ngIf="form.get('date')?.hasError('required')">Date is required</mat-error>
            </mat-form-field>

            <div class="expense-form__field">
              <mat-checkbox formControlName="isRecurring">Recurring Expense</mat-checkbox>
            </div>

            <mat-form-field appearance="outline" class="expense-form__field expense-form__field--full">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="expense-form__actions">
              <button mat-raised-button type="button" (click)="onCancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .expense-form {
      padding: 2rem;
    }

    .expense-form__header {
      margin-bottom: 2rem;
    }

    .expense-form__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .expense-form__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 1rem;
    }

    .expense-form__field {
      width: 100%;
    }

    .expense-form__field--full {
      grid-column: 1 / -1;
    }

    .expense-form__actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class ExpenseForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly expenseService = inject(ExpenseService);
  private readonly propertyService = inject(PropertyService);

  form!: FormGroup;
  isEditMode = false;
  expenseId?: string;
  properties: Property[] = [];

  ngOnInit(): void {
    this.form = this.fb.group({
      propertyId: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      amount: [0, Validators.required],
      date: [new Date(), Validators.required],
      isRecurring: [false],
      notes: ['']
    });

    this.propertyService.getProperties().subscribe(properties => {
      this.properties = properties;
    });

    this.expenseId = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.expenseId) {
      this.isEditMode = true;
      this.loadExpense(this.expenseId);
    }
  }

  loadExpense(id: string): void {
    this.expenseService.getExpense(id).subscribe(expense => {
      this.form.patchValue({
        propertyId: expense.propertyId,
        description: expense.description,
        category: expense.category,
        amount: expense.amount,
        date: new Date(expense.date),
        isRecurring: expense.isRecurring,
        notes: expense.notes
      });
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const expenseData = {
      ...formValue,
      date: formValue.date.toISOString()
    };

    if (this.isEditMode && this.expenseId) {
      this.expenseService.updateExpense({ expenseId: this.expenseId, ...expenseData }).subscribe(() => {
        this.router.navigate(['/expenses']);
      });
    } else {
      this.expenseService.createExpense(expenseData).subscribe(() => {
        this.router.navigate(['/expenses']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/expenses']);
  }
}
