import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Watchlist } from './watchlist';
import { WatchlistService } from '../../services';
import { of } from 'rxjs';

describe('Watchlist', () => {
  let component: Watchlist;
  let fixture: ComponentFixture<Watchlist>;
  let watchlistService: jest.Mocked<WatchlistService>;

  const mockWatchlistService = {
    loadWatchlist: jest.fn().mockReturnValue(of([])),
    filteredWatchlist$: of([]),
    filter$: of({
      contentTypes: ['movie', 'tvshow'],
      genres: [],
      moods: [],
      availableNow: false,
      comingSoon: false,
      unavailable: false
    }),
    sort$: of('priority'),
    setFilter: jest.fn(),
    clearFilters: jest.fn(),
    setSort: jest.fn(),
    removeItem: jest.fn().mockReturnValue(of(void 0))
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Watchlist, NoopAnimationsModule],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: WatchlistService, useValue: mockWatchlistService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Watchlist);
    component = fixture.componentInstance;
    watchlistService = TestBed.inject(WatchlistService) as jest.Mocked<WatchlistService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have sort options', () => {
    expect(component.sortOptions.length).toBe(4);
  });

  describe('onFilterChange', () => {
    it('should call service setFilter', () => {
      component.onFilterChange({ genres: ['action'] });
      expect(watchlistService.setFilter).toHaveBeenCalledWith({ genres: ['action'] });
    });
  });

  describe('onClearFilters', () => {
    it('should call service clearFilters', () => {
      component.onClearFilters();
      expect(watchlistService.clearFilters).toHaveBeenCalled();
    });
  });

  describe('onSortChange', () => {
    it('should call service setSort', () => {
      component.onSortChange('title');
      expect(watchlistService.setSort).toHaveBeenCalledWith('title');
    });
  });

  describe('onRemove', () => {
    it('should call service removeItem', () => {
      component.onRemove('test-id');
      expect(watchlistService.removeItem).toHaveBeenCalledWith('test-id');
    });
  });
});
