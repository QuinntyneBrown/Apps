import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-tax-summary-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tax-summary-card.component.html',
  styleUrls: ['./tax-summary-card.component.scss']
})
export class TaxSummaryCardComponent {
  @Input({ required: true }) year!: string;
  @Input({ required: true }) totalDonated!: string;
  @Input({ required: true }) taxDeductible!: string;
  @Input({ required: true }) deductiblePercentage!: number;
}
