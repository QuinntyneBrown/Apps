import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Statistics } from './statistics';
import { StatsService } from '../../services';
import { of } from 'rxjs';

describe('Statistics', () => {
  let component: Statistics;
  let fixture: ComponentFixture<Statistics>;
  let statsService: jest.Mocked<StatsService>;

  const mockStats = {
    moviesWatched: 100,
    showsWatched: 50,
    hoursWatched: 300,
    averageRating: 4.5,
    currentStreak: 5,
    longestStreak: 15,
    milestones: 3,
    nextMilestone: '150 movies',
    moviesChange: 10,
    showsChange: 5,
    hoursChange: 30,
    genreBreakdown: [{ genre: 'Drama', percentage: 40 }],
    monthlyData: [{ month: 'Jan', count: 10 }]
  };

  const mockStatsService = {
    loadStats: jest.fn().mockReturnValue(of(mockStats)),
    stats$: of(mockStats),
    period$: of('this-year'),
    setPeriod: jest.fn()
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Statistics, NoopAnimationsModule],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: StatsService, useValue: mockStatsService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Statistics);
    component = fixture.componentInstance;
    statsService = TestBed.inject(StatsService) as jest.Mocked<StatsService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have period options', () => {
    expect(component.periodOptions.length).toBe(4);
  });

  describe('onPeriodChange', () => {
    it('should call service setPeriod', () => {
      component.onPeriodChange('this-month');
      expect(statsService.setPeriod).toHaveBeenCalledWith('this-month');
    });
  });

  describe('getMaxCount', () => {
    it('should return max count from monthly data', () => {
      const data = [
        { month: 'Jan', count: 10 },
        { month: 'Feb', count: 20 },
        { month: 'Mar', count: 15 }
      ];
      expect(component.getMaxCount(data)).toBe(20);
    });
  });

  describe('getBarHeight', () => {
    it('should calculate correct bar height percentage', () => {
      expect(component.getBarHeight(10, 20)).toBe('50%');
    });

    it('should return 100% for max value', () => {
      expect(component.getBarHeight(20, 20)).toBe('100%');
    });
  });
});
