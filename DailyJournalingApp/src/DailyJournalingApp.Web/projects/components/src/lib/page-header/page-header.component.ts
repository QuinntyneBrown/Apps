import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-page-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent {
  @Input({ required: true }) title!: string;
  @Input() showBack: boolean = false;
  @Input() actionLabel: string = '';
  @Input() actionIcon: string = '';
  @Output() back = new EventEmitter<void>();
  @Output() action = new EventEmitter<void>();

  onBack(): void {
    this.back.emit();
  }

  onAction(): void {
    this.action.emit();
  }
}
