import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-swiper-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './swiper-card.component.html',
  styleUrls: ['./swiper-card.component.scss']
})
export class SwiperCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input() imageUrl: string = '';
  @Input() category: string = '';
  @Input() categoryColor: string = '#FFF3E0';
  @Input() time: string = '';
  @Input() cost: string = '';
  @Output() skip = new EventEmitter<void>();
  @Output() save = new EventEmitter<void>();
  @Output() love = new EventEmitter<void>();
}
