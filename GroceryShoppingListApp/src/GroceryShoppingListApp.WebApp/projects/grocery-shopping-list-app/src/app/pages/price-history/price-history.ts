import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable, BehaviorSubject, switchMap, combineLatest, map, catchError, of, startWith } from 'rxjs';
import { PriceHistory, Store, GroceryItem } from '../../models';
import { PriceHistoryService, StoreService, GroceryItemService } from '../../services';

interface PriceHistoryViewModel {
  priceHistories: (PriceHistory & { itemName?: string; storeName?: string })[];
  loading: boolean;
  error: string | null;
}

@Component({
  selector: 'app-price-history',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatSnackBarModule
  ],
  templateUrl: './price-history.html',
  styleUrl: './price-history.scss'
})
export class PriceHistoryPage implements OnInit {
  private _priceHistoryService = inject(PriceHistoryService);
  private _storeService = inject(StoreService);
  private _groceryItemService = inject(GroceryItemService);
  private _snackBar = inject(MatSnackBar);

  private _refreshSubject = new BehaviorSubject<void>(undefined);

  displayedColumns = ['item', 'store', 'price', 'date', 'actions'];

  viewModel$!: Observable<PriceHistoryViewModel>;

  ngOnInit(): void {
    this.viewModel$ = this._refreshSubject.pipe(
      switchMap(() => combineLatest([
        this._priceHistoryService.getAll(),
        this._storeService.getAll(),
        this._groceryItemService.getAll()
      ]).pipe(
        map(([histories, stores, items]) => {
          const storeMap = new Map(stores.map(s => [s.storeId, s.name]));
          const itemMap = new Map(items.map(i => [i.groceryItemId, i.name]));

          const enrichedHistories = histories.map(h => ({
            ...h,
            storeName: storeMap.get(h.storeId) || 'Unknown Store',
            itemName: itemMap.get(h.groceryItemId) || 'Unknown Item'
          }));

          return { priceHistories: enrichedHistories, loading: false, error: null };
        }),
        startWith({ priceHistories: [], loading: true, error: null }),
        catchError(err => of({ priceHistories: [], loading: false, error: 'Failed to load price history' }))
      ))
    );
  }

  onDelete(history: PriceHistory): void {
    this._priceHistoryService.delete(history.priceHistoryId).subscribe({
      next: () => {
        this._snackBar.open('Price record deleted successfully', 'Close', { duration: 3000 });
        this._refreshSubject.next();
      },
      error: () => {
        this._snackBar.open('Failed to delete price record', 'Close', { duration: 3000 });
      }
    });
  }
}
