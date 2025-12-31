import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Intake, BeverageType, CreateIntakeCommand, UpdateIntakeCommand } from '../../models';

@Component({
  selector: 'app-intake-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './intake-dialog.html',
  styleUrls: ['./intake-dialog.scss']
})
export class IntakeDialog implements OnInit {
  form!: FormGroup;
  beverageTypes = Object.keys(BeverageType).filter(k => !isNaN(Number(k))).map(k => ({
    value: Number(k),
    label: BeverageType[Number(k)]
  }));

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<IntakeDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { intake?: Intake }
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      beverageType: [this.data.intake?.beverageType ?? BeverageType.Water, Validators.required],
      amountMl: [this.data.intake?.amountMl ?? 250, [Validators.required, Validators.min(1)]],
      intakeTime: [this.data.intake?.intakeTime ?? new Date(), Validators.required],
      notes: [this.data.intake?.notes ?? '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const command: CreateIntakeCommand | UpdateIntakeCommand = this.data.intake
        ? { ...formValue, intakeId: this.data.intake.intakeId }
        : formValue;
      this.dialogRef.close(command);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
