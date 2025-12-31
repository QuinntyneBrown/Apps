import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Registry, RegistryType } from '../../models';

@Component({
  selector: 'app-registry-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './registry-card.html',
  styleUrl: './registry-card.scss'
})
export class RegistryCard {
  @Input({ required: true }) registry!: Registry;
  @Output() edit = new EventEmitter<Registry>();
  @Output() delete = new EventEmitter<Registry>();
  @Output() viewItems = new EventEmitter<Registry>();

  RegistryType = RegistryType;

  getRegistryTypeLabel(type: RegistryType): string {
    const labels: Record<RegistryType, string> = {
      [RegistryType.Wedding]: 'Wedding',
      [RegistryType.BabyShower]: 'Baby Shower',
      [RegistryType.Birthday]: 'Birthday',
      [RegistryType.Graduation]: 'Graduation',
      [RegistryType.Housewarming]: 'Housewarming',
      [RegistryType.Anniversary]: 'Anniversary',
      [RegistryType.Holiday]: 'Holiday',
      [RegistryType.Other]: 'Other'
    };
    return labels[type];
  }

  onEdit(): void {
    this.edit.emit(this.registry);
  }

  onDelete(): void {
    this.delete.emit(this.registry);
  }

  onViewItems(): void {
    this.viewItems.emit(this.registry);
  }
}
