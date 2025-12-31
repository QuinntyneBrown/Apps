import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AuditReport } from '../../models';

@Component({
  selector: 'app-audit-report-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './audit-report-dialog.html',
  styleUrl: './audit-report-dialog.scss'
})
export class AuditReportDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<AuditReportDialog>);

  form: FormGroup;
  isEditMode: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { auditReport?: AuditReport; userId: string }) {
    this.isEditMode = !!data.auditReport;

    this.form = this._fb.group({
      title: [data.auditReport?.title || '', Validators.required],
      startDate: [data.auditReport?.startDate ? new Date(data.auditReport.startDate) : new Date(), Validators.required],
      endDate: [data.auditReport?.endDate ? new Date(data.auditReport.endDate) : new Date(), Validators.required],
      totalTrackedHours: [data.auditReport?.totalTrackedHours || 0, [Validators.required, Validators.min(0)]],
      productiveHours: [data.auditReport?.productiveHours || 0, [Validators.required, Validators.min(0)]],
      summary: [data.auditReport?.summary || ''],
      insights: [data.auditReport?.insights || ''],
      recommendations: [data.auditReport?.recommendations || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        userId: this.data.userId
      };
      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
