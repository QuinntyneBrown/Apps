import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { WatchlistCard } from './watchlist-card';
import { WatchlistItem } from '../../models';

describe('WatchlistCard', () => {
  let component: WatchlistCard;
  let fixture: ComponentFixture<WatchlistCard>;
  const mockItem: WatchlistItem = {
    watchlistItemId: '1',
    title: 'Test Movie',
    contentType: 'movie',
    releaseYear: 2020,
    genres: ['action', 'drama'],
    priority: 'high',
    platform: 'Netflix',
    runtime: 120,
    addedDate: new Date(),
    mood: 'action-packed'
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WatchlistCard, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(WatchlistCard);
    component = fixture.componentInstance;
    component.item = mockItem;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('isMovie', () => {
    it('should return true for movie content type', () => {
      expect(component.isMovie).toBe(true);
    });

    it('should return false for tvshow content type', () => {
      component.item = { ...mockItem, contentType: 'tvshow' };
      expect(component.isMovie).toBe(false);
    });
  });

  describe('formattedDuration', () => {
    it('should format runtime for movies', () => {
      expect(component.formattedDuration).toBe('120 min');
    });

    it('should format seasons for TV shows', () => {
      component.item = { ...mockItem, runtime: undefined, seasons: 5 };
      expect(component.formattedDuration).toBe('5 Seasons');
    });

    it('should return empty string if no runtime or seasons', () => {
      component.item = { ...mockItem, runtime: undefined };
      expect(component.formattedDuration).toBe('');
    });
  });

  describe('addedAgo', () => {
    it('should return "Added today" for items added today', () => {
      component.item = { ...mockItem, addedDate: new Date() };
      expect(component.addedAgo).toBe('Added today');
    });

    it('should return "Added yesterday" for items added yesterday', () => {
      const yesterday = new Date();
      yesterday.setDate(yesterday.getDate() - 1);
      component.item = { ...mockItem, addedDate: yesterday };
      expect(component.addedAgo).toBe('Added yesterday');
    });

    it('should return "Added X days ago" for recent items', () => {
      const fiveDaysAgo = new Date();
      fiveDaysAgo.setDate(fiveDaysAgo.getDate() - 5);
      component.item = { ...mockItem, addedDate: fiveDaysAgo };
      expect(component.addedAgo).toBe('Added 5 days ago');
    });
  });

  describe('priorityClass', () => {
    it('should return correct class for high priority', () => {
      expect(component.priorityClass).toBe('watchlist-card__priority--high');
    });

    it('should return correct class for medium priority', () => {
      component.item = { ...mockItem, priority: 'medium' };
      expect(component.priorityClass).toBe('watchlist-card__priority--medium');
    });

    it('should return correct class for low priority', () => {
      component.item = { ...mockItem, priority: 'low' };
      expect(component.priorityClass).toBe('watchlist-card__priority--low');
    });
  });

  describe('onWatch', () => {
    it('should emit watch event with item', () => {
      jest.spyOn(component.watch, 'emit');
      component.onWatch();
      expect(component.watch.emit).toHaveBeenCalledWith(mockItem);
    });
  });

  describe('onRemove', () => {
    it('should emit remove event with item id', () => {
      jest.spyOn(component.remove, 'emit');
      component.onRemove();
      expect(component.remove.emit).toHaveBeenCalledWith('1');
    });
  });

  describe('formatGenre', () => {
    it('should capitalize first letter of genre', () => {
      expect(component.formatGenre('action')).toBe('Action');
    });
  });
});
