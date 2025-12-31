import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TransactionService } from '../../services';

@Component({
  selector: 'app-transaction-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './transaction-detail.html',
  styleUrl: './transaction-detail.scss'
})
export class TransactionDetail implements OnInit {
  transaction$ = this.transactionService.selectedTransaction$;

  constructor(
    private route: ActivatedRoute,
    private transactionService: TransactionService
  ) {}

  ngOnInit(): void {
    const transactionId = this.route.snapshot.paramMap.get('id');
    if (transactionId) {
      this.transactionService.getTransactionById(transactionId).subscribe();
    }
  }
}
