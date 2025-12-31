import { Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Income } from '../../models';

@Component({
  selector: 'app-income-card',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './income-card.html',
  styleUrl: './income-card.scss',
})
export class IncomeCard {
  income = input.required<Income>();
  edit = output<Income>();
  delete = output<Income>();

  onEdit(): void {
    this.edit.emit(this.income());
  }

  onDelete(): void {
    this.delete.emit(this.income());
  }
}
