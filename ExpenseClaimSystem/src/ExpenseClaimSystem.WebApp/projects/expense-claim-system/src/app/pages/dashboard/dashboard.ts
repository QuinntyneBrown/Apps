import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { combineLatest, map } from 'rxjs';
import { ExpenseClaimsService, EmployeesService, CategoriesService } from '../../services';
import { ExpenseClaimCard } from '../../components';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink, ExpenseClaimCard],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  private readonly _expenseClaimsService = inject(ExpenseClaimsService);
  private readonly _employeesService = inject(EmployeesService);
  private readonly _categoriesService = inject(CategoriesService);

  viewModel$ = combineLatest([
    this._expenseClaimsService.expenseClaims$,
    this._employeesService.employees$,
    this._categoriesService.categories$,
  ]).pipe(
    map(([expenseClaims, employees, categories]) => ({
      pendingClaims: expenseClaims.filter(c => c.status === 'Submitted' || c.status === 'UnderReview').slice(0, 5),
      approvedClaims: expenseClaims.filter(c => c.status === 'Approved'),
      recentClaims: expenseClaims.slice(0, 5),
      totalClaims: expenseClaims.length,
      totalEmployees: employees.length,
      totalCategories: categories.length,
      totalAmount: expenseClaims
        .filter(c => c.status === 'Approved' || c.status === 'Paid')
        .reduce((sum, c) => sum + c.amount, 0),
      pendingAmount: expenseClaims
        .filter(c => c.status === 'Submitted' || c.status === 'UnderReview')
        .reduce((sum, c) => sum + c.amount, 0),
    }))
  );

  ngOnInit(): void {
    this._expenseClaimsService.getAll().subscribe();
    this._employeesService.getAll().subscribe();
    this._categoriesService.getAll().subscribe();
  }
}
