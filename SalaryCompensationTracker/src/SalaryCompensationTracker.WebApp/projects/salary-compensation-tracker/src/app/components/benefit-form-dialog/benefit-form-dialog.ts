import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Benefit } from '../../models';

@Component({
  selector: 'app-benefit-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './benefit-form-dialog.html',
  styleUrl: './benefit-form-dialog.scss'
})
export class BenefitFormDialog implements OnInit {
  form!: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<BenefitFormDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Benefit | null
  ) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      userId: [this.data?.userId || '', Validators.required],
      compensationId: [this.data?.compensationId || ''],
      name: [this.data?.name || '', Validators.required],
      category: [this.data?.category || '', Validators.required],
      description: [this.data?.description || ''],
      estimatedValue: [this.data?.estimatedValue],
      employerContribution: [this.data?.employerContribution],
      employeeContribution: [this.data?.employeeContribution]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
