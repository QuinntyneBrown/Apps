import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-donation-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './donation-card.component.html',
  styleUrls: ['./donation-card.component.scss']
})
export class DonationCardComponent {
  @Input({ required: true }) organizationName!: string;
  @Input({ required: true }) date!: string;
  @Input({ required: true }) method!: string;
  @Input({ required: true }) amount!: string;
  @Input({ required: true }) status!: 'Deductible' | 'Pending' | 'Non-Deductible';
}
