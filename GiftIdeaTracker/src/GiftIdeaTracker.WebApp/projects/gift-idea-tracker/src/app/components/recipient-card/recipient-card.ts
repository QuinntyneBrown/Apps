import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Recipient } from '../../models';

@Component({
  selector: 'app-recipient-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './recipient-card.html',
  styleUrl: './recipient-card.scss'
})
export class RecipientCard {
  @Input() recipient!: Recipient;
  @Input() giftIdeasCount = 0;
  @Output() viewIdeasClick = new EventEmitter<Recipient>();
  @Output() editClick = new EventEmitter<Recipient>();
  @Output() deleteClick = new EventEmitter<Recipient>();

  onViewIdeasClick(): void {
    this.viewIdeasClick.emit(this.recipient);
  }

  onEditClick(): void {
    this.editClick.emit(this.recipient);
  }

  onDeleteClick(): void {
    this.deleteClick.emit(this.recipient);
  }

  getInitials(): string {
    return this.recipient.name
      .split(' ')
      .map(n => n[0])
      .join('')
      .toUpperCase()
      .substring(0, 2);
  }
}
