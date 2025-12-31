import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Wine, DrinkingWindow } from '../../models';

@Component({
  selector: 'app-drinking-window-form',
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
  templateUrl: './drinking-window-form.html',
  styleUrl: './drinking-window-form.scss'
})
export class DrinkingWindowForm {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<DrinkingWindowForm>);

  drinkingWindowForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { wine: Wine; drinkingWindow?: DrinkingWindow }) {
    this.drinkingWindowForm = this._fb.group({
      wineId: [data.wine.wineId, Validators.required],
      startDate: [data?.drinkingWindow?.startDate ? new Date(data.drinkingWindow.startDate) : null, Validators.required],
      endDate: [data?.drinkingWindow?.endDate ? new Date(data.drinkingWindow.endDate) : null, Validators.required],
      notes: [data?.drinkingWindow?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.drinkingWindowForm.valid) {
      this._dialogRef.close(this.drinkingWindowForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
