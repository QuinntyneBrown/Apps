import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Refill } from '../../models';

@Component({
  selector: 'app-refill-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './refill-card.html',
  styleUrl: './refill-card.scss'
})
export class RefillCard {
  @Input({ required: true }) refill!: Refill;
  @Output() edit = new EventEmitter<Refill>();
  @Output() remove = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.refill);
  }

  onDelete(): void {
    this.remove.emit(this.refill.refillId);
  }
}
