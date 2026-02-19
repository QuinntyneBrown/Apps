import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MeetingService } from '../services';
import { CreateMeetingDto, UpdateMeetingDto } from '../models';

@Component({
  selector: 'app-meeting-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './meeting-form.html',
  styleUrl: './meeting-form.scss'
})
export class MeetingForm implements OnInit {
  private fb = inject(FormBuilder);
  private meetingService = inject(MeetingService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  meetingForm!: FormGroup;
  isEditMode = false;
  meetingId?: string;
  attendeeInput = '';

  ngOnInit(): void {
    this.meetingForm = this.fb.group({
      title: ['', Validators.required],
      meetingDateTime: ['', Validators.required],
      durationMinutes: [''],
      location: [''],
      attendees: this.fb.array([]),
      agenda: [''],
      summary: ['']
    });

    this.route.params.subscribe(params => {
      if (params['id'] && params['id'] !== 'new') {
        this.isEditMode = true;
        this.meetingId = params['id'];
        this.loadMeeting(this.meetingId);
      }
    });
  }

  get attendees(): FormArray {
    return this.meetingForm.get('attendees') as FormArray;
  }

  loadMeeting(id: string): void {
    this.meetingService.getMeetingById(id).subscribe(meeting => {
      this.meetingForm.patchValue({
        title: meeting.title,
        meetingDateTime: new Date(meeting.meetingDateTime),
        durationMinutes: meeting.durationMinutes,
        location: meeting.location,
        agenda: meeting.agenda,
        summary: meeting.summary
      });

      meeting.attendees.forEach(attendee => {
        this.attendees.push(this.fb.control(attendee));
      });
    });
  }

  addAttendee(): void {
    if (this.attendeeInput.trim()) {
      this.attendees.push(this.fb.control(this.attendeeInput.trim()));
      this.attendeeInput = '';
    }
  }

  removeAttendee(index: number): void {
    this.attendees.removeAt(index);
  }

  onSubmit(): void {
    if (this.meetingForm.valid) {
      const formValue = this.meetingForm.value;
      const meetingDateTime = new Date(formValue.meetingDateTime).toISOString();

      if (this.isEditMode && this.meetingId) {
        const dto: UpdateMeetingDto = {
          meetingId: this.meetingId,
          title: formValue.title,
          meetingDateTime,
          durationMinutes: formValue.durationMinutes || undefined,
          location: formValue.location || undefined,
          attendees: formValue.attendees || [],
          agenda: formValue.agenda || undefined,
          summary: formValue.summary || undefined
        };
        this.meetingService.updateMeeting(dto).subscribe(() => {
          this.router.navigate(['/meetings']);
        });
      } else {
        const dto: CreateMeetingDto = {
          userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
          title: formValue.title,
          meetingDateTime,
          durationMinutes: formValue.durationMinutes || undefined,
          location: formValue.location || undefined,
          attendees: formValue.attendees || [],
          agenda: formValue.agenda || undefined,
          summary: formValue.summary || undefined
        };
        this.meetingService.createMeeting(dto).subscribe(() => {
          this.router.navigate(['/meetings']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/meetings']);
  }
}
