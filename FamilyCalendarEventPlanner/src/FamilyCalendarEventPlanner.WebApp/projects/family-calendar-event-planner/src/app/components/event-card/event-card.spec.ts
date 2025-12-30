import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EventCard } from './event-card';
import { CalendarEvent, FamilyMember, EventAttendee, EventType, EventStatus, RecurrenceFrequency, MemberRole, RSVPStatus } from '../../services/models';

describe('EventCard', () => {
  let component: EventCard;
  let fixture: ComponentFixture<EventCard>;

  const mockEvent: CalendarEvent = {
    eventId: '1',
    familyId: 'family1',
    creatorId: 'member1',
    title: 'Test Event',
    description: 'Test Description',
    startTime: '2025-12-29T10:00:00Z',
    endTime: '2025-12-29T11:00:00Z',
    location: 'Test Location',
    eventType: EventType.Sports,
    recurrencePattern: { frequency: RecurrenceFrequency.None, interval: 1, endDate: null, daysOfWeek: [] },
    status: EventStatus.Scheduled
  };

  const mockMembers: FamilyMember[] = [
    { memberId: 'member1', familyId: 'family1', name: 'John Doe', email: 'john@example.com', color: '#3b82f6', role: MemberRole.Admin }
  ];

  const mockAttendees: EventAttendee[] = [
    { attendeeId: 'att1', eventId: '1', familyMemberId: 'member1', rsvpStatus: RSVPStatus.Accepted, responseTime: null, notes: '' }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventCard]
    }).compileComponents();

    fixture = TestBed.createComponent(EventCard);
    component = fixture.componentInstance;
    component.event = mockEvent;
    component.members = mockMembers;
    component.attendees = mockAttendees;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display event title', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Test Event');
  });

  it('should display location', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Test Location');
  });

  it('should return correct event icon for Sports', () => {
    expect(component.getEventIcon()).toBe('sports_soccer');
  });

  it('should return correct event icon for Appointment', () => {
    component.event = { ...mockEvent, eventType: EventType.Appointment };
    expect(component.getEventIcon()).toBe('event');
  });

  it('should return correct event icon for Birthday', () => {
    component.event = { ...mockEvent, eventType: EventType.Birthday };
    expect(component.getEventIcon()).toBe('cake');
  });

  it('should get member color', () => {
    expect(component.getMemberColor('member1')).toBe('#3b82f6');
  });

  it('should return default color for unknown member', () => {
    expect(component.getMemberColor('unknown')).toBe('#7c3aed');
  });

  it('should get member initials', () => {
    expect(component.getMemberInitials('member1')).toBe('JD');
  });

  it('should return ? for unknown member', () => {
    expect(component.getMemberInitials('unknown')).toBe('?');
  });

  it('should emit eventClick on click', () => {
    jest.spyOn(component.eventClick, 'emit');
    component.onClick();
    expect(component.eventClick.emit).toHaveBeenCalledWith(mockEvent);
  });

  it('should show conflict indicator when hasConflict is true', () => {
    component.hasConflict = true;
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.event-card--conflict')).toBeTruthy();
  });

  it('should get attendee ids', () => {
    const ids = component.getAttendeeIds();
    expect(ids).toEqual(['member1']);
  });
});
