import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Contact, ContactTypeLabels } from '../../models';

@Component({
  selector: 'app-contact-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './contact-card.html',
  styleUrl: './contact-card.scss'
})
export class ContactCard {
  @Input({ required: true }) contact!: Contact;
  @Output() edit = new EventEmitter<Contact>();
  @Output() delete = new EventEmitter<Contact>();
  @Output() viewDetails = new EventEmitter<Contact>();

  contactTypeLabels = ContactTypeLabels;

  onEdit(): void {
    this.edit.emit(this.contact);
  }

  onDelete(): void {
    this.delete.emit(this.contact);
  }

  onViewDetails(): void {
    this.viewDetails.emit(this.contact);
  }
}
