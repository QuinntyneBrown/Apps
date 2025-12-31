import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { WalletService, CryptoHoldingService, TransactionService } from '../../services';
import { Wallet, CryptoHolding, Transaction } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  wallets$ = this.walletService.wallets$;
  cryptoHoldings$ = this.cryptoHoldingService.cryptoHoldings$;
  transactions$ = this.transactionService.transactions$;

  totalPortfolioValue = 0;
  totalHoldings = 0;
  totalWallets = 0;
  recentTransactions: Transaction[] = [];

  constructor(
    private walletService: WalletService,
    private cryptoHoldingService: CryptoHoldingService,
    private transactionService: TransactionService
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.walletService.getWallets().subscribe(wallets => {
      this.totalWallets = wallets.length;
      this.totalPortfolioValue = wallets.reduce((sum, w) => sum + w.totalValue, 0);
    });

    this.cryptoHoldingService.getCryptoHoldings().subscribe(holdings => {
      this.totalHoldings = holdings.length;
    });

    this.transactionService.getTransactions().subscribe(transactions => {
      this.recentTransactions = transactions
        .sort((a, b) => new Date(b.transactionDate).getTime() - new Date(a.transactionDate).getTime())
        .slice(0, 5);
    });
  }
}
