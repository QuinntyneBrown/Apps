import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Reminder, CreateReminderCommand, UpdateReminderCommand } from '../../models';

@Component({
  selector: 'app-reminder-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  templateUrl: './reminder-dialog.html',
  styleUrls: ['./reminder-dialog.scss']
})
export class ReminderDialog implements OnInit {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ReminderDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { reminder?: Reminder }
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      reminderTime: [this.data.reminder?.reminderTime ?? '09:00:00', Validators.required],
      message: [this.data.reminder?.message ?? ''],
      isEnabled: [this.data.reminder?.isEnabled ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const command: CreateReminderCommand | UpdateReminderCommand = this.data.reminder
        ? { ...formValue, reminderId: this.data.reminder.reminderId }
        : formValue;
      this.dialogRef.close(command);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
