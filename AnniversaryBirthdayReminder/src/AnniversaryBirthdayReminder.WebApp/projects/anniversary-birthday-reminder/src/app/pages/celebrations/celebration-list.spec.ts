import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CelebrationList } from './celebration-list';
import { CelebrationStatus } from '../../models';
import { apiBaseUrl } from '../../services';

describe('CelebrationList', () => {
  let component: CelebrationList;
  let fixture: ComponentFixture<CelebrationList>;
  let httpMock: HttpTestingController;

  const mockCelebrations = [
    {
      celebrationId: '1',
      dateId: 'date1',
      celebrationDate: new Date(),
      notes: 'Great party!',
      photos: [],
      attendees: ['John'],
      rating: 5,
      status: CelebrationStatus.Completed
    }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CelebrationList],
      providers: [
        provideRouter([]),
        provideHttpClient(),
        provideHttpClientTesting(),
        provideAnimationsAsync()
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CelebrationList);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    const req = httpMock.expectOne(`${apiBaseUrl}/api/celebrations`);
    req.flush([]);
  });

  it('should display celebration history title', () => {
    fixture.detectChanges();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/celebrations`);
    req.flush([]);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.celebration-list__title')?.textContent).toContain('Celebration History');
  });

  it('should return correct status icon', () => {
    httpMock.expectOne(`${apiBaseUrl}/api/celebrations`).flush([]);

    expect(component.getStatusIcon(CelebrationStatus.Completed)).toBe('check_circle');
    expect(component.getStatusIcon(CelebrationStatus.Skipped)).toBe('cancel');
  });

  it('should return correct status color', () => {
    httpMock.expectOne(`${apiBaseUrl}/api/celebrations`).flush([]);

    expect(component.getStatusColor(CelebrationStatus.Completed)).toBe('primary');
    expect(component.getStatusColor(CelebrationStatus.Skipped)).toBe('warn');
  });

  it('should show empty state when no celebrations', () => {
    fixture.detectChanges();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/celebrations`);
    req.flush([]);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.celebration-list__empty')).toBeTruthy();
  });
});
