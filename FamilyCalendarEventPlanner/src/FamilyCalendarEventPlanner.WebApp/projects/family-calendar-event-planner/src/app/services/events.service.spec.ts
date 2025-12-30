import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { EventsService } from './events.service';
import { CalendarEvent, CreateEventRequest, EventType, EventStatus, RecurrenceFrequency } from './models';

describe('EventsService', () => {
  let service: EventsService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:3200';

  const mockEvent: CalendarEvent = {
    eventId: '1',
    familyId: 'family1',
    creatorId: 'member1',
    title: 'Test Event',
    description: 'Test Description',
    startTime: '2025-12-29T10:00:00Z',
    endTime: '2025-12-29T11:00:00Z',
    location: 'Test Location',
    eventType: EventType.Other,
    recurrencePattern: { frequency: RecurrenceFrequency.None, interval: 1, endDate: null, daysOfWeek: [] },
    status: EventStatus.Scheduled
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [EventsService]
    });
    service = TestBed.inject(EventsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getEvents', () => {
    it('should get all events without familyId', () => {
      service.getEvents().subscribe(events => {
        expect(events).toEqual([mockEvent]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events`);
      expect(req.request.method).toBe('GET');
      req.flush([mockEvent]);
    });

    it('should get events with familyId filter', () => {
      service.getEvents('family1').subscribe(events => {
        expect(events).toEqual([mockEvent]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events?familyId=family1`);
      expect(req.request.method).toBe('GET');
      req.flush([mockEvent]);
    });
  });

  describe('getEventById', () => {
    it('should get event by id', () => {
      service.getEventById('1').subscribe(event => {
        expect(event).toEqual(mockEvent);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events/1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockEvent);
    });
  });

  describe('createEvent', () => {
    it('should create a new event', () => {
      const createRequest: CreateEventRequest = {
        familyId: 'family1',
        creatorId: 'member1',
        title: 'New Event',
        startTime: '2025-12-29T10:00:00Z',
        endTime: '2025-12-29T11:00:00Z',
        eventType: EventType.Other
      };

      service.createEvent(createRequest).subscribe(event => {
        expect(event).toEqual(mockEvent);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush(mockEvent);
    });
  });

  describe('updateEvent', () => {
    it('should update an event', () => {
      const updateRequest = { eventId: '1', title: 'Updated Event' };

      service.updateEvent(updateRequest).subscribe(event => {
        expect(event.title).toBe(mockEvent.title);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events/1`);
      expect(req.request.method).toBe('PUT');
      req.flush(mockEvent);
    });
  });

  describe('cancelEvent', () => {
    it('should cancel an event', () => {
      service.cancelEvent('1').subscribe(event => {
        expect(event).toEqual(mockEvent);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events/1/cancel`);
      expect(req.request.method).toBe('POST');
      req.flush(mockEvent);
    });
  });

  describe('completeEvent', () => {
    it('should mark an event as complete', () => {
      service.completeEvent('1').subscribe(event => {
        expect(event).toEqual(mockEvent);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/events/1/complete`);
      expect(req.request.method).toBe('POST');
      req.flush(mockEvent);
    });
  });
});
