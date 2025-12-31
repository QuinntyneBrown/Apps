import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EfficiencyReportsService, VehiclesService } from '../services';

@Component({
  selector: 'app-report-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>Generate Efficiency Report</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="report-dialog__content">
        <mat-form-field appearance="outline" class="report-dialog__field">
          <mat-label>Vehicle</mat-label>
          <mat-select formControlName="vehicleId" required>
            <mat-option *ngFor="let vehicle of vehicles$ | async" [value]="vehicle.vehicleId">
              {{ vehicle.year }} {{ vehicle.make }} {{ vehicle.model }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="report-dialog__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
          <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="report-dialog__field">
          <mat-label>End Date</mat-label>
          <input matInput [matDatepicker]="endPicker" formControlName="endDate" required>
          <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
          <mat-datepicker #endPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="report-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          Generate
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .report-dialog__content {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .report-dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class ReportDialog implements OnInit {
  private fb = inject(FormBuilder);
  private reportsService = inject(EfficiencyReportsService);
  private vehiclesService = inject(VehiclesService);
  private snackBar = inject(MatSnackBar);
  private dialogRef = inject(MatDialogRef<ReportDialog>);

  vehicles$ = this.vehiclesService.vehicles$;
  form: FormGroup;

  constructor() {
    const now = new Date();
    const monthAgo = new Date(now.getFullYear(), now.getMonth() - 1, now.getDate());

    this.form = this.fb.group({
      vehicleId: ['', Validators.required],
      startDate: [monthAgo, Validators.required],
      endDate: [now, Validators.required],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.vehiclesService.getAll(true).subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const request = {
        vehicleId: formValue.vehicleId,
        startDate: formValue.startDate,
        endDate: formValue.endDate,
        notes: formValue.notes || undefined
      };

      this.reportsService.generate(request).subscribe({
        next: () => {
          this.snackBar.open('Report generated successfully', 'Close', { duration: 3000 });
          this.dialogRef.close(true);
        },
        error: () => {
          this.snackBar.open('Error generating report', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
