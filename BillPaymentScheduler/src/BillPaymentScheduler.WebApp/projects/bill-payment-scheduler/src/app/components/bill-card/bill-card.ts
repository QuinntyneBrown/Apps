import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Bill } from '../../models';

@Component({
  selector: 'app-bill-card',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './bill-card.html',
  styleUrl: './bill-card.scss',
})
export class BillCard {
  @Input() bill!: Bill;
  @Output() edit = new EventEmitter<Bill>();
  @Output() delete = new EventEmitter<Bill>();
  @Output() pay = new EventEmitter<Bill>();

  onEdit(): void {
    this.edit.emit(this.bill);
  }

  onDelete(): void {
    this.delete.emit(this.bill);
  }

  onPay(): void {
    this.pay.emit(this.bill);
  }

  getStatusColor(): string {
    switch (this.bill.status) {
      case 'Paid':
        return 'primary';
      case 'Overdue':
        return 'warn';
      case 'Cancelled':
        return '';
      default:
        return 'accent';
    }
  }
}
