import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { ServiceRecord, ServiceType } from '../../models';

export interface ServiceRecordDialogData {
  serviceRecord?: ServiceRecord;
  vehicleId: string;
  mode: 'create' | 'edit';
}

@Component({
  selector: 'app-service-record-dialog',
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
    MatIconModule
  ],
  templateUrl: './service-record-dialog.html',
  styleUrl: './service-record-dialog.scss'
})
export class ServiceRecordDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<ServiceRecordDialog>);
  public data = inject<ServiceRecordDialogData>(MAT_DIALOG_DATA);

  serviceForm!: FormGroup;
  serviceTypes = Object.values(ServiceType);

  ngOnInit(): void {
    this.serviceForm = this.fb.group({
      serviceType: [this.data.serviceRecord?.serviceType || ServiceType.OilChange, Validators.required],
      serviceDate: [this.data.serviceRecord?.serviceDate ? new Date(this.data.serviceRecord.serviceDate) : new Date(), Validators.required],
      mileageAtService: [this.data.serviceRecord?.mileageAtService || 0, [Validators.required, Validators.min(0)]],
      cost: [this.data.serviceRecord?.cost || 0, [Validators.required, Validators.min(0)]],
      serviceProvider: [this.data.serviceRecord?.serviceProvider || ''],
      description: [this.data.serviceRecord?.description || '', Validators.required],
      notes: [this.data.serviceRecord?.notes || ''],
      partsReplaced: this.fb.array((this.data.serviceRecord?.partsReplaced || []).map(part => this.fb.control(part))),
      invoiceNumber: [this.data.serviceRecord?.invoiceNumber || ''],
      warrantyExpirationDate: [this.data.serviceRecord?.warrantyExpirationDate ? new Date(this.data.serviceRecord.warrantyExpirationDate) : null]
    });
  }

  get partsReplaced(): FormArray {
    return this.serviceForm.get('partsReplaced') as FormArray;
  }

  addPart(): void {
    this.partsReplaced.push(this.fb.control(''));
  }

  removePart(index: number): void {
    this.partsReplaced.removeAt(index);
  }

  onSubmit(): void {
    if (this.serviceForm.valid) {
      const formValue = this.serviceForm.value;
      const result = {
        vehicleId: this.data.vehicleId,
        ...formValue,
        serviceDate: formValue.serviceDate ? formValue.serviceDate.toISOString() : new Date().toISOString(),
        warrantyExpirationDate: formValue.warrantyExpirationDate ? formValue.warrantyExpirationDate.toISOString() : null,
        partsReplaced: formValue.partsReplaced.filter((part: string) => part.trim() !== '')
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
