import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { EventService } from '../services';
import { Event, CreateEvent, UpdateEvent } from '../models';

@Component({
  selector: 'app-event-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Event</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="event-form">
        <mat-form-field class="event-form__field">
          <mat-label>Created By Neighbor ID</mat-label>
          <input matInput formControlName="createdByNeighborId" required>
        </mat-form-field>

        <mat-form-field class="event-form__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="event-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="event-form__field">
          <mat-label>Event Date & Time</mat-label>
          <input matInput type="datetime-local" formControlName="eventDateTime" required>
        </mat-form-field>

        <mat-form-field class="event-form__field">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location">
        </mat-form-field>

        <mat-checkbox formControlName="isPublic" class="event-form__checkbox">
          Public Event
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="form.invalid" (click)="save()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .event-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }

      &__checkbox {
        margin-top: 8px;
      }
    }
  `]
})
export class EventDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data?: Event;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      createdByNeighborId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      eventDateTime: ['', Validators.required],
      location: [''],
      isPublic: [true]
    });

    if (this.data) {
      const formData = {
        ...this.data,
        eventDateTime: this.data.eventDateTime ? new Date(this.data.eventDateTime).toISOString().slice(0, 16) : ''
      };
      this.form.patchValue(formData);
    }
  }

  save() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = this.data
        ? { ...formValue, eventId: this.data.eventId, eventDateTime: new Date(formValue.eventDateTime).toISOString() }
        : { ...formValue, eventDateTime: new Date(formValue.eventDateTime).toISOString() };
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="events">
      <div class="events__header">
        <h1 class="events__title">Events</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Event
        </button>
      </div>

      <mat-card class="events__card">
        <table mat-table [dataSource]="events$ | async" class="events__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let event">{{ event.title }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let event">{{ event.description }}</td>
          </ng-container>

          <ng-container matColumnDef="eventDateTime">
            <th mat-header-cell *matHeaderCellDef>Date & Time</th>
            <td mat-cell *matCellDef="let event">{{ event.eventDateTime | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let event">{{ event.location || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="isPublic">
            <th mat-header-cell *matHeaderCellDef>Public</th>
            <td mat-cell *matCellDef="let event">
              <mat-icon [class.public]="event.isPublic">
                {{ event.isPublic ? 'public' : 'lock' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let event">
              <button mat-icon-button color="primary" (click)="openDialog(event)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(event.eventId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .events {
      padding: 24px;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;

        .public {
          color: #4caf50;
        }

        mat-icon:not(.public) {
          color: #ff9800;
        }
      }
    }
  `]
})
export class Events implements OnInit {
  private eventService = inject(EventService);
  private dialog = inject(MatDialog);

  events$ = this.eventService.events$;
  displayedColumns = ['title', 'description', 'eventDateTime', 'location', 'isPublic', 'actions'];

  ngOnInit() {
    this.eventService.getAll().subscribe();
  }

  openDialog(event?: Event) {
    const dialogRef = this.dialog.open(EventDialog, {
      width: '500px',
      data: event
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (event) {
          this.eventService.update(result as UpdateEvent).subscribe();
        } else {
          this.eventService.create(result as CreateEvent).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this event?')) {
      this.eventService.delete(id).subscribe();
    }
  }
}
