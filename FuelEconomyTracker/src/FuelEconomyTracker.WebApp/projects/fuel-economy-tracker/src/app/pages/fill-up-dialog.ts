import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FillUpsService, VehiclesService } from '../services';
import { FillUp } from '../models';

@Component({
  selector: 'app-fill-up-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ isEdit ? 'Edit Fill-Up' : 'Add Fill-Up' }}</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="fill-up-dialog__content">
        <mat-form-field appearance="outline" class="fill-up-dialog__field" *ngIf="!isEdit">
          <mat-label>Vehicle</mat-label>
          <mat-select formControlName="vehicleId" required>
            <mat-option *ngFor="let vehicle of vehicles$ | async" [value]="vehicle.vehicleId">
              {{ vehicle.year }} {{ vehicle.make }} {{ vehicle.model }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Fill-Up Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="fillUpDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Odometer</mat-label>
          <input matInput type="number" formControlName="odometer" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Gallons</mat-label>
          <input matInput type="number" step="0.01" formControlName="gallons" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Price Per Gallon</mat-label>
          <input matInput type="number" step="0.01" formControlName="pricePerGallon" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Fuel Grade</mat-label>
          <input matInput formControlName="fuelGrade">
        </mat-form-field>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Gas Station</mat-label>
          <input matInput formControlName="gasStation">
        </mat-form-field>

        <mat-checkbox formControlName="isFullTank" class="fill-up-dialog__checkbox">
          Full Tank
        </mat-checkbox>

        <mat-form-field appearance="outline" class="fill-up-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ isEdit ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .fill-up-dialog__content {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .fill-up-dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }

    .fill-up-dialog__checkbox {
      margin-bottom: 1rem;
    }
  `]
})
export class FillUpDialog implements OnInit {
  private fb = inject(FormBuilder);
  private fillUpsService = inject(FillUpsService);
  private vehiclesService = inject(VehiclesService);
  private snackBar = inject(MatSnackBar);
  private dialogRef = inject(MatDialogRef<FillUpDialog>);

  vehicles$ = this.vehiclesService.vehicles$;
  isEdit = false;
  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: FillUp | null) {
    this.isEdit = !!data;
    this.form = this.fb.group({
      vehicleId: [data?.vehicleId || '', this.isEdit ? [] : [Validators.required]],
      fillUpDate: [data?.fillUpDate ? new Date(data.fillUpDate) : new Date(), Validators.required],
      odometer: [data?.odometer || '', Validators.required],
      gallons: [data?.gallons || '', Validators.required],
      pricePerGallon: [data?.pricePerGallon || '', Validators.required],
      fuelGrade: [data?.fuelGrade || ''],
      gasStation: [data?.gasStation || ''],
      isFullTank: [data?.isFullTank ?? true],
      notes: [data?.notes || '']
    });
  }

  ngOnInit(): void {
    if (!this.isEdit) {
      this.vehiclesService.getAll(true).subscribe();
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const request = {
        vehicleId: formValue.vehicleId,
        fillUpDate: formValue.fillUpDate,
        odometer: formValue.odometer,
        gallons: formValue.gallons,
        pricePerGallon: formValue.pricePerGallon,
        fuelGrade: formValue.fuelGrade || undefined,
        gasStation: formValue.gasStation || undefined,
        isFullTank: formValue.isFullTank,
        notes: formValue.notes || undefined
      };

      if (this.isEdit && this.data) {
        const { vehicleId, ...updateRequest } = request;
        this.fillUpsService.update(this.data.fillUpId, updateRequest).subscribe({
          next: () => {
            this.snackBar.open('Fill-up updated successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: () => {
            this.snackBar.open('Error updating fill-up', 'Close', { duration: 3000 });
          }
        });
      } else {
        this.fillUpsService.create(request).subscribe({
          next: () => {
            this.snackBar.open('Fill-up created successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: () => {
            this.snackBar.open('Error creating fill-up', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
