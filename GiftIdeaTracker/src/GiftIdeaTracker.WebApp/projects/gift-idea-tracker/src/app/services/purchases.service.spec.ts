import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PurchasesService } from './purchases.service';
import { Purchase, CreatePurchaseRequest } from '../models';

describe('PurchasesService', () => {
  let service: PurchasesService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:5200';

  const mockPurchase: Purchase = {
    purchaseId: 'purchase-1',
    giftIdeaId: 'gift-1',
    purchaseDate: '2025-01-01T00:00:00Z',
    actualPrice: 99.99,
    store: 'Amazon',
    createdAt: '2025-01-01T00:00:00Z'
  };

  const mockPurchases: Purchase[] = [
    mockPurchase,
    {
      purchaseId: 'purchase-2',
      giftIdeaId: 'gift-2',
      purchaseDate: '2025-01-02T00:00:00Z',
      actualPrice: 49.99,
      store: 'Target',
      createdAt: '2025-01-02T00:00:00Z'
    }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PurchasesService]
    });
    service = TestBed.inject(PurchasesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getPurchases', () => {
    it('should return all purchases without filter', () => {
      service.getPurchases().subscribe(purchases => {
        expect(purchases).toEqual(mockPurchases);
        expect(purchases.length).toBe(2);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases`);
      expect(req.request.method).toBe('GET');
      req.flush(mockPurchases);
    });

    it('should return purchases filtered by giftIdeaId', () => {
      service.getPurchases('gift-1').subscribe(purchases => {
        expect(purchases.length).toBe(1);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases?giftIdeaId=gift-1`);
      expect(req.request.method).toBe('GET');
      req.flush([mockPurchase]);
    });

    it('should return empty array when no purchases exist', () => {
      service.getPurchases().subscribe(purchases => {
        expect(purchases).toEqual([]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases`);
      req.flush([]);
    });
  });

  describe('getPurchase', () => {
    it('should return a single purchase by id', () => {
      service.getPurchase('purchase-1').subscribe(purchase => {
        expect(purchase).toEqual(mockPurchase);
        expect(purchase.actualPrice).toBe(99.99);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases/purchase-1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockPurchase);
    });
  });

  describe('createPurchase', () => {
    it('should create a new purchase', () => {
      const createRequest: CreatePurchaseRequest = {
        giftIdeaId: 'gift-1',
        purchaseDate: '2025-01-01T00:00:00Z',
        actualPrice: 75.00,
        store: 'Best Buy'
      };

      service.createPurchase(createRequest).subscribe(purchase => {
        expect(purchase.actualPrice).toBe(75.00);
        expect(purchase.store).toBe('Best Buy');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush({
        ...mockPurchase,
        actualPrice: 75.00,
        store: 'Best Buy'
      });
    });

    it('should create a purchase without store', () => {
      const createRequest: CreatePurchaseRequest = {
        giftIdeaId: 'gift-1',
        purchaseDate: '2025-01-01T00:00:00Z',
        actualPrice: 50.00
      };

      service.createPurchase(createRequest).subscribe(purchase => {
        expect(purchase.actualPrice).toBe(50.00);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases`);
      expect(req.request.body).toEqual(createRequest);
      req.flush({
        ...mockPurchase,
        actualPrice: 50.00,
        store: null
      });
    });
  });

  describe('deletePurchase', () => {
    it('should delete a purchase', () => {
      service.deletePurchase('purchase-1').subscribe(result => {
        expect(result).toBeUndefined();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/purchases/purchase-1`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });
});
