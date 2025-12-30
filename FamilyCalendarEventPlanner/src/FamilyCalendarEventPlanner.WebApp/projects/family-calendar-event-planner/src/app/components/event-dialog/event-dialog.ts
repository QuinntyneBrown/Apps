import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, FormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import {
  CalendarEvent,
  CreateEventRequest,
  EventType,
  FamilyMember,
  RecurrenceFrequency
} from '../../services/models';

export interface EventDialogData {
  event?: CalendarEvent;
  members: FamilyMember[];
  familyId: string;
  creatorId: string;
}

export interface EventDialogResult {
  action: 'create' | 'update' | 'cancel' | 'delete';
  data?: CreateEventRequest;
}

@Component({
  selector: 'app-event-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    MatChipsModule
  ],
  templateUrl: './event-dialog.html',
  styleUrl: './event-dialog.scss'
})
export class EventDialog {
  private fb = inject(FormBuilder);

  form: FormGroup;
  eventTypes = Object.values(EventType);
  recurrenceFrequencies = Object.values(RecurrenceFrequency);
  isRecurring = false;
  selectedAttendees: string[] = [];

  constructor(
    public dialogRef: MatDialogRef<EventDialog>,
    @Inject(MAT_DIALOG_DATA) public data: EventDialogData
  ) {
    this.form = this.fb.group({
      title: [data.event?.title || '', Validators.required],
      description: [data.event?.description || ''],
      startDate: [data.event ? new Date(data.event.startTime) : new Date(), Validators.required],
      startTime: [data.event ? this.formatTime(data.event.startTime) : '09:00', Validators.required],
      endDate: [data.event ? new Date(data.event.endTime) : new Date(), Validators.required],
      endTime: [data.event ? this.formatTime(data.event.endTime) : '10:00', Validators.required],
      location: [data.event?.location || ''],
      eventType: [data.event?.eventType || EventType.Other, Validators.required],
      recurrenceFrequency: [data.event?.recurrencePattern?.frequency || RecurrenceFrequency.None],
      recurrenceInterval: [data.event?.recurrencePattern?.interval || 1],
      recurrenceEndDate: [data.event?.recurrencePattern?.endDate ? new Date(data.event.recurrencePattern.endDate) : null]
    });

    this.isRecurring = data.event?.recurrencePattern?.frequency !== RecurrenceFrequency.None;
  }

  get isEditMode(): boolean {
    return !!this.data.event;
  }

  getEventTypeIcon(type: EventType): string {
    const icons: Record<string, string> = {
      'Appointment': 'event',
      'FamilyDinner': 'restaurant',
      'Sports': 'sports_soccer',
      'School': 'school',
      'Vacation': 'flight',
      'Birthday': 'cake',
      'Other': 'event_note'
    };
    return icons[type] || 'event';
  }

  toggleAttendee(memberId: string): void {
    const index = this.selectedAttendees.indexOf(memberId);
    if (index === -1) {
      this.selectedAttendees.push(memberId);
    } else {
      this.selectedAttendees.splice(index, 1);
    }
  }

  isAttendeeSelected(memberId: string): boolean {
    return this.selectedAttendees.includes(memberId);
  }

  getMemberInitials(member: FamilyMember): string {
    const names = member.name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const startDateTime = this.combineDateTime(formValue.startDate, formValue.startTime);
      const endDateTime = this.combineDateTime(formValue.endDate, formValue.endTime);

      const hasRecurrence = formValue.recurrenceFrequency && formValue.recurrenceFrequency !== RecurrenceFrequency.None;

      const request: CreateEventRequest = {
        familyId: this.data.familyId,
        creatorId: this.data.creatorId,
        title: formValue.title,
        description: formValue.description || undefined,
        startTime: startDateTime.toISOString(),
        endTime: endDateTime.toISOString(),
        location: formValue.location || undefined,
        eventType: formValue.eventType,
        recurrencePattern: hasRecurrence ? {
          frequency: formValue.recurrenceFrequency,
          interval: formValue.recurrenceInterval,
          endDate: formValue.recurrenceEndDate?.toISOString() || null,
          daysOfWeek: []
        } : undefined
      };

      this.dialogRef.close({
        action: this.isEditMode ? 'update' : 'create',
        data: request
      } as EventDialogResult);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as EventDialogResult);
  }

  onDelete(): void {
    this.dialogRef.close({ action: 'delete' } as EventDialogResult);
  }

  private formatTime(dateString: string): string {
    const date = new Date(dateString);
    return `${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}`;
  }

  private combineDateTime(date: Date, time: string): Date {
    const [hours, minutes] = time.split(':').map(Number);
    const combined = new Date(date);
    combined.setHours(hours, minutes, 0, 0);
    return combined;
  }
}
