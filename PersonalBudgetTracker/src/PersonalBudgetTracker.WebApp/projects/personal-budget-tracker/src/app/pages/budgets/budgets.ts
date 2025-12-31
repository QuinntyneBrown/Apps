import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { BudgetsService } from '../../services';
import { BudgetCard, BudgetDialog } from '../../components';
import { Budget } from '../../models';

@Component({
  selector: 'app-budgets',
  imports: [CommonModule, MatButtonModule, MatIconModule, BudgetCard],
  templateUrl: './budgets.html',
  styleUrl: './budgets.scss',
})
export class Budgets implements OnInit {
  private readonly _budgetsService = inject(BudgetsService);
  private readonly _dialog = inject(MatDialog);

  budgets$ = this._budgetsService.budgets$;

  ngOnInit(): void {
    this._budgetsService.getAll().subscribe();
  }

  onCreate(): void {
    const dialogRef = this._dialog.open(BudgetDialog, {
      width: '600px',
      data: null,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._budgetsService.create(result).subscribe();
      }
    });
  }

  onEdit(budget: Budget): void {
    const dialogRef = this._dialog.open(BudgetDialog, {
      width: '600px',
      data: budget,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._budgetsService.update(budget.budgetId, result).subscribe();
      }
    });
  }

  onDelete(budget: Budget): void {
    if (confirm(`Are you sure you want to delete budget "${budget.name}"?`)) {
      this._budgetsService.delete(budget.budgetId).subscribe();
    }
  }
}
