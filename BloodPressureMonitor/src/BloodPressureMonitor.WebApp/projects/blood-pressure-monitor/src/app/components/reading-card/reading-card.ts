import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Reading, BloodPressureCategory } from '../../models';

@Component({
  selector: 'app-reading-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './reading-card.html',
  styleUrl: './reading-card.scss'
})
export class ReadingCard {
  @Input() reading!: Reading;
  @Output() edit = new EventEmitter<Reading>();
  @Output() delete = new EventEmitter<string>();

  BloodPressureCategory = BloodPressureCategory;

  getCategoryLabel(category: BloodPressureCategory): string {
    switch (category) {
      case BloodPressureCategory.Normal:
        return 'Normal';
      case BloodPressureCategory.Elevated:
        return 'Elevated';
      case BloodPressureCategory.HypertensionStage1:
        return 'Hypertension Stage 1';
      case BloodPressureCategory.HypertensionStage2:
        return 'Hypertension Stage 2';
      case BloodPressureCategory.HypertensiveCrisis:
        return 'Hypertensive Crisis';
      default:
        return 'Unknown';
    }
  }

  getCategoryClass(category: BloodPressureCategory): string {
    switch (category) {
      case BloodPressureCategory.Normal:
        return 'reading-card__category--normal';
      case BloodPressureCategory.Elevated:
        return 'reading-card__category--elevated';
      case BloodPressureCategory.HypertensionStage1:
        return 'reading-card__category--stage1';
      case BloodPressureCategory.HypertensionStage2:
        return 'reading-card__category--stage2';
      case BloodPressureCategory.HypertensiveCrisis:
        return 'reading-card__category--crisis';
      default:
        return '';
    }
  }

  onEdit(): void {
    this.edit.emit(this.reading);
  }

  onDelete(): void {
    this.delete.emit(this.reading.readingId);
  }
}
