import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { PaymentScheduleService, OfferService } from '../services';

@Component({
  selector: 'app-payment-schedule-form',
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
    <div class="payment-schedule-form">
      <h1 class="payment-schedule-form__title">{{ isEditMode ? 'Edit Payment Schedule' : 'New Payment Schedule' }}</h1>

      <mat-card class="payment-schedule-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="payment-schedule-form__form">
            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Offer</mat-label>
              <mat-select formControlName="offerId" required>
                @for (offer of offerService.offers$ | async; track offer.offerId) {
                  <mat-option [value]="offer.offerId">{{ offer.lenderName }} - {{ offer.loanAmount | currency }}</mat-option>
                }
              </mat-select>
              @if (form.get('offerId')?.hasError('required') && form.get('offerId')?.touched) {
                <mat-error>Offer is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Payment Number</mat-label>
              <input matInput type="number" formControlName="paymentNumber" required>
              @if (form.get('paymentNumber')?.hasError('required') && form.get('paymentNumber')?.touched) {
                <mat-error>Payment number is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Due Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="dueDate" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
              @if (form.get('dueDate')?.hasError('required') && form.get('dueDate')?.touched) {
                <mat-error>Due date is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Payment Amount</mat-label>
              <input matInput type="number" step="0.01" formControlName="paymentAmount" required>
              @if (form.get('paymentAmount')?.hasError('required') && form.get('paymentAmount')?.touched) {
                <mat-error>Payment amount is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Principal Amount</mat-label>
              <input matInput type="number" step="0.01" formControlName="principalAmount" required>
              @if (form.get('principalAmount')?.hasError('required') && form.get('principalAmount')?.touched) {
                <mat-error>Principal amount is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Interest Amount</mat-label>
              <input matInput type="number" step="0.01" formControlName="interestAmount" required>
              @if (form.get('interestAmount')?.hasError('required') && form.get('interestAmount')?.touched) {
                <mat-error>Interest amount is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field appearance="outline" class="payment-schedule-form__field">
              <mat-label>Remaining Balance</mat-label>
              <input matInput type="number" step="0.01" formControlName="remainingBalance" required>
              @if (form.get('remainingBalance')?.hasError('required') && form.get('remainingBalance')?.touched) {
                <mat-error>Remaining balance is required</mat-error>
              }
            </mat-form-field>

            <div class="payment-schedule-form__actions">
              <button mat-raised-button type="button" (click)="onCancel()" class="payment-schedule-form__cancel-button">
                Cancel
              </button>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid" class="payment-schedule-form__submit-button">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .payment-schedule-form {
      padding: 24px;
      max-width: 800px;
      margin: 0 auto;
    }

    .payment-schedule-form__title {
      margin: 0 0 24px 0;
      font-size: 32px;
      font-weight: 400;
    }

    .payment-schedule-form__form {
      display: flex;
      flex-direction: column;
      gap: 16px;
    }

    .payment-schedule-form__field {
      width: 100%;
    }

    .payment-schedule-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 16px;
      margin-top: 16px;
    }
  `]
})
export class PaymentScheduleForm implements OnInit {
  private fb = inject(FormBuilder);
  private paymentScheduleService = inject(PaymentScheduleService);
  offerService = inject(OfferService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  paymentScheduleId?: string;

  constructor() {
    this.form = this.fb.group({
      offerId: ['', Validators.required],
      paymentNumber: [0, Validators.required],
      dueDate: ['', Validators.required],
      paymentAmount: [0, Validators.required],
      principalAmount: [0, Validators.required],
      interestAmount: [0, Validators.required],
      remainingBalance: [0, Validators.required]
    });
  }

  ngOnInit(): void {
    this.offerService.getAll().subscribe();
    this.paymentScheduleId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.paymentScheduleId;

    if (this.isEditMode && this.paymentScheduleId) {
      this.paymentScheduleService.getById(this.paymentScheduleId).subscribe(schedule => {
        this.form.patchValue({
          ...schedule,
          dueDate: new Date(schedule.dueDate)
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = {
        ...this.form.value,
        dueDate: this.form.value.dueDate instanceof Date
          ? this.form.value.dueDate.toISOString()
          : this.form.value.dueDate
      };

      if (this.isEditMode && this.paymentScheduleId) {
        this.paymentScheduleService.update({ ...formValue, paymentScheduleId: this.paymentScheduleId }).subscribe(() => {
          this.router.navigate(['/payment-schedules']);
        });
      } else {
        this.paymentScheduleService.create(formValue).subscribe(() => {
          this.router.navigate(['/payment-schedules']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/payment-schedules']);
  }
}
