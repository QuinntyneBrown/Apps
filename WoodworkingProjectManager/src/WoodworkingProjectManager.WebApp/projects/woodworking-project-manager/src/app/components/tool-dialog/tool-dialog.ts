import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Tool } from '../../models';

@Component({
  selector: 'app-tool-dialog',
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
  templateUrl: './tool-dialog.html',
  styleUrl: './tool-dialog.scss'
})
export class ToolDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<ToolDialog>);
  data = inject(MAT_DIALOG_DATA);

  toolForm!: FormGroup;

  ngOnInit(): void {
    this.toolForm = this._fb.group({
      name: [this.data?.name || '', Validators.required],
      brand: [this.data?.brand || ''],
      model: [this.data?.model || ''],
      description: [this.data?.description || ''],
      purchasePrice: [this.data?.purchasePrice || null],
      purchaseDate: [this.data?.purchaseDate || null],
      location: [this.data?.location || ''],
      notes: [this.data?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.toolForm.valid) {
      this._dialogRef.close(this.toolForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
