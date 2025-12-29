import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RecommendationService } from './recommendation.service';
import { API_CONFIG } from './api-config';
import { Recommendation } from '../models';

describe('RecommendationService', () => {
  let service: RecommendationService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://test-api.com';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        RecommendationService,
        { provide: API_CONFIG, useValue: { baseUrl } }
      ]
    });
    service = TestBed.inject(RecommendationService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('loadRecommendations', () => {
    it('should load recommendations from API', () => {
      const mockRecs: Recommendation[] = [
        {
          recommendationId: '1',
          title: 'Recommended Movie',
          contentType: 'movie',
          releaseYear: 2020,
          genres: ['action'],
          matchScore: 90,
          reason: 'Based on your history',
          source: 'system'
        }
      ];

      service.loadRecommendations().subscribe(recs => {
        expect(recs).toEqual(mockRecs);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recommendations`);
      expect(req.request.method).toBe('GET');
      req.flush(mockRecs);
    });

    it('should use mock data on error', () => {
      service.loadRecommendations().subscribe(recs => {
        expect(recs.length).toBeGreaterThan(0);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recommendations`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('dismissRecommendation', () => {
    it('should dismiss recommendation', () => {
      service.dismissRecommendation('test-id').subscribe();

      const req = httpMock.expectOne(`${baseUrl}/api/recommendations/test-id`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });
});
