import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { Dashboard } from './dashboard';
import { DateType, RecurrencePattern } from '../../models';
import { apiBaseUrl } from '../../services';

describe('Dashboard', () => {
  let component: Dashboard;
  let fixture: ComponentFixture<Dashboard>;
  let httpMock: HttpTestingController;

  const mockDates = [
    {
      dateId: '1',
      userId: 'user1',
      personName: 'John Doe',
      dateType: DateType.Birthday,
      dateValue: new Date(new Date().setDate(new Date().getDate() + 1)),
      recurrencePattern: RecurrencePattern.Annual,
      relationship: 'Friend',
      notes: 'Birthday party',
      isActive: true,
      createdAt: new Date()
    }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Dashboard],
      providers: [
        provideRouter([]),
        provideHttpClient(),
        provideHttpClientTesting(),
        provideAnimationsAsync()
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/upcoming`);
    req.flush([]);
  });

  it('should display upcoming dates', () => {
    fixture.detectChanges();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/upcoming`);
    req.flush(mockDates);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.dashboard__title')?.textContent).toContain('Upcoming Celebrations');
  });

  it('should return correct days label', () => {
    httpMock.expectOne(`${apiBaseUrl}/api/dates/upcoming`).flush([]);

    expect(component.getDaysLabel(0)).toBe('Today');
    expect(component.getDaysLabel(1)).toBe('Tomorrow');
    expect(component.getDaysLabel(5)).toBe('In 5 days');
  });

  it('should show empty state when no dates', () => {
    fixture.detectChanges();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/upcoming`);
    req.flush([]);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.dashboard__empty')).toBeTruthy();
  });
});
