import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { OfferService, LoanService } from '../services';

@Component({
  selector: 'app-offer-form',
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
    <div class="offer-form">
      <h1 class="offer-form__title">{{ isEditMode ? 'Edit Offer' : 'New Offer' }}</h1>

      <mat-card class="offer-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="offer-form__form">
            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Loan</mat-label>
              <mat-select formControlName="loanId" required>
                @for (loan of loanService.loans$ | async; track loan.loanId) {
                  <mat-option [value]="loan.loanId">{{ loan.name }}</mat-option>
                }
              </mat-select>
              @if (form.get('loanId')?.hasError('required') && form.get('loanId')?.touched) {
                <mat-error>Loan is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Lender Name</mat-label>
              <input matInput formControlName="lenderName" required>
              @if (form.get('lenderName')?.hasError('required') && form.get('lenderName')?.touched) {
                <mat-error>Lender name is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Loan Amount</mat-label>
              <input matInput type="number" formControlName="loanAmount" required>
              @if (form.get('loanAmount')?.hasError('required') && form.get('loanAmount')?.touched) {
                <mat-error>Loan amount is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Interest Rate (%)</mat-label>
              <input matInput type="number" step="0.01" formControlName="interestRate" required>
              @if (form.get('interestRate')?.hasError('required') && form.get('interestRate')?.touched) {
                <mat-error>Interest rate is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Term (Months)</mat-label>
              <input matInput type="number" formControlName="termMonths" required>
              @if (form.get('termMonths')?.hasError('required') && form.get('termMonths')?.touched) {
                <mat-error>Term is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Monthly Payment</mat-label>
              <input matInput type="number" step="0.01" formControlName="monthlyPayment" required>
              @if (form.get('monthlyPayment')?.hasError('required') && form.get('monthlyPayment')?.touched) {
                <mat-error>Monthly payment is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Fees</mat-label>
              <input matInput type="number" step="0.01" formControlName="fees" required>
              @if (form.get('fees')?.hasError('required') && form.get('fees')?.touched) {
                <mat-error>Fees is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="offer-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="4"></textarea>
            </mat-form-field>

            <div class="offer-form__actions">
              <button mat-raised-button type="button" (click)="onCancel()" class="offer-form__cancel-button">
                Cancel
              </button>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid" class="offer-form__submit-button">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .offer-form {
      padding: 24px;
      max-width: 800px;
      margin: 0 auto;
    }

    .offer-form__title {
      margin: 0 0 24px 0;
      font-size: 32px;
      font-weight: 400;
    }

    .offer-form__form {
      display: flex;
      flex-direction: column;
      gap: 16px;
    }

    .offer-form__field {
      width: 100%;
    }

    .offer-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 16px;
      margin-top: 16px;
    }
  `]
})
export class OfferForm implements OnInit {
  private fb = inject(FormBuilder);
  private offerService = inject(OfferService);
  loanService = inject(LoanService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  offerId?: string;

  constructor() {
    this.form = this.fb.group({
      loanId: ['', Validators.required],
      lenderName: ['', Validators.required],
      loanAmount: [0, Validators.required],
      interestRate: [0, Validators.required],
      termMonths: [0, Validators.required],
      monthlyPayment: [0, Validators.required],
      fees: [0, Validators.required],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.loanService.getAll().subscribe();
    this.offerId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.offerId;

    if (this.isEditMode && this.offerId) {
      this.offerService.getById(this.offerId).subscribe(offer => {
        this.form.patchValue(offer);
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.offerId) {
        this.offerService.update({ ...formValue, offerId: this.offerId }).subscribe(() => {
          this.router.navigate(['/offers']);
        });
      } else {
        this.offerService.create(formValue).subscribe(() => {
          this.router.navigate(['/offers']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/offers']);
  }
}
