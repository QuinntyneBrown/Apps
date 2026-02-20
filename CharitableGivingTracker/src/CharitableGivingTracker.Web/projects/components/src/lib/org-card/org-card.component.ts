import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-org-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './org-card.component.html',
  styleUrls: ['./org-card.component.scss']
})
export class OrgCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) category!: string;
  @Input() totalDonated: string = '';
  @Input() donationCount: number = 0;
  @Input() initial: string = '';

  get displayInitial(): string {
    return this.initial || this.name.charAt(0).toUpperCase();
  }
}
