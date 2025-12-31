import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { IncomesService, BudgetsService } from '../../services';
import { IncomeCard, IncomeDialog } from '../../components';
import { Income } from '../../models';

@Component({
  selector: 'app-incomes',
  imports: [CommonModule, MatButtonModule, MatIconModule, IncomeCard],
  templateUrl: './incomes.html',
  styleUrl: './incomes.scss',
})
export class Incomes implements OnInit {
  private readonly _incomesService = inject(IncomesService);
  private readonly _budgetsService = inject(BudgetsService);
  private readonly _dialog = inject(MatDialog);

  incomes$ = this._incomesService.incomes$;
  budgets$ = this._budgetsService.budgets$;

  ngOnInit(): void {
    this._incomesService.getAll().subscribe();
    this._budgetsService.getAll().subscribe();
  }

  onCreate(): void {
    const dialogRef = this._dialog.open(IncomeDialog, {
      width: '600px',
      data: { income: null, budgetId: null },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._incomesService.create(result).subscribe();
      }
    });
  }

  onEdit(income: Income): void {
    const dialogRef = this._dialog.open(IncomeDialog, {
      width: '600px',
      data: { income, budgetId: income.budgetId },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._incomesService.update(income.incomeId, result).subscribe();
      }
    });
  }

  onDelete(income: Income): void {
    if (confirm(`Are you sure you want to delete income "${income.description}"?`)) {
      this._incomesService.delete(income.incomeId).subscribe();
    }
  }
}
