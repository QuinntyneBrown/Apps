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
import { LeaseService, PropertyService } from '../services';
import { Property } from '../models';

@Component({
  selector: 'app-lease-form',
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
    <div class="lease-form">
      <div class="lease-form__header">
        <h1 class="lease-form__title">{{ isEditMode ? 'Edit Lease' : 'Add Lease' }}</h1>
      </div>

      <mat-card class="lease-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="lease-form__form">
            <mat-form-field appearance="outline" class="lease-form__field">
              <mat-label>Property</mat-label>
              <mat-select formControlName="propertyId" required>
                <mat-option *ngFor="let property of properties" [value]="property.propertyId">
                  {{ property.address }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('propertyId')?.hasError('required')">Property is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="lease-form__field">
              <mat-label>Tenant Name</mat-label>
              <input matInput formControlName="tenantName" required>
              <mat-error *ngIf="form.get('tenantName')?.hasError('required')">Tenant name is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="lease-form__field">
              <mat-label>Monthly Rent</mat-label>
              <input matInput type="number" formControlName="monthlyRent" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('monthlyRent')?.hasError('required')">Monthly rent is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="lease-form__field">
              <mat-label>Security Deposit</mat-label>
              <input matInput type="number" formControlName="securityDeposit" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('securityDeposit')?.hasError('required')">Security deposit is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="lease-form__field">
              <mat-label>Start Date</mat-label>
              <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
              <mat-datepicker-toggle matIconSuffix [for]="startPicker"></mat-datepicker-toggle>
              <mat-datepicker #startPicker></mat-datepicker>
              <mat-error *ngIf="form.get('startDate')?.hasError('required')">Start date is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="lease-form__field">
              <mat-label>End Date</mat-label>
              <input matInput [matDatepicker]="endPicker" formControlName="endDate" required>
              <mat-datepicker-toggle matIconSuffix [for]="endPicker"></mat-datepicker-toggle>
              <mat-datepicker #endPicker></mat-datepicker>
              <mat-error *ngIf="form.get('endDate')?.hasError('required')">End date is required</mat-error>
            </mat-form-field>

            <div class="lease-form__field" *ngIf="isEditMode">
              <mat-checkbox formControlName="isActive">Active</mat-checkbox>
            </div>

            <mat-form-field appearance="outline" class="lease-form__field lease-form__field--full">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="lease-form__actions">
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
    .lease-form {
      padding: 2rem;
    }

    .lease-form__header {
      margin-bottom: 2rem;
    }

    .lease-form__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .lease-form__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 1rem;
    }

    .lease-form__field {
      width: 100%;
    }

    .lease-form__field--full {
      grid-column: 1 / -1;
    }

    .lease-form__actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class LeaseForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly leaseService = inject(LeaseService);
  private readonly propertyService = inject(PropertyService);

  form!: FormGroup;
  isEditMode = false;
  leaseId?: string;
  properties: Property[] = [];

  ngOnInit(): void {
    this.form = this.fb.group({
      propertyId: ['', Validators.required],
      tenantName: ['', Validators.required],
      monthlyRent: [0, Validators.required],
      securityDeposit: [0, Validators.required],
      startDate: [new Date(), Validators.required],
      endDate: [new Date(), Validators.required],
      isActive: [true],
      notes: ['']
    });

    this.propertyService.getProperties().subscribe(properties => {
      this.properties = properties;
    });

    this.leaseId = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.leaseId) {
      this.isEditMode = true;
      this.loadLease(this.leaseId);
    }
  }

  loadLease(id: string): void {
    this.leaseService.getLease(id).subscribe(lease => {
      this.form.patchValue({
        propertyId: lease.propertyId,
        tenantName: lease.tenantName,
        monthlyRent: lease.monthlyRent,
        securityDeposit: lease.securityDeposit,
        startDate: new Date(lease.startDate),
        endDate: new Date(lease.endDate),
        isActive: lease.isActive,
        notes: lease.notes
      });
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const leaseData = {
      ...formValue,
      startDate: formValue.startDate.toISOString(),
      endDate: formValue.endDate.toISOString()
    };

    if (this.isEditMode && this.leaseId) {
      this.leaseService.updateLease({ leaseId: this.leaseId, ...leaseData }).subscribe(() => {
        this.router.navigate(['/leases']);
      });
    } else {
      const { isActive, ...createData } = leaseData;
      this.leaseService.createLease(createData).subscribe(() => {
        this.router.navigate(['/leases']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/leases']);
  }
}
