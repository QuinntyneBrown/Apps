import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { WalletService, CryptoHoldingService, TransactionService } from '../../services';

@Component({
  selector: 'app-wallet-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  templateUrl: './wallet-detail.html',
  styleUrl: './wallet-detail.scss'
})
export class WalletDetail implements OnInit {
  wallet$ = this.walletService.selectedWallet$;
  holdings$ = this.cryptoHoldingService.cryptoHoldings$;
  transactions$ = this.transactionService.transactions$;

  holdingsColumns: string[] = ['symbol', 'name', 'quantity', 'currentPrice', 'marketValue', 'unrealizedGainLoss'];
  transactionsColumns: string[] = ['date', 'type', 'symbol', 'quantity', 'pricePerUnit', 'totalCost'];

  constructor(
    private route: ActivatedRoute,
    private walletService: WalletService,
    private cryptoHoldingService: CryptoHoldingService,
    private transactionService: TransactionService
  ) {}

  ngOnInit(): void {
    const walletId = this.route.snapshot.paramMap.get('id');
    if (walletId) {
      this.walletService.getWalletById(walletId).subscribe();
      this.cryptoHoldingService.getCryptoHoldings(walletId).subscribe();
      this.transactionService.getTransactions(walletId).subscribe();
    }
  }
}
