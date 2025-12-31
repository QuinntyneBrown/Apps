import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Trip } from '../../models';

@Component({
  selector: 'app-trip-card',
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './trip-card.html',
  styleUrl: './trip-card.scss'
})
export class TripCard {
  @Input() trip!: Trip;
  @Input() destinationName?: string;
  @Output() edit = new EventEmitter<Trip>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.trip);
  }

  onDelete(): void {
    this.delete.emit(this.trip.tripId);
  }

  getDuration(): number {
    const start = new Date(this.trip.startDate);
    const end = new Date(this.trip.endDate);
    return Math.ceil((end.getTime() - start.getTime()) / (1000 * 60 * 60 * 24));
  }
}
