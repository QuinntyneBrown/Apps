import { Component, inject } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { combineLatest, of, switchMap } from 'rxjs';
import { map } from 'rxjs/operators';
import { GiftService, DateService } from '../../services';
import { GiftStatus } from '../../models';
import { GiftFormDialog } from './gift-form-dialog';

@Component({
  selector: 'app-gift-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    MatChipsModule,
    MatProgressBarModule,
    MatDialogModule,
    CurrencyPipe
  ],
  templateUrl: './gift-list.html',
  styleUrl: './gift-list.scss'
})
export class GiftList {
  private readonly giftService = inject(GiftService);
  private readonly dateService = inject(DateService);
  private readonly route = inject(ActivatedRoute);
  private readonly dialog = inject(MatDialog);

  GiftStatus = GiftStatus;

  viewModel$ = this.route.paramMap.pipe(
    switchMap(params => {
      const dateId = params.get('dateId');
      if (dateId) {
        return combineLatest([
          this.giftService.getGiftsByDate(dateId),
          this.dateService.getDate(dateId)
        ]).pipe(
          map(([gifts, date]) => ({
            gifts,
            date,
            dateId,
            totalBudget: gifts.reduce((sum, g) => sum + g.estimatedPrice, 0),
            totalSpent: gifts
              .filter(g => g.status !== GiftStatus.Idea)
              .reduce((sum, g) => sum + (g.actualPrice || g.estimatedPrice), 0),
            purchasedCount: gifts.filter(g => g.status !== GiftStatus.Idea).length
          }))
        );
      }
      return this.giftService.getGifts().pipe(
        map(gifts => ({
          gifts,
          date: null,
          dateId: null,
          totalBudget: gifts.reduce((sum, g) => sum + g.estimatedPrice, 0),
          totalSpent: gifts
            .filter(g => g.status !== GiftStatus.Idea)
            .reduce((sum, g) => sum + (g.actualPrice || g.estimatedPrice), 0),
          purchasedCount: gifts.filter(g => g.status !== GiftStatus.Idea).length
        }))
      );
    })
  );

  openAddDialog(dateId: string | null): void {
    const dialogRef = this.dialog.open(GiftFormDialog, {
      width: '400px',
      data: { dateId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && dateId) {
        this.giftService.addGift(dateId, result).subscribe();
      }
    });
  }

  markAsPurchased(giftId: string): void {
    this.giftService.markAsPurchased(giftId, 0).subscribe();
  }

  markAsDelivered(giftId: string): void {
    this.giftService.markAsDelivered(giftId).subscribe();
  }

  deleteGift(giftId: string): void {
    this.giftService.deleteGift(giftId).subscribe();
  }

  getStatusColor(status: GiftStatus): string {
    switch (status) {
      case GiftStatus.Idea:
        return 'default';
      case GiftStatus.Purchased:
        return 'accent';
      case GiftStatus.Delivered:
        return 'primary';
    }
  }

  getProgress(vm: { totalSpent: number; totalBudget: number }): number {
    return vm.totalBudget > 0 ? (vm.totalSpent / vm.totalBudget) * 100 : 0;
  }
}
