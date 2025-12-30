import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Recipients } from './recipients';
import { RecipientsService, GiftIdeasService } from '../../services';
import { Recipient, GiftIdea, Occasion } from '../../models';

describe('Recipients', () => {
  let component: Recipients;
  let fixture: ComponentFixture<Recipients>;
  let recipientsService: jest.Mocked<RecipientsService>;
  let giftIdeasService: jest.Mocked<GiftIdeasService>;
  let dialog: jest.Mocked<MatDialog>;
  let router: jest.Mocked<Router>;

  const mockRecipients: Recipient[] = [
    {
      recipientId: 'recipient-1',
      userId: 'user-1',
      name: 'John Doe',
      relationship: 'Friend',
      createdAt: '2025-01-01T00:00:00Z'
    },
    {
      recipientId: 'recipient-2',
      userId: 'user-1',
      name: 'Jane Smith',
      relationship: 'Family',
      createdAt: '2025-01-02T00:00:00Z'
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
    const recipientsServiceMock = {
      getRecipients: jest.fn().mockReturnValue(of(mockRecipients)),
      createRecipient: jest.fn().mockReturnValue(of(mockRecipients[0])),
      updateRecipient: jest.fn().mockReturnValue(of(mockRecipients[0])),
      deleteRecipient: jest.fn().mockReturnValue(of(undefined))
    };

    const giftIdeasServiceMock = {
      getGiftIdeas: jest.fn().mockReturnValue(of(mockGiftIdeas))
    };

    const dialogMock = {
      open: jest.fn().mockReturnValue({
        afterClosed: () => of(undefined)
      })
    };

    const routerMock = {
      navigate: jest.fn()
    };

    await TestBed.configureTestingModule({
      imports: [
        Recipients,
        HttpClientTestingModule,
        RouterTestingModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: RecipientsService, useValue: recipientsServiceMock },
        { provide: GiftIdeasService, useValue: giftIdeasServiceMock },
        { provide: MatDialog, useValue: dialogMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Recipients);
    component = fixture.componentInstance;
    recipientsService = TestBed.inject(RecipientsService) as jest.Mocked<RecipientsService>;
    giftIdeasService = TestBed.inject(GiftIdeasService) as jest.Mocked<GiftIdeasService>;
    dialog = TestBed.inject(MatDialog) as jest.Mocked<MatDialog>;
    router = TestBed.inject(Router) as jest.Mocked<Router>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display page title', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Recipients');
  });

  it('should load recipients on init', fakeAsync(() => {
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(giftIdeasService.getGiftIdeas).toHaveBeenCalled();
  }));

  it('should calculate ideas count for recipient', () => {
    const ideasCountMap = new Map<string, number>();
    ideasCountMap.set('recipient-1', 2);
    ideasCountMap.set('recipient-2', 0);

    expect(component.getIdeasCount('recipient-1', ideasCountMap)).toBe(2);
    expect(component.getIdeasCount('recipient-2', ideasCountMap)).toBe(0);
    expect(component.getIdeasCount('recipient-3', ideasCountMap)).toBe(0);
  });

  it('should open add recipient dialog', () => {
    component.onAddRecipient();
    expect(dialog.open).toHaveBeenCalled();
  });

  it('should navigate to gift ideas on view ideas', () => {
    component.onViewIdeas(mockRecipients[0]);
    expect(router.navigate).toHaveBeenCalledWith(['/gift-ideas'], {
      queryParams: { recipientId: 'recipient-1' }
    });
  });

  it('should open edit dialog', () => {
    component.onEditRecipient(mockRecipients[0]);
    expect(dialog.open).toHaveBeenCalled();
  });

  it('should delete recipient', fakeAsync(() => {
    component.onDeleteRecipient(mockRecipients[0]);
    tick();
    expect(recipientsService.deleteRecipient).toHaveBeenCalledWith('recipient-1');
  }));
});
