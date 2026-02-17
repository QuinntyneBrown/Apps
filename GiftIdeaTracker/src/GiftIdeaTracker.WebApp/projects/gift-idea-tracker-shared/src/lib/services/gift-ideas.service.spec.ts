import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { GiftIdeasService } from './gift-ideas.service';
import { GiftIdea, CreateGiftIdeaRequest, UpdateGiftIdeaRequest, Occasion } from '../models';

describe('GiftIdeasService', () => {
  let service: GiftIdeasService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:5200';

  const mockGiftIdea: GiftIdea = {
    giftIdeaId: 'gift-1',
    userId: 'user-1',
    recipientId: 'recipient-1',
    name: 'New iPhone',
    occasion: Occasion.Birthday,
    estimatedPrice: 999,
    isPurchased: false,
    createdAt: '2025-01-01T00:00:00Z'
  };

  const mockGiftIdeas: GiftIdea[] = [
    mockGiftIdea,
    {
      giftIdeaId: 'gift-2',
      userId: 'user-1',
      recipientId: 'recipient-2',
      name: 'Book Collection',
      occasion: Occasion.Christmas,
      estimatedPrice: 50,
      isPurchased: true,
      createdAt: '2025-01-02T00:00:00Z'
    }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GiftIdeasService]
    });
    service = TestBed.inject(GiftIdeasService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getGiftIdeas', () => {
    it('should return all gift ideas without filter', () => {
      service.getGiftIdeas().subscribe(ideas => {
        expect(ideas).toEqual(mockGiftIdeas);
        expect(ideas.length).toBe(2);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas`);
      expect(req.request.method).toBe('GET');
      req.flush(mockGiftIdeas);
    });

    it('should return gift ideas filtered by recipientId', () => {
      service.getGiftIdeas('recipient-1').subscribe(ideas => {
        expect(ideas.length).toBe(1);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas?recipientId=recipient-1`);
      expect(req.request.method).toBe('GET');
      req.flush([mockGiftIdea]);
    });

    it('should return empty array when no gift ideas exist', () => {
      service.getGiftIdeas().subscribe(ideas => {
        expect(ideas).toEqual([]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas`);
      req.flush([]);
    });
  });

  describe('getGiftIdea', () => {
    it('should return a single gift idea by id', () => {
      service.getGiftIdea('gift-1').subscribe(idea => {
        expect(idea).toEqual(mockGiftIdea);
        expect(idea.name).toBe('New iPhone');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas/gift-1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockGiftIdea);
    });
  });

  describe('createGiftIdea', () => {
    it('should create a new gift idea', () => {
      const createRequest: CreateGiftIdeaRequest = {
        recipientId: 'recipient-1',
        name: 'New Gift',
        occasion: Occasion.Birthday,
        estimatedPrice: 100
      };

      service.createGiftIdea(createRequest).subscribe(idea => {
        expect(idea.name).toBe('New Gift');
        expect(idea.occasion).toBe(Occasion.Birthday);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush({
        ...mockGiftIdea,
        name: 'New Gift'
      });
    });

    it('should create a gift idea without recipient', () => {
      const createRequest: CreateGiftIdeaRequest = {
        name: 'General Gift',
        occasion: Occasion.Other
      };

      service.createGiftIdea(createRequest).subscribe(idea => {
        expect(idea.name).toBe('General Gift');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas`);
      expect(req.request.body).toEqual(createRequest);
      req.flush({
        ...mockGiftIdea,
        name: 'General Gift',
        recipientId: null
      });
    });
  });

  describe('updateGiftIdea', () => {
    it('should update an existing gift idea', () => {
      const updateRequest: UpdateGiftIdeaRequest = {
        giftIdeaId: 'gift-1',
        name: 'Updated Gift',
        occasion: Occasion.Anniversary,
        estimatedPrice: 200
      };

      service.updateGiftIdea(updateRequest).subscribe(idea => {
        expect(idea.name).toBe('Updated Gift');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas/gift-1`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updateRequest);
      req.flush({
        ...mockGiftIdea,
        name: 'Updated Gift',
        occasion: Occasion.Anniversary
      });
    });
  });

  describe('deleteGiftIdea', () => {
    it('should delete a gift idea', () => {
      service.deleteGiftIdea('gift-1').subscribe(result => {
        expect(result).toBeUndefined();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas/gift-1`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });

  describe('markAsPurchased', () => {
    it('should mark a gift idea as purchased', () => {
      service.markAsPurchased('gift-1').subscribe(idea => {
        expect(idea.isPurchased).toBe(true);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/giftideas/gift-1/purchase`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual({});
      req.flush({
        ...mockGiftIdea,
        isPurchased: true
      });
    });
  });
});
