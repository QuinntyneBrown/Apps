import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-reading-row',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reading-row.html',
  styleUrls: ['./reading-row.scss']
})
export class ReadingRowComponent {
  @Input({ required: true }) reading!: string;
  @Input({ required: true }) pulse!: string;
  @Input({ required: true }) timestamp!: string;
  @Input() context: string = '';
  @Input() status: 'normal' | 'elevated' | 'high' = 'normal';
}
