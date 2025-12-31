import { Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Budget, BudgetStatus } from '../../models';

@Component({
  selector: 'app-budget-card',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './budget-card.html',
  styleUrl: './budget-card.scss',
})
export class BudgetCard {
  budget = input.required<Budget>();
  edit = output<Budget>();
  delete = output<Budget>();

  BudgetStatus = BudgetStatus;

  getStatusColor(status: BudgetStatus): string {
    switch (status) {
      case BudgetStatus.Draft:
        return 'accent';
      case BudgetStatus.Active:
        return 'primary';
      case BudgetStatus.Completed:
        return '';
      default:
        return '';
    }
  }

  getStatusLabel(status: BudgetStatus): string {
    switch (status) {
      case BudgetStatus.Draft:
        return 'Draft';
      case BudgetStatus.Active:
        return 'Active';
      case BudgetStatus.Completed:
        return 'Completed';
      default:
        return 'Unknown';
    }
  }

  onEdit(): void {
    this.edit.emit(this.budget());
  }

  onDelete(): void {
    this.delete.emit(this.budget());
  }
}
