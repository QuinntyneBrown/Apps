import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { TransactionService } from '../../services';

@Component({
  selector: 'app-transactions-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatTableModule, MatIconModule],
  templateUrl: './transactions-list.html',
  styleUrl: './transactions-list.scss'
})
export class TransactionsList implements OnInit {
  transactions$ = this.transactionService.transactions$;
  displayedColumns: string[] = ['transactionDate', 'transactionType', 'symbol', 'quantity', 'pricePerUnit', 'totalCost', 'actions'];

  constructor(private transactionService: TransactionService) {}

  ngOnInit(): void {
    this.transactionService.getTransactions().subscribe();
  }

  deleteTransaction(id: string): void {
    if (confirm('Are you sure you want to delete this transaction?')) {
      this.transactionService.deleteTransaction(id).subscribe();
    }
  }
}
