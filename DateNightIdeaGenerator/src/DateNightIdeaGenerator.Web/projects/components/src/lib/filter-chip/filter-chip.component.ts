import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-filter-chip',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './filter-chip.component.html',
  styleUrls: ['./filter-chip.component.scss']
})
export class FilterChipComponent {
  @Input({ required: true }) label!: string;
  @Input() active: boolean = false;
  @Output() toggle = new EventEmitter<void>();
}
