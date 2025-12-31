import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { LessonReminderService, LessonService } from '../services';
import { LessonReminder, CreateLessonReminder, UpdateLessonReminder } from '../models';

@Component({
  selector: 'app-reminder-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Reminder' : 'New Reminder' }}</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="reminder-dialog">
        <mat-form-field class="reminder-dialog__field">
          <mat-label>Lesson</mat-label>
          <mat-select formControlName="lessonId" required>
            @for (lesson of lessons$ | async; track lesson.lessonId) {
              <mat-option [value]="lesson.lessonId">{{ lesson.title }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="reminder-dialog__field">
          <mat-label>Reminder Date & Time</mat-label>
          <input matInput type="datetime-local" formControlName="reminderDateTime" required>
        </mat-form-field>

        <mat-form-field class="reminder-dialog__field">
          <mat-label>Message</mat-label>
          <textarea matInput formControlName="message" rows="3"></textarea>
        </mat-form-field>

        @if (data) {
          <div class="reminder-dialog__field">
            <mat-checkbox formControlName="isSent">Is Sent</mat-checkbox>
          </div>
          <div class="reminder-dialog__field">
            <mat-checkbox formControlName="isDismissed">Is Dismissed</mat-checkbox>
          </div>
        }
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">Save</button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .reminder-dialog {
      display: flex;
      flex-direction: column;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
        margin-bottom: 1rem;
      }
    }
  `]
})
export class ReminderDialog {
  private fb = inject(FormBuilder);
  private lessonService = inject(LessonService);
  public dialog = inject(MatDialog);

  data?: LessonReminder;
  form: FormGroup;
  lessons$ = this.lessonService.lessons$;

  constructor() {
    this.form = this.fb.group({
      lessonId: ['', Validators.required],
      reminderDateTime: ['', Validators.required],
      message: [''],
      isSent: [false],
      isDismissed: [false]
    });

    if (this.data) {
      const reminderDateTime = this.data.reminderDateTime.substring(0, 16);
      this.form.patchValue({
        ...this.data,
        reminderDateTime
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialog.getDialogById('reminder-dialog')?.close(this.form.value);
    }
  }
}

@Component({
  selector: 'app-lesson-reminders',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './lesson-reminders.html',
  styleUrl: './lesson-reminders.scss'
})
export class LessonReminders implements OnInit {
  private reminderService = inject(LessonReminderService);
  private lessonService = inject(LessonService);
  private dialog = inject(MatDialog);

  reminders$ = this.reminderService.reminders$;
  lessons$ = this.lessonService.lessons$;
  loading$ = this.reminderService.loading$;
  displayedColumns = ['lesson', 'reminderDateTime', 'isSent', 'isDismissed', 'actions'];

  ngOnInit(): void {
    this.reminderService.getReminders().subscribe();
    this.lessonService.getLessons().subscribe();
  }

  getLessonTitle(lessonId: string): string {
    const lessons = this.lessonService.lessons$.value;
    const lesson = lessons.find(l => l.lessonId === lessonId);
    return lesson?.title || 'Unknown';
  }

  openDialog(reminder?: LessonReminder): void {
    const dialogRef = this.dialog.open(ReminderDialog, {
      id: 'reminder-dialog',
      width: '600px',
      data: reminder
    });

    if (reminder) {
      dialogRef.componentInstance.data = reminder;
    }

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (reminder) {
          const updateData: UpdateLessonReminder = {
            lessonReminderId: reminder.lessonReminderId,
            reminderDateTime: result.reminderDateTime,
            message: result.message,
            isSent: result.isSent,
            isDismissed: result.isDismissed
          };
          this.reminderService.updateReminder(updateData).subscribe();
        } else {
          const createData: CreateLessonReminder = {
            userId: '00000000-0000-0000-0000-000000000000',
            lessonId: result.lessonId,
            reminderDateTime: result.reminderDateTime,
            message: result.message
          };
          this.reminderService.createReminder(createData).subscribe();
        }
      }
    });
  }

  deleteReminder(reminder: LessonReminder): void {
    const lessonTitle = this.getLessonTitle(reminder.lessonId);
    if (confirm(`Are you sure you want to delete the reminder for "${lessonTitle}"?`)) {
      this.reminderService.deleteReminder(reminder.lessonReminderId).subscribe();
    }
  }
}
