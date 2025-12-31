import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Registry, RegistryType } from '../../models';

export interface RegistryDialogData {
  registry?: Registry;
  userId: string;
}

@Component({
  selector: 'app-registry-dialog',
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
    MatCheckboxModule
  ],
  templateUrl: './registry-dialog.html',
  styleUrl: './registry-dialog.scss'
})
export class RegistryDialog implements OnInit {
  form!: FormGroup;
  registryTypes = Object.values(RegistryType).filter(v => typeof v === 'number') as RegistryType[];
  RegistryType = RegistryType;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<RegistryDialog>,
    @Inject(MAT_DIALOG_DATA) public data: RegistryDialogData
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.registry;
    this.initForm();
  }

  initForm(): void {
    this.form = this.fb.group({
      name: [this.data.registry?.name || '', Validators.required],
      description: [this.data.registry?.description || ''],
      type: [this.data.registry?.type ?? RegistryType.Other, Validators.required],
      eventDate: [this.data.registry?.eventDate ? new Date(this.data.registry.eventDate) : new Date(), Validators.required],
      isActive: [this.data.registry?.isActive ?? true]
    });
  }

  getRegistryTypeLabel(type: RegistryType): string {
    const labels: Record<RegistryType, string> = {
      [RegistryType.Wedding]: 'Wedding',
      [RegistryType.BabyShower]: 'Baby Shower',
      [RegistryType.Birthday]: 'Birthday',
      [RegistryType.Graduation]: 'Graduation',
      [RegistryType.Housewarming]: 'Housewarming',
      [RegistryType.Anniversary]: 'Anniversary',
      [RegistryType.Holiday]: 'Holiday',
      [RegistryType.Other]: 'Other'
    };
    return labels[type];
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        eventDate: formValue.eventDate.toISOString(),
        userId: this.data.userId,
        ...(this.isEditMode && { registryId: this.data.registry!.registryId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
