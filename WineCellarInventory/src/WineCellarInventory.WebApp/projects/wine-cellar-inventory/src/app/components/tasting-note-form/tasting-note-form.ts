import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Wine, TastingNote } from '../../models';

@Component({
  selector: 'app-tasting-note-form',
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
  templateUrl: './tasting-note-form.html',
  styleUrl: './tasting-note-form.scss'
})
export class TastingNoteForm {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<TastingNoteForm>);

  tastingNoteForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { wine: Wine; tastingNote?: TastingNote }) {
    this.tastingNoteForm = this._fb.group({
      wineId: [data.wine.wineId, Validators.required],
      tastingDate: [data?.tastingNote?.tastingDate ? new Date(data.tastingNote.tastingDate) : new Date(), Validators.required],
      rating: [data?.tastingNote?.rating || null, [Validators.required, Validators.min(1), Validators.max(100)]],
      appearance: [data?.tastingNote?.appearance || ''],
      aroma: [data?.tastingNote?.aroma || ''],
      taste: [data?.tastingNote?.taste || ''],
      finish: [data?.tastingNote?.finish || ''],
      overallImpression: [data?.tastingNote?.overallImpression || '']
    });
  }

  onSubmit(): void {
    if (this.tastingNoteForm.valid) {
      this._dialogRef.close(this.tastingNoteForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
