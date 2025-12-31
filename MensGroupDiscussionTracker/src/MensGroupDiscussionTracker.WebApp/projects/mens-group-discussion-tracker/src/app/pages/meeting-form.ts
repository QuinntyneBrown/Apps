import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MeetingService, GroupService } from '../services';

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
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="meeting-form">
      <h1 class="meeting-form__title">{{ isEditMode ? 'Edit Meeting' : 'New Meeting' }}</h1>

      <mat-card class="meeting-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="meeting-form__form">
          <mat-form-field appearance="outline" class="meeting-form__field" *ngIf="!isEditMode">
            <mat-label>Group</mat-label>
            <mat-select formControlName="groupId" required>
              <mat-option *ngFor="let group of groups$ | async" [value]="group.groupId">
                {{ group.name }}
              </mat-option>
            </mat-select>
            <mat-error *ngIf="form.get('groupId')?.hasError('required')">Group is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="meeting-form__field">
            <mat-label>Title</mat-label>
            <input matInput formControlName="title" required>
            <mat-error *ngIf="form.get('title')?.hasError('required')">Title is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="meeting-form__field">
            <mat-label>Date & Time</mat-label>
            <input matInput type="datetime-local" formControlName="meetingDateTime" required>
            <mat-error *ngIf="form.get('meetingDateTime')?.hasError('required')">Date & Time is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="meeting-form__field">
            <mat-label>Location</mat-label>
            <input matInput formControlName="location">
          </mat-form-field>

          <mat-form-field appearance="outline" class="meeting-form__field">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="3"></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="meeting-form__field">
            <mat-label>Attendee Count</mat-label>
            <input matInput type="number" formControlName="attendeeCount" required min="0">
            <mat-error *ngIf="form.get('attendeeCount')?.hasError('required')">Attendee count is required</mat-error>
          </mat-form-field>

          <div class="meeting-form__actions">
            <button mat-raised-button type="button" (click)="cancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              {{ isEditMode ? 'Update' : 'Create' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .meeting-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;

      &__title {
        margin: 0 0 2rem 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        padding: 2rem;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
      }

      &__field {
        width: 100%;
      }

      &__actions {
        display: flex;
        justify-content: flex-end;
        gap: 1rem;
        margin-top: 1rem;
      }
    }
  `]
})
export class MeetingForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly meetingService = inject(MeetingService);
  private readonly groupService = inject(GroupService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  meetingId: string | null = null;
  groups$ = this.groupService.groups$;

  constructor() {
    this.form = this.fb.group({
      groupId: ['', Validators.required],
      title: ['', Validators.required],
      meetingDateTime: ['', Validators.required],
      location: [''],
      notes: [''],
      attendeeCount: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.groupService.getAll().subscribe();

    this.meetingId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.meetingId && this.meetingId !== 'new';

    if (this.isEditMode && this.meetingId) {
      this.meetingService.getById(this.meetingId).subscribe(meeting => {
        const dateTime = new Date(meeting.meetingDateTime).toISOString().slice(0, 16);
        this.form.patchValue({
          groupId: meeting.groupId,
          title: meeting.title,
          meetingDateTime: dateTime,
          location: meeting.location,
          notes: meeting.notes,
          attendeeCount: meeting.attendeeCount
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;
    const meetingDateTime = new Date(formValue.meetingDateTime).toISOString();

    if (this.isEditMode && this.meetingId) {
      this.meetingService.update({
        meetingId: this.meetingId,
        title: formValue.title,
        meetingDateTime: meetingDateTime,
        location: formValue.location,
        notes: formValue.notes,
        attendeeCount: formValue.attendeeCount
      }).subscribe(() => {
        this.router.navigate(['/meetings']);
      });
    } else {
      this.meetingService.create({
        groupId: formValue.groupId,
        title: formValue.title,
        meetingDateTime: meetingDateTime,
        location: formValue.location,
        notes: formValue.notes,
        attendeeCount: formValue.attendeeCount
      }).subscribe(() => {
        this.router.navigate(['/meetings']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/meetings']);
  }
}
