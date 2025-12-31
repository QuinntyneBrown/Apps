import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Destination } from '../../models';

@Component({
  selector: 'app-destination-card',
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './destination-card.html',
  styleUrl: './destination-card.scss'
})
export class DestinationCard {
  @Input() destination!: Destination;
  @Output() edit = new EventEmitter<Destination>();
  @Output() delete = new EventEmitter<string>();
  @Output() markVisited = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.destination);
  }

  onDelete(): void {
    this.delete.emit(this.destination.destinationId);
  }

  onMarkVisited(): void {
    this.markVisited.emit(this.destination.destinationId);
  }

  getPriorityLabel(priority: number): string {
    if (priority <= 2) return 'High';
    if (priority <= 4) return 'Medium';
    return 'Low';
  }
}
