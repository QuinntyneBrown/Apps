import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialog } from '@angular/material/dialog';
import { map, combineLatest, BehaviorSubject, switchMap, of } from 'rxjs';

import { EventsService, FamilyMembersService, ConflictsService, HouseholdsService } from '../../services';
import { EventDialog, EventDialogData, EventDialogResult } from '../../components';
import { CalendarEvent, FamilyMember, ScheduleConflict, Household } from '../../models';

type ViewMode = 'day' | 'week' | 'month';

interface CalendarDay {
  date: Date;
  dayNumber: number;
  isCurrentMonth: boolean;
  isToday: boolean;
  events: CalendarEvent[];
  hasConflict: boolean;
}

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatIconModule,
    MatChipsModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  templateUrl: './calendar.html',
  styleUrl: './calendar.scss'
})
export class Calendar {
  private eventsService = inject(EventsService);
  private membersService = inject(FamilyMembersService);
  private conflictsService = inject(ConflictsService);
  private householdsService = inject(HouseholdsService);
  private dialog = inject(MatDialog);

  currentDate$ = new BehaviorSubject<Date>(new Date());
  viewMode$ = new BehaviorSubject<ViewMode>('month');
  selectedMembers$ = new BehaviorSubject<string[]>([]);
  selectedHousehold$ = new BehaviorSubject<string | null>(null);

  weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

  households$ = this.householdsService.getHouseholds();

  members$ = this.selectedHousehold$.pipe(
    switchMap(householdId => {
      if (householdId) {
        return this.membersService.getFamilyMembers({ householdId });
      }
      return this.membersService.getFamilyMembers();
    })
  );

  viewModel$ = combineLatest([
    this.eventsService.getEvents(),
    this.members$,
    this.conflictsService.getConflicts(),
    this.currentDate$,
    this.viewMode$,
    this.selectedMembers$,
    this.selectedHousehold$
  ]).pipe(
    map(([events, members, conflicts, currentDate, viewMode, selectedMembers, selectedHousehold]) => {
      let filteredEvents = events;

      // Filter events by household members if a household is selected
      if (selectedHousehold) {
        const householdMemberIds = members.map(m => m.memberId);
        filteredEvents = events.filter(e => householdMemberIds.includes(e.creatorId));
      }

      // Further filter by selected members
      if (selectedMembers.length > 0) {
        filteredEvents = filteredEvents.filter(e => selectedMembers.includes(e.creatorId));
      }

      return {
        events: filteredEvents,
        members,
        conflicts,
        currentDate,
        viewMode,
        selectedMembers,
        selectedHousehold,
        calendarDays: this.generateCalendarDays(currentDate, filteredEvents, conflicts),
        monthTitle: this.formatMonthTitle(currentDate)
      };
    })
  );

  private generateCalendarDays(currentDate: Date, events: CalendarEvent[], conflicts: ScheduleConflict[]): CalendarDay[] {
    const days: CalendarDay[] = [];
    const year = currentDate.getFullYear();
    const month = currentDate.getMonth();
    const today = new Date();

    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);
    const startDay = firstDay.getDay();
    const totalDays = lastDay.getDate();

    const prevMonthLastDay = new Date(year, month, 0).getDate();
    for (let i = startDay - 1; i >= 0; i--) {
      const date = new Date(year, month - 1, prevMonthLastDay - i);
      days.push(this.createCalendarDay(date, false, today, events, conflicts));
    }

    for (let i = 1; i <= totalDays; i++) {
      const date = new Date(year, month, i);
      days.push(this.createCalendarDay(date, true, today, events, conflicts));
    }

    const remainingDays = 42 - days.length;
    for (let i = 1; i <= remainingDays; i++) {
      const date = new Date(year, month + 1, i);
      days.push(this.createCalendarDay(date, false, today, events, conflicts));
    }

    return days;
  }

  private createCalendarDay(
    date: Date,
    isCurrentMonth: boolean,
    today: Date,
    events: CalendarEvent[],
    conflicts: ScheduleConflict[]
  ): CalendarDay {
    const dayEvents = this.getEventsForDate(events, date);
    const hasConflict = dayEvents.some(e =>
      conflicts.some(c => c.conflictingEventIds.includes(e.eventId) && !c.isResolved)
    );

    return {
      date,
      dayNumber: date.getDate(),
      isCurrentMonth,
      isToday: this.isSameDay(date, today),
      events: dayEvents.slice(0, 3),
      hasConflict
    };
  }

  private getEventsForDate(events: CalendarEvent[], date: Date): CalendarEvent[] {
    return events.filter(event => {
      const eventDate = new Date(event.startTime);
      return this.isSameDay(eventDate, date);
    }).sort((a, b) => new Date(a.startTime).getTime() - new Date(b.startTime).getTime());
  }

  private isSameDay(date1: Date, date2: Date): boolean {
    return (
      date1.getFullYear() === date2.getFullYear() &&
      date1.getMonth() === date2.getMonth() &&
      date1.getDate() === date2.getDate()
    );
  }

  private formatMonthTitle(date: Date): string {
    return date.toLocaleDateString('en-US', { month: 'long', year: 'numeric' });
  }

  previousMonth(): void {
    const current = this.currentDate$.value;
    this.currentDate$.next(new Date(current.getFullYear(), current.getMonth() - 1, 1));
  }

  nextMonth(): void {
    const current = this.currentDate$.value;
    this.currentDate$.next(new Date(current.getFullYear(), current.getMonth() + 1, 1));
  }

  goToToday(): void {
    this.currentDate$.next(new Date());
  }

  setViewMode(mode: ViewMode): void {
    this.viewMode$.next(mode);
  }

  onHouseholdFilterChange(householdId: string | null): void {
    this.selectedHousehold$.next(householdId);
    // Reset member selection when household changes
    this.selectedMembers$.next([]);
  }

  toggleMemberFilter(memberId: string): void {
    const current = this.selectedMembers$.value;
    const index = current.indexOf(memberId);
    if (index === -1) {
      this.selectedMembers$.next([...current, memberId]);
    } else {
      this.selectedMembers$.next(current.filter(id => id !== memberId));
    }
  }

  isMemberSelected(memberId: string): boolean {
    return this.selectedMembers$.value.includes(memberId);
  }

  getMemberColor(members: FamilyMember[], creatorId: string): string {
    const member = members.find(m => m.memberId === creatorId);
    return member?.color || '#7c3aed';
  }

  getMoreEventsCount(day: CalendarDay, events: CalendarEvent[]): number {
    const allEvents = this.getEventsForDate(events, day.date);
    return Math.max(0, allEvents.length - 3);
  }

  openAddEventDialog(members: FamilyMember[]): void {
    if (!members || members.length === 0) {
      console.error('Cannot create event: No family members available');
      return;
    }

    const dialogRef = this.dialog.open(EventDialog, {
      width: '600px',
      data: {
        members,
        familyId: members[0].familyId,
        creatorId: members[0].memberId
      } as EventDialogData
    });

    dialogRef.afterClosed().subscribe((result: EventDialogResult) => {
      if (result?.action === 'create' && result.data) {
        this.eventsService.createEvent(result.data).subscribe({
          next: (event) => {
            console.log('Event created successfully:', event);
            window.location.reload();
          },
          error: (error) => {
            console.error('Error creating event:', error);
          }
        });
      }
    });
  }

  onDayClick(day: CalendarDay): void {
    this.currentDate$.next(day.date);
  }
}
