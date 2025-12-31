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
import { CashFlowService, PropertyService } from '../services';
import { Property } from '../models';

@Component({
  selector: 'app-cash-flow-form',
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
    MatNativeDateModule
  ],
  template: `
    <div class="cash-flow-form">
      <div class="cash-flow-form__header">
        <h1 class="cash-flow-form__title">{{ isEditMode ? 'Edit Cash Flow' : 'Add Cash Flow' }}</h1>
      </div>

      <mat-card class="cash-flow-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="cash-flow-form__form">
            <mat-form-field appearance="outline" class="cash-flow-form__field">
              <mat-label>Property</mat-label>
              <mat-select formControlName="propertyId" required>
                <mat-option *ngFor="let property of properties" [value]="property.propertyId">
                  {{ property.address }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('propertyId')?.hasError('required')">Property is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="cash-flow-form__field">
              <mat-label>Date</mat-label>
              <input matInput [matDatepicker]="datePicker" formControlName="date" required>
              <mat-datepicker-toggle matIconSuffix [for]="datePicker"></mat-datepicker-toggle>
              <mat-datepicker #datePicker></mat-datepicker>
              <mat-error *ngIf="form.get('date')?.hasError('required')">Date is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="cash-flow-form__field">
              <mat-label>Income</mat-label>
              <input matInput type="number" formControlName="income" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('income')?.hasError('required')">Income is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="cash-flow-form__field">
              <mat-label>Expenses</mat-label>
              <input matInput type="number" formControlName="expenses" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('expenses')?.hasError('required')">Expenses is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="cash-flow-form__field cash-flow-form__field--full">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="cash-flow-form__summary" *ngIf="netCashFlow !== null">
              <strong>Net Cash Flow:</strong>
              <span [class.cash-flow-form__summary--positive]="netCashFlow >= 0"
                    [class.cash-flow-form__summary--negative]="netCashFlow < 0">
                {{ netCashFlow | currency }}
              </span>
            </div>

            <div class="cash-flow-form__actions">
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
    .cash-flow-form {
      padding: 2rem;
    }

    .cash-flow-form__header {
      margin-bottom: 2rem;
    }

    .cash-flow-form__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .cash-flow-form__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 1rem;
    }

    .cash-flow-form__field {
      width: 100%;
    }

    .cash-flow-form__field--full {
      grid-column: 1 / -1;
    }

    .cash-flow-form__summary {
      grid-column: 1 / -1;
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 1rem;
      background-color: #f5f5f5;
      border-radius: 4px;
      font-size: 1.25rem;
    }

    .cash-flow-form__summary--positive {
      color: #4caf50;
      font-weight: 600;
    }

    .cash-flow-form__summary--negative {
      color: #f44336;
      font-weight: 600;
    }

    .cash-flow-form__actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class CashFlowForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly cashFlowService = inject(CashFlowService);
  private readonly propertyService = inject(PropertyService);

  form!: FormGroup;
  isEditMode = false;
  cashFlowId?: string;
  properties: Property[] = [];
  netCashFlow: number | null = null;

  ngOnInit(): void {
    this.form = this.fb.group({
      propertyId: ['', Validators.required],
      date: [new Date(), Validators.required],
      income: [0, Validators.required],
      expenses: [0, Validators.required],
      notes: ['']
    });

    this.form.valueChanges.subscribe(() => {
      this.calculateNetCashFlow();
    });

    this.propertyService.getProperties().subscribe(properties => {
      this.properties = properties;
    });

    this.cashFlowId = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.cashFlowId) {
      this.isEditMode = true;
      this.loadCashFlow(this.cashFlowId);
    }
  }

  loadCashFlow(id: string): void {
    this.cashFlowService.getCashFlow(id).subscribe(cashFlow => {
      this.form.patchValue({
        propertyId: cashFlow.propertyId,
        date: new Date(cashFlow.date),
        income: cashFlow.income,
        expenses: cashFlow.expenses,
        notes: cashFlow.notes
      });
      this.calculateNetCashFlow();
    });
  }

  calculateNetCashFlow(): void {
    const income = this.form.get('income')?.value || 0;
    const expenses = this.form.get('expenses')?.value || 0;
    this.netCashFlow = income - expenses;
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const cashFlowData = {
      ...formValue,
      date: formValue.date.toISOString()
    };

    if (this.isEditMode && this.cashFlowId) {
      this.cashFlowService.updateCashFlow({ cashFlowId: this.cashFlowId, ...cashFlowData }).subscribe(() => {
        this.router.navigate(['/cash-flows']);
      });
    } else {
      this.cashFlowService.createCashFlow(cashFlowData).subscribe(() => {
        this.router.navigate(['/cash-flows']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/cash-flows']);
  }
}
