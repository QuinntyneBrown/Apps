import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { Wine, WineType, Region } from '../../models';

@Component({
  selector: 'app-wine-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './wine-form.html',
  styleUrl: './wine-form.scss'
})
export class WineForm {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<WineForm>);

  wineForm: FormGroup;
  wineTypes = Object.values(WineType);
  regions = Object.values(Region);

  constructor(@Inject(MAT_DIALOG_DATA) public data: { wine?: Wine }) {
    this.wineForm = this._fb.group({
      name: [data?.wine?.name || '', Validators.required],
      wineType: [data?.wine?.wineType || '', Validators.required],
      region: [data?.wine?.region || '', Validators.required],
      vintage: [data?.wine?.vintage || null],
      producer: [data?.wine?.producer || ''],
      purchasePrice: [data?.wine?.purchasePrice || null],
      bottleCount: [data?.wine?.bottleCount || 1, [Validators.required, Validators.min(1)]],
      storageLocation: [data?.wine?.storageLocation || ''],
      notes: [data?.wine?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.wineForm.valid) {
      this._dialogRef.close(this.wineForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
