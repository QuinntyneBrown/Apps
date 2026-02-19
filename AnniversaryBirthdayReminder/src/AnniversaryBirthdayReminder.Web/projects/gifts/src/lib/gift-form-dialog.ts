import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

interface DialogData {
  dateId: string | null;
}

@Component({
  selector: 'app-gift-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './gift-form-dialog.html',
  styleUrl: './gift-form-dialog.scss'
})
export class GiftFormDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<GiftFormDialog>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);

  form: FormGroup = this.fb.group({
    description: ['', Validators.required],
    estimatedPrice: [0, [Validators.required, Validators.min(0)]],
    purchaseUrl: ['']
  });

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close({
        ...this.form.value,
        status: 'Idea',
        actualPrice: null,
        purchasedAt: null
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
