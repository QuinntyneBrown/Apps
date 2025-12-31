import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { DoseSchedule } from '../../models';

export interface DoseScheduleDialogData {
  schedule?: DoseSchedule;
  userId: string;
  medicationId: string;
}

@Component({
  selector: 'app-dose-schedule-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSlideToggleModule
  ],
  templateUrl: './dose-schedule-dialog.html',
  styleUrl: './dose-schedule-dialog.scss'
})
export class DoseScheduleDialog implements OnInit {
  form!: FormGroup;
  isEdit = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DoseScheduleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DoseScheduleDialogData
  ) {}

  ngOnInit(): void {
    this.isEdit = !!this.data.schedule;
    this.initForm();
  }

  private initForm(): void {
    const schedule = this.data.schedule;
    this.form = this.fb.group({
      scheduledTime: [schedule?.scheduledTime || '', Validators.required],
      daysOfWeek: [schedule?.daysOfWeek || '', Validators.required],
      frequency: [schedule?.frequency || '', Validators.required],
      reminderEnabled: [schedule?.reminderEnabled ?? true],
      reminderOffsetMinutes: [schedule?.reminderOffsetMinutes || 30, [Validators.required, Validators.min(0)]],
      isActive: [schedule?.isActive ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = {
        ...this.form.value,
        userId: this.data.userId,
        medicationId: this.data.medicationId,
        ...(this.isEdit && { doseScheduleId: this.data.schedule!.doseScheduleId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
