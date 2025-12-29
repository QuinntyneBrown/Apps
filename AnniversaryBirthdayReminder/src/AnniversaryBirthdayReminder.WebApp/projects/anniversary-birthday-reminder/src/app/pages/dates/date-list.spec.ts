import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DateList } from './date-list';
import { DateType, RecurrencePattern } from '../../models';
import { apiBaseUrl } from '../../services';

describe('DateList', () => {
  let component: DateList;
  let fixture: ComponentFixture<DateList>;
  let httpMock: HttpTestingController;

  const mockDates = [
    {
      dateId: '1',
      userId: 'user1',
      personName: 'John Doe',
      dateType: DateType.Birthday,
      dateValue: new Date(),
      recurrencePattern: RecurrencePattern.Annual,
      relationship: 'Friend',
      notes: '',
      isActive: true,
      createdAt: new Date()
    }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateList],
      providers: [
        provideRouter([]),
        provideHttpClient(),
        provideHttpClientTesting(),
        provideAnimationsAsync()
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(DateList);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates`);
    req.flush([]);
  });

  it('should display dates in table', () => {
    fixture.detectChanges();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates`);
    req.flush(mockDates);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.date-list__title')?.textContent).toContain('Important Dates');
  });

  it('should have correct displayed columns', () => {
    httpMock.expectOne(`${apiBaseUrl}/api/dates`).flush([]);

    expect(component.displayedColumns).toContain('personName');
    expect(component.displayedColumns).toContain('dateType');
    expect(component.displayedColumns).toContain('actions');
  });

  it('should show empty state when no dates', () => {
    fixture.detectChanges();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates`);
    req.flush([]);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.date-list__empty')).toBeTruthy();
  });
});
