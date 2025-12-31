import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MeetingService } from '../services';
import { Meeting } from '../models';

@Component({
  selector: 'app-meetings-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="meetings-list">
      <div class="meetings-list__header">
        <h1 class="meetings-list__title">Meetings</h1>
        <button mat-raised-button color="primary" (click)="createMeeting()">
          <mat-icon>add</mat-icon>
          New Meeting
        </button>
      </div>

      <mat-card class="meetings-list__card">
        <table mat-table [dataSource]="meetings$ | async" class="meetings-list__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let meeting">{{ meeting.title }}</td>
          </ng-container>

          <ng-container matColumnDef="meetingDateTime">
            <th mat-header-cell *matHeaderCellDef>Date & Time</th>
            <td mat-cell *matCellDef="let meeting">{{ meeting.meetingDateTime | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let meeting">{{ meeting.location || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="attendeeCount">
            <th mat-header-cell *matHeaderCellDef>Attendees</th>
            <td mat-cell *matCellDef="let meeting">{{ meeting.attendeeCount }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let meeting">
              <button mat-icon-button color="primary" (click)="editMeeting(meeting.meetingId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteMeeting(meeting.meetingId)">
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
    .meetings-list {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow: auto;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class MeetingsList implements OnInit {
  private readonly meetingService = inject(MeetingService);
  private readonly router = inject(Router);

  meetings$ = this.meetingService.meetings$;
  displayedColumns = ['title', 'meetingDateTime', 'location', 'attendeeCount', 'actions'];

  ngOnInit(): void {
    this.meetingService.getAll().subscribe();
  }

  createMeeting(): void {
    this.router.navigate(['/meetings/new']);
  }

  editMeeting(id: string): void {
    this.router.navigate(['/meetings', id]);
  }

  deleteMeeting(id: string): void {
    if (confirm('Are you sure you want to delete this meeting?')) {
      this.meetingService.delete(id).subscribe();
    }
  }
}
