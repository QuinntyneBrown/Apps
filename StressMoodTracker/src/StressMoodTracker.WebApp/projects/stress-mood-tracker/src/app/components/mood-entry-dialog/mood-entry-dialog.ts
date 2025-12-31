import { Component, inject, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MoodEntry, MoodLevel, StressLevel } from '../../models';

@Component({
  selector: 'app-mood-entry-dialog',
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
    MatNativeDateModule
  ],
  templateUrl: './mood-entry-dialog.html',
  styleUrl: './mood-entry-dialog.scss'
})
export class MoodEntryDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<MoodEntryDialog>);

  form: FormGroup;
  moodLevels = Object.values(MoodLevel);
  stressLevels = Object.values(StressLevel);

  constructor(@Inject(MAT_DIALOG_DATA) public data: { moodEntry?: MoodEntry }) {
    this.form = this._fb.group({
      moodLevel: [data.moodEntry?.moodLevel || '', Validators.required],
      stressLevel: [data.moodEntry?.stressLevel || '', Validators.required],
      entryTime: [data.moodEntry?.entryTime ? new Date(data.moodEntry.entryTime) : new Date(), Validators.required],
      notes: [data.moodEntry?.notes || ''],
      activities: [data.moodEntry?.activities || '']
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
