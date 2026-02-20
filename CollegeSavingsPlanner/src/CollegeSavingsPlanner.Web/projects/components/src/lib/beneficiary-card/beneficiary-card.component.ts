import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface BeneficiaryStat {
  value: string;
  label: string;
}

@Component({
  selector: 'lib-beneficiary-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './beneficiary-card.component.html',
  styleUrls: ['./beneficiary-card.component.scss']
})
export class BeneficiaryCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) age!: string;
  @Input() initial: string = '';
  @Input() stats: BeneficiaryStat[] = [];

  get displayInitial(): string {
    return this.initial || this.name.charAt(0).toUpperCase();
  }
}
