import { Component, inject, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { Journal } from '../../models';

@Component({
  selector: 'app-journal-dialog',
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
  templateUrl: './journal-dialog.html',
  styleUrl: './journal-dialog.scss'
})
export class JournalDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<JournalDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { journal?: Journal }) {
    this.form = this._fb.group({
      title: [data.journal?.title || '', Validators.required],
      content: [data.journal?.content || '', Validators.required],
      entryDate: [data.journal?.entryDate ? new Date(data.journal.entryDate) : new Date(), Validators.required],
      tags: [data.journal?.tags || '']
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
