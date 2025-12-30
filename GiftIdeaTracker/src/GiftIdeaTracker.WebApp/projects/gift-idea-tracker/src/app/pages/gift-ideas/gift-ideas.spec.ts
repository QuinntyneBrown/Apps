import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { GiftIdeas } from './gift-ideas';
import { GiftIdeasService, RecipientsService, PurchasesService } from '../../services';
import { GiftIdea, Recipient, Occasion } from '../../models';

describe('GiftIdeas', () => {
  let component: GiftIdeas;
  let fixture: ComponentFixture<GiftIdeas>;
  let giftIdeasService: jest.Mocked<GiftIdeasService>;
  let recipientsService: jest.Mocked<RecipientsService>;
  let purchasesService: jest.Mocked<PurchasesService>;
  let dialog: jest.Mocked<MatDialog>;

  const mockRecipients: Recipient[] = [
    {
      recipientId: 'recipient-1',
      userId: 'user-1',
      name: 'John Doe',
      relationship: 'Friend',
      createdAt: '2025-01-01T00:00:00Z'
    }
  ];

  const mockGiftIdeas: GiftIdea[] = [
    {
      giftIdeaId: 'gift-1',
      userId: 'user-1',
      recipientId: 'recipient-1',
      name: 'New iPhone',
      occasion: Occasion.Birthday,
      estimatedPrice: 999,
      isPurchased: false,
      createdAt: '2025-01-01T00:00:00Z'
    },
    {
      giftIdeaId: 'gift-2',
      userId: 'user-1',
      recipientId: 'recipient-1',
      name: 'Book',
      occasion: Occasion.Christmas,
      estimatedPrice: 25,
      isPurchased: true,
      createdAt: '2025-01-02T00:00:00Z'
    }
  ];

  beforeEach(async () => {
    const giftIdeasServiceMock = {
      getGiftIdeas: jest.fn().mockReturnValue(of(mockGiftIdeas)),
      createGiftIdea: jest.fn().mockReturnValue(of(mockGiftIdeas[0])),
      updateGiftIdea: jest.fn().mockReturnValue(of(mockGiftIdeas[0])),
      deleteGiftIdea: jest.fn().mockReturnValue(of(undefined)),
      markAsPurchased: jest.fn().mockReturnValue(of({ ...mockGiftIdeas[0], isPurchased: true }))
    };

    const recipientsServiceMock = {
      getRecipients: jest.fn().mockReturnValue(of(mockRecipients))
    };

    const purchasesServiceMock = {
      createPurchase: jest.fn().mockReturnValue(of({ purchaseId: 'purchase-1' }))
    };

    const dialogMock = {
      open: jest.fn().mockReturnValue({
        afterClosed: () => of(undefined)
      })
    };

    await TestBed.configureTestingModule({
      imports: [
        GiftIdeas,
        HttpClientTestingModule,
        RouterTestingModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: GiftIdeasService, useValue: giftIdeasServiceMock },
        { provide: RecipientsService, useValue: recipientsServiceMock },
        { provide: PurchasesService, useValue: purchasesServiceMock },
        { provide: MatDialog, useValue: dialogMock },
        {
          provide: ActivatedRoute,
          useValue: { queryParams: of({}) }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(GiftIdeas);
    component = fixture.componentInstance;
    giftIdeasService = TestBed.inject(GiftIdeasService) as jest.Mocked<GiftIdeasService>;
    recipientsService = TestBed.inject(RecipientsService) as jest.Mocked<RecipientsService>;
    purchasesService = TestBed.inject(PurchasesService) as jest.Mocked<PurchasesService>;
    dialog = TestBed.inject(MatDialog) as jest.Mocked<MatDialog>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display page title', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Gift Ideas');
  });

  it('should have all occasions for filter', () => {
    expect(component.occasions).toEqual(Object.values(Occasion));
  });

  it('should load gift ideas on init', fakeAsync(() => {
    tick();
    expect(giftIdeasService.getGiftIdeas).toHaveBeenCalled();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
  }));

  it('should open add gift idea dialog', fakeAsync(() => {
    component.onAddGiftIdea();
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(dialog.open).toHaveBeenCalled();
  }));

  it('should purchase gift idea and create purchase', fakeAsync(() => {
    component.onPurchaseGiftIdea(mockGiftIdeas[0]);
    tick();
    expect(purchasesService.createPurchase).toHaveBeenCalled();
    expect(giftIdeasService.markAsPurchased).toHaveBeenCalledWith('gift-1');
  }));

  it('should open edit dialog', fakeAsync(() => {
    component.onEditGiftIdea(mockGiftIdeas[0]);
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(dialog.open).toHaveBeenCalled();
  }));

  it('should delete gift idea', fakeAsync(() => {
    component.onDeleteGiftIdea(mockGiftIdeas[0]);
    tick();
    expect(giftIdeasService.deleteGiftIdea).toHaveBeenCalledWith('gift-1');
  }));

  it('should filter by recipient', fakeAsync(() => {
    component.onRecipientFilterChange('recipient-1');
    tick();
    expect(giftIdeasService.getGiftIdeas).toHaveBeenCalled();
  }));

  it('should filter by occasion', fakeAsync(() => {
    component.onOccasionFilterChange(Occasion.Birthday);
    tick();
    expect(giftIdeasService.getGiftIdeas).toHaveBeenCalled();
  }));
});
