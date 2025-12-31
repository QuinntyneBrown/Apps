import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { LoanService } from '../services';
import { LoanType, LoanTypeLabels } from '../models';

@Component({
  selector: 'app-loan-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  template: `
    <div class="loan-form">
      <h1 class="loan-form__title">{{ isEditMode ? 'Edit Loan' : 'New Loan' }}</h1>

      <mat-card class="loan-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="loan-form__form">
            <mat-form-field appearance="outline" class="loan-form__field">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
              @if (form.get('name')?.hasError('required') && form.get('name')?.touched) {
                <mat-error>Name is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="loan-form__field">
              <mat-label>Loan Type</mat-label>
              <mat-select formControlName="loanType" required>
                @for (type of loanTypes; track type.value) {
                  <mat-option [value]="type.value">{{ type.label }}</mat-option>
                }
              </mat-select>
              @if (form.get('loanType')?.hasError('required') && form.get('loanType')?.touched) {
                <mat-error>Loan type is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="loan-form__field">
              <mat-label>Requested Amount</mat-label>
              <input matInput type="number" formControlName="requestedAmount" required>
              @if (form.get('requestedAmount')?.hasError('required') && form.get('requestedAmount')?.touched) {
                <mat-error>Requested amount is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="loan-form__field">
              <mat-label>Purpose</mat-label>
              <input matInput formControlName="purpose" required>
              @if (form.get('purpose')?.hasError('required') && form.get('purpose')?.touched) {
                <mat-error>Purpose is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="loan-form__field">
              <mat-label>Credit Score</mat-label>
              <input matInput type="number" formControlName="creditScore" required>
              @if (form.get('creditScore')?.hasError('required') && form.get('creditScore')?.touched) {
                <mat-error>Credit score is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="loan-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="4"></textarea>
            </mat-form-field>

            <div class="loan-form__actions">
              <button mat-raised-button type="button" (click)="onCancel()" class="loan-form__cancel-button">
                Cancel
              </button>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid" class="loan-form__submit-button">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .loan-form {
      padding: 24px;
      max-width: 800px;
      margin: 0 auto;
    }

    .loan-form__title {
      margin: 0 0 24px 0;
      font-size: 32px;
      font-weight: 400;
    }

    .loan-form__form {
      display: flex;
      flex-direction: column;
      gap: 16px;
    }

    .loan-form__field {
      width: 100%;
    }

    .loan-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 16px;
      margin-top: 16px;
    }
  `]
})
export class LoanForm implements OnInit {
  private fb = inject(FormBuilder);
  private loanService = inject(LoanService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  loanId?: string;

  loanTypes = Object.keys(LoanType)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: LoanTypeLabels[Number(key) as LoanType]
    }));

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      loanType: [LoanType.Personal, Validators.required],
      requestedAmount: [0, Validators.required],
      purpose: ['', Validators.required],
      creditScore: [0, Validators.required],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.loanId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.loanId;

    if (this.isEditMode && this.loanId) {
      this.loanService.getById(this.loanId).subscribe(loan => {
        this.form.patchValue(loan);
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.loanId) {
        this.loanService.update({ ...formValue, loanId: this.loanId }).subscribe(() => {
          this.router.navigate(['/loans']);
        });
      } else {
        this.loanService.create(formValue).subscribe(() => {
          this.router.navigate(['/loans']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/loans']);
  }
}
