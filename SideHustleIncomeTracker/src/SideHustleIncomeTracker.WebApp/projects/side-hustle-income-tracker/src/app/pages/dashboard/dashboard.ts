import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { BusinessService, IncomeService, ExpenseService, TaxEstimateService } from '../../services';
import { map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _businessService = inject(BusinessService);
  private _incomeService = inject(IncomeService);
  private _expenseService = inject(ExpenseService);
  private _taxEstimateService = inject(TaxEstimateService);

  businesses$ = this._businessService.businesses$;
  incomes$ = this._incomeService.incomes$;
  expenses$ = this._expenseService.expenses$;
  taxEstimates$ = this._taxEstimateService.taxEstimates$;

  totalIncome$ = this.incomes$.pipe(
    map(incomes => incomes.reduce((sum, income) => sum + income.amount, 0))
  );

  totalExpenses$ = this.expenses$.pipe(
    map(expenses => expenses.reduce((sum, expense) => sum + expense.amount, 0))
  );

  netProfit$ = this.businesses$.pipe(
    map(businesses => businesses.reduce((sum, business) => sum + business.netProfit, 0))
  );

  activeBusinessesCount$ = this.businesses$.pipe(
    map(businesses => businesses.filter(b => b.isActive).length)
  );

  unpaidIncomesCount$ = this.incomes$.pipe(
    map(incomes => incomes.filter(i => !i.isPaid).length)
  );

  unpaidTaxesCount$ = this.taxEstimates$.pipe(
    map(estimates => estimates.filter(t => !t.isPaid).length)
  );

  ngOnInit(): void {
    this._businessService.getAll().subscribe();
    this._incomeService.getAll().subscribe();
    this._expenseService.getAll().subscribe();
    this._taxEstimateService.getAll().subscribe();
  }
}
