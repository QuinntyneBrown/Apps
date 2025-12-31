import { Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Expense, ExpenseCategory } from '../../models';

@Component({
  selector: 'app-expense-card',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './expense-card.html',
  styleUrl: './expense-card.scss',
})
export class ExpenseCard {
  expense = input.required<Expense>();
  edit = output<Expense>();
  delete = output<Expense>();

  getCategoryLabel(category: ExpenseCategory): string {
    const labels: Record<ExpenseCategory, string> = {
      [ExpenseCategory.Housing]: 'Housing',
      [ExpenseCategory.Transportation]: 'Transportation',
      [ExpenseCategory.Food]: 'Food',
      [ExpenseCategory.Healthcare]: 'Healthcare',
      [ExpenseCategory.Entertainment]: 'Entertainment',
      [ExpenseCategory.PersonalCare]: 'Personal Care',
      [ExpenseCategory.Education]: 'Education',
      [ExpenseCategory.DebtPayment]: 'Debt Payment',
      [ExpenseCategory.Savings]: 'Savings',
      [ExpenseCategory.Other]: 'Other',
    };
    return labels[category] || 'Unknown';
  }

  onEdit(): void {
    this.edit.emit(this.expense());
  }

  onDelete(): void {
    this.delete.emit(this.expense());
  }
}
