import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { GiftService } from './gift-service';
import { GiftStatus, Gift } from '../models';
import { apiBaseUrl } from './api-config';

describe('GiftService', () => {
  let service: GiftService;
  let httpMock: HttpTestingController;

  const mockGift: Gift = {
    giftId: '1',
    dateId: 'date1',
    description: 'Watch',
    estimatedPrice: 200,
    actualPrice: null,
    purchaseUrl: 'https://example.com',
    status: GiftStatus.Idea,
    purchasedAt: null
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        GiftService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(GiftService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all gifts', () => {
    service.getGifts().subscribe(gifts => {
      expect(gifts.length).toBe(1);
      expect(gifts[0].description).toBe('Watch');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/gifts`);
    expect(req.request.method).toBe('GET');
    req.flush([mockGift]);
  });

  it('should get gifts by date', () => {
    service.getGiftsByDate('date1').subscribe(gifts => {
      expect(gifts.length).toBe(1);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/date1/gifts`);
    expect(req.request.method).toBe('GET');
    req.flush([mockGift]);
  });

  it('should add a gift', () => {
    const newGift = {
      description: 'Book',
      estimatedPrice: 25,
      actualPrice: null,
      purchaseUrl: '',
      status: GiftStatus.Idea,
      purchasedAt: null
    };

    service.addGift('date1', newGift).subscribe(gift => {
      expect(gift.description).toBe('Book');
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/dates/date1/gifts`);
    expect(req.request.method).toBe('POST');
    req.flush({ ...newGift, giftId: '2', dateId: 'date1' });
  });

  it('should mark gift as purchased', () => {
    service.markAsPurchased('1', 180).subscribe(gift => {
      expect(gift.status).toBe(GiftStatus.Purchased);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/gifts/1/purchase`);
    expect(req.request.method).toBe('POST');
    req.flush({ ...mockGift, status: GiftStatus.Purchased, actualPrice: 180 });
  });

  it('should mark gift as delivered', () => {
    service.markAsDelivered('1').subscribe(gift => {
      expect(gift.status).toBe(GiftStatus.Delivered);
    });

    const req = httpMock.expectOne(`${apiBaseUrl}/api/gifts/1/deliver`);
    expect(req.request.method).toBe('POST');
    req.flush({ ...mockGift, status: GiftStatus.Delivered });
  });

  it('should delete a gift', () => {
    service.deleteGift('1').subscribe();

    const req = httpMock.expectOne(`${apiBaseUrl}/api/gifts/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
