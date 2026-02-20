import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-quick-action',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './quick-action.html',
  styleUrl: './quick-action.scss'
})
export class QuickAction {
  @Input({ required: true }) icon!: string;
  @Input({ required: true }) label!: string;
  @Input() iconColor: 'primary' | 'info' | 'success' | 'warning' = 'primary';
  @Output() actionClick = new EventEmitter<void>();
}
