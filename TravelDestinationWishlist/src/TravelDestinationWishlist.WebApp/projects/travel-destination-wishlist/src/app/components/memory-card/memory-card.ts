import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Memory } from '../../models';

@Component({
  selector: 'app-memory-card',
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './memory-card.html',
  styleUrl: './memory-card.scss'
})
export class MemoryCard {
  @Input() memory!: Memory;
  @Output() edit = new EventEmitter<Memory>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.memory);
  }

  onDelete(): void {
    this.delete.emit(this.memory.memoryId);
  }
}
