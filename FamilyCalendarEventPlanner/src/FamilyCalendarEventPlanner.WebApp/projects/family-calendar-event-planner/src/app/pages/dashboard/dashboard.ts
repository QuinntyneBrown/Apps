import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { map, combineLatest } from 'rxjs';

import { EventsService, FamilyMembersService, ConflictsService, AttendeesService } from '../../services';
import { EventCard, MiniCalendar, EventDialog, EventDialogData, EventDialogResult } from '../../components';
import { CalendarEvent, FamilyMember, ScheduleConflict, EventAttendee } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    EventCard,
    MiniCalendar
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private eventsService = inject(EventsService);
  private membersService = inject(FamilyMembersService);
  private conflictsService = inject(ConflictsService);
  private dialog = inject(MatDialog);

  selectedDate = new Date();

  viewModel$ = combineLatest([
    this.eventsService.getEvents(),
    this.membersService.getFamilyMembers(),
    this.conflictsService.getConflicts()
  ]).pipe(
    map(([events, members, conflicts]) => {
      const today = new Date();
      const todayEvents = this.getEventsForDate(events, today);
      const upcomingEvents = this.getUpcomingEvents(events, 7);
      const eventDates = events.map(e => new Date(e.startTime));
      const unresolvedConflicts = conflicts.filter(c => !c.isResolved);
      const pendingRsvps = 0;

      return {
        events,
        members,
        conflicts: unresolvedConflicts,
        todayEvents,
        upcomingEvents,
        eventDates,
        stats: {
          eventsThisWeek: upcomingEvents.length,
          conflictsDetected: unresolvedConflicts.length,
          pendingRsvps
        }
      };
    })
  );

  private getEventsForDate(events: CalendarEvent[], date: Date): CalendarEvent[] {
    return events.filter(event => {
      const eventDate = new Date(event.startTime);
      return (
        eventDate.getFullYear() === date.getFullYear() &&
        eventDate.getMonth() === date.getMonth() &&
        eventDate.getDate() === date.getDate()
      );
    }).sort((a, b) => new Date(a.startTime).getTime() - new Date(b.startTime).getTime());
  }

  private getUpcomingEvents(events: CalendarEvent[], days: number): CalendarEvent[] {
    const now = new Date();
    const endDate = new Date();
    endDate.setDate(endDate.getDate() + days);

    return events.filter(event => {
      const eventDate = new Date(event.startTime);
      return eventDate >= now && eventDate <= endDate;
    }).sort((a, b) => new Date(a.startTime).getTime() - new Date(b.startTime).getTime());
  }

  hasConflict(event: CalendarEvent, conflicts: ScheduleConflict[]): boolean {
    return conflicts.some(c => c.conflictingEventIds.includes(event.eventId));
  }

  getAttendeesForEvent(eventId: string): EventAttendee[] {
    return [];
  }

  getMemberInitials(member: FamilyMember): string {
    const names = member.name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  onDateSelect(date: Date): void {
    this.selectedDate = date;
  }

  onEventClick(event: CalendarEvent): void {
    console.log('Event clicked:', event);
  }

  openAddEventDialog(members: FamilyMember[]): void {
    const dialogRef = this.dialog.open(EventDialog, {
      width: '600px',
      data: {
        members,
        familyId: members[0]?.familyId || '',
        creatorId: members[0]?.memberId || ''
      } as EventDialogData
    });

    dialogRef.afterClosed().subscribe((result: EventDialogResult) => {
      if (result?.action === 'create' && result.data) {
        this.eventsService.createEvent(result.data).subscribe();
      }
    });
  }
}
