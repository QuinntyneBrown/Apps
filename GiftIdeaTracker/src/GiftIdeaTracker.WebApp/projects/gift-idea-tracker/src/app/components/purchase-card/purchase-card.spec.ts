import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { PurchaseCard } from './purchase-card';
import { Purchase, GiftIdea, Occasion } from '../../models';

describe('PurchaseCard', () => {
  let component: PurchaseCard;
  let fixture: ComponentFixture<PurchaseCard>;

  const mockPurchase: Purchase = {
    purchaseId: 'purchase-1',
    giftIdeaId: 'gift-1',
    purchaseDate: '2025-01-01T00:00:00Z',
    actualPrice: 99.99,
    store: 'Amazon',
    createdAt: '2025-01-01T00:00:00Z'
  };

  const mockGiftIdea: GiftIdea = {
    giftIdeaId: 'gift-1',
    userId: 'user-1',
    recipientId: 'recipient-1',
    name: 'New iPhone',
    occasion: Occasion.Birthday,
    estimatedPrice: 999,
    isPurchased: true,
    createdAt: '2025-01-01T00:00:00Z'
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurchaseCard, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(PurchaseCard);
    component = fixture.componentInstance;
    component.purchase = mockPurchase;
    component.giftIdea = mockGiftIdea;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display gift idea name when provided', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('New iPhone');
  });

  it('should display "Gift Purchase" when no gift idea provided', () => {
    component.giftIdea = undefined;
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Gift Purchase');
  });

  it('should display store name when provided', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Amazon');
  });

  it('should display formatted price', () => {
    expect(component.formatPrice()).toBe('$99.99');
  });

  it('should display formatted price with two decimal places', () => {
    component.purchase = { ...mockPurchase, actualPrice: 100 };
    expect(component.formatPrice()).toBe('$100.00');
  });

  it('should emit deleteClick when delete button is clicked', () => {
    jest.spyOn(component.deleteClick, 'emit');
    component.onDeleteClick();
    expect(component.deleteClick.emit).toHaveBeenCalledWith(mockPurchase);
  });

  it('should display purchased status', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Purchased');
  });

  it('should have delete button', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Delete');
  });
});
