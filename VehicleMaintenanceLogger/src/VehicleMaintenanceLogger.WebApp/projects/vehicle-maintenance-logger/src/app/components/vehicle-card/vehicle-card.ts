import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { RouterLink } from '@angular/router';
import { Vehicle } from '../../models';

@Component({
  selector: 'app-vehicle-card',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './vehicle-card.html',
  styleUrl: './vehicle-card.scss'
})
export class VehicleCard {
  @Input() vehicle!: Vehicle;
  @Output() edit = new EventEmitter<Vehicle>();
  @Output() delete = new EventEmitter<Vehicle>();

  onEdit(): void {
    this.edit.emit(this.vehicle);
  }

  onDelete(): void {
    this.delete.emit(this.vehicle);
  }
}
