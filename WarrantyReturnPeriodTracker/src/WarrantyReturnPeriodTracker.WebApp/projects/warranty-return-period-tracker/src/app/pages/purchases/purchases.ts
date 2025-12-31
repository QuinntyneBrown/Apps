import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { PurchaseService } from '../../services';

@Component({
  selector: 'app-purchases',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule
  ],
  templateUrl: './purchases.html',
  styleUrl: './purchases.scss'
})
export class Purchases {
  private _purchaseService = inject(PurchaseService);

  purchases$ = this._purchaseService.purchases$;
  displayedColumns: string[] = ['productName', 'category', 'storeName', 'purchaseDate', 'price', 'status', 'actions'];

  ngOnInit(): void {
    this._purchaseService.getAll().subscribe();
  }

  deletePurchase(id: string): void {
    if (confirm('Are you sure you want to delete this purchase?')) {
      this._purchaseService.delete(id).subscribe();
    }
  }
}
