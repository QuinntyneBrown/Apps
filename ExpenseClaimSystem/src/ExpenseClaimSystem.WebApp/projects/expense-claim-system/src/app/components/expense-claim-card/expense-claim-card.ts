import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ExpenseClaim } from '../../models';

@Component({
  selector: 'app-expense-claim-card',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './expense-claim-card.html',
  styleUrl: './expense-claim-card.scss',
})
export class ExpenseClaimCard {
  @Input() expenseClaim!: ExpenseClaim;
  @Output() edit = new EventEmitter<ExpenseClaim>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.expenseClaim);
  }

  onDelete(): void {
    this.delete.emit(this.expenseClaim.expenseClaimId);
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'Draft':
        return '';
      case 'Submitted':
        return 'primary';
      case 'UnderReview':
        return 'accent';
      case 'Approved':
        return 'primary';
      case 'Rejected':
        return 'warn';
      case 'Paid':
        return 'primary';
      default:
        return '';
    }
  }
}
