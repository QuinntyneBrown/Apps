import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable, combineLatest } from 'rxjs';
import { map } from 'rxjs/operators';
import { VehicleService, ValueAssessmentService, MarketComparisonService } from '../../services';

interface DashboardStats {
  totalVehicles: number;
  ownedVehicles: number;
  totalAssessments: number;
  totalComparisons: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _vehicleService = inject(VehicleService);
  private _assessmentService = inject(ValueAssessmentService);
  private _comparisonService = inject(MarketComparisonService);
  private _router = inject(Router);

  stats$!: Observable<DashboardStats>;

  ngOnInit(): void {
    this._vehicleService.getVehicles().subscribe();
    this._assessmentService.getValueAssessments().subscribe();
    this._comparisonService.getMarketComparisons().subscribe();

    this.stats$ = combineLatest([
      this._vehicleService.vehicles$,
      this._assessmentService.assessments$,
      this._comparisonService.comparisons$
    ]).pipe(
      map(([vehicles, assessments, comparisons]) => ({
        totalVehicles: vehicles.length,
        ownedVehicles: vehicles.filter(v => v.isCurrentlyOwned).length,
        totalAssessments: assessments.length,
        totalComparisons: comparisons.length
      }))
    );
  }

  navigateTo(route: string): void {
    this._router.navigate([route]);
  }
}
