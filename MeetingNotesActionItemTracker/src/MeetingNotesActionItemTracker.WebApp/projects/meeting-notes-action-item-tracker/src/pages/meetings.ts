import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MeetingService } from '../services';
import { Meeting } from '../models';

@Component({
  selector: 'app-meetings',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './meetings.html',
  styleUrl: './meetings.scss'
})
export class Meetings implements OnInit {
  private meetingService = inject(MeetingService);
  private router = inject(Router);

  meetings$ = this.meetingService.meetings$;
  displayedColumns: string[] = ['title', 'meetingDateTime', 'location', 'attendees', 'actions'];

  ngOnInit(): void {
    this.meetingService.getMeetings().subscribe();
  }

  editMeeting(meeting: Meeting): void {
    this.router.navigate(['/meetings', meeting.meetingId]);
  }

  deleteMeeting(meeting: Meeting): void {
    if (confirm(`Are you sure you want to delete the meeting "${meeting.title}"?`)) {
      this.meetingService.deleteMeeting(meeting.meetingId).subscribe();
    }
  }

  formatDateTime(dateTime: string): string {
    return new Date(dateTime).toLocaleString();
  }
}
