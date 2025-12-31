import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable, combineLatest, map } from 'rxjs';
import { DeductionService, TaxYearService } from '../../services';
import { Deduction, TaxYear, DeductionCategory } from '../../models';

interface DashboardStats {
  totalDeductions: number;
  totalAmount: number;
  recentDeductions: Deduction[];
  taxYears: TaxYear[];
  deductionsByCategory: { category: string; amount: number; count: number }[];
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _deductionService = inject(DeductionService);
  private _taxYearService = inject(TaxYearService);

  stats$!: Observable<DashboardStats>;

  categoryNames: { [key: number]: string } = {
    [DeductionCategory.MedicalExpenses]: 'Medical Expenses',
    [DeductionCategory.CharitableDonations]: 'Charitable Donations',
    [DeductionCategory.MortgageInterest]: 'Mortgage Interest',
    [DeductionCategory.StateAndLocalTaxes]: 'State and Local Taxes',
    [DeductionCategory.BusinessExpenses]: 'Business Expenses',
    [DeductionCategory.EducationExpenses]: 'Education Expenses',
    [DeductionCategory.HomeOffice]: 'Home Office',
    [DeductionCategory.Other]: 'Other'
  };

  ngOnInit(): void {
    this._deductionService.getAll().subscribe();
    this._taxYearService.getAll().subscribe();

    this.stats$ = combineLatest([
      this._deductionService.deductions$,
      this._taxYearService.taxYears$
    ]).pipe(
      map(([deductions, taxYears]) => {
        const totalAmount = deductions.reduce((sum, d) => sum + d.amount, 0);
        const recentDeductions = [...deductions]
          .sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime())
          .slice(0, 5);

        const categoryMap = new Map<DeductionCategory, { amount: number; count: number }>();
        deductions.forEach(d => {
          const existing = categoryMap.get(d.category) || { amount: 0, count: 0 };
          categoryMap.set(d.category, {
            amount: existing.amount + d.amount,
            count: existing.count + 1
          });
        });

        const deductionsByCategory = Array.from(categoryMap.entries()).map(([cat, data]) => ({
          category: this.categoryNames[cat],
          amount: data.amount,
          count: data.count
        })).sort((a, b) => b.amount - a.amount);

        return {
          totalDeductions: deductions.length,
          totalAmount,
          recentDeductions,
          taxYears,
          deductionsByCategory
        };
      })
    );
  }
}
