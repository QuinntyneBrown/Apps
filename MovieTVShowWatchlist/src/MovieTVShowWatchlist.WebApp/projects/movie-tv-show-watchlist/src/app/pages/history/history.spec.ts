import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { History } from './history';
import { HistoryService } from '../../services';
import { of } from 'rxjs';

describe('History', () => {
  let component: History;
  let fixture: ComponentFixture<History>;

  const mockRecords = [
    {
      viewingRecordId: '1',
      title: 'Test Movie',
      contentType: 'movie',
      watchDate: new Date(),
      platform: 'Netflix',
      rating: 5,
      isRewatch: false,
      runtime: 120
    }
  ];

  const mockHistoryService = {
    loadHistory: jest.fn().mockReturnValue(of(mockRecords)),
    history$: of(mockRecords)
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [History, NoopAnimationsModule],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: HistoryService, useValue: mockHistoryService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(History);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('getStars', () => {
    it('should return correct star rating for 5', () => {
      expect(component.getStars(5)).toBe('★★★★★');
    });

    it('should return correct star rating for 3', () => {
      expect(component.getStars(3)).toBe('★★★☆☆');
    });

    it('should return correct star rating for 1', () => {
      expect(component.getStars(1)).toBe('★☆☆☆☆');
    });

    it('should return empty string for undefined', () => {
      expect(component.getStars(undefined)).toBe('');
    });
  });

  describe('formatRuntime', () => {
    it('should format runtime with hours and minutes', () => {
      expect(component.formatRuntime(120)).toBe('2h 0m');
    });

    it('should format runtime less than an hour', () => {
      expect(component.formatRuntime(45)).toBe('45m');
    });

    it('should format runtime with hours and remaining minutes', () => {
      expect(component.formatRuntime(152)).toBe('2h 32m');
    });
  });
});
