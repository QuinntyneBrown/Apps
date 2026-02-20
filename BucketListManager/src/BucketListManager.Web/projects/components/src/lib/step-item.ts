import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-step-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './step-item.html',
  styleUrls: ['./step-item.scss']
})
export class StepItemComponent {
  @Input({ required: true }) label!: string;
  @Input() checked: boolean = false;
  @Output() toggle = new EventEmitter<void>();
}
