import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { PurchaseService, ReceiptService, ReturnWindowService, WarrantyService } from '../../services';
import { combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private _purchaseService = inject(PurchaseService);
  private _receiptService = inject(ReceiptService);
  private _returnWindowService = inject(ReturnWindowService);
  private _warrantyService = inject(WarrantyService);

  stats$ = combineLatest([
    this._purchaseService.purchases$,
    this._receiptService.receipts$,
    this._returnWindowService.returnWindows$,
    this._warrantyService.warranties$
  ]).pipe(
    map(([purchases, receipts, returnWindows, warranties]) => ({
      totalPurchases: purchases.length,
      activePurchases: purchases.filter(p => p.status === 'Active').length,
      totalReceipts: receipts.length,
      activeReceipts: receipts.filter(r => r.status === 'Active').length,
      openReturnWindows: returnWindows.filter(rw => rw.status === 'Open').length,
      totalReturnWindows: returnWindows.length,
      activeWarranties: warranties.filter(w => w.status === 'Active').length,
      totalWarranties: warranties.length
    }))
  );

  ngOnInit(): void {
    this._purchaseService.getAll().subscribe();
    this._receiptService.getAll().subscribe();
    this._returnWindowService.getAll().subscribe();
    this._warrantyService.getAll().subscribe();
  }
}
