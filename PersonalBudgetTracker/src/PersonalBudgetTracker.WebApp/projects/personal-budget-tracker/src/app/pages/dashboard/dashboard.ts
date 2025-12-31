import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { combineLatest, map } from 'rxjs';
import { BudgetsService, ExpensesService, IncomesService } from '../../services';
import { BudgetCard } from '../../components';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink, BudgetCard],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  private readonly _budgetsService = inject(BudgetsService);
  private readonly _expensesService = inject(ExpensesService);
  private readonly _incomesService = inject(IncomesService);

  viewModel$ = combineLatest([
    this._budgetsService.budgets$,
    this._expensesService.expenses$,
    this._incomesService.incomes$,
  ]).pipe(
    map(([budgets, expenses, incomes]) => ({
      activeBudgets: budgets.filter(b => b.status === 1),
      recentBudgets: budgets.slice(0, 3),
      totalBudgets: budgets.length,
      totalExpenses: expenses.reduce((sum, e) => sum + e.amount, 0),
      totalIncomes: incomes.reduce((sum, i) => sum + i.amount, 0),
      expenseCount: expenses.length,
      incomeCount: incomes.length,
    }))
  );

  ngOnInit(): void {
    this._budgetsService.getAll().subscribe();
    this._expensesService.getAll().subscribe();
    this._incomesService.getAll().subscribe();
  }
}
