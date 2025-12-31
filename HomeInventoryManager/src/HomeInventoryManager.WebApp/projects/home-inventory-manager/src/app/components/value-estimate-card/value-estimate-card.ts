import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ValueEstimate } from '../../models';

@Component({
  selector: 'app-value-estimate-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './value-estimate-card.html',
  styleUrls: ['./value-estimate-card.scss']
})
export class ValueEstimateCard {
  @Input() estimate!: ValueEstimate;
  @Output() edit = new EventEmitter<ValueEstimate>();
  @Output() delete = new EventEmitter<ValueEstimate>();

  onEdit(): void {
    this.edit.emit(this.estimate);
  }

  onDelete(): void {
    this.delete.emit(this.estimate);
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }
}
