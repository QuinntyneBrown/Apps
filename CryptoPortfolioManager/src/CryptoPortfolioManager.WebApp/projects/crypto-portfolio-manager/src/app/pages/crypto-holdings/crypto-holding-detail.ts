import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { CryptoHoldingService, TaxLotService } from '../../services';

@Component({
  selector: 'app-crypto-holding-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  templateUrl: './crypto-holding-detail.html',
  styleUrl: './crypto-holding-detail.scss'
})
export class CryptoHoldingDetail implements OnInit {
  cryptoHolding$ = this.cryptoHoldingService.selectedCryptoHolding$;
  taxLots$ = this.taxLotService.taxLots$;

  taxLotsColumns: string[] = ['acquisitionDate', 'quantity', 'costBasis', 'isDisposed', 'disposalDate', 'realizedGainLoss'];

  constructor(
    private route: ActivatedRoute,
    private cryptoHoldingService: CryptoHoldingService,
    private taxLotService: TaxLotService
  ) {}

  ngOnInit(): void {
    const holdingId = this.route.snapshot.paramMap.get('id');
    if (holdingId) {
      this.cryptoHoldingService.getCryptoHoldingById(holdingId).subscribe();
      this.taxLotService.getTaxLots(holdingId).subscribe();
    }
  }
}
