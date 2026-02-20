import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-weight-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './weight-banner.component.html',
  styleUrls: ['./weight-banner.component.scss']
})
export class WeightBannerComponent {
  @Input({ required: true }) totalWeight!: string;
  @Input() label: string = 'Total Pack Weight';
}
