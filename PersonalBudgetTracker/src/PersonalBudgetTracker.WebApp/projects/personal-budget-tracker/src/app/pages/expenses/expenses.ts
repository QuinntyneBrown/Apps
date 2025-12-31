import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { ExpensesService, BudgetsService } from '../../services';
import { ExpenseCard, ExpenseDialog } from '../../components';
import { Expense } from '../../models';

@Component({
  selector: 'app-expenses',
  imports: [CommonModule, MatButtonModule, MatIconModule, ExpenseCard],
  templateUrl: './expenses.html',
  styleUrl: './expenses.scss',
})
export class Expenses implements OnInit {
  private readonly _expensesService = inject(ExpensesService);
  private readonly _budgetsService = inject(BudgetsService);
  private readonly _dialog = inject(MatDialog);

  expenses$ = this._expensesService.expenses$;
  budgets$ = this._budgetsService.budgets$;

  ngOnInit(): void {
    this._expensesService.getAll().subscribe();
    this._budgetsService.getAll().subscribe();
  }

  onCreate(): void {
    const dialogRef = this._dialog.open(ExpenseDialog, {
      width: '600px',
      data: { expense: null, budgetId: null },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._expensesService.create(result).subscribe();
      }
    });
  }

  onEdit(expense: Expense): void {
    const dialogRef = this._dialog.open(ExpenseDialog, {
      width: '600px',
      data: { expense, budgetId: expense.budgetId },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._expensesService.update(expense.expenseId, result).subscribe();
      }
    });
  }

  onDelete(expense: Expense): void {
    if (confirm(`Are you sure you want to delete expense "${expense.description}"?`)) {
      this._expensesService.delete(expense.expenseId).subscribe();
    }
  }
}
