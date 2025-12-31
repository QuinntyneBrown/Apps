import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MeetingService, NoteService, ActionItemService } from '../services';
import { ActionItemStatus } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private meetingService = inject(MeetingService);
  private noteService = inject(NoteService);
  private actionItemService = inject(ActionItemService);

  meetings$ = this.meetingService.meetings$;
  notes$ = this.noteService.notes$;
  actionItems$ = this.actionItemService.actionItems$;

  totalMeetings = 0;
  totalNotes = 0;
  totalActionItems = 0;
  pendingActionItems = 0;
  completedActionItems = 0;

  ngOnInit(): void {
    this.meetingService.getMeetings().subscribe(meetings => {
      this.totalMeetings = meetings.length;
    });

    this.noteService.getNotes().subscribe(notes => {
      this.totalNotes = notes.length;
    });

    this.actionItemService.getActionItems().subscribe(actionItems => {
      this.totalActionItems = actionItems.length;
      this.pendingActionItems = actionItems.filter(a =>
        a.status === ActionItemStatus.NotStarted || a.status === ActionItemStatus.InProgress
      ).length;
      this.completedActionItems = actionItems.filter(a =>
        a.status === ActionItemStatus.Completed
      ).length;
    });
  }
}
