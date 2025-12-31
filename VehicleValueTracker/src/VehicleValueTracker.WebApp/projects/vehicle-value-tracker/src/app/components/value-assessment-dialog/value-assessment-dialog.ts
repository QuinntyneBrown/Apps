import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { ValueAssessment, ConditionGrade } from '../../models';

@Component({
  selector: 'app-value-assessment-dialog',
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
    MatNativeDateModule,
    MatChipsModule,
    MatIconModule
  ],
  templateUrl: './value-assessment-dialog.html',
  styleUrl: './value-assessment-dialog.scss'
})
export class ValueAssessmentDialog {
  private _fb = inject(FormBuilder);
  form: FormGroup;
  conditionGrades = Object.values(ConditionGrade);

  constructor(
    public dialogRef: MatDialogRef<ValueAssessmentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { assessment?: ValueAssessment; vehicleId?: string }
  ) {
    this.form = this._fb.group({
      vehicleId: [data.assessment?.vehicleId || data.vehicleId || '', Validators.required],
      assessmentDate: [data.assessment?.assessmentDate ? new Date(data.assessment.assessmentDate) : new Date(), Validators.required],
      estimatedValue: [data.assessment?.estimatedValue || null, [Validators.required, Validators.min(0)]],
      mileageAtAssessment: [data.assessment?.mileageAtAssessment || 0, [Validators.required, Validators.min(0)]],
      conditionGrade: [data.assessment?.conditionGrade || ConditionGrade.Good, Validators.required],
      valuationSource: [data.assessment?.valuationSource || ''],
      exteriorCondition: [data.assessment?.exteriorCondition || ''],
      interiorCondition: [data.assessment?.interiorCondition || ''],
      mechanicalCondition: [data.assessment?.mechanicalCondition || ''],
      modifications: [data.assessment?.modifications?.join(', ') || ''],
      knownIssues: [data.assessment?.knownIssues?.join(', ') || ''],
      notes: [data.assessment?.notes || '']
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        modifications: formValue.modifications ? formValue.modifications.split(',').map((m: string) => m.trim()).filter((m: string) => m) : [],
        knownIssues: formValue.knownIssues ? formValue.knownIssues.split(',').map((i: string) => i.trim()).filter((i: string) => i) : []
      };
      this.dialogRef.close(result);
    }
  }
}
