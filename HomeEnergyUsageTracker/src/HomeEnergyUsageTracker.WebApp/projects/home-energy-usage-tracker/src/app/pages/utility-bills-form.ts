import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { UtilityBillService } from '../services';
import { UtilityType } from '../models';

@Component({
  selector: 'app-utility-bills-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="utility-bills-form">
      <mat-card class="utility-bills-form__card">
        <mat-card-header>
          <mat-card-title class="utility-bills-form__title">
            {{ isEditMode ? 'Edit' : 'Create' }} Utility Bill
          </mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="utility-bills-form__form">
            <mat-form-field class="utility-bills-form__field">
              <mat-label>Utility Type</mat-label>
              <mat-select formControlName="utilityType">
                <mat-option *ngFor="let type of utilityTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="utility-bills-form__field">
              <mat-label>Billing Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="billingDate">
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="utility-bills-form__field">
              <mat-label>Amount</mat-label>
              <input matInput type="number" formControlName="amount" placeholder="0.00">
              <span matPrefix>$&nbsp;</span>
            </mat-form-field>

            <mat-form-field class="utility-bills-form__field">
              <mat-label>Usage Amount (Optional)</mat-label>
              <input matInput type="number" formControlName="usageAmount" placeholder="0.00">
            </mat-form-field>

            <mat-form-field class="utility-bills-form__field">
              <mat-label>Unit (Optional)</mat-label>
              <input matInput formControlName="unit" placeholder="kWh, gallons, etc.">
            </mat-form-field>

            <div class="utility-bills-form__actions">
              <button mat-raised-button type="button" (click)="cancel()">
                Cancel
              </button>
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
    .utility-bills-form {
      padding: 24px;

      &__card {
        max-width: 600px;
        margin: 0 auto;
      }

      &__title {
        font-size: 24px;
        margin-bottom: 16px;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 16px;
      }

      &__field {
        width: 100%;
      }

      &__actions {
        display: flex;
        gap: 16px;
        justify-content: flex-end;
        margin-top: 16px;
      }
    }
  `]
})
export class UtilityBillsForm implements OnInit {
  form: FormGroup;
  isEditMode = false;
  billId: string | null = null;
  utilityTypes = [
    { value: UtilityType.Electricity, label: 'Electricity' },
    { value: UtilityType.Gas, label: 'Gas' },
    { value: UtilityType.Water, label: 'Water' },
    { value: UtilityType.Internet, label: 'Internet' },
    { value: UtilityType.Other, label: 'Other' }
  ];

  constructor(
    private fb: FormBuilder,
    private utilityBillService: UtilityBillService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      utilityType: [UtilityType.Electricity, Validators.required],
      billingDate: [new Date(), Validators.required],
      amount: [0, [Validators.required, Validators.min(0)]],
      usageAmount: [null],
      unit: ['']
    });
  }

  ngOnInit(): void {
    this.billId = this.route.snapshot.paramMap.get('id');
    if (this.billId) {
      this.isEditMode = true;
      this.loadBill();
    }
  }

  loadBill(): void {
    if (this.billId) {
      this.utilityBillService.getById(this.billId).subscribe(bill => {
        this.form.patchValue({
          utilityType: bill.utilityType,
          billingDate: new Date(bill.billingDate),
          amount: bill.amount,
          usageAmount: bill.usageAmount,
          unit: bill.unit
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const request = {
        ...formValue,
        userId: '00000000-0000-0000-0000-000000000000' // Default user ID
      };

      if (this.isEditMode && this.billId) {
        this.utilityBillService.update(this.billId, {
          utilityBillId: this.billId,
          ...request
        }).subscribe(() => {
          this.router.navigate(['/utility-bills']);
        });
      } else {
        this.utilityBillService.create(request).subscribe(() => {
          this.router.navigate(['/utility-bills']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/utility-bills']);
  }
}
