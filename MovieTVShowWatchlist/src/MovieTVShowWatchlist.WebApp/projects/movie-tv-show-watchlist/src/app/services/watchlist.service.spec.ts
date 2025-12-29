import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { WatchlistService } from './watchlist.service';
import { API_CONFIG } from './api-config';
import { WatchlistItem } from '../models';

describe('WatchlistService', () => {
  let service: WatchlistService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://test-api.com';

  const mockItem: WatchlistItem = {
    watchlistItemId: '1',
    title: 'Test Movie',
    contentType: 'movie',
    releaseYear: 2020,
    genres: ['action'],
    priority: 'high',
    platform: 'Netflix',
    runtime: 120,
    addedDate: new Date()
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        WatchlistService,
        { provide: API_CONFIG, useValue: { baseUrl } }
      ]
    });
    service = TestBed.inject(WatchlistService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('loadWatchlist', () => {
    it('should load watchlist items from API', () => {
      const mockItems: WatchlistItem[] = [mockItem];

      service.loadWatchlist().subscribe(items => {
        expect(items).toEqual(mockItems);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist`);
      expect(req.request.method).toBe('GET');
      req.flush(mockItems);
    });

    it('should return cached data on subsequent calls', () => {
      const mockItems: WatchlistItem[] = [];

      service.loadWatchlist().subscribe();
      httpMock.expectOne(`${baseUrl}/api/watchlist`).flush(mockItems);

      service.loadWatchlist().subscribe(items => {
        expect(items).toEqual([]);
      });

      httpMock.expectNone(`${baseUrl}/api/watchlist`);
    });

    it('should use mock data on error', () => {
      service.loadWatchlist().subscribe(items => {
        expect(items.length).toBeGreaterThan(0);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('addItem', () => {
    it('should add item to watchlist', () => {
      const newItem = {
        title: 'New Movie',
        contentType: 'movie' as const,
        releaseYear: 2023,
        genres: ['drama' as const],
        priority: 'medium' as const,
        platform: 'HBO'
      };

      service.addItem(newItem).subscribe(item => {
        expect(item.title).toBe('New Movie');
        expect(item.watchlistItemId).toBeTruthy();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist`);
      expect(req.request.method).toBe('POST');
      req.flush({ ...newItem, watchlistItemId: 'new-id', addedDate: new Date() });
    });

    it('should handle add error and add locally', () => {
      const newItem = {
        title: 'New Movie',
        contentType: 'movie' as const,
        releaseYear: 2023,
        genres: ['drama' as const],
        priority: 'medium' as const,
        platform: 'HBO'
      };

      service.addItem(newItem).subscribe(item => {
        expect(item.title).toBe('New Movie');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('removeItem', () => {
    it('should remove item from watchlist', () => {
      service.removeItem('test-id').subscribe();

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist/test-id`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });

    it('should handle remove error and remove locally', () => {
      service.removeItem('test-id').subscribe();

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist/test-id`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('updatePriority', () => {
    it('should update item priority', () => {
      // First load items
      service.loadWatchlist().subscribe();
      httpMock.expectOne(`${baseUrl}/api/watchlist`).flush([mockItem]);

      service.updatePriority('1', 'low').subscribe();

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist/1`);
      expect(req.request.method).toBe('PATCH');
      req.flush({ ...mockItem, priority: 'low' });
    });

    it('should handle update error', () => {
      service.loadWatchlist().subscribe();
      httpMock.expectOne(`${baseUrl}/api/watchlist`).flush([mockItem]);

      service.updatePriority('1', 'low').subscribe();

      const req = httpMock.expectOne(`${baseUrl}/api/watchlist/1`);
      req.error(new ProgressEvent('error'));
    });
  });

  describe('filter operations', () => {
    it('should set filter', () => {
      service.setFilter({ genres: ['action'] });

      service.filter$.subscribe(filter => {
        expect(filter.genres).toContain('action');
      });
    });

    it('should merge filter with existing', () => {
      service.setFilter({ genres: ['action'] });
      service.setFilter({ moods: ['relaxing'] });

      service.filter$.subscribe(filter => {
        expect(filter.genres).toContain('action');
        expect(filter.moods).toContain('relaxing');
      });
    });

    it('should clear filters', () => {
      service.setFilter({ genres: ['action'] });
      service.clearFilters();

      service.filter$.subscribe(filter => {
        expect(filter.genres).toEqual([]);
        expect(filter.contentTypes).toEqual(['movie', 'tvshow']);
      });
    });
  });

  describe('sort operations', () => {
    it('should set sort to title', () => {
      service.setSort('title');
      service.sort$.subscribe(sort => {
        expect(sort).toBe('title');
      });
    });

    it('should set sort to recently-added', () => {
      service.setSort('recently-added');
      service.sort$.subscribe(sort => {
        expect(sort).toBe('recently-added');
      });
    });

    it('should set sort to release-year', () => {
      service.setSort('release-year');
      service.sort$.subscribe(sort => {
        expect(sort).toBe('release-year');
      });
    });
  });

  describe('getItemCount', () => {
    it('should return 0 initially', () => {
      expect(service.getItemCount()).toBe(0);
    });

    it('should return count after loading', () => {
      service.loadWatchlist().subscribe();
      httpMock.expectOne(`${baseUrl}/api/watchlist`).flush([mockItem]);
      expect(service.getItemCount()).toBe(1);
    });
  });

  describe('filteredWatchlist$', () => {
    it('should return filtered items', (done) => {
      service.loadWatchlist().subscribe();
      httpMock.expectOne(`${baseUrl}/api/watchlist`).flush([mockItem]);

      service.filteredWatchlist$.subscribe(items => {
        expect(items.length).toBe(1);
        done();
      });
    });
  });

  describe('without API_CONFIG', () => {
    it('should use default baseUrl', () => {
      TestBed.resetTestingModule();
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        providers: [WatchlistService]
      });
      const serviceNoConfig = TestBed.inject(WatchlistService);
      const httpMockNoConfig = TestBed.inject(HttpTestingController);

      serviceNoConfig.loadWatchlist().subscribe();
      const req = httpMockNoConfig.expectOne('http://localhost:5000/api/watchlist');
      req.flush([]);
    });
  });
});
