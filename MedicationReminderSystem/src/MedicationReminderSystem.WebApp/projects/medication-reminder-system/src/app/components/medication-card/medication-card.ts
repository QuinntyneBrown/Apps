import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Medication, MedicationTypeLabels } from '../../models';

@Component({
  selector: 'app-medication-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './medication-card.html',
  styleUrl: './medication-card.scss'
})
export class MedicationCard {
  @Input({ required: true }) medication!: Medication;
  @Output() edit = new EventEmitter<Medication>();
  @Output() remove = new EventEmitter<string>();

  getMedicationTypeLabel(type: number): string {
    return MedicationTypeLabels[type] || 'Unknown';
  }

  onEdit(): void {
    this.edit.emit(this.medication);
  }

  onDelete(): void {
    this.remove.emit(this.medication.medicationId);
  }
}
