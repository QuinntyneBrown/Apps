import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BehaviorSubject, switchMap, map, startWith, combineLatest } from 'rxjs';
import { PurchasesService, GiftIdeasService } from '../../services';
import { PurchaseCard } from '../../components';
import { Purchase, GiftIdea } from '../../models';

@Component({
  selector: 'app-purchases',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    PurchaseCard
  ],
  templateUrl: './purchases.html',
  styleUrl: './purchases.scss'
})
export class Purchases {
  private readonly purchasesService = inject(PurchasesService);
  private readonly giftIdeasService = inject(GiftIdeasService);

  private refresh$ = new BehaviorSubject<void>(undefined);

  viewModel$ = this.refresh$.pipe(
    switchMap(() => combineLatest([
      this.purchasesService.getPurchases(),
      this.giftIdeasService.getGiftIdeas()
    ])),
    map(([purchases, giftIdeas]) => {
      const giftIdeasMap = new Map<string, GiftIdea>();
      giftIdeas.forEach(idea => {
        giftIdeasMap.set(idea.giftIdeaId, idea);
      });

      const totalSpent = purchases.reduce((sum, p) => sum + p.actualPrice, 0);

      const sortedPurchases = [...purchases].sort(
        (a, b) => new Date(b.purchaseDate).getTime() - new Date(a.purchaseDate).getTime()
      );

      return {
        purchases: sortedPurchases,
        giftIdeasMap,
        totalSpent,
        purchaseCount: purchases.length
      };
    }),
    startWith({
      purchases: [] as Purchase[],
      giftIdeasMap: new Map<string, GiftIdea>(),
      totalSpent: 0,
      purchaseCount: 0
    })
  );

  onDeletePurchase(purchase: Purchase): void {
    this.purchasesService.deletePurchase(purchase.purchaseId).subscribe(() => {
      this.refresh$.next();
    });
  }

  getGiftIdea(giftIdeaId: string, giftIdeasMap: Map<string, GiftIdea>): GiftIdea | undefined {
    return giftIdeasMap.get(giftIdeaId);
  }
}
