import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-warranty-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './warranty-card.html',
  styleUrl: './warranty-card.scss'
})
export class WarrantyCard {
  @Input({ required: true }) applianceName!: string;
  @Input({ required: true }) provider!: string;
  @Input() statusLabel = 'Active';
  @Input() status: 'active' | 'expiring' | 'expired' = 'active';
  @Input() progressPercent = 0;
  @Output() cardClick = new EventEmitter<void>();
}
