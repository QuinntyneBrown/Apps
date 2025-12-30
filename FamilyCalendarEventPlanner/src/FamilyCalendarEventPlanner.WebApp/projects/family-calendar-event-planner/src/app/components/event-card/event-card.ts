import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CalendarEvent, FamilyMember, EventAttendee } from '../../services/models';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatChipsModule, MatIconModule, MatButtonModule, DatePipe],
  templateUrl: './event-card.html',
  styleUrl: './event-card.scss'
})
export class EventCard {
  @Input() event!: CalendarEvent;
  @Input() members: FamilyMember[] = [];
  @Input() attendees: EventAttendee[] = [];
  @Input() hasConflict = false;
  @Output() eventClick = new EventEmitter<CalendarEvent>();

  getEventIcon(): string {
    const icons: Record<string, string> = {
      'Appointment': 'event',
      'FamilyDinner': 'restaurant',
      'Sports': 'sports_soccer',
      'School': 'school',
      'Vacation': 'flight',
      'Birthday': 'cake',
      'Other': 'event_note'
    };
    return icons[this.event.eventType] || 'event';
  }

  getMemberColor(memberId: string): string {
    const member = this.members.find(m => m.memberId === memberId);
    return member?.color || '#7c3aed';
  }

  getMemberInitials(memberId: string): string {
    const member = this.members.find(m => m.memberId === memberId);
    if (!member) return '?';
    const names = member.name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  getAttendeeIds(): string[] {
    return this.attendees.map(a => a.familyMemberId);
  }

  onClick(): void {
    this.eventClick.emit(this.event);
  }
}
