import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-breakdown-row',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './breakdown-row.component.html',
  styleUrls: ['./breakdown-row.component.scss']
})
export class BreakdownRowComponent {
  @Input({ required: true }) category!: string;
  @Input({ required: true }) amount!: string;
  @Input() description: string = '';
}
