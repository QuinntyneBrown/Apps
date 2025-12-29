import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StatsService } from './stats.service';
import { API_CONFIG } from './api-config';
import { ViewingStats } from '../models';

describe('StatsService', () => {
  let service: StatsService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://test-api.com';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        StatsService,
        { provide: API_CONFIG, useValue: { baseUrl } }
      ]
    });
    service = TestBed.inject(StatsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('loadStats', () => {
    it('should load stats from API', () => {
      const mockStats: ViewingStats = {
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

      service.loadStats('this-year').subscribe(stats => {
        expect(stats).toEqual(mockStats);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/stats?period=this-year`);
      expect(req.request.method).toBe('GET');
      req.flush(mockStats);
    });

    it('should use mock data on error', () => {
      service.loadStats().subscribe(stats => {
        expect(stats.moviesWatched).toBe(142);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/stats?period=this-year`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('setPeriod', () => {
    it('should update period and load stats', () => {
      service.setPeriod('this-month');

      service.period$.subscribe(period => {
        expect(period).toBe('this-month');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/stats?period=this-month`);
      req.flush({});
    });
  });
});
