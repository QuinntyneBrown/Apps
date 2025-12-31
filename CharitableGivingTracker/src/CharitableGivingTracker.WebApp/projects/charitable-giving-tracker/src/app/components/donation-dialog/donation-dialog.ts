import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Donation, DonationType, Organization } from '../../models';

export interface DonationDialogData {
  donation?: Donation;
  organizations: Organization[];
}

@Component({
  selector: 'app-donation-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './donation-dialog.html',
  styleUrl: './donation-dialog.scss'
})
export class DonationDialog implements OnInit {
  form: FormGroup;
  donationTypes = [
    { value: DonationType.Cash, label: 'Cash' },
    { value: DonationType.Check, label: 'Check' },
    { value: DonationType.CreditCard, label: 'Credit Card' },
    { value: DonationType.Stock, label: 'Stock' },
    { value: DonationType.InKind, label: 'In-Kind' },
    { value: DonationType.Other, label: 'Other' }
  ];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DonationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DonationDialogData
  ) {
    this.form = this.fb.group({
      organizationId: [data.donation?.organizationId || '', Validators.required],
      amount: [data.donation?.amount || 0, [Validators.required, Validators.min(0.01)]],
      donationDate: [data.donation?.donationDate ? new Date(data.donation.donationDate) : new Date(), Validators.required],
      donationType: [data.donation?.donationType ?? DonationType.Cash, Validators.required],
      receiptNumber: [data.donation?.receiptNumber || ''],
      isTaxDeductible: [data.donation?.isTaxDeductible ?? true],
      notes: [data.donation?.notes || '']
    });
  }

  ngOnInit(): void {}

  get isEditMode(): boolean {
    return !!this.data.donation;
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        donationDate: formValue.donationDate.toISOString()
      };

      if (this.isEditMode) {
        result.donationId = this.data.donation!.donationId;
      }

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
