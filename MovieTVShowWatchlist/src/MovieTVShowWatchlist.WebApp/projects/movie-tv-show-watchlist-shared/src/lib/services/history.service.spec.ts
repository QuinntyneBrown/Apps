import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HistoryService } from './history.service';
import { API_CONFIG } from './api-config';
import { ViewingRecord } from '../models';

describe('HistoryService', () => {
  let service: HistoryService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://test-api.com';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        HistoryService,
        { provide: API_CONFIG, useValue: { baseUrl } }
      ]
    });
    service = TestBed.inject(HistoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('loadHistory', () => {
    it('should load history from API', () => {
      const mockRecords: ViewingRecord[] = [
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

      service.loadHistory().subscribe(records => {
        expect(records).toEqual(mockRecords);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/history`);
      expect(req.request.method).toBe('GET');
      req.flush(mockRecords);
    });

    it('should use mock data on error', () => {
      service.loadHistory().subscribe(records => {
        expect(records.length).toBeGreaterThan(0);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/history`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('markAsWatched', () => {
    it('should add viewing record', () => {
      const newRecord = {
        title: 'New Movie',
        contentType: 'movie' as const,
        watchDate: new Date(),
        platform: 'Netflix',
        rating: 4,
        isRewatch: false,
        runtime: 100
      };

      service.markAsWatched(newRecord).subscribe(record => {
        expect(record.title).toBe('New Movie');
        expect(record.viewingRecordId).toBeTruthy();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/history`);
      expect(req.request.method).toBe('POST');
      req.flush({ ...newRecord, viewingRecordId: 'new-id' });
    });
  });
});
