import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-alert-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert-card.html',
  styleUrls: ['./alert-card.scss']
})
export class AlertCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) timestamp!: string;
  @Input() reading: string = '';
  @Input() pulse: string = '';
  @Input() icon: string = 'warning';
  @Input() acknowledged: boolean = false;
  @Output() acknowledge = new EventEmitter<void>();
}
