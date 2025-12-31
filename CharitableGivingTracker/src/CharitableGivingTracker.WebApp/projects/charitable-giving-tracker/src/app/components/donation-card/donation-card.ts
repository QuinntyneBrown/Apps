import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Donation } from '../../models';

@Component({
  selector: 'app-donation-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './donation-card.html',
  styleUrl: './donation-card.scss'
})
export class DonationCard {
  @Input() donation!: Donation;
  @Output() edit = new EventEmitter<Donation>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.donation);
  }

  onDelete(): void {
    this.delete.emit(this.donation.donationId);
  }

  getDonationTypeLabel(type: number): string {
    const types = ['Cash', 'Check', 'Credit Card', 'Stock', 'In-Kind', 'Other'];
    return types[type] || 'Unknown';
  }
}
