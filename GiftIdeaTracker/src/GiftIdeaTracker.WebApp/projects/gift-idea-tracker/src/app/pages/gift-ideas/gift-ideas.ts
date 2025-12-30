import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, switchMap, map, startWith, combineLatest } from 'rxjs';
import { GiftIdeasService, RecipientsService, PurchasesService } from '../../services';
import { GiftIdeaCard, GiftIdeaDialog, GiftIdeaDialogResult } from '../../components';
import { GiftIdea, Recipient, Occasion, CreateGiftIdeaRequest, UpdateGiftIdeaRequest, CreatePurchaseRequest } from '../../models';

@Component({
  selector: 'app-gift-ideas',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatFormFieldModule,
    GiftIdeaCard
  ],
  templateUrl: './gift-ideas.html',
  styleUrl: './gift-ideas.scss'
})
export class GiftIdeas {
  private readonly giftIdeasService = inject(GiftIdeasService);
  private readonly recipientsService = inject(RecipientsService);
  private readonly purchasesService = inject(PurchasesService);
  private readonly dialog = inject(MatDialog);
  private readonly route = inject(ActivatedRoute);

  private refresh$ = new BehaviorSubject<void>(undefined);
  private filterRecipientId$ = new BehaviorSubject<string>('');
  private filterOccasion$ = new BehaviorSubject<string>('');

  occasions = Object.values(Occasion);

  viewModel$ = combineLatest([
    this.refresh$,
    this.filterRecipientId$,
    this.filterOccasion$,
    this.route.queryParams
  ]).pipe(
    switchMap(([, filterRecipientId, filterOccasion, queryParams]) => {
      const recipientIdFromQuery = queryParams['recipientId'] || '';
      const activeRecipientFilter = filterRecipientId || recipientIdFromQuery;

      return combineLatest([
        this.giftIdeasService.getGiftIdeas(),
        this.recipientsService.getRecipients()
      ]).pipe(
        map(([giftIdeas, recipients]) => {
          let filteredIdeas = giftIdeas;

          if (activeRecipientFilter) {
            filteredIdeas = filteredIdeas.filter(idea => idea.recipientId === activeRecipientFilter);
          }

          if (filterOccasion) {
            filteredIdeas = filteredIdeas.filter(idea => idea.occasion === filterOccasion);
          }

          const selectedRecipient = activeRecipientFilter
            ? recipients.find(r => r.recipientId === activeRecipientFilter)
            : null;

          return {
            giftIdeas: filteredIdeas,
            recipients,
            selectedRecipientId: activeRecipientFilter,
            selectedRecipient,
            selectedOccasion: filterOccasion
          };
        })
      );
    }),
    startWith({
      giftIdeas: [] as GiftIdea[],
      recipients: [] as Recipient[],
      selectedRecipientId: '',
      selectedRecipient: null as Recipient | null,
      selectedOccasion: ''
    })
  );

  onRecipientFilterChange(recipientId: string): void {
    this.filterRecipientId$.next(recipientId);
  }

  onOccasionFilterChange(occasion: string): void {
    this.filterOccasion$.next(occasion);
  }

  onAddGiftIdea(): void {
    this.recipientsService.getRecipients().subscribe(recipients => {
      const dialogRef = this.dialog.open(GiftIdeaDialog, {
        width: '500px',
        data: { recipients }
      });

      dialogRef.afterClosed().subscribe((result: GiftIdeaDialogResult | undefined) => {
        if (result?.action === 'create') {
          this.giftIdeasService.createGiftIdea(result.data as CreateGiftIdeaRequest).subscribe(() => {
            this.refresh$.next();
          });
        }
      });
    });
  }

  onPurchaseGiftIdea(giftIdea: GiftIdea): void {
    const purchaseRequest: CreatePurchaseRequest = {
      giftIdeaId: giftIdea.giftIdeaId,
      purchaseDate: new Date().toISOString(),
      actualPrice: giftIdea.estimatedPrice || 0,
      store: undefined
    };

    this.purchasesService.createPurchase(purchaseRequest).subscribe(() => {
      this.giftIdeasService.markAsPurchased(giftIdea.giftIdeaId).subscribe(() => {
        this.refresh$.next();
      });
    });
  }

  onEditGiftIdea(giftIdea: GiftIdea): void {
    this.recipientsService.getRecipients().subscribe(recipients => {
      const dialogRef = this.dialog.open(GiftIdeaDialog, {
        width: '500px',
        data: { giftIdea, recipients }
      });

      dialogRef.afterClosed().subscribe((result: GiftIdeaDialogResult | undefined) => {
        if (result?.action === 'update') {
          this.giftIdeasService.updateGiftIdea(result.data as UpdateGiftIdeaRequest).subscribe(() => {
            this.refresh$.next();
          });
        }
      });
    });
  }

  onDeleteGiftIdea(giftIdea: GiftIdea): void {
    this.giftIdeasService.deleteGiftIdea(giftIdea.giftIdeaId).subscribe(() => {
      this.refresh$.next();
    });
  }
}
