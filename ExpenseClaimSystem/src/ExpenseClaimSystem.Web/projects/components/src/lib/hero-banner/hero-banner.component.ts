import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-hero-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hero-banner.component.html',
  styleUrls: ['./hero-banner.component.scss']
})
export class HeroBannerComponent {
  @Input({ required: true }) greeting!: string;
  @Input({ required: true }) amount!: string;
  @Input() subtitle: string = '';
  @Input() actionLabel: string = '';
  @Output() action = new EventEmitter<void>();
}
