import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Medication, MedicationType, MedicationTypeLabels } from '../../models';

export interface MedicationDialogData {
  medication?: Medication;
  userId: string;
}

@Component({
  selector: 'app-medication-dialog',
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
    MatSlideToggleModule
  ],
  templateUrl: './medication-dialog.html',
  styleUrl: './medication-dialog.scss'
})
export class MedicationDialog implements OnInit {
  form!: FormGroup;
  isEdit = false;
  medicationTypes = Object.keys(MedicationType)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: MedicationTypeLabels[Number(key)]
    }));

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<MedicationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: MedicationDialogData
  ) {}

  ngOnInit(): void {
    this.isEdit = !!this.data.medication;
    this.initForm();
  }

  private initForm(): void {
    const med = this.data.medication;
    this.form = this.fb.group({
      name: [med?.name || '', Validators.required],
      medicationType: [med?.medicationType ?? 0, Validators.required],
      dosage: [med?.dosage || '', Validators.required],
      prescribingDoctor: [med?.prescribingDoctor || ''],
      prescriptionDate: [med?.prescriptionDate ? new Date(med.prescriptionDate) : null],
      purpose: [med?.purpose || ''],
      instructions: [med?.instructions || ''],
      sideEffects: [med?.sideEffects || ''],
      isActive: [med?.isActive ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        prescriptionDate: formValue.prescriptionDate?.toISOString() || null,
        userId: this.data.userId,
        ...(this.isEdit && { medicationId: this.data.medication!.medicationId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
