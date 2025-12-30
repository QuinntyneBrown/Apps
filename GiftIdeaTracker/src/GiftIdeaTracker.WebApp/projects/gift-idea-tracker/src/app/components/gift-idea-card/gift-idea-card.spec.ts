import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { GiftIdeaCard } from './gift-idea-card';
import { GiftIdea, Occasion } from '../../models';

describe('GiftIdeaCard', () => {
  let component: GiftIdeaCard;
  let fixture: ComponentFixture<GiftIdeaCard>;

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

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftIdeaCard, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(GiftIdeaCard);
    component = fixture.componentInstance;
    component.giftIdea = mockGiftIdea;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display gift idea name', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('New iPhone');
  });

  it('should display occasion', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Birthday');
  });

  it('should display formatted price', () => {
    expect(component.formatPrice()).toBe('$999.00');
  });

  it('should display "Price not set" when no price', () => {
    component.giftIdea = { ...mockGiftIdea, estimatedPrice: null };
    expect(component.formatPrice()).toBe('Price not set');
  });

  it('should return correct icon for Birthday occasion', () => {
    component.giftIdea = { ...mockGiftIdea, occasion: Occasion.Birthday };
    expect(component.getOccasionIcon()).toBe('cake');
  });

  it('should return correct icon for Christmas occasion', () => {
    component.giftIdea = { ...mockGiftIdea, occasion: Occasion.Christmas };
    expect(component.getOccasionIcon()).toBe('park');
  });

  it('should return correct icon for Anniversary occasion', () => {
    component.giftIdea = { ...mockGiftIdea, occasion: Occasion.Anniversary };
    expect(component.getOccasionIcon()).toBe('favorite');
  });

  it('should return correct icon for Graduation occasion', () => {
    component.giftIdea = { ...mockGiftIdea, occasion: Occasion.Graduation };
    expect(component.getOccasionIcon()).toBe('school');
  });

  it('should return correct icon for Wedding occasion', () => {
    component.giftIdea = { ...mockGiftIdea, occasion: Occasion.Wedding };
    expect(component.getOccasionIcon()).toBe('favorite_border');
  });

  it('should return default icon for Other occasion', () => {
    component.giftIdea = { ...mockGiftIdea, occasion: Occasion.Other };
    expect(component.getOccasionIcon()).toBe('card_giftcard');
  });

  it('should return occasion label', () => {
    expect(component.getOccasionLabel()).toBe('Birthday');
  });

  it('should emit purchaseClick when purchase button is clicked', () => {
    jest.spyOn(component.purchaseClick, 'emit');
    component.onPurchaseClick();
    expect(component.purchaseClick.emit).toHaveBeenCalledWith(mockGiftIdea);
  });

  it('should emit editClick when edit button is clicked', () => {
    jest.spyOn(component.editClick, 'emit');
    component.onEditClick();
    expect(component.editClick.emit).toHaveBeenCalledWith(mockGiftIdea);
  });

  it('should emit deleteClick when delete button is clicked', () => {
    jest.spyOn(component.deleteClick, 'emit');
    component.onDeleteClick();
    expect(component.deleteClick.emit).toHaveBeenCalledWith(mockGiftIdea);
  });

  it('should show purchase button when not purchased', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Purchase');
  });

  it('should hide purchase button when already purchased', () => {
    component.giftIdea = { ...mockGiftIdea, isPurchased: true };
    fixture.detectChanges();
    const buttons = fixture.nativeElement.querySelectorAll('button');
    const purchaseButton = Array.from(buttons).find((btn: Element) =>
      btn.textContent?.includes('Purchase') && !btn.textContent?.includes('Not Purchased')
    );
    expect(purchaseButton).toBeFalsy();
  });

  it('should display "Not Purchased" status when not purchased', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Not Purchased');
  });

  it('should display "Purchased" status when purchased', () => {
    component.giftIdea = { ...mockGiftIdea, isPurchased: true };
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Purchased');
  });
});
