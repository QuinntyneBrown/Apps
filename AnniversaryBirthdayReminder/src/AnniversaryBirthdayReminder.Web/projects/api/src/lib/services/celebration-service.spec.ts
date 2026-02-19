import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { CelebrationService } from './celebration-service';
import { CelebrationStatus, Celebration } from '../models';
import { apiBaseUrl } from './api-config';

describe('CelebrationService', () => {
  let service: CelebrationService;
  let httpMock: HttpTestingController;

  const mockCelebration: Celebration = {
    celebrationId: '1',
    dateId: 'date1',
    celebrationDate: new Date(),
    notes: 'Great party!',
    photos: ['photo1.jpg'],
    attendees: ['John', 'Jane'],
    rating: 5,
    status: CelebrationStatus.Completed
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        CelebrationService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(CelebrationService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all celebrations', () => {
    service.getCelebrations().subscribe(celebrations => {
      expect(celebrations.length).toBe(1);
      expect(celebrations[0].notes).toBe('Great party!');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/celebrations`);
    expect(req.request.method).toBe('GET');
    req.flush([mockCelebration]);
  });

  it('should get celebrations by date', () => {
    service.getCelebrationsByDate('date1').subscribe(celebrations => {
      expect(celebrations.length).toBe(1);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/date1/celebrations`);
    expect(req.request.method).toBe('GET');
    req.flush([mockCelebration]);
  });

  it('should get a single celebration', () => {
    service.getCelebration('1').subscribe(celebration => {
      expect(celebration.notes).toBe('Great party!');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/celebrations/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockCelebration);
  });

  it('should mark celebration as completed', () => {
    service.markAsCompleted('date1', { notes: 'Fun celebration!' }).subscribe(celebration => {
      expect(celebration.status).toBe(CelebrationStatus.Completed);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/date1/celebrations`);
    expect(req.request.method).toBe('POST');
    req.flush({ ...mockCelebration, notes: 'Fun celebration!' });
  });

  it('should mark celebration as skipped', () => {
    service.markAsSkipped('date1', 'Could not attend').subscribe(celebration => {
      expect(celebration.status).toBe(CelebrationStatus.Skipped);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/date1/celebrations`);
    expect(req.request.method).toBe('POST');
    req.flush({ ...mockCelebration, status: CelebrationStatus.Skipped, notes: 'Could not attend' });
  });

  it('should return rating stars', () => {
    expect(service.getRatingStars(5)).toBe('★★★★★');
    expect(service.getRatingStars(3)).toBe('★★★☆☆');
    expect(service.getRatingStars(0)).toBe('☆☆☆☆☆');
  });
});
