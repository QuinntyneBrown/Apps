import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { map, combineLatest, BehaviorSubject, switchMap } from 'rxjs';

import { RemindersService, EventsService, FamilyMembersService } from '../../services';
import { EventReminder, CalendarEvent, FamilyMember, NotificationChannel } from '../../models';

@Component({
  selector: 'app-reminders',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatDialogModule
  ],
  templateUrl: './reminders.html',
  styleUrl: './reminders.scss'
})
export class Reminders {
  private remindersService = inject(RemindersService);
  private eventsService = inject(EventsService);
  private membersService = inject(FamilyMembersService);
  private dialog = inject(MatDialog);

  selectedEvent$ = new BehaviorSubject<string | null>(null);
  selectedRecipient$ = new BehaviorSubject<string | null>(null);

  displayedColumns = ['event', 'recipient', 'reminderTime', 'channel', 'status', 'actions'];

  events$ = this.eventsService.getEvents();
  members$ = this.membersService.getFamilyMembers();

  reminders$ = combineLatest([
    this.selectedEvent$,
    this.selectedRecipient$
  ]).pipe(
    switchMap(([eventId, recipientId]) =>
      this.remindersService.getReminders(
        eventId || undefined,
        recipientId || undefined
      )
    )
  );

  viewModel$ = combineLatest([
    this.reminders$,
    this.events$,
    this.members$
  ]).pipe(
    map(([reminders, events, members]) => ({
      reminders: reminders.map(reminder => ({
        ...reminder,
        event: events.find(e => e.eventId === reminder.eventId),
        recipient: members.find(m => m.memberId === reminder.recipientId)
      })),
      events,
      members,
      pendingReminders: reminders.filter(r => !r.sent).length,
      sentReminders: reminders.filter(r => r.sent).length
    }))
  );

  onEventFilterChange(eventId: string | null): void {
    this.selectedEvent$.next(eventId);
  }

  onRecipientFilterChange(recipientId: string | null): void {
    this.selectedRecipient$.next(recipientId);
  }

  clearFilters(): void {
    this.selectedEvent$.next(null);
    this.selectedRecipient$.next(null);
  }

  sendReminder(reminderId: string): void {
    this.remindersService.sendReminder(reminderId).subscribe({
      next: () => {
        this.refreshReminders();
      },
      error: (error) => {
        console.error('Error sending reminder:', error);
      }
    });
  }

  deleteReminder(reminderId: string): void {
    if (confirm('Are you sure you want to delete this reminder?')) {
      this.remindersService.deleteReminder(reminderId).subscribe({
        next: () => {
          this.refreshReminders();
        },
        error: (error) => {
          console.error('Error deleting reminder:', error);
        }
      });
    }
  }

  getChannelIcon(channel: NotificationChannel): string {
    switch (channel) {
      case NotificationChannel.Email:
        return 'email';
      case NotificationChannel.SMS:
        return 'sms';
      case NotificationChannel.Push:
        return 'notifications';
      default:
        return 'notification_important';
    }
  }

  formatDateTime(dateTime: string): string {
    const date = new Date(dateTime);
    return date.toLocaleString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric',
      hour: 'numeric',
      minute: '2-digit',
      hour12: true
    });
  }

  private refreshReminders(): void {
    this.selectedEvent$.next(this.selectedEvent$.value);
  }
}
