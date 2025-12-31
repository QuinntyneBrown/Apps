import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { RegistryItem, Priority } from '../../models';

@Component({
  selector: 'app-registry-item-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatProgressBarModule],
  templateUrl: './registry-item-card.html',
  styleUrl: './registry-item-card.scss'
})
export class RegistryItemCard {
  @Input({ required: true }) item!: RegistryItem;
  @Output() edit = new EventEmitter<RegistryItem>();
  @Output() delete = new EventEmitter<RegistryItem>();
  @Output() contribute = new EventEmitter<RegistryItem>();

  Priority = Priority;

  getPriorityLabel(priority: Priority): string {
    const labels: Record<Priority, string> = {
      [Priority.Low]: 'Low',
      [Priority.Medium]: 'Medium',
      [Priority.High]: 'High',
      [Priority.MustHave]: 'Must Have'
    };
    return labels[priority];
  }

  getProgressPercentage(): number {
    if (this.item.quantityDesired === 0) return 0;
    return (this.item.quantityReceived / this.item.quantityDesired) * 100;
  }

  onEdit(): void {
    this.edit.emit(this.item);
  }

  onDelete(): void {
    this.delete.emit(this.item);
  }

  onContribute(): void {
    this.contribute.emit(this.item);
  }
}
