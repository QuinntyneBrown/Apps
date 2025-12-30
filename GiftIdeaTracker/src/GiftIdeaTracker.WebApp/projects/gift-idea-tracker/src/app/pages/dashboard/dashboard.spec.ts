import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material/dialog';
import { of } from 'rxjs';
import { Dashboard } from './dashboard';
import { RecipientsService, GiftIdeasService, PurchasesService } from '../../services';
import { GiftIdea, Recipient, Purchase, Occasion } from '../../models';

describe('Dashboard', () => {
  let component: Dashboard;
  let fixture: ComponentFixture<Dashboard>;
  let recipientsService: jest.Mocked<RecipientsService>;
  let giftIdeasService: jest.Mocked<GiftIdeasService>;
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

  const mockPurchases: Purchase[] = [
    {
      purchaseId: 'purchase-1',
      giftIdeaId: 'gift-2',
      purchaseDate: '2025-01-01T00:00:00Z',
      actualPrice: 20,
      store: 'Amazon',
      createdAt: '2025-01-01T00:00:00Z'
    }
  ];

  beforeEach(async () => {
    const recipientsServiceMock = {
      getRecipients: jest.fn().mockReturnValue(of(mockRecipients))
    };

    const giftIdeasServiceMock = {
      getGiftIdeas: jest.fn().mockReturnValue(of(mockGiftIdeas)),
      createGiftIdea: jest.fn().mockReturnValue(of(mockGiftIdeas[0])),
      updateGiftIdea: jest.fn().mockReturnValue(of(mockGiftIdeas[0])),
      deleteGiftIdea: jest.fn().mockReturnValue(of(undefined)),
      markAsPurchased: jest.fn().mockReturnValue(of({ ...mockGiftIdeas[0], isPurchased: true }))
    };

    const purchasesServiceMock = {
      getPurchases: jest.fn().mockReturnValue(of(mockPurchases))
    };

    const dialogMock = {
      open: jest.fn().mockReturnValue({
        afterClosed: () => of(undefined)
      })
    };

    await TestBed.configureTestingModule({
      imports: [
        Dashboard,
        HttpClientTestingModule,
        RouterTestingModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: RecipientsService, useValue: recipientsServiceMock },
        { provide: GiftIdeasService, useValue: giftIdeasServiceMock },
        { provide: PurchasesService, useValue: purchasesServiceMock },
        { provide: MatDialog, useValue: dialogMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
    recipientsService = TestBed.inject(RecipientsService) as jest.Mocked<RecipientsService>;
    giftIdeasService = TestBed.inject(GiftIdeasService) as jest.Mocked<GiftIdeasService>;
    purchasesService = TestBed.inject(PurchasesService) as jest.Mocked<PurchasesService>;
    dialog = TestBed.inject(MatDialog) as jest.Mocked<MatDialog>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display dashboard title', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Dashboard');
  });

  it('should load data on init', fakeAsync(() => {
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(giftIdeasService.getGiftIdeas).toHaveBeenCalled();
    expect(purchasesService.getPurchases).toHaveBeenCalled();
  }));

  it('should calculate budget percentage correctly', () => {
    expect(component.getBudgetPercentage(50, 100)).toBe(50);
    expect(component.getBudgetPercentage(75, 100)).toBe(75);
    expect(component.getBudgetPercentage(100, 100)).toBe(100);
  });

  it('should return 0 when total budget is 0', () => {
    expect(component.getBudgetPercentage(50, 0)).toBe(0);
  });

  it('should cap budget percentage at 100', () => {
    expect(component.getBudgetPercentage(150, 100)).toBe(100);
  });

  it('should open add gift idea dialog', fakeAsync(() => {
    component.onAddGiftIdea();
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(dialog.open).toHaveBeenCalled();
  }));

  it('should mark gift idea as purchased', fakeAsync(() => {
    component.onPurchaseGiftIdea(mockGiftIdeas[0]);
    tick();
    expect(giftIdeasService.markAsPurchased).toHaveBeenCalledWith('gift-1');
  }));

  it('should delete gift idea', fakeAsync(() => {
    component.onDeleteGiftIdea(mockGiftIdeas[0]);
    tick();
    expect(giftIdeasService.deleteGiftIdea).toHaveBeenCalledWith('gift-1');
  }));

  it('should open edit dialog', fakeAsync(() => {
    component.onEditGiftIdea(mockGiftIdeas[0]);
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(dialog.open).toHaveBeenCalled();
  }));
});
