import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Discover } from './discover';
import { RecommendationService, WatchlistService } from '../../services';
import { of } from 'rxjs';

describe('Discover', () => {
  let component: Discover;
  let fixture: ComponentFixture<Discover>;
  let recommendationService: jest.Mocked<RecommendationService>;
  let watchlistService: jest.Mocked<WatchlistService>;

  const mockRecommendations = [
    {
      recommendationId: '1',
      title: 'Recommended Movie',
      contentType: 'movie' as const,
      releaseYear: 2020,
      genres: ['action' as const],
      matchScore: 90,
      reason: 'Based on your history',
      source: 'system' as const
    }
  ];

  const mockRecommendationService = {
    loadRecommendations: jest.fn().mockReturnValue(of(mockRecommendations)),
    recommendations$: of(mockRecommendations),
    dismissRecommendation: jest.fn().mockReturnValue(of(void 0))
  };

  const mockWatchlistService = {
    addItem: jest.fn().mockReturnValue(of({}))
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Discover, NoopAnimationsModule],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: RecommendationService, useValue: mockRecommendationService },
        { provide: WatchlistService, useValue: mockWatchlistService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Discover);
    component = fixture.componentInstance;
    recommendationService = TestBed.inject(RecommendationService) as jest.Mocked<RecommendationService>;
    watchlistService = TestBed.inject(WatchlistService) as jest.Mocked<WatchlistService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('getSourceIcon', () => {
    it('should return robot icon for system', () => {
      expect(component.getSourceIcon('system')).toBe('🤖');
    });

    it('should return people icon for friend', () => {
      expect(component.getSourceIcon('friend')).toBe('👥');
    });

    it('should return pencil icon for critic', () => {
      expect(component.getSourceIcon('critic')).toBe('📝');
    });

    it('should return lightbulb icon for unknown', () => {
      expect(component.getSourceIcon('unknown')).toBe('💡');
    });
  });

  describe('onAddToWatchlist', () => {
    it('should call watchlist service addItem', () => {
      const recommendation = mockRecommendations[0];
      component.onAddToWatchlist(recommendation);
      expect(watchlistService.addItem).toHaveBeenCalledWith({
        title: 'Recommended Movie',
        contentType: 'movie',
        releaseYear: 2020,
        genres: ['action'],
        priority: 'medium',
        platform: 'Unknown'
      });
    });
  });

  describe('onDismiss', () => {
    it('should call recommendation service dismissRecommendation', () => {
      component.onDismiss('test-id');
      expect(recommendationService.dismissRecommendation).toHaveBeenCalledWith('test-id');
    });
  });

  describe('formatGenre', () => {
    it('should capitalize first letter', () => {
      expect(component.formatGenre('action')).toBe('Action');
    });
  });
});
