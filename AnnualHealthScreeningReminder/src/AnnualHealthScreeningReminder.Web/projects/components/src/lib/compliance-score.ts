import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-compliance-score',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './compliance-score.html',
  styleUrl: './compliance-score.scss'
})
export class ComplianceScore {
  @Input({ required: true }) score!: number;
  @Input({ required: true }) completed!: number;
  @Input({ required: true }) total!: number;
  @Input() status: 'on-track' | 'needs-attention' | 'overdue' = 'on-track';

  get statusLabel(): string {
    switch (this.status) {
      case 'on-track': return 'On Track';
      case 'needs-attention': return 'Needs Attention';
      case 'overdue': return 'Overdue';
    }
  }
}
