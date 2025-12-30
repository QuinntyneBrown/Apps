import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CalendarEventDto, CreateEventCommand, EVENT_TYPES, EventType } from '../../models/calendar-event-dto';

export interface CreateOrEditEventDialogData {
  event?: CalendarEventDto;
}

export interface CreateOrEditEventDialogResult {
  action: 'create' | 'update' | 'cancel';
  data?: CreateEventCommand & { eventId?: string };
}

@Component({
  selector: 'app-create-or-edit-event-dialog',
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
  templateUrl: './create-or-edit-event-dialog.html',
  styleUrls: ['./create-or-edit-event-dialog.scss']
})
export class CreateOrEditEventDialog {
  private fb = inject(FormBuilder);

  form: FormGroup;

  availableEventTypes = EVENT_TYPES;

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditEventDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CreateOrEditEventDialogData
  ) {
    const event = data.event;
    const startDate = event ? new Date(event.startTime) : new Date();
    const endDate = event ? new Date(event.endTime) : new Date(startDate.getTime() + 60 * 60 * 1000);

    this.form = this.fb.group({
      title: [event?.title || '', Validators.required],
      description: [event?.description || ''],
      startDate: [startDate, Validators.required],
      startTime: [this.formatTime(startDate), Validators.required],
      endDate: [endDate, Validators.required],
      endTime: [this.formatTime(endDate), Validators.required],
      location: [event?.location || ''],
      eventType: [event?.eventType || 'Other', Validators.required]
    });
  }

  get isEditMode(): boolean {
    return !!this.data.event;
  }

  get dialogTitle(): string {
    return this.isEditMode ? 'Edit Event' : 'Create Event';
  }

  private formatTime(date: Date): string {
    return date.toTimeString().slice(0, 5);
  }

  private combineDateAndTime(date: Date, time: string): string {
    const [hours, minutes] = time.split(":").map(Number);
    // Create a new Date in local time
    const combined = new Date(date);
    combined.setHours(hours, minutes, 0, 0);
    // Convert to UTC explicitly
    const utcDate = new Date(
      Date.UTC(
        combined.getFullYear(),
        combined.getMonth(),
        combined.getDate(),
        combined.getHours(),
        combined.getMinutes(),
        0,
        0
      )
    );
    return utcDate.toISOString();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      const startTime = this.combineDateAndTime(formValue.startDate, formValue.startTime);
      const endTime = this.combineDateAndTime(formValue.endDate, formValue.endTime);

      const result: CreateOrEditEventDialogResult = {
        action: this.isEditMode ? 'update' : 'create',
        data: {
          familyId: this.data.event?.familyId || '00000000-0000-0000-0000-000000000000',
          creatorId: this.data.event?.creatorId || '00000000-0000-0000-0000-000000000000',
          title: formValue.title,
          description: formValue.description || undefined,
          startTime,
          endTime,
          location: formValue.location || undefined,
          eventType: formValue.eventType as EventType,
          ...(this.isEditMode && { eventId: this.data.event!.eventId })
        }
      };

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as CreateOrEditEventDialogResult);
  }
}
