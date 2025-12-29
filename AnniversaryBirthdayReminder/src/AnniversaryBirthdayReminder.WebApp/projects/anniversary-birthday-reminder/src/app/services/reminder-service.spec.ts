import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ReminderService } from './reminder-service';
import { DeliveryChannel, ReminderStatus, ReminderSettings } from '../models';
import { apiBaseUrl } from './api-config';

describe('ReminderService', () => {
  let service: ReminderService;
  let httpMock: HttpTestingController;

  const mockSettings: ReminderSettings = {
    oneWeekBefore: true,
    threeDaysBefore: true,
    oneDayBefore: true,
    channels: [DeliveryChannel.Email, DeliveryChannel.Push]
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ReminderService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(ReminderService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get reminders', () => {
    service.getReminders().subscribe(reminders => {
      expect(reminders.length).toBe(1);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/reminders`);
    expect(req.request.method).toBe('GET');
    req.flush([{
      reminderId: '1',
      dateId: 'date1',
      scheduledTime: new Date(),
      advanceNoticeDays: 7,
      deliveryChannel: DeliveryChannel.Email,
      status: ReminderStatus.Scheduled,
      sentAt: null
    }]);
  });

  it('should get reminders by date', () => {
    service.getRemindersByDate('date1').subscribe(reminders => {
      expect(reminders.length).toBe(1);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/date1/reminders`);
    expect(req.request.method).toBe('GET');
    req.flush([{
      reminderId: '1',
      dateId: 'date1',
      scheduledTime: new Date(),
      advanceNoticeDays: 7,
      deliveryChannel: DeliveryChannel.Email,
      status: ReminderStatus.Scheduled,
      sentAt: null
    }]);
  });

  it('should dismiss a reminder', () => {
    service.dismissReminder('1').subscribe();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/reminders/1/dismiss`);
    expect(req.request.method).toBe('POST');
    req.flush(null);
  });

  it('should update settings', () => {
    service.updateSettings(mockSettings).subscribe(settings => {
      expect(settings.oneWeekBefore).toBe(true);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/reminders/settings`);
    expect(req.request.method).toBe('PUT');
    req.flush(mockSettings);
  });

  it('should update local settings', () => {
    const newSettings: ReminderSettings = {
      ...mockSettings,
      oneWeekBefore: false
    };

    service.updateLocalSettings(newSettings);

    service.settings$.subscribe(settings => {
      expect(settings.oneWeekBefore).toBe(false);
    });
  });
});
