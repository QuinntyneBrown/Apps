import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { UsageService, UtilityBillService } from '../services';

@Component({
  selector: 'app-usages-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule
  ],
  template: `
    <div class="usages-form">
      <mat-card class="usages-form__card">
        <mat-card-header>
          <mat-card-title class="usages-form__title">
            {{ isEditMode ? 'Edit' : 'Create' }} Usage Reading
          </mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="usages-form__form">
            <mat-form-field class="usages-form__field">
              <mat-label>Utility Bill</mat-label>
              <mat-select formControlName="utilityBillId">
                <mat-option *ngFor="let bill of utilityBills$ | async" [value]="bill.utilityBillId">
                  {{ bill.utilityType }} - {{ bill.billingDate | date: 'short' }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="usages-form__field">
              <mat-label>Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="date">
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="usages-form__field">
              <mat-label>Amount</mat-label>
              <input matInput type="number" formControlName="amount" placeholder="0.00">
            </mat-form-field>

            <div class="usages-form__actions">
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
    .usages-form {
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
export class UsagesForm implements OnInit {
  form: FormGroup;
  isEditMode = false;
  usageId: string | null = null;
  utilityBills$ = this.utilityBillService.utilityBills$;

  constructor(
    private fb: FormBuilder,
    private usageService: UsageService,
    private utilityBillService: UtilityBillService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      utilityBillId: ['', Validators.required],
      date: [new Date(), Validators.required],
      amount: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.utilityBillService.getAll().subscribe();
    this.usageId = this.route.snapshot.paramMap.get('id');
    if (this.usageId) {
      this.isEditMode = true;
      this.loadUsage();
    }
  }

  loadUsage(): void {
    if (this.usageId) {
      this.usageService.getById(this.usageId).subscribe(usage => {
        this.form.patchValue({
          utilityBillId: usage.utilityBillId,
          date: new Date(usage.date),
          amount: usage.amount
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.usageId) {
        this.usageService.update(this.usageId, {
          usageId: this.usageId,
          ...formValue
        }).subscribe(() => {
          this.router.navigate(['/usages']);
        });
      } else {
        this.usageService.create(formValue).subscribe(() => {
          this.router.navigate(['/usages']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/usages']);
  }
}
