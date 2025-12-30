import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { combineLatest, map, startWith, switchMap, BehaviorSubject } from 'rxjs';
import { RecipientsService, GiftIdeasService, PurchasesService } from '../../services';
import { GiftIdeaCard, GiftIdeaDialog, GiftIdeaDialogResult } from '../../components';
import { GiftIdea, Occasion } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    GiftIdeaCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private readonly recipientsService = inject(RecipientsService);
  private readonly giftIdeasService = inject(GiftIdeasService);
  private readonly purchasesService = inject(PurchasesService);
  private readonly dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);

  viewModel$ = this.refresh$.pipe(
    switchMap(() => combineLatest([
      this.recipientsService.getRecipients(),
      this.giftIdeasService.getGiftIdeas(),
      this.purchasesService.getPurchases()
    ])),
    map(([recipients, giftIdeas, purchases]) => {
      const totalBudget = giftIdeas.reduce((sum, idea) => sum + (idea.estimatedPrice || 0), 0);
      const totalSpent = purchases.reduce((sum, p) => sum + p.actualPrice, 0);
      const pendingIdeas = giftIdeas.filter(idea => !idea.isPurchased);
      const purchasedIdeas = giftIdeas.filter(idea => idea.isPurchased);
      const recentIdeas = [...giftIdeas]
        .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
        .slice(0, 4);

      return {
        recipients,
        giftIdeas,
        purchases,
        totalBudget,
        totalSpent,
        pendingIdeas,
        purchasedIdeas,
        recentIdeas,
        recipientCount: recipients.length,
        giftIdeaCount: giftIdeas.length,
        purchasedCount: purchasedIdeas.length
      };
    }),
    startWith({
      recipients: [],
      giftIdeas: [],
      purchases: [],
      totalBudget: 0,
      totalSpent: 0,
      pendingIdeas: [],
      purchasedIdeas: [],
      recentIdeas: [],
      recipientCount: 0,
      giftIdeaCount: 0,
      purchasedCount: 0
    })
  );

  onAddGiftIdea(): void {
    this.recipientsService.getRecipients().subscribe(recipients => {
      const dialogRef = this.dialog.open(GiftIdeaDialog, {
        width: '500px',
        data: { recipients }
      });

      dialogRef.afterClosed().subscribe((result: GiftIdeaDialogResult | undefined) => {
        if (result?.action === 'create') {
          this.giftIdeasService.createGiftIdea(result.data).subscribe(() => {
            this.refresh$.next();
          });
        }
      });
    });
  }

  onPurchaseGiftIdea(giftIdea: GiftIdea): void {
    this.giftIdeasService.markAsPurchased(giftIdea.giftIdeaId).subscribe(() => {
      this.refresh$.next();
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
          this.giftIdeasService.updateGiftIdea(result.data).subscribe(() => {
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

  getBudgetPercentage(totalSpent: number, totalBudget: number): number {
    if (totalBudget === 0) return 0;
    return Math.min(100, Math.round((totalSpent / totalBudget) * 100));
  }
}
