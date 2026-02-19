import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material/dialog';
import { of } from 'rxjs';
import { AppComponent } from './app.component';
import { RecipientsService, GiftIdeasService } from './services';
import { Recipient, Occasion } from './models';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let dialog: jest.Mocked<MatDialog>;
  let recipientsService: jest.Mocked<RecipientsService>;
  let giftIdeasService: jest.Mocked<GiftIdeasService>;

  const mockRecipients: Recipient[] = [
    {
      recipientId: 'recipient-1',
      userId: 'user-1',
      name: 'John Doe',
      relationship: 'Friend',
      createdAt: '2025-01-01T00:00:00Z'
    }
  ];

  beforeEach(async () => {
    const dialogMock = {
      open: jest.fn().mockReturnValue({
        afterClosed: () => of(undefined)
      })
    };

    const recipientsServiceMock = {
      getRecipients: jest.fn().mockReturnValue(of(mockRecipients))
    };

    const giftIdeasServiceMock = {
      createGiftIdea: jest.fn().mockReturnValue(of({
        giftIdeaId: 'gift-1',
        name: 'Test',
        occasion: Occasion.Birthday
      }))
    };

    await TestBed.configureTestingModule({
      imports: [
        AppComponent,
        HttpClientTestingModule,
        RouterTestingModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: MatDialog, useValue: dialogMock },
        { provide: RecipientsService, useValue: recipientsServiceMock },
        { provide: GiftIdeasService, useValue: giftIdeasServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    dialog = TestBed.inject(MatDialog) as jest.Mocked<MatDialog>;
    recipientsService = TestBed.inject(RecipientsService) as jest.Mocked<RecipientsService>;
    giftIdeasService = TestBed.inject(GiftIdeasService) as jest.Mocked<GiftIdeasService>;
    fixture.detectChanges();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should display header component', () => {
    const compiled = fixture.nativeElement;
    const header = compiled.querySelector('app-header');
    expect(header).toBeTruthy();
  });

  it('should have router outlet', () => {
    const compiled = fixture.nativeElement;
    const routerOutlet = compiled.querySelector('router-outlet');
    expect(routerOutlet).toBeTruthy();
  });

  it('should open add gift idea dialog', fakeAsync(() => {
    component.onAddGiftIdea();
    tick();
    expect(recipientsService.getRecipients).toHaveBeenCalled();
    expect(dialog.open).toHaveBeenCalled();
  }));
});
