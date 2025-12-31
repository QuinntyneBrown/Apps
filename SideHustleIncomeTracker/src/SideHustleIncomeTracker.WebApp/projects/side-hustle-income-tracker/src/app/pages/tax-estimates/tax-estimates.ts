import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { TaxEstimateService, BusinessService } from '../../services';

@Component({
  selector: 'app-tax-estimates',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './tax-estimates.html',
  styleUrl: './tax-estimates.scss'
})
export class TaxEstimates implements OnInit {
  private _taxEstimateService = inject(TaxEstimateService);
  private _businessService = inject(BusinessService);

  taxEstimates$ = this._taxEstimateService.taxEstimates$;
  businesses$ = this._businessService.businesses$;

  displayedColumns: string[] = ['business', 'taxYear', 'quarter', 'netProfit', 'selfEmploymentTax', 'incomeTax', 'totalEstimatedTax', 'isPaid', 'paymentDate'];

  ngOnInit(): void {
    this._taxEstimateService.getAll().subscribe();
    this._businessService.getAll().subscribe();
  }

  getBusinessName(businessId: string): string {
    let businessName = '';
    this.businesses$.subscribe(businesses => {
      const business = businesses.find(b => b.businessId === businessId);
      businessName = business?.name || 'Unknown';
    });
    return businessName;
  }
}
