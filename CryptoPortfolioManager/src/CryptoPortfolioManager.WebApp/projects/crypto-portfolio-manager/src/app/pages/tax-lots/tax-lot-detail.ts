import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TaxLotService } from '../../services';

@Component({
  selector: 'app-tax-lot-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './tax-lot-detail.html',
  styleUrl: './tax-lot-detail.scss'
})
export class TaxLotDetail implements OnInit {
  taxLot$ = this.taxLotService.selectedTaxLot$;

  constructor(
    private route: ActivatedRoute,
    private taxLotService: TaxLotService
  ) {}

  ngOnInit(): void {
    const taxLotId = this.route.snapshot.paramMap.get('id');
    if (taxLotId) {
      this.taxLotService.getTaxLotById(taxLotId).subscribe();
    }
  }
}
