import { Component, inject, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Trigger } from '../../models';

@Component({
  selector: 'app-trigger-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './trigger-dialog.html',
  styleUrl: './trigger-dialog.scss'
})
export class TriggerDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<TriggerDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { trigger?: Trigger }) {
    this.form = this._fb.group({
      name: [data.trigger?.name || '', Validators.required],
      description: [data.trigger?.description || ''],
      triggerType: [data.trigger?.triggerType || '', Validators.required],
      impactLevel: [data.trigger?.impactLevel || 1, [Validators.required, Validators.min(1), Validators.max(10)]]
    });
  }

  onCancel(): void {
    this._dialogRef.close();
  }

  onSave(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }
}
