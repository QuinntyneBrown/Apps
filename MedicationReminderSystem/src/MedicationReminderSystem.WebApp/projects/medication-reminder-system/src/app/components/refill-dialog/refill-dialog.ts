import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Refill } from '../../models';

export interface RefillDialogData {
  refill?: Refill;
  userId: string;
  medicationId: string;
}

@Component({
  selector: 'app-refill-dialog',
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
  templateUrl: './refill-dialog.html',
  styleUrl: './refill-dialog.scss'
})
export class RefillDialog implements OnInit {
  form!: FormGroup;
  isEdit = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<RefillDialog>,
    @Inject(MAT_DIALOG_DATA) public data: RefillDialogData
  ) {}

  ngOnInit(): void {
    this.isEdit = !!this.data.refill;
    this.initForm();
  }

  private initForm(): void {
    const refill = this.data.refill;
    this.form = this.fb.group({
      refillDate: [refill?.refillDate ? new Date(refill.refillDate) : new Date(), Validators.required],
      quantity: [refill?.quantity || 0, [Validators.required, Validators.min(1)]],
      pharmacyName: [refill?.pharmacyName || ''],
      cost: [refill?.cost || null, [Validators.min(0)]],
      nextRefillDate: [refill?.nextRefillDate ? new Date(refill.nextRefillDate) : null],
      refillsRemaining: [refill?.refillsRemaining || null, [Validators.min(0)]],
      notes: [refill?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        refillDate: formValue.refillDate?.toISOString(),
        nextRefillDate: formValue.nextRefillDate?.toISOString() || null,
        userId: this.data.userId,
        medicationId: this.data.medicationId,
        ...(this.isEdit && { refillId: this.data.refill!.refillId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
