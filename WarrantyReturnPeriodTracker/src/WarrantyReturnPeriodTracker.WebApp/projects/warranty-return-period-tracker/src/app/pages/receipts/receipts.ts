import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { ReceiptService } from '../../services';

@Component({
  selector: 'app-receipts',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule
  ],
  templateUrl: './receipts.html',
  styleUrl: './receipts.scss'
})
export class Receipts {
  private _receiptService = inject(ReceiptService);

  receipts$ = this._receiptService.receipts$;
  displayedColumns: string[] = ['receiptNumber', 'storeName', 'receiptDate', 'totalAmount', 'paymentMethod', 'status', 'actions'];

  ngOnInit(): void {
    this._receiptService.getAll().subscribe();
  }

  deleteReceipt(id: string): void {
    if (confirm('Are you sure you want to delete this receipt?')) {
      this._receiptService.delete(id).subscribe();
    }
  }
}
