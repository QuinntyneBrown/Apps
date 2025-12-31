import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { WalletService } from '../../services';

@Component({
  selector: 'app-wallets-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatTableModule, MatIconModule],
  templateUrl: './wallets-list.html',
  styleUrl: './wallets-list.scss'
})
export class WalletsList implements OnInit {
  wallets$ = this.walletService.wallets$;
  displayedColumns: string[] = ['name', 'walletType', 'address', 'totalValue', 'isActive', 'actions'];

  constructor(private walletService: WalletService) {}

  ngOnInit(): void {
    this.walletService.getWallets().subscribe();
  }

  deleteWallet(id: string): void {
    if (confirm('Are you sure you want to delete this wallet?')) {
      this.walletService.deleteWallet(id).subscribe();
    }
  }
}
