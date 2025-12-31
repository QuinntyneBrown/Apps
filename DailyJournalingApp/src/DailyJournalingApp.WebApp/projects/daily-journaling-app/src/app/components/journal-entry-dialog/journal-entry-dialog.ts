import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { JournalEntry, Mood } from '../../models';

export interface JournalEntryDialogData {
  entry?: JournalEntry;
  userId: string;
}

@Component({
  selector: 'app-journal-entry-dialog',
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
  templateUrl: './journal-entry-dialog.html',
  styleUrl: './journal-entry-dialog.scss'
})
export class JournalEntryDialog implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<JournalEntryDialog>);

  form!: FormGroup;
  moods = [
    { value: Mood.VeryHappy, label: 'Very Happy' },
    { value: Mood.Happy, label: 'Happy' },
    { value: Mood.Neutral, label: 'Neutral' },
    { value: Mood.Sad, label: 'Sad' },
    { value: Mood.VerySad, label: 'Very Sad' },
    { value: Mood.Anxious, label: 'Anxious' },
    { value: Mood.Calm, label: 'Calm' },
    { value: Mood.Energetic, label: 'Energetic' },
    { value: Mood.Tired, label: 'Tired' }
  ];

  constructor(@Inject(MAT_DIALOG_DATA) public data: JournalEntryDialogData) {}

  ngOnInit(): void {
    const entry = this.data.entry;
    this.form = this.fb.group({
      title: [entry?.title || '', Validators.required],
      content: [entry?.content || '', Validators.required],
      entryDate: [entry?.entryDate ? new Date(entry.entryDate) : new Date(), Validators.required],
      mood: [entry?.mood ?? Mood.Neutral, Validators.required],
      tags: [entry?.tags || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        entryDate: formValue.entryDate.toISOString(),
        userId: this.data.userId,
        ...(this.data.entry && { journalEntryId: this.data.entry.journalEntryId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
