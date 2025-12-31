import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Pattern } from '../../models';

@Component({
  selector: 'app-pattern-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './pattern-card.html',
  styleUrl: './pattern-card.scss'
})
export class PatternCard {
  @Input() pattern!: Pattern;
  @Output() edit = new EventEmitter<Pattern>();
  @Output() delete = new EventEmitter<string>();

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    });
  }

  onEdit(): void {
    this.edit.emit(this.pattern);
  }

  onDelete(): void {
    this.delete.emit(this.pattern.patternId);
  }
}
