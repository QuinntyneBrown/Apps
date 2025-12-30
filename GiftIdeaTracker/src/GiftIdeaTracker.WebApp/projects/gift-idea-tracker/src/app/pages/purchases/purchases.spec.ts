import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { of } from 'rxjs';
import { Purchases } from './purchases';
import { PurchasesService, GiftIdeasService } from '../../services';
import { Purchase, GiftIdea, Occasion } from '../../models';

describe('Purchases', () => {
  let component: Purchases;
  let fixture: ComponentFixture<Purchases>;
  let purchasesService: jest.Mocked<PurchasesService>;
  let giftIdeasService: jest.Mocked<GiftIdeasService>;

  const mockGiftIdeas: GiftIdea[] = [
    {
      giftIdeaId: 'gift-1',
      userId: 'user-1',
      recipientId: 'recipient-1',
      name: 'New iPhone',
      occasion: Occasion.Birthday,
      estimatedPrice: 999,
      isPurchased: true,
      createdAt: '2025-01-01T00:00:00Z'
    }
  ];

  const mockPurchases: Purchase[] = [
    {
      purchaseId: 'purchase-1',
      giftIdeaId: 'gift-1',
      purchaseDate: '2025-01-01T00:00:00Z',
      actualPrice: 999,
      store: 'Amazon',
      createdAt: '2025-01-01T00:00:00Z'
    },
    {
      purchaseId: 'purchase-2',
      giftIdeaId: 'gift-2',
      purchaseDate: '2025-01-02T00:00:00Z',
      actualPrice: 50,
      store: 'Target',
      createdAt: '2025-01-02T00:00:00Z'
    }
  ];

  beforeEach(async () => {
    const purchasesServiceMock = {
      getPurchases: jest.fn().mockReturnValue(of(mockPurchases)),
      deletePurchase: jest.fn().mockReturnValue(of(undefined))
    };

    const giftIdeasServiceMock = {
      getGiftIdeas: jest.fn().mockReturnValue(of(mockGiftIdeas))
    };

    await TestBed.configureTestingModule({
      imports: [
        Purchases,
        HttpClientTestingModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: PurchasesService, useValue: purchasesServiceMock },
        { provide: GiftIdeasService, useValue: giftIdeasServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Purchases);
    component = fixture.componentInstance;
    purchasesService = TestBed.inject(PurchasesService) as jest.Mocked<PurchasesService>;
    giftIdeasService = TestBed.inject(GiftIdeasService) as jest.Mocked<GiftIdeasService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display page title', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Purchases');
  });

  it('should load purchases on init', fakeAsync(() => {
    tick();
    expect(purchasesService.getPurchases).toHaveBeenCalled();
    expect(giftIdeasService.getGiftIdeas).toHaveBeenCalled();
  }));

  it('should delete purchase', fakeAsync(() => {
    component.onDeletePurchase(mockPurchases[0]);
    tick();
    expect(purchasesService.deletePurchase).toHaveBeenCalledWith('purchase-1');
  }));

  it('should get gift idea from map', () => {
    const giftIdeasMap = new Map<string, GiftIdea>();
    giftIdeasMap.set('gift-1', mockGiftIdeas[0]);

    const result = component.getGiftIdea('gift-1', giftIdeasMap);
    expect(result).toEqual(mockGiftIdeas[0]);
  });

  it('should return undefined for non-existent gift idea', () => {
    const giftIdeasMap = new Map<string, GiftIdea>();
    const result = component.getGiftIdea('non-existent', giftIdeasMap);
    expect(result).toBeUndefined();
  });
});
