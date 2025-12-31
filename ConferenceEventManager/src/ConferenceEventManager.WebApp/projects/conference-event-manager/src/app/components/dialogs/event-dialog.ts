import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Event, EventType } from '../../models';

@Component({
  selector: 'app-event-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './event-dialog.html',
  styleUrl: './event-dialog.scss'
})
export class EventDialog {
  form: FormGroup;
  isEdit: boolean;
  eventTypes = Object.keys(EventType).filter(key => isNaN(Number(key)));

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<EventDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { event?: Event }
  ) {
    this.isEdit = !!data?.event;
    this.form = this.fb.group({
      name: [data?.event?.name || '', Validators.required],
      eventType: [data?.event?.eventType ?? EventType.Conference, Validators.required],
      startDate: [data?.event?.startDate || '', Validators.required],
      endDate: [data?.event?.endDate || '', Validators.required],
      location: [data?.event?.location || ''],
      isVirtual: [data?.event?.isVirtual || false],
      website: [data?.event?.website || ''],
      registrationFee: [data?.event?.registrationFee || null],
      isRegistered: [data?.event?.isRegistered || false],
      didAttend: [data?.event?.didAttend || false],
      notes: [data?.event?.notes || '']
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  getEventTypeValue(key: string): EventType {
    return EventType[key as keyof typeof EventType];
  }
}
