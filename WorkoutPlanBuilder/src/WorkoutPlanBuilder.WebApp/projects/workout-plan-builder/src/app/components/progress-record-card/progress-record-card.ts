import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProgressRecord } from '../../models';

@Component({
  selector: 'app-progress-record-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './progress-record-card.html',
  styleUrl: './progress-record-card.scss'
})
export class ProgressRecordCard {
  @Input() progressRecord!: ProgressRecord;
  @Output() edit = new EventEmitter<ProgressRecord>();
  @Output() delete = new EventEmitter<ProgressRecord>();

  onEdit(): void {
    this.edit.emit(this.progressRecord);
  }

  onDelete(): void {
    this.delete.emit(this.progressRecord);
  }
}
