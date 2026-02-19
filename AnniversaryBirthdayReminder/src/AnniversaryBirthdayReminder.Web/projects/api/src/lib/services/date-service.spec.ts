import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { DateService } from './date-service';
import { DateType, RecurrencePattern, ImportantDate } from '../models';
import { apiBaseUrl } from './api-config';

describe('DateService', () => {
  let service: DateService;
  let httpMock: HttpTestingController;

  const mockDate: ImportantDate = {
    dateId: '1',
    userId: 'user1',
    personName: 'John Doe',
    dateType: DateType.Birthday,
    dateValue: new Date('2024-03-15'),
    recurrencePattern: RecurrencePattern.Annual,
    relationship: 'Friend',
    notes: 'Test note',
    isActive: true,
    createdAt: new Date()
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        DateService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(DateService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all dates', () => {
    service.getDates().subscribe(dates => {
      expect(dates.length).toBe(1);
      expect(dates[0].personName).toBe('John Doe');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates`);
    expect(req.request.method).toBe('GET');
    req.flush([mockDate]);
  });

  it('should get upcoming dates', () => {
    service.getUpcomingDates().subscribe(dates => {
      expect(dates.length).toBe(1);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/upcoming`);
    expect(req.request.method).toBe('GET');
    req.flush([mockDate]);
  });

  it('should get a single date', () => {
    service.getDate('1').subscribe(date => {
      expect(date.personName).toBe('John Doe');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockDate);
  });

  it('should create a date', () => {
    const newDate = {
      personName: 'Jane Doe',
      dateType: DateType.Anniversary,
      dateValue: new Date(),
      recurrencePattern: RecurrencePattern.Annual,
      relationship: 'Family',
      notes: '',
      isActive: true
    };

    service.createDate(newDate).subscribe(date => {
      expect(date.personName).toBe('Jane Doe');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates`);
    expect(req.request.method).toBe('POST');
    req.flush({ ...newDate, dateId: '2', userId: 'user1', createdAt: new Date() });
  });

  it('should update a date', () => {
    service.updateDate('1', { personName: 'John Smith' }).subscribe(date => {
      expect(date.personName).toBe('John Smith');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/1`);
    expect(req.request.method).toBe('PUT');
    req.flush({ ...mockDate, personName: 'John Smith' });
  });

  it('should delete a date', () => {
    service.deleteDate('1').subscribe();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('should return correct icon for date type', () => {
    expect(service.getDateTypeIcon(DateType.Birthday)).toBe('cake');
    expect(service.getDateTypeIcon(DateType.Anniversary)).toBe('favorite');
    expect(service.getDateTypeIcon(DateType.Custom)).toBe('event');
  });

  it('should calculate days until date', () => {
    const today = new Date();
    const tomorrow = new Date(today);
    tomorrow.setDate(tomorrow.getDate() + 1);

    const tomorrowDate: ImportantDate = {
      ...mockDate,
      dateValue: tomorrow
    };

    const days = service.getDaysUntil(tomorrowDate);
    expect(days).toBe(1);
  });
});
