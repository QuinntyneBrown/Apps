import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { CryptoHoldingService } from '../../services';

@Component({
  selector: 'app-crypto-holdings-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatTableModule, MatIconModule],
  templateUrl: './crypto-holdings-list.html',
  styleUrl: './crypto-holdings-list.scss'
})
export class CryptoHoldingsList implements OnInit {
  cryptoHoldings$ = this.cryptoHoldingService.cryptoHoldings$;
  displayedColumns: string[] = ['symbol', 'name', 'quantity', 'averageCost', 'currentPrice', 'marketValue', 'unrealizedGainLoss', 'actions'];

  constructor(private cryptoHoldingService: CryptoHoldingService) {}

  ngOnInit(): void {
    this.cryptoHoldingService.getCryptoHoldings().subscribe();
  }

  deleteCryptoHolding(id: string): void {
    if (confirm('Are you sure you want to delete this crypto holding?')) {
      this.cryptoHoldingService.deleteCryptoHolding(id).subscribe();
    }
  }
}
