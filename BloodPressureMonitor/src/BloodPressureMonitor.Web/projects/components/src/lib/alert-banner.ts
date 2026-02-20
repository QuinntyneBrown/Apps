import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-alert-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert-banner.html',
  styleUrls: ['./alert-banner.scss']
})
export class AlertBannerComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input() icon: string = 'error';
}
