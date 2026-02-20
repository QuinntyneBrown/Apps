import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-status-badge',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './status-badge.component.html',
  styleUrls: ['./status-badge.component.scss']
})
export class StatusBadgeComponent {
  @Input({ required: true }) status!: string;
  @Input() color: string = '';
  @Input() background: string = '';

  get computedColor(): string {
    if (this.color) return this.color;
    switch (this.status.toLowerCase()) {
      case 'approved': return '#2E7D32';
      case 'rejected': return '#C62828';
      case 'under review': return '#E65100';
      case 'submitted':
      case 'pending': return '#1565C0';
      default: return '#64748B';
    }
  }

  get computedBackground(): string {
    if (this.background) return this.background;
    switch (this.status.toLowerCase()) {
      case 'approved': return '#E8F5E9';
      case 'rejected': return '#FFEBEE';
      case 'under review': return '#FFF3E0';
      case 'submitted':
      case 'pending': return '#E3F2FD';
      default: return '#F5F7FA';
    }
  }
}
