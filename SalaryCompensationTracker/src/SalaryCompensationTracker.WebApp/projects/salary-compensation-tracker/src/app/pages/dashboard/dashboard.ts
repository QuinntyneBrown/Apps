import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable, combineLatest, map } from 'rxjs';
import { CompensationService, BenefitService, MarketComparisonService } from '../../services';
import { Compensation, Benefit, MarketComparison } from '../../models';

interface DashboardViewModel {
  compensations: Compensation[];
  benefits: Benefit[];
  marketComparisons: MarketComparison[];
  totalCompensation: number;
  totalBenefitValue: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _compensationService = inject(CompensationService);
  private _benefitService = inject(BenefitService);
  private _marketComparisonService = inject(MarketComparisonService);

  viewModel$!: Observable<DashboardViewModel>;

  ngOnInit(): void {
    this._compensationService.getCompensations().subscribe();
    this._benefitService.getBenefits().subscribe();
    this._marketComparisonService.getMarketComparisons().subscribe();

    this.viewModel$ = combineLatest([
      this._compensationService.compensations$,
      this._benefitService.benefits$,
      this._marketComparisonService.marketComparisons$
    ]).pipe(
      map(([compensations, benefits, marketComparisons]) => ({
        compensations,
        benefits,
        marketComparisons,
        totalCompensation: compensations.reduce((sum, c) => sum + c.totalCompensation, 0),
        totalBenefitValue: benefits.reduce((sum, b) => sum + (b.estimatedValue || 0), 0)
      }))
    );
  }
}
