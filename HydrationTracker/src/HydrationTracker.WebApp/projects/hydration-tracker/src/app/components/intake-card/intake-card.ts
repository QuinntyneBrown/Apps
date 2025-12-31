import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Intake, BeverageType } from '../../models';

@Component({
  selector: 'app-intake-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './intake-card.html',
  styleUrls: ['./intake-card.scss']
})
export class IntakeCard {
  @Input() intake!: Intake;
  @Output() edit = new EventEmitter<Intake>();
  @Output() delete = new EventEmitter<string>();

  BeverageType = BeverageType;

  getBeverageIcon(type: BeverageType): string {
    const icons: Record<BeverageType, string> = {
      [BeverageType.Water]: 'water_drop',
      [BeverageType.Tea]: 'local_cafe',
      [BeverageType.Coffee]: 'coffee',
      [BeverageType.Juice]: 'emoji_food_beverage',
      [BeverageType.Sports]: 'sports',
      [BeverageType.Other]: 'local_drink'
    };
    return icons[type];
  }

  getBeverageName(type: BeverageType): string {
    return BeverageType[type];
  }

  onEdit(): void {
    this.edit.emit(this.intake);
  }

  onDelete(): void {
    this.delete.emit(this.intake.intakeId);
  }
}
