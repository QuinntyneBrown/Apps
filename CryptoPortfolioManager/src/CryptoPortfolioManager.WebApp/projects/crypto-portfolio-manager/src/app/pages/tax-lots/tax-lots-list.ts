import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { TaxLotService } from '../../services';

@Component({
  selector: 'app-tax-lots-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatTableModule, MatIconModule],
  templateUrl: './tax-lots-list.html',
  styleUrl: './tax-lots-list.scss'
})
export class TaxLotsList implements OnInit {
  taxLots$ = this.taxLotService.taxLots$;
  displayedColumns: string[] = ['acquisitionDate', 'quantity', 'costBasis', 'isDisposed', 'disposalDate', 'realizedGainLoss', 'actions'];

  constructor(private taxLotService: TaxLotService) {}

  ngOnInit(): void {
    this.taxLotService.getTaxLots().subscribe();
  }

  deleteTaxLot(id: string): void {
    if (confirm('Are you sure you want to delete this tax lot?')) {
      this.taxLotService.deleteTaxLot(id).subscribe();
    }
  }
}
